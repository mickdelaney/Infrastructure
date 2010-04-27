using System;

namespace Md.Infrastructure.Clr
{
    public class MonthRange
    {
        public MonthRange(){}
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
    }
}