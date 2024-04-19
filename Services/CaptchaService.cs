using Newtonsoft.Json;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class CaptchaService 
    {
        private readonly DataContext _context;
        private const string Letters = "123456789";

        public CaptchaService(DataContext context)
        {
            _context = context;
        }

        public async Task<CaptchaResult> GenerateCaptcha(int width, int height)
        {
            var code = GenerateCaptchaCode();
            if (height > 300)
                height = 300;
            if (width > 300)
                width = 300;
            var image = GenerateCaptchaImage(width, height, code);

            var captchaCode = new CaptchaCode
            {
                Id = Guid.NewGuid().ToString(),
                Code = code,
                ExpireAt = DateTime.Now.AddSeconds(40),
            };

            _context.CaptchaCodes.Add(captchaCode);
            await _context.SaveChangesAsync();

            return new CaptchaResult
            {
                Id = captchaCode.Id,
                Image = image,
            };
        }

        public string GenerateCaptchaCode()
        {
            var rand = new Random();
            var maxRand = Letters.Length - 1;

            var sb = new StringBuilder();

            for (int i = 0; i < 6; i++)
            {
                var index = rand.Next(maxRand);
                sb.Append(Letters[index]);
            }

            return sb.ToString();
        }

        public byte[] GenerateCaptchaImage(int width, int height, string captchaCode)
        {
            using (var baseMap = new Bitmap(width, height))
            using (var graph = Graphics.FromImage(baseMap))
            {
                var rand = new Random();

                graph.Clear(GetRandomLightColor());

                DrawCaptchaCode();
                DrawDisorderLine();
                AdjustRippleEffect();

                var memoryStream = new MemoryStream();

                baseMap.Save(memoryStream, ImageFormat.Png);

                return memoryStream.ToArray();

                int GetFontSize(int imageWidth, int captchaCodeCount)
                {
                    var averageSize = imageWidth / captchaCodeCount;

                    return Convert.ToInt32(averageSize);
                }

                Color GetRandomDeepColor()
                {
                    const int redLow = 160;
                    const int greenLow = 100;
                    const int blueLow = 160;
                    return Color.FromArgb(rand.Next(redLow), rand.Next(greenLow), rand.Next(blueLow));
                }

                Color GetRandomLightColor()
                {
                    const int low = 180;
                    const int high = 255;

                    var nRend = rand.Next(high) % (high - low) + low;
                    var nGreen = rand.Next(high) % (high - low) + low;
                    var nBlue = rand.Next(high) % (high - low) + low;

                    return Color.FromArgb(nRend, nGreen, nBlue);
                }

                void DrawCaptchaCode()
                {
                    var fontBrush = new SolidBrush(Color.Black);
                    var fontSize = GetFontSize(width, captchaCode.Length);
                    var font = new Font(FontFamily.GenericSerif, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

                    for (int i = 0; i < captchaCode.Length; i++)
                    {
                        fontBrush.Color = GetRandomDeepColor();

                        var shiftPx = fontSize / 6;

                        float x = i * fontSize + rand.Next(-shiftPx, shiftPx) + rand.Next(-shiftPx, shiftPx);
                        var maxY = height - fontSize;

                        if (maxY < 0)
                        {
                            maxY = 0;
                        }

                        float y = rand.Next(0, maxY);

                        graph.DrawString(captchaCode[i].ToString(), font, fontBrush, x, y);
                    }
                }

                void DrawDisorderLine()
                {
                    var linePen = new Pen(new SolidBrush(Color.Black), 3);

                    for (int i = 0; i < rand.Next(3, 5); i++)
                    {
                        linePen.Color = GetRandomDeepColor();

                        var startPoint = new Point(rand.Next(0, width), rand.Next(0, height));
                        var endPoint = new Point(rand.Next(0, width), rand.Next(0, height));
                        graph.DrawLine(linePen, startPoint, endPoint);
                    }
                }

                void AdjustRippleEffect()
                {
                    const short nWave = 6;
                    var nWidth = baseMap.Width;
                    var nHeight = baseMap.Height;

                    var pt = new Point[nWidth, nHeight];

                    for (int x = 0; x < nWidth; ++x)
                    {
                        for (int y = 0; y < nHeight; ++y)
                        {
                            var xo = nWave * Math.Sin(2.0 * 3.1415 * y / 128.0);
                            var yo = nWave * Math.Cos(2.0 * 3.1415 * x / 128.0);

                            var newX = x + xo;
                            var newY = y + yo;

                            if (newX > 0 && newX < nWidth)
                            {
                                pt[x, y].X = (int) newX;
                            }
                            else
                            {
                                pt[x, y].X = 0;
                            }


                            if (newY > 0 && newY < nHeight)
                            {
                                pt[x, y].Y = (int) newY;
                            }
                            else
                            {
                                pt[x, y].Y = 0;
                            }
                        }
                    }

                    var bSrc = (Bitmap) baseMap.Clone();

                    var bitmapData = baseMap.LockBits(new Rectangle(0, 0, baseMap.Width, baseMap.Height),
                        ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                    var bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height),
                        ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                    var scanLine = bitmapData.Stride;

                    var scan0 = bitmapData.Scan0;
                    var srcScan0 = bmSrc.Scan0;

                    unsafe
                    {
                        var p = (byte*) (void*) scan0;
                        var pSrc = (byte*) (void*) srcScan0;

                        var nOffset = bitmapData.Stride - baseMap.Width * 3;

                        for (int y = 0; y < nHeight; ++y)
                        {
                            for (int x = 0; x < nWidth; ++x)
                            {
                                var xOffset = pt[x, y].X;
                                var yOffset = pt[x, y].Y;

                                if (yOffset >= 0 && yOffset < nHeight && xOffset >= 0 && xOffset < nWidth)
                                {
                                    if (pSrc != null)
                                    {
                                        p[0] = pSrc[yOffset * scanLine + xOffset * 3];
                                        p[1] = pSrc[yOffset * scanLine + xOffset * 3 + 1];
                                        p[2] = pSrc[yOffset * scanLine + xOffset * 3 + 2];
                                    }
                                }

                                p += 3;
                            }

                            p += nOffset;
                        }
                    }

                    baseMap.UnlockBits(bitmapData);
                    bSrc.UnlockBits(bmSrc);
                    bSrc.Dispose();
                }
            }
        }
    }

    public class CaptchaResult
    {
        public string Id { get; set; }
        public byte[] Image { get; set; }
    }
}