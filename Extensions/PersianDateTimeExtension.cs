using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Extensions
{
    public static class PersianDateTimeExtension
    {
        public static string[] MonthNames = new string[]
        {
            "فروردین",
            "اردیبهشت",
            "خرداد",
            "تیر",
            "مرداد",
            "شهریور",
            "مهر",
            "آبان",
            "آذر",
            "دی",
            "بهمن",
            "اسفند"
        };
        public static string ToShamsiYMD(this System.DateTime dateTime)
        {
            var persianCalendar = new PersianCalendar();

            return persianCalendar.GetYear(dateTime) + "/" +
                   persianCalendar.GetMonth(dateTime).ToString("00") + "/" +
                   persianCalendar.GetDayOfMonth(dateTime).ToString("00")+
                   persianCalendar.GetDayOfWeek(dateTime);
        }

        public static string ToShamsiFormat(this System.DateTime dateTime)
        {
            var shamsiDateTime = dateTime.ToShamsiYMD();

            return shamsiDateTime.ToShamsiFormat();
        }

        public static string ToShamsiFormat(this string shamsiDateTime)
        {
            string result = null;

            if (shamsiDateTime.Contains('/'))
            {
                result = shamsiDateTime.Replace("/", "");
            }

            return result;
        }

        public static string GetTimeOfPersianDateTime(this string shamsiDateTime)
            => (shamsiDateTime.Split(' '))[1];

        public static string GetHourOfPersianDateTime(this string shamsiDateTime)
            => GetTimeOfPersianDateTime(shamsiDateTime).Split(':')[0];

        public static string GetMinuteOfPersianDateTime(this string shamsiDateTime)
            => GetTimeOfPersianDateTime(shamsiDateTime).Split(':')[1];

        public static string GetSecondOfPersianDateTime(this string shamsiDateTime)
            => GetTimeOfPersianDateTime(shamsiDateTime).Split(':')[2];

        public static string GetDateOfPersianDateTime(this string shamsiDateTime)
            => (shamsiDateTime.Split(' '))[0];

        public static string GetYearOfPersianDateTime(this string shamsiDateTime)
            => GetDateOfPersianDateTime(shamsiDateTime).Split('/')[0];

        public static string GetMonthOfPersianDateTime(this string shamsiDateTime)
            => GetDateOfPersianDateTime(shamsiDateTime).Split('/')[1];

        public static string GetDayOfPersianDateTime(this string shamsiDateTime)
            => GetDateOfPersianDateTime(shamsiDateTime).Split('/')[2];

        public static bool IsToday(this string shamsiDateTime)
        {
            var nowDate = System.DateTime.Now.ToShamsiYMD();

            return nowDate == shamsiDateTime.GetDateOfPersianDateTime();
        }

        public static string GetMonthNameInFarsi(int month)
            => MonthNames[month - 1];

        public static string GetMonthNameInFarsi(this string shamsiDateTime)
        {
            var month = shamsiDateTime.GetMonthOfPersianDateTime();

            return GetMonthNameInFarsi(int.Parse(month));
        }

        public static System.DateTime ToMiladi(this string shamsiDateTime)
        {
            if (string.IsNullOrEmpty(shamsiDateTime))
            {
                return System.DateTime.MinValue;
            }

            var shamsiYear = int.Parse(shamsiDateTime.GetYearOfPersianDateTime());
            var shamsiMonth = int.Parse(shamsiDateTime.GetMonthOfPersianDateTime());
            var shamsiDay = int.Parse(shamsiDateTime.GetDayOfPersianDateTime());

            var persianCalendar = new PersianCalendar();
            return persianCalendar.ToDateTime
                (shamsiYear, shamsiMonth, shamsiDay, 0, 0, 0, 0);
        }

        public static System.DateTime ShamsiFormatToMiladi(this string shamsiDateTime)
        {
            var shamsiYear = shamsiDateTime[0] + "" + shamsiDateTime[1] + shamsiDateTime[2] + shamsiDateTime[3];
            var shamsiMonth = shamsiDateTime[4] + "" + shamsiDateTime[5];
            var shamsiDay = shamsiDateTime[6] + "" + shamsiDateTime[7];

            var persianCalendar = new PersianCalendar();
            return persianCalendar.ToDateTime
                (int.Parse(shamsiYear), int.Parse(shamsiMonth), int.Parse(shamsiDay), 0, 0, 0, 0);
        }
    }
}
