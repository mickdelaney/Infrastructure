using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Md.Infrastructure.Data
{
    public class Generate
    {
        public IList<int> NumberList(int min, int max)
        {
            var rnd = new Random();
            IList<int> numbers = new List<int>(Enumerable.Range(min, max).OrderBy(r => rnd.Next()));
            return numbers;
        }

        public int RandomNumberBetween(int min, int max)
        {
            var rnd = new Random();
            return Enumerable.Range(min, max).OrderBy(r => rnd.Next()).First();
        }
    }
}
