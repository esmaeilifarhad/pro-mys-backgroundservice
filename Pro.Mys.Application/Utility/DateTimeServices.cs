using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Mys.Application.Utility
{
    public static class DateTimeServices
    {

        /// <summary>
        /// تاریخ امروز
        /// </summary>
        /// <returns></returns>
        public static string Utl_Date_shamsi_date(bool isRemoveSlash = false)
        {
            System.Globalization.PersianCalendar g;
            g = new System.Globalization.PersianCalendar();
            Int32 y, m, d;
            string yy, mm, dd;
            string rd;
            d = g.GetDayOfMonth(DateTime.Now);
            m = g.GetMonth(DateTime.Now);
            y = g.GetYear(DateTime.Now);
            yy = y.ToString();
            if (d < 10)
            {
                dd = "0" + d.ToString();
            }
            else
            {
                dd = d.ToString();
            }
            if (m < 10)
            {
                mm = "0" + m.ToString();
            }
            else
            {
                mm = m.ToString();
            }
            if (isRemoveSlash)
            {
                rd = yy + mm + dd;
            }
            else
            {
                rd = yy + "/" + mm + "/" + dd;
            }
            return rd;
            // return rd.Remove(0, 2);
        }
        public static string Utl_ghamary_date()
        {
            string shamsyDate = "14001016";
            DateData dateData = new DateData();
            var year = int.Parse(shamsyDate.Substring(0, 4));
            var month = int.Parse(shamsyDate.Substring(4, 2));
            var day = int.Parse(shamsyDate.Substring(6, 2));
            PersianCalendar pc = new PersianCalendar();
            dateData.ShamsyDateFormat = string.Format("{0}/{1}/{2}", shamsyDate.Substring(0, 4), shamsyDate.Substring(4, 2), shamsyDate.Substring(6, 2));
            var datetimeRes = pc.ToDateTime(year, month, day, 0, 0, 0, 0, 0);
            var result = Utility.DateTimeServices.Utl_Date_ConvertToMilday(datetimeRes);
            DateTime myDate = DateTime.Parse(string.Format("{0}/{1}/{2}", result.Substring(0, 4), result.Substring(5, 2), result.Substring(8, 2)));

            //dateData.weekDurationShamsy1 = Utl_Date_WeekDuration(datetimeRes);
            //dateData.MildayDateFormat = datetimeRes.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            //DateTime myDate = DateTime.ParseExact(result, "yyyy/MM/dd",
            //                           System.Globalization.CultureInfo.InvariantCulture);

            var xxxx = myDate.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE"));

            var x = myDate.ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));

            //DateTimeFormatInfo Calendar = new System.Globalization.HijriCalendar();

            System.Globalization.HijriCalendar g;
            g = new System.Globalization.HijriCalendar();
            var d = g.GetDayOfMonth(myDate);
            var m = g.GetMonth(myDate);
            var y = g.GetYear(myDate);
            var yy = y.ToString();
            return "";
        }
        public static string Utl_Date_shamsi_dateTomarrow()
        {
            var today = DateTime.Now;
            var tomorrow = today.AddDays(1);
            var yesterday = today.AddDays(-1);

            System.Globalization.PersianCalendar g;
            g = new System.Globalization.PersianCalendar();
            Int32 y, m, d;
            string yy, mm, dd;
            string rd;
            d = g.GetDayOfMonth(tomorrow);
            m = g.GetMonth(tomorrow);
            y = g.GetYear(tomorrow);
            yy = y.ToString();
            if (d < 10)
            {
                dd = "0" + d.ToString();
            }
            else
            {
                dd = d.ToString();
            }
            if (m < 10)
            {
                mm = "0" + m.ToString();
            }
            else
            {
                mm = m.ToString();
            }
            rd = yy + "/" + mm + "/" + dd;
            return rd.Remove(0, 2);
        }
        //چند روز قبل یا بعد
        public static long Utl_Date_shamsi_NDays(int n)
        {
            var today = DateTime.Now;
            var tomorrow = today.AddDays(n);
            //var yesterday = today.AddDays(-1);

            System.Globalization.PersianCalendar g;
            g = new System.Globalization.PersianCalendar();
            Int32 y, m, d;
            string yy, mm, dd;
            string rd;
            d = g.GetDayOfMonth(tomorrow);
            m = g.GetMonth(tomorrow);
            y = g.GetYear(tomorrow);
            yy = y.ToString();
            if (d < 10)
            {
                dd = "0" + d.ToString();
            }
            else
            {
                dd = d.ToString();
            }
            if (m < 10)
            {
                mm = "0" + m.ToString();
            }
            else
            {
                mm = m.ToString();
            }
            rd = yy + mm + dd;
            return Int64.Parse(rd);
        }
        /// <summary>
        /// نمایش چند روز قبل یا بعد از تاریخ ورودی
        /// </summary>
        /// <param name="date"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string Utl_Date_shamsi_NDays(string date, int n)
        {
            if (date.Length == 8)
            {
                date = Utl_Date_ConvertDateToSlash(date);
            }
            // Persian Date
            //  var value = "1396/11/27";
            // Convert to Miladi
            DateTime dt = DateTime.Parse(date, new CultureInfo("fa-IR"));
            var res = dt.AddDays(n);

            System.Globalization.PersianCalendar g;
            g = new System.Globalization.PersianCalendar();

            Int32 y, m, d;
            string yy, mm, dd;
            string rd;
            d = g.GetDayOfMonth(res);
            m = g.GetMonth(res);
            y = g.GetYear(res);
            yy = y.ToString();
            if (d < 10)
            {
                dd = "0" + d.ToString();
            }
            else
            {
                dd = d.ToString();
            }
            if (m < 10)
            {
                mm = "0" + m.ToString();
            }
            else
            {
                mm = m.ToString();
            }
            rd = yy + mm + dd;
            return rd;
        }
        public static string Utl_Date_ConvertDateToSqlFormat(this string DateSlash)
        {
            string strNew = DateSlash;
            if (DateSlash.Length == 8)
            {
                strNew = DateSlash.Replace("/", string.Empty);
            }
            if (DateSlash.Length == 10)
            {
                strNew = DateSlash.Replace("/", string.Empty);
                /*string Year = strNew.Substring(2, 2);
                string Month = strNew.Substring(4, 2);
                string Day = strNew.Substring(6, 2);
                strNew= Year+ Month+ Day;*/
            }
            return strNew;
        }
        public static string Utl_Date_ConvertDateToDateFormat(this string DateSlash)
        {
            string strNew = string.Empty;
            if (DateSlash.Length == 8)
            {
                string Year = DateSlash.Substring(0, 4);
                string Month = DateSlash.Substring(4, 2);
                string Day = DateSlash.Substring(6, 2);
                strNew = Year + '/' + Month + '/' + Day;

            }
            if (DateSlash.Length == 10)
            {
                string Year = DateSlash.Substring(0, 4);
                string Month = DateSlash.Substring(4, 2);
                string Day = DateSlash.Substring(6, 2);
                strNew = Year + '/' + Month + '/' + Day;
            }
            return strNew;


        }
        public static string Utl_Date_ConvertDateToSlash(this string str)
        {
            string Result = str;
            if (str.Length == 6)
            {
                string Yaer = str.Substring(0, 2);
                string Moth = str.Substring(2, 2);
                string Dayy = str.Substring(4, 2);
                Result = (string.Format("{0}/{1}/{2}", Yaer, Moth, Dayy));
            }
            if (str.Length == 8)
            {
                string Yaer = str.Substring(0, 4);
                string Moth = str.Substring(4, 2);
                string Dayy = str.Substring(6, 2);
                Result = (string.Format("{0}/{1}/{2}", Yaer, Moth, Dayy));
            }
            return Result;
        }
        /// <summary>
        /// تبدیل تاریخ میلادی به شمسی
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string Utl_Date_convertToShamsi(this DateTime date)
        {
            //string GregorianDate = "Thursday, October 24, 2013";
            //DateTime d = DateTime.Parse(GregorianDate);
            //PersianCalendar pc = new PersianCalendar();
            //Console.WriteLine(string.Format("{0}/{1}/{2}", pc.GetYear(d), pc.GetMonth(d), pc.GetDayOfMonth(d)));

            System.Globalization.PersianCalendar persianCalandar =
                                    new System.Globalization.PersianCalendar();

            int year = persianCalandar.GetYear(date);
            int month = persianCalandar.GetMonth(date);
            int day = persianCalandar.GetDayOfMonth(date);

            var result = year + "" + (month < 10 ? "0" + month.ToString() : month.ToString()) + "" + (day < 10 ? "0" + day.ToString() : day.ToString());
            return result;
        }
        /// <summary>
        /// تبدیل شمسی به میلادی
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string Utl_Date_ConvertToMilday(this DateTime date)
        {
            return date.ToString("yyyy/MM/dd/hh/mm/ss/ffffff", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// 14000709  input string param    output DateData 
        /// </summary>
        /// <param name="shamsyDate"></param>
        /// <returns></returns>
        public static DateData Utl_Date_DayOfWeek(string shamsyDate = null, DateTime? date = null)
        {
            DateData dateData = new DateData();
            if (shamsyDate == null && date == null)
            {
                shamsyDate = Utility.DateTimeServices.Utl_Date_shamsi_date();
            }
            if (shamsyDate == null && date != null)
            {
                shamsyDate = Utility.DateTimeServices.Utl_Date_convertToShamsi((DateTime)date);
            }
            if (shamsyDate != null && date != null)
            {
                return dateData;
            }


            int year = 0;
            int month = 0;
            int day = 0;


            if (shamsyDate.Length == 8)
            {
                year = int.Parse(shamsyDate.Substring(0, 4));
                month = int.Parse(shamsyDate.Substring(4, 2));
                day = int.Parse(shamsyDate.Substring(6, 2));
            }
            else if (shamsyDate.Length == 10)
            {
                year = int.Parse(shamsyDate.Substring(0, 4));
                month = int.Parse(shamsyDate.Substring(5, 2));
                day = int.Parse(shamsyDate.Substring(8, 2));
            }
            else
            {
                return null;
            }
            PersianCalendar pc = new PersianCalendar();
            if (shamsyDate.Length == 8)
            {
                dateData.ShamsyDateFormat = string.Format("{0}/{1}/{2}", shamsyDate.Substring(0, 4), shamsyDate.Substring(4, 2), shamsyDate.Substring(6, 2));
                dateData.ShamsyDateFormatSlashLess = shamsyDate;
            }
            else
            {
                dateData.ShamsyDateFormat = shamsyDate;
                dateData.ShamsyDateFormatSlashLess = Utl_Date_ConvertDateToSqlFormat(shamsyDate);
            }
            var datetimeRes = pc.ToDateTime(year, month, day, 0, 0, 0, 0, 0);
            //----
            var result = Utility.DateTimeServices.Utl_Date_ConvertToMilday(datetimeRes);

            dateData.georgian = DateTime.ParseExact(result.Substring(8, 2) + "/" + result.Substring(5, 2) + "/" + result.Substring(0, 4), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //DateTime myDate = DateTime.Parse(string.Format("{0}/{1}/{2}", result.Substring(0, 4), result.Substring(5, 2), result.Substring(8, 2)));
            //----
            //var ghamary = myDate.ToString("dd/MM/yyyy", new CultureInfo("ar-AE"));
            dateData.ghamary = ConvertDateCalendar(dateData.georgian.AddDays(-1), "Hijri", "en-US");

            //----
            dateData.weekDurationShamsy1 = Utl_Date_WeekDuration(datetimeRes);
            dateData.MildayDateFormat = datetimeRes.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            var x = pc.GetDayOfWeek(datetimeRes);
            dateData.dayOfWeek = x;
            switch (x)
            {
                case DayOfWeek.Sunday:
                    dateData.DayOfWeekPersian = "یکشنبه";
                    dateData.DayOfWeekNum = 1;
                    break;
                case DayOfWeek.Monday:
                    dateData.DayOfWeekPersian = "دوشنبه";
                    dateData.DayOfWeekNum = 2;
                    break;
                case DayOfWeek.Tuesday:
                    dateData.DayOfWeekPersian = "سه شنبه";
                    dateData.DayOfWeekNum = 3;
                    break;
                case DayOfWeek.Wednesday:
                    dateData.DayOfWeekPersian = "چهارشنبه";
                    dateData.DayOfWeekNum = 4;
                    break;
                case DayOfWeek.Thursday:
                    dateData.DayOfWeekPersian = "پنجشنبه";
                    dateData.DayOfWeekNum = 5;
                    break;
                case DayOfWeek.Friday:
                    dateData.DayOfWeekPersian = "جمعه";
                    dateData.DayOfWeekNum = 6;
                    break;
                case DayOfWeek.Saturday:
                    dateData.DayOfWeekPersian = "شنبه";
                    dateData.DayOfWeekNum = 0;
                    break;
                default:
                    break;
            }

            if (month <= 6)
            {
                dateData.MonthDays = 31;
            }
            else if (month <= 11 && month >= 7)
            {
                dateData.MonthDays = 30;
            }
            else if (month == 12)
            {
                //کبیسه
                if (year % 4 == 3)
                {
                    dateData.MonthDays = 30;
                }
                else
                {
                    dateData.MonthDays = 29;
                }
            }
            dateData.FirstMounth = year + "" + (month < 10 ? "0" + month.ToString() : month.ToString()) + "" + "01";
            dateData.LastMounth = year + "" + (month < 10 ? "0" + month.ToString() : month.ToString()) + "" + dateData.MonthDays.ToString();
            dateData.TommarowNoSlash = Utl_Date_shamsi_NDays(1);
            dateData.TommarowFormat =DateTimeServices.Utl_Date_ConvertDateToDateFormat(Utl_Date_shamsi_NDays(1).ToString());
            if (year % 4 == 3)
                dateData.IsKabise = true;
            dateData.IsKabise = false;
            //اختلاف تعداد روز تاریخ تا امروز
            dateData.countDayBetween = (dateData.georgian - DateTime.Now).Days;
            return dateData;
        }


        public static string ConvertDateCalendar(DateTime DateConv, string Calendar, string DateLangCulture)
        {
            DateTimeFormatInfo DTFormat;
            DateLangCulture = DateLangCulture.ToLower();
            /// We can't have the hijri date writen in English. We will get a runtime error

            if (Calendar == "Hijri" && DateLangCulture.StartsWith("en-"))
            {
                DateLangCulture = "ar-sa";
            }

            /// Set the date time format to the given culture
            DTFormat = new System.Globalization.CultureInfo(DateLangCulture, false).DateTimeFormat;

            /// Set the calendar property of the date time format to the given calendar
            switch (Calendar)
            {
                case "Hijri":
                    DTFormat.Calendar = new System.Globalization.HijriCalendar();
                    break;

                case "Gregorian":
                    DTFormat.Calendar = new System.Globalization.GregorianCalendar();
                    break;

                default:
                    return "";
            }
            /// We format the date structure to whatever we want
            DTFormat.ShortDatePattern = "dd/MM/yyyy";
            //return (DateConv.Date.ToString("f", DTFormat));
            return DateConv.Date.ToString("yyyy-MM-dd", DTFormat);
        }

        /// <summary>
        /// نمایش تاریخ اول هفته و آخر هفته
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static weekDurationShamsy Utl_Date_WeekDuration(this DateTime date)
        {
            weekDurationShamsy weekDurationShamsy = new weekDurationShamsy();
            // date = date.AddDays(-12);

            //یکشنبه
            if ((int)date.DayOfWeek == 0)
            {
                weekDurationShamsy.FirstDate = date.AddDays(-1).Utl_Date_convertToShamsi();
                weekDurationShamsy.LastDate = date.AddDays(5).Utl_Date_convertToShamsi();
                weekDurationShamsy.DayofWeek = date.DayOfWeek;
                weekDurationShamsy.Date = date.Utl_Date_convertToShamsi();
            }
            //دوشنبه
            if ((int)date.DayOfWeek == 1)
            {
                weekDurationShamsy.FirstDate = DateTime.Now.AddDays(-2).Utl_Date_convertToShamsi();
                weekDurationShamsy.LastDate = DateTime.Now.AddDays(4).Utl_Date_convertToShamsi();
                weekDurationShamsy.DayofWeek = date.DayOfWeek;
                weekDurationShamsy.Date = date.Utl_Date_convertToShamsi();
            }
            //سه شنبه
            if ((int)date.DayOfWeek == 2)
            {
                weekDurationShamsy.FirstDate = date.AddDays(-3).Utl_Date_convertToShamsi();
                weekDurationShamsy.LastDate = date.AddDays(3).Utl_Date_convertToShamsi();
                weekDurationShamsy.DayofWeek = date.DayOfWeek;
                weekDurationShamsy.Date = date.Utl_Date_convertToShamsi();
            }
            //چهارشنبه
            if ((int)date.DayOfWeek == 3)
            {
                weekDurationShamsy.FirstDate = date.AddDays(-4).Utl_Date_convertToShamsi();
                weekDurationShamsy.LastDate = date.AddDays(2).Utl_Date_convertToShamsi();
                weekDurationShamsy.DayofWeek = date.DayOfWeek;
                weekDurationShamsy.Date = date.Utl_Date_convertToShamsi();
            }
            //پنجشنبه
            if ((int)date.DayOfWeek == 4)
            {
                weekDurationShamsy.FirstDate = date.AddDays(-5).Utl_Date_convertToShamsi();
                weekDurationShamsy.LastDate = date.AddDays(1).Utl_Date_convertToShamsi();
                weekDurationShamsy.DayofWeek = date.DayOfWeek;
                weekDurationShamsy.Date = date.Utl_Date_convertToShamsi();
            }
            //جمعه
            if ((int)date.DayOfWeek == 5)
            {
                weekDurationShamsy.FirstDate = date.AddDays(-6).Utl_Date_convertToShamsi();
                weekDurationShamsy.LastDate = date.AddDays(0).Utl_Date_convertToShamsi();
                weekDurationShamsy.DayofWeek = date.DayOfWeek;
                weekDurationShamsy.Date = date.Utl_Date_convertToShamsi();
            }
            //شنبه
            if ((int)date.DayOfWeek == 6)
            {
                weekDurationShamsy.FirstDate = date.Utl_Date_convertToShamsi();
                weekDurationShamsy.LastDate = date.AddDays(6).Utl_Date_convertToShamsi();
                weekDurationShamsy.DayofWeek = date.DayOfWeek;
                weekDurationShamsy.Date = date.Utl_Date_convertToShamsi();
            }

            return weekDurationShamsy;
        }
        /// <summary>
        /// تعداد روز بین دو تا تاریخ
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns></returns>
        public static int NDaysBetwwenTwoDate(DateTime firstDate, DateTime secondDate)
        {

            TimeSpan t = firstDate - secondDate;
            ///تعداد روز خوانده نشده
            double NrOfDays = t.TotalDays;
            return (int)NrOfDays;
        }
        /// <summary>
        /// تعداد روز بین تاریخ و تاریخ امروز
        /// </summary>
        /// <param name="firstDate"></param>
        /// <returns></returns>
        public static int NDaysBetwwenTwoDate(DateTime firstDate)
        {

            TimeSpan t = DateTime.Now.Date - firstDate.Date;
            ///تعداد روز خوانده نشده
            double NrOfDays = t.TotalDays;
            return (int)NrOfDays;
        }
    }
    public static class MyUtility
    {
        public static string GetIPAddress()
        {
            string IPAddress = "";
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }
    }
    public class weekDurationShamsy
    {
        public string FirstDate { get; set; }
        public string LastDate { get; set; }
        public DayOfWeek DayofWeek { get; set; }
        public string Date { get; set; }
    }
    public class DateData
    {
        public string ghamary { get; set; }
        public DateTime georgian { get; set; }
        public string ShamsyDateFormat { get; set; }
        public string MildayDateFormat { get; set; }
        public string DayOfWeekPersian { get; set; }
        public weekDurationShamsy weekDurationShamsy1 { get; set; }
        public DayOfWeek dayOfWeek { get; set; }
        public int MonthDays { get; set; }
        public bool IsKabise { get; set; }
        public string FirstMounth { get; set; }
        public string LastMounth { get; set; }
        public int countDayBetween { get; internal set; }
        public string ShamsyDateFormatSlashLess { get; internal set; }
        public int DayOfWeekNum { get; internal set; }
        public string TommarowFormat { get; internal set; }
        public long TommarowNoSlash { get; internal set; }
    }
}
