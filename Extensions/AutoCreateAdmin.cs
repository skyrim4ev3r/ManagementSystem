using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Project.Core;
using Project.ProjectDataBase;
using Project.ProjectDataBase.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Extensions
{
    public static class AutoCreateAminExtension
    {
        public static IHost AutoCreateAdmin(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var _context = services.GetRequiredService<DataContext>();

                    if (_context.Fields.Count() < 2)
                    {
                        _context.Fields.Add(new Field
                        {
                            Id = Guid.NewGuid().ToString(),
                            Title = "مهندسی کامپیوتر"
                        });
                        _context.Fields.Add(new Field
                        {
                            Id = Guid.NewGuid().ToString(),
                            Title = "علوم کامپیوتر"
                        });
                        _context.SaveChangesAsync().Wait();
                    }

                    if (_context.Universities.Count() < 2)
                    {
                        _context.Universities.Add(new University
                        {
                            Id = Guid.NewGuid().ToString(),
                            Title = "دانشگاه شهید باهنر کرمان"
                        });
                        _context.Universities.Add(new University
                        {
                            Id = Guid.NewGuid().ToString(),
                            Title = "دانشگاه آزاد کرمان"
                        });
                        _context.SaveChangesAsync().Wait();
                    }

                    if (_context.Provinces.Count() < 20)
                    {
                        var pp = new List<Province>()
                        {
                            new Province{
                                Id = "1",Name = "آذربایجان شرقی",
                            },
                            new Province{Id = "2",Name = "آذربایجان غربی",},
                            new Province{Id = "3",Name = "اردبیل",},
                            new Province{Id = "4",Name = "اصفهان",},
                            new Province{Id = "5",Name = "البرز",},
                            new Province{Id = "6",Name = "ایلام",},
                            new Province{Id = "7",Name = "بوشهر",},
                            new Province{Id = "8",Name = "تهران",},
                            new Province{Id = "9",Name = "چهارمحال و بختیاری",},
                            new Province{Id = "10",Name = "خراسان جنوبی",},
                            new Province{Id = "11",Name = "خراسان رضوی",},
                            new Province{Id = "12",Name = "خراسان شمالی",},
                            new Province{Id = "13",Name = "خوزستان",},
                            new Province{Id = "14",Name = "زنجان",},
                            new Province{Id = "15",Name = "سمنان",},
                            new Province{Id = "16",Name = "سیستان و بلوچستان",},
                            new Province{Id = "17",Name = "فارس",},
                            new Province{Id = "18",Name = "قزوین",},
                            new Province{Id = "19",Name = "قم",},
                            new Province{Id = "20",Name = "کردستان",},
                            new Province{Id = "21",Name = "کرمان",},
                            new Province{Id = "22",Name = "کرمانشاه",},
                            new Province{Id = "23",Name = "کهگلویه و بوایرحمد",},
                            new Province{Id = "24",Name = "گلستان",},
                            new Province{Id = "25",Name = "گیلان",},
                            new Province{Id = "26",Name = "لرستان",},
                            new Province{Id = "27",Name = "مازندران",},
                            new Province{Id = "28",Name = "مرکزی",},
                            new Province{Id = "29",Name = "هرمزگان",},
                            new Province{Id = "30",Name = "همدان",},
                            new Province{Id = "31",Name = "یزد",},
                        };
                        foreach (var p in pp)
                        {
                            _context.Provinces.Add(p);
                        }
                        _context.SaveChangesAsync().Wait();
                    }
                    if (_context.Cities.Count() < 100)
                    {
                        var pp = new List<City>()
                        {
                            new City{Id = "1",ProvinceId = "1",Name ="هشترود",},new City{Id = "2",ProvinceId = "1",Name ="اهر",},new City{Id = "3",ProvinceId = "1",Name ="جلفا",},new City{Id = "4",ProvinceId = "1",Name ="آذرشهر",},new City{Id = "5",ProvinceId = "1",Name ="چاراويماق",},new City{Id = "6",ProvinceId = "1",Name ="مراغه",},new City{Id = "7",ProvinceId = "1",Name ="هريس",},new City{Id = "8",ProvinceId = "1",Name ="شبستر",},new City{Id = "9",ProvinceId = "1",Name ="ميانه",},new City{Id = "10",ProvinceId = "1",Name ="ورزقان",},new City{Id = "11",ProvinceId = "1",Name ="بستان آباد",},new City{Id = "12",ProvinceId = "1",Name ="تبريز",},new City{Id = "13",ProvinceId = "1",Name ="سراب",},new City{Id = "14",ProvinceId = "1",Name ="كليبر",},new City{Id = "15",ProvinceId = "1",Name ="مرند",},new City{Id = "16",ProvinceId = "1",Name ="خدا آفرين",},new City{Id = "17",ProvinceId = "1",Name ="ملكان",},new City{Id = "18",ProvinceId = "1",Name ="بناب",},new City{Id = "19",ProvinceId = "1",Name ="اسكو",},new City{Id = "20",ProvinceId = "1",Name ="عجب شير",},new City{Id = "21",ProvinceId = "2",Name ="مياندوآب",},new City{Id = "22",ProvinceId = "2",Name ="خوي",},new City{Id = "23",ProvinceId = "2",Name ="پلدشت",},new City{Id = "24",ProvinceId = "2",Name ="سر دشت",},new City{Id = "25",ProvinceId = "2",Name ="ماكو",},new City{Id = "26",ProvinceId = "2",Name ="تكاب",},new City{Id = "27",ProvinceId = "2",Name ="پيرانشهر",},new City{Id = "28",ProvinceId = "2",Name ="شوط",},new City{Id = "29",ProvinceId = "2",Name ="اشنويه",},new City{Id = "30",ProvinceId = "2",Name ="شاهين دژ",},new City{Id = "31",ProvinceId = "2",Name ="چالدران",},new City{Id = "32",ProvinceId = "2",Name ="چايپاره",},new City{Id = "33",ProvinceId = "2",Name ="بوكان",},new City{Id = "34",ProvinceId = "2",Name ="سلماس",},new City{Id = "35",ProvinceId = "2",Name ="اروميه",},new City{Id = "36",ProvinceId = "2",Name ="نقده",},new City{Id = "37",ProvinceId = "2",Name ="مهاباد",},new City{Id = "38",ProvinceId = "3",Name ="نير",},new City{Id = "39",ProvinceId = "3",Name ="اردبيل",},new City{Id = "40",ProvinceId = "3",Name ="خلخال",},new City{Id = "41",ProvinceId = "3",Name ="كوثر",},new City{Id = "42",ProvinceId = "3",Name ="مشگين شهر",},new City{Id = "43",ProvinceId = "3",Name ="بيله سوار",},new City{Id = "44",ProvinceId = "3",Name ="سرعين",},new City{Id = "45",ProvinceId = "3",Name ="پارس آباد",},new City{Id = "46",ProvinceId = "3",Name ="گرمي",},new City{Id = "47",ProvinceId = "3",Name ="نمين",},new City{Id = "48",ProvinceId = "4",Name ="نائين",},new City{Id = "49",ProvinceId = "4",Name ="نجف آباد",},new City{Id = "50",ProvinceId = "4",Name ="آران و بيدگل",},new City{Id = "51",ProvinceId = "4",Name ="چادگان",},new City{Id = "52",ProvinceId = "4",Name ="تيران و كرون",},new City{Id = "53",ProvinceId = "4",Name ="اصفهان",},new City{Id = "54",ProvinceId = "4",Name ="شهرضا",},new City{Id = "55",ProvinceId = "4",Name ="سميرم",},new City{Id = "56",ProvinceId = "4",Name ="خميني شهر",},new City{Id = "57",ProvinceId = "4",Name ="دهاقان",},new City{Id = "58",ProvinceId = "4",Name ="برخوار",},new City{Id = "59",ProvinceId = "4",Name ="شاهين شهر و ميمه",},new City{Id = "60",ProvinceId = "4",Name ="خوانسار",},new City{Id = "61",ProvinceId = "4",Name ="نطنز",},new City{Id = "62",ProvinceId = "4",Name ="مباركه",},new City{Id = "63",ProvinceId = "4",Name ="اردستان",},new City{Id = "64",ProvinceId = "4",Name ="خور و بيابانك",},new City{Id = "65",ProvinceId = "4",Name ="فلاورجان",},new City{Id = "66",ProvinceId = "4",Name ="فريدونشهر",},new City{Id = "67",ProvinceId = "4",Name ="كاشان",},new City{Id = "68",ProvinceId = "4",Name ="لنجان",},new City{Id = "69",ProvinceId = "4",Name ="گلپايگان",},new City{Id = "70",ProvinceId = "4",Name ="فريدن",},new City{Id = "71",ProvinceId = "5",Name ="اشتهارد",},new City{Id = "72",ProvinceId = "5",Name ="طالقان",},new City{Id = "73",ProvinceId = "5",Name ="نظر آباد",},new City{Id = "74",ProvinceId = "5",Name ="كرج",},new City{Id = "75",ProvinceId = "5",Name ="ساوجبلاغ",},new City{Id = "76",ProvinceId = "6",Name ="مهران",},new City{Id = "77",ProvinceId = "6",Name ="ايلام",},new City{Id = "78",ProvinceId = "6",Name ="ايوان",},new City{Id = "79",ProvinceId = "6",Name ="شيروان و چرداول",},new City{Id = "80",ProvinceId = "6",Name ="دره شهر",},new City{Id = "81",ProvinceId = "6",Name ="آبدانان",},new City{Id = "82",ProvinceId = "6",Name ="دهلران",},new City{Id = "83",ProvinceId = "6",Name ="ملكشاهي",},new City{Id = "84",ProvinceId = "7",Name ="تنگستان",},new City{Id = "85",ProvinceId = "7",Name ="ديلم",},new City{Id = "86",ProvinceId = "7",Name ="جم",},new City{Id = "87",ProvinceId = "7",Name ="گناوه",},new City{Id = "88",ProvinceId = "7",Name ="دشتي",},new City{Id = "89",ProvinceId = "7",Name ="دشتستان",},new City{Id = "90",ProvinceId = "7",Name ="دير",},new City{Id = "91",ProvinceId = "7",Name ="بوشهر",},new City{Id = "92",ProvinceId = "7",Name ="كنگان",},new City{Id = "93",ProvinceId = "8",Name ="شميرانات",},new City{Id = "94",ProvinceId = "8",Name ="رباط كريم",},new City{Id = "95",ProvinceId = "8",Name ="فيروز كوه",},new City{Id = "96",ProvinceId = "8",Name ="ورامين",},new City{Id = "97",ProvinceId = "8",Name ="اسلامشهر",},new City{Id = "98",ProvinceId = "8",Name ="تهران",},new City{Id = "99",ProvinceId = "8",Name ="ري",},new City{Id = "100",ProvinceId = "8",Name ="پاكدشت",},new City{Id = "101",ProvinceId = "8",Name ="پيشوا",},new City{Id = "102",ProvinceId = "8",Name ="قدس",},new City{Id = "103",ProvinceId = "8",Name ="ملارد",},new City{Id = "104",ProvinceId = "8",Name ="شهريار",},new City{Id = "105",ProvinceId = "8",Name ="دماوند",},new City{Id = "106",ProvinceId = "9",Name ="شهر كرد",},new City{Id = "107",ProvinceId = "9",Name ="اردل",},new City{Id = "108",ProvinceId = "9",Name ="كوهرنگ",},new City{Id = "109",ProvinceId = "9",Name ="لردگان",},new City{Id = "110",ProvinceId = "9",Name ="كيار",},new City{Id = "111",ProvinceId = "9",Name ="بروجن",},new City{Id = "112",ProvinceId = "10",Name ="زيركوه",},new City{Id = "113",ProvinceId = "10",Name ="خوسف",},new City{Id = "114",ProvinceId = "10",Name ="قائنات",},new City{Id = "115",ProvinceId = "10",Name ="درميان",},new City{Id = "116",ProvinceId = "10",Name ="بشرويه",},new City{Id = "117",ProvinceId = "10",Name ="فردوس",},new City{Id = "118",ProvinceId = "10",Name ="سربيشه",},new City{Id = "119",ProvinceId = "10",Name ="بيرجند",},new City{Id = "120",ProvinceId = "10",Name ="سرايان",},new City{Id = "121",ProvinceId = "10",Name ="نهبندان",},new City{Id = "122",ProvinceId = "11",Name ="داورزن",},new City{Id = "123",ProvinceId = "11",Name ="كلات",},new City{Id = "124",ProvinceId = "11",Name ="بردسكن",},new City{Id = "125",ProvinceId = "11",Name ="نيشابور",},new City{Id = "126",ProvinceId = "11",Name ="تربت حيدريه",},new City{Id = "127",ProvinceId = "11",Name ="تايباد",},new City{Id = "128",ProvinceId = "11",Name ="خواف",},new City{Id = "129",ProvinceId = "11",Name ="مه ولات",},new City{Id = "130",ProvinceId = "11",Name ="چناران",},new City{Id = "131",ProvinceId = "11",Name ="درگز",},new City{Id = "132",ProvinceId = "11",Name ="فيروزه",},new City{Id = "133",ProvinceId = "11",Name ="سرخس",},new City{Id = "134",ProvinceId = "11",Name ="گناباد",},new City{Id = "135",ProvinceId = "11",Name ="رشتخوار",},new City{Id = "136",ProvinceId = "11",Name ="سبزوار",},new City{Id = "137",ProvinceId = "11",Name ="بينالود",},new City{Id = "138",ProvinceId = "11",Name ="زاوه",},new City{Id = "139",ProvinceId = "11",Name ="جوين",},new City{Id = "140",ProvinceId = "11",Name ="مشهد",},new City{Id = "141",ProvinceId = "11",Name ="بجستان",},new City{Id = "142",ProvinceId = "11",Name ="باخرز",},new City{Id = "143",ProvinceId = "11",Name ="فريمان",},new City{Id = "144",ProvinceId = "11",Name ="قوچان",},new City{Id = "145",ProvinceId = "11",Name ="تربت جام",},new City{Id = "146",ProvinceId = "11",Name ="خليل آباد",},new City{Id = "147",ProvinceId = "11",Name ="كاشمر",},new City{Id = "148",ProvinceId = "11",Name ="جغتاي",},new City{Id = "149",ProvinceId = "11",Name ="خوشاب",},new City{Id = "150",ProvinceId = "12",Name ="بجنورد",},new City{Id = "151",ProvinceId = "12",Name ="جاجرم",},new City{Id = "152",ProvinceId = "12",Name ="اسفراين",},new City{Id = "153",ProvinceId = "12",Name ="فاروج",},new City{Id = "154",ProvinceId = "12",Name ="مانه و سملقان",},new City{Id = "155",ProvinceId = "12",Name ="شيروان",},new City{Id = "156",ProvinceId = "12",Name ="گرمه",},new City{Id = "157",ProvinceId = "13",Name ="آغاجاري",},new City{Id = "158",ProvinceId = "13",Name ="شوشتر",},new City{Id = "159",ProvinceId = "13",Name ="دشت آزادگان",},new City{Id = "160",ProvinceId = "13",Name ="اميديه",},new City{Id = "161",ProvinceId = "13",Name ="گتوند",},new City{Id = "162",ProvinceId = "13",Name ="شادگان",},new City{Id = "163",ProvinceId = "13",Name ="دزفول",},new City{Id = "164",ProvinceId = "13",Name ="رامشير",},new City{Id = "165",ProvinceId = "13",Name ="بهبهان",},new City{Id = "166",ProvinceId = "13",Name ="باوي",},new City{Id = "167",ProvinceId = "13",Name ="انديمشك",},new City{Id = "168",ProvinceId = "13",Name ="اندیکا",},new City{Id = "169",ProvinceId = "13",Name ="هنديجان",},new City{Id = "170",ProvinceId = "13",Name ="رامهرمز",},new City{Id = "171",ProvinceId = "13",Name ="شوش",},new City{Id = "172",ProvinceId = "13",Name ="لالي",},new City{Id = "173",ProvinceId = "13",Name ="ايذه",},new City{Id = "174",ProvinceId = "13",Name ="هويزه",},new City{Id = "175",ProvinceId = "13",Name ="مسجد سليمان",},new City{Id = "176",ProvinceId = "13",Name ="آبادان",},new City{Id = "177",ProvinceId = "13",Name ="اهواز",},new City{Id = "178",ProvinceId = "13",Name ="خرمشهر",},new City{Id = "179",ProvinceId = "13",Name ="باغ ملك",},new City{Id = "180",ProvinceId = "13",Name ="بندر ماهشهر",},new City{Id = "181",ProvinceId = "13",Name ="هفتگل",},new City{Id = "182",ProvinceId = "14",Name ="خدابنده",},new City{Id = "183",ProvinceId = "14",Name ="خرمدره",},new City{Id = "184",ProvinceId = "14",Name ="زنجان",},new City{Id = "185",ProvinceId = "14",Name ="طارم",},new City{Id = "186",ProvinceId = "14",Name ="ايجرود",},new City{Id = "187",ProvinceId = "14",Name ="ابهر",},new City{Id = "188",ProvinceId = "14",Name ="ماهنشان",},new City{Id = "189",ProvinceId = "15",Name ="ميامي",},new City{Id = "190",ProvinceId = "15",Name ="آرادان",},new City{Id = "191",ProvinceId = "15",Name ="مهدي شهر",},new City{Id = "192",ProvinceId = "15",Name ="دامغان",},new City{Id = "193",ProvinceId = "15",Name ="شاهرود",},new City{Id = "194",ProvinceId = "15",Name ="گرمسار",},new City{Id = "195",ProvinceId = "15",Name ="سمنان",},new City{Id = "196",ProvinceId = "16",Name ="كنارك",},new City{Id = "197",ProvinceId = "16",Name ="نيك شهر",},new City{Id = "198",ProvinceId = "16",Name ="چاه بهار",},new City{Id = "199",ProvinceId = "16",Name ="مهرستان",},new City{Id = "200",ProvinceId = "16",Name ="سرباز",},new City{Id = "201",ProvinceId = "16",Name ="دلگان",},new City{Id = "202",ProvinceId = "16",Name ="خاش",},new City{Id = "203",ProvinceId = "16",Name ="هيرمند",},new City{Id = "204",ProvinceId = "16",Name ="زهك",},new City{Id = "205",ProvinceId = "16",Name ="زابل",},new City{Id = "206",ProvinceId = "16",Name ="ايرانشهر",},new City{Id = "207",ProvinceId = "16",Name ="زاهدان",},new City{Id = "208",ProvinceId = "16",Name ="سراوان",},new City{Id = "209",ProvinceId = "16",Name ="سيب سوران",},new City{Id = "210",ProvinceId = "17",Name ="خرامه",},new City{Id = "211",ProvinceId = "17",Name ="لارستان",},new City{Id = "212",ProvinceId = "17",Name ="مرودشت",},new City{Id = "213",ProvinceId = "17",Name ="بوانات",},new City{Id = "214",ProvinceId = "17",Name ="اقليد",},new City{Id = "215",ProvinceId = "17",Name ="فسا",},new City{Id = "216",ProvinceId = "17",Name ="داراب",},new City{Id = "217",ProvinceId = "17",Name ="ممسني",},new City{Id = "218",ProvinceId = "17",Name ="كازرون",},new City{Id = "219",ProvinceId = "17",Name ="استهبان",},new City{Id = "220",ProvinceId = "17",Name ="ارسنجان",},new City{Id = "221",ProvinceId = "17",Name ="جهرم",},new City{Id = "222",ProvinceId = "17",Name ="خنج",},new City{Id = "223",ProvinceId = "17",Name ="زرين دشت",},new City{Id = "224",ProvinceId = "17",Name ="آباده",},new City{Id = "225",ProvinceId = "17",Name ="مهر",},new City{Id = "226",ProvinceId = "17",Name ="گراش",},new City{Id = "227",ProvinceId = "17",Name ="سپيدان",},new City{Id = "228",ProvinceId = "17",Name ="فراشبند",},new City{Id = "229",ProvinceId = "17",Name ="پاسارگاد",},new City{Id = "230",ProvinceId = "17",Name ="شيراز",},new City{Id = "231",ProvinceId = "17",Name ="رستم",},new City{Id = "232",ProvinceId = "17",Name ="لامرد",},new City{Id = "233",ProvinceId = "17",Name ="ني ريز",},new City{Id = "234",ProvinceId = "17",Name ="سروستان",},new City{Id = "235",ProvinceId = "17",Name ="كوار",},new City{Id = "236",ProvinceId = "17",Name ="خرم بيد",},new City{Id = "237",ProvinceId = "17",Name ="قيروكارزين",},new City{Id = "238",ProvinceId = "17",Name ="فيروز آباد",},new City{Id = "239",ProvinceId = "18",Name ="آوج",},new City{Id = "240",ProvinceId = "18",Name ="تاكستان",},new City{Id = "241",ProvinceId = "18",Name ="بوئين زهرا",},new City{Id = "242",ProvinceId = "18",Name ="آبيك",},new City{Id = "243",ProvinceId = "20",Name ="دهگلان",},new City{Id = "244",ProvinceId = "20",Name ="سنندج",},new City{Id = "245",ProvinceId = "20",Name ="ديواندره",},new City{Id = "246",ProvinceId = "20",Name ="سروآباد",},new City{Id = "247",ProvinceId = "20",Name ="بانه",},new City{Id = "248",ProvinceId = "20",Name ="كامياران",},new City{Id = "249",ProvinceId = "20",Name ="قروه",},new City{Id = "250",ProvinceId = "20",Name ="سقز",},new City{Id = "251",ProvinceId = "20",Name ="بيجار",},new City{Id = "252",ProvinceId = "20",Name ="مريوان",},new City{Id = "253",ProvinceId = "21",Name ="فارياب",},new City{Id = "254",ProvinceId = "21",Name ="ارزوئيه",},new City{Id = "255",ProvinceId = "21",Name ="نرماشير",},new City{Id = "256",ProvinceId = "21",Name ="فهرج",},new City{Id = "257",ProvinceId = "21",Name ="رفسنجان",},new City{Id = "258",ProvinceId = "21",Name ="كوهبنان",},new City{Id = "259",ProvinceId = "21",Name ="رودبار جنوب",},new City{Id = "260",ProvinceId = "21",Name ="عنبر آباد",},new City{Id = "261",ProvinceId = "21",Name ="كهنوج",},new City{Id = "262",ProvinceId = "21",Name ="جيرفت",},new City{Id = "263",ProvinceId = "21",Name ="شهر بابك",},new City{Id = "264",ProvinceId = "21",Name ="کرمان",},new City{Id = "265",ProvinceId = "21",Name ="بم",},new City{Id = "266",ProvinceId = "21",Name ="رابر",},new City{Id = "267",ProvinceId = "21",Name ="بافت",},new City{Id = "268",ProvinceId = "21",Name ="منوجان",},new City{Id = "269",ProvinceId = "21",Name ="زرند",},new City{Id = "270",ProvinceId = "21",Name ="ريگان",},new City{Id = "271",ProvinceId = "21",Name ="انار",},new City{Id = "272",ProvinceId = "21",Name ="راور",},new City{Id = "273",ProvinceId = "21",Name ="قلعه گنج",},new City{Id = "274",ProvinceId = "21",Name ="بردسير",},new City{Id = "275",ProvinceId = "21",Name ="سيرجان",},new City{Id = "276",ProvinceId = "22",Name ="اسلام آباد غرب",},new City{Id = "277",ProvinceId = "22",Name ="سنقر",},new City{Id = "278",ProvinceId = "22",Name ="پاوه",},new City{Id = "279",ProvinceId = "22",Name ="ثلاث باباجاني",},new City{Id = "280",ProvinceId = "22",Name ="دالاهو",},new City{Id = "281",ProvinceId = "22",Name ="جوانرود",},new City{Id = "282",ProvinceId = "22",Name ="هرسين",},new City{Id = "283",ProvinceId = "22",Name ="سر پل ذهاب",},new City{Id = "284",ProvinceId = "22",Name ="قصر شيرين",},new City{Id = "285",ProvinceId = "22",Name ="كنگاور",},new City{Id = "286",ProvinceId = "22",Name ="روانسر",},new City{Id = "287",ProvinceId = "22",Name ="صحنه",},new City{Id = "288",ProvinceId = "23",Name ="دنا",},new City{Id = "289",ProvinceId = "23",Name ="باشت",},new City{Id = "290",ProvinceId = "23",Name ="گچساران",},new City{Id = "291",ProvinceId = "23",Name ="بهمئي",},new City{Id = "292",ProvinceId = "23",Name ="چرام",},new City{Id = "293",ProvinceId = "23",Name ="كهگيلويه",},new City{Id = "294",ProvinceId = "23",Name ="بوير احمد",},new City{Id = "295",ProvinceId = "24",Name ="تركمن",},new City{Id = "296",ProvinceId = "24",Name ="بندر گز",},new City{Id = "297",ProvinceId = "24",Name ="گاليكش",},new City{Id = "298",ProvinceId = "24",Name ="كلاله",},new City{Id = "299",ProvinceId = "24",Name ="مينودشت",},new City{Id = "300",ProvinceId = "24",Name ="علي آباد",},new City{Id = "301",ProvinceId = "24",Name ="گنبد كاووس",},new City{Id = "302",ProvinceId = "24",Name ="مراوه تپه",},new City{Id = "303",ProvinceId = "24",Name ="راميان",},new City{Id = "304",ProvinceId = "24",Name ="كردكوي",},new City{Id = "305",ProvinceId = "24",Name ="گميشان",},new City{Id = "306",ProvinceId = "24",Name ="آق قلا",},new City{Id = "307",ProvinceId = "24",Name ="آزاد شهر",},new City{Id = "308",ProvinceId = "24",Name ="گرگان",},new City{Id = "309",ProvinceId = "25",Name ="بندر انزلي",},new City{Id = "310",ProvinceId = "25",Name ="صومعه سرا",},new City{Id = "311",ProvinceId = "25",Name ="لاهيجان",},new City{Id = "312",ProvinceId = "25",Name ="رشت",},new City{Id = "313",ProvinceId = "25",Name ="لنگرود",},new City{Id = "314",ProvinceId = "25",Name ="رودسر",},new City{Id = "315",ProvinceId = "25",Name ="املش",},new City{Id = "316",ProvinceId = "25",Name ="ماسال",},new City{Id = "317",ProvinceId = "25",Name ="شفت",},new City{Id = "318",ProvinceId = "25",Name ="رودبار",},new City{Id = "319",ProvinceId = "25",Name ="فومن",},new City{Id = "320",ProvinceId = "25",Name ="آستارا",},new City{Id = "321",ProvinceId = "25",Name ="سياهكل",},new City{Id = "322",ProvinceId = "25",Name ="آستانه اشرفيه",},new City{Id = "323",ProvinceId = "25",Name ="رضوانشهر",},new City{Id = "324",ProvinceId = "25",Name ="طوالش",},new City{Id = "325",ProvinceId = "26",Name ="دوره",},new City{Id = "326",ProvinceId = "26",Name ="دورود",},new City{Id = "327",ProvinceId = "26",Name ="ازنا",},new City{Id = "328",ProvinceId = "26",Name ="سلسله",},new City{Id = "329",ProvinceId = "26",Name ="اليگودرز",},new City{Id = "330",ProvinceId = "26",Name ="خرم آباد",},new City{Id = "331",ProvinceId = "26",Name ="پلدختر",},new City{Id = "332",ProvinceId = "26",Name ="دلفان",},new City{Id = "333",ProvinceId = "26",Name ="بروجرد",},new City{Id = "334",ProvinceId = "26",Name ="كوهدشت",},new City{Id = "335",ProvinceId = "27",Name ="عباس آباد",},new City{Id = "336",ProvinceId = "27",Name ="بابل",},new City{Id = "337",ProvinceId = "27",Name ="تنكابن",},new City{Id = "338",ProvinceId = "27",Name ="بابلسر",},new City{Id = "339",ProvinceId = "27",Name ="آمل",},new City{Id = "340",ProvinceId = "27",Name ="محمود آباد",},new City{Id = "341",ProvinceId = "27",Name ="گلوگاه",},new City{Id = "342",ProvinceId = "27",Name ="سواد كوه",},new City{Id = "343",ProvinceId = "27",Name ="ساري",},new City{Id = "344",ProvinceId = "27",Name ="نوشهر",},new City{Id = "345",ProvinceId = "27",Name ="نور",},new City{Id = "346",ProvinceId = "27",Name ="مياندورود",},new City{Id = "347",ProvinceId = "27",Name ="رامسر",},new City{Id = "348",ProvinceId = "27",Name ="چالوس",},new City{Id = "349",ProvinceId = "27",Name ="جويبار",},new City{Id = "350",ProvinceId = "27",Name ="بهشهر",},new City{Id = "351",ProvinceId = "27",Name ="قائم شهر",},new City{Id = "352",ProvinceId = "27",Name ="فريدونكنار",},new City{Id = "353",ProvinceId = "27",Name ="نكا",},new City{Id = "354",ProvinceId = "28",Name ="فراهان",},new City{Id = "355",ProvinceId = "28",Name ="خنداب",},new City{Id = "356",ProvinceId = "28",Name ="زرنديه",},new City{Id = "357",ProvinceId = "28",Name ="دليجان",},new City{Id = "358",ProvinceId = "28",Name ="آشتيان",},new City{Id = "359",ProvinceId = "28",Name ="شازند",},new City{Id = "360",ProvinceId = "28",Name ="اراك",},new City{Id = "361",ProvinceId = "28",Name ="محلات",},new City{Id = "362",ProvinceId = "28",Name ="كميجان",},new City{Id = "363",ProvinceId = "28",Name ="ساوه",},new City{Id = "364",ProvinceId = "28",Name ="تفرش",},new City{Id = "365",ProvinceId = "28",Name ="خمين",},new City{Id = "366",ProvinceId = "29",Name ="رودان",},new City{Id = "367",ProvinceId = "29",Name ="ابوموسي",},new City{Id = "368",ProvinceId = "29",Name ="ميناب",},new City{Id = "369",ProvinceId = "29",Name ="جاسك",},new City{Id = "370",ProvinceId = "29",Name ="پارسيان",},new City{Id = "371",ProvinceId = "29",Name ="بندرلنگه",},new City{Id = "372",ProvinceId = "29",Name ="قشم",},new City{Id = "373",ProvinceId = "29",Name ="بندرعباس",},new City{Id = "374",ProvinceId = "29",Name ="بستك",},new City{Id = "375",ProvinceId = "29",Name ="سيريك",},new City{Id = "376",ProvinceId = "29",Name ="خمير",},new City{Id = "377",ProvinceId = "29",Name ="بشاگرد",},new City{Id = "378",ProvinceId = "29",Name ="حاجي آباد",},new City{Id = "379",ProvinceId = "30",Name ="اسد آباد",},new City{Id = "380",ProvinceId = "30",Name ="تويسركان",},new City{Id = "381",ProvinceId = "30",Name ="رزن",},new City{Id = "382",ProvinceId = "30",Name ="نهاوند",},new City{Id = "383",ProvinceId = "30",Name ="بهار",},new City{Id = "384",ProvinceId = "30",Name ="كبودرآهنگ",},new City{Id = "385",ProvinceId = "30",Name ="فامنين",},new City{Id = "386",ProvinceId = "30",Name ="ملاير",},new City{Id = "387",ProvinceId = "31",Name ="مهريز",},new City{Id = "388",ProvinceId = "31",Name ="اردكان",},new City{Id = "389",ProvinceId = "31",Name ="ابركوه",},new City{Id = "390",ProvinceId = "31",Name ="ميبد",},new City{Id = "391",ProvinceId = "31",Name ="خاتم",},new City{Id = "392",ProvinceId = "31",Name ="بافق",},new City{Id = "393",ProvinceId = "31",Name ="بهاباد",},new City{Id = "394",ProvinceId = "31",Name ="تفت",},new City{Id = "395",ProvinceId = "31",Name ="طبس",},new City{Id = "396",ProvinceId = "31",Name ="صدوق",},
                        };
                        foreach (var p in pp)
                        {
                            _context.Cities.Add(p);
                        }
                        _context.SaveChangesAsync().Wait();
                    }
                    
                    var admin = userManager.FindByNameAsync("admin").Result;

                    if (admin == null)
                    {
                        var user = new AppUser
                        {
                            PhoneNumber = "1234",
                            UserName = "admin",
                            IsDeleted = false,
                            CreatedAt = DateTime.Now,
                        };

                        var identityResult = userManager.CreateAsync(user, "Test123456").Result;
                        if (!identityResult.Succeeded)
                        {
                            throw new Exception();
                        }
                        userManager.AddToRoleAsync(user, Roles.Admin).Wait();
                    }
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "Error During Creating Admin");
                }
            }
            return host;
        }
    }
}
