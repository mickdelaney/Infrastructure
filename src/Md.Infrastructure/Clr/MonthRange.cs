using System;

namespace Md.Infrastructure.Clr
{
    public class MonthRange
    {
        public MonthRange() { }
        public MonthRange(DateTime start, DateTime finish)
        {
            StartMonth = start.Month;
            StartYear = start.Year;
            EndMonth = finish.Month;
            EndYear = finish.Year;
        }

        public MonthRange(Month startMonth, int startYear, Month endMonth, int endYear)
        {
            StartYear = startYear;
            StartMonth = (int)startMonth;
            EndMonth = (int)endMonth;
            EndYear = endYear;
        }

        public virtual int StartMonth { get; set; }
        public virtual int StartYear { get; set; }
        public virtual int EndMonth { get; set; }
        public virtual int EndYear { get; set; }

        public static MonthRange CreateForYears(int startYear, int endYear)
        {
            var start = DateTime.Now.AddYears(startYear);
            var end = DateTime.Now.AddYears(endYear);
            return new MonthRange(start, end);
        }

        public static MonthRange Create(DateTime start, DateTime end)
        {
            return new MonthRange(start, end);
        }

        public string FriendlyName()
        {
            return string.Format("{0} {1} - {2} {3}", ((Month)StartMonth), StartYear, ((Month)EndMonth), EndYear);
        }
    }
}