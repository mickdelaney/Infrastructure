using System.Collections.Generic;

namespace Md.Web.Mvc.Extensions.UI
{
    public static class DescriptiveListExtensions
    {
        public static IEnumerable<DescriptiveListItem<T>> ToDescriptiveList<T>(this IEnumerable<T> items)
        {
            // To avoid evaluating the whole collection up-front (which may be undesirable, for example
            // if the collection contains infinitely many members), read-ahead just one item at a time.

            // Get the first item
            var enumerator = items.GetEnumerator();
            if(!enumerator.MoveNext())
                yield break;
            T currentItem = enumerator.Current;
            int index = 0;

            while (true) {
                // Read ahead so we know whether we're at the end or not
                bool isLast = !enumerator.MoveNext();

                // Describe and yield the current item
                ItemDescription description = (index % 2 == 0 ? ItemDescription.Odd : ItemDescription.Even);
                if (index == 0) description |= ItemDescription.First;
                if (isLast) description |= ItemDescription.Last;
                if (index > 0 && !isLast) description |= ItemDescription.Interior;
                yield return new DescriptiveListItem<T>(index, currentItem, description);

                // Terminate or continue
                if (isLast)
                    yield break;
                index++;
                currentItem = enumerator.Current;
            }
        }
    }
}