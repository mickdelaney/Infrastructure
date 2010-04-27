using System;

namespace Md.Infrastructure.Clr
{
    public class DayRange
    {
        public DayRange(){}
        public DayRange(DateTime start, DateTime finish)
        {
            StartDay = start.Day;
            StartYear = start.Year;
            EndDay = finish.Day;
            EndYear = finish.Year;
        }

        public virtual int StartDay { get; set; }
        public virtual int StartYear { get; set; }

        public virtual int EndDay { get; set; }
        public virtual int EndYear { get; set; }

    }
}