using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Extensions
{
    public static class ImageExtensions
    {
        public static Image Copyright(this Image original, string text)
        {
            var graphicsImage = Graphics.FromImage(original);

            var format = new StringFormat
            {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Far
            };

            var color = ColorTranslator.FromHtml("#808080");

            graphicsImage.DrawString(text, new Font("Tahoma", 30, FontStyle.Regular), new SolidBrush(color), new Point(original.Width, original.Height), format);

            return original;
        }

        public static Image SetResolution(this Image original, int xDpi, int yDpi)
        {
            var bitmap = (Bitmap)original;
            bitmap.SetResolution(xDpi, yDpi);
            return bitmap;
        }

        public static Image ResizeImage(this Image original, int targetWidth)
        {
            var percent = (double)original.Width / targetWidth;
            var destWidth = (int)(original.Width / percent);
            var destHeight = (int)(original.Height / percent);

            var b = new Bitmap(destWidth, destHeight);
            var g = Graphics.FromImage(b);
            try
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImage(original, 0, 0, destWidth, destHeight);
            }
            finally
            {
                g.Dispose();
            }

            return b;
        }
    }
}
