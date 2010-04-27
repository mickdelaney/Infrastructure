using System;

namespace Md.Infrastructure.Clr
{
    public class DateRange
    {
        public DateRange(){}
        public DateRange(DateTime start, DateTime finish)
        {
            Start = start;
            Finish = finish;
        }

        public virtual DateTime Start { get; set; }
        public virtual DateTime Finish { get; set; }
    }
}