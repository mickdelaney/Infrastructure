using System;
using System.Linq;

namespace Md.Web.Mvc.Extensions.UI
{
    public class DescriptiveListItem<T>
    {
        public int Index { get; private set; }
        public T Item { get; private set; }
        public ItemDescription Description { get; private set; }

        public DescriptiveListItem(int index, T item, ItemDescription description)
        {
            Index = index;
            Item = item;
            Description = description;
        }

        public bool Is(ItemDescription description)
        {
            return (Description & description) == description;
        }

        public string CssClass(string prefix)
        {
            if (prefix == null) throw new ArgumentNullException("prefix");

            var matchingDescriptions = Enum.GetValues(typeof(ItemDescription)).Cast<ItemDescription>().Where(x => this.Is(x));
            return string.Join(" ", matchingDescriptions.Select(x => prefix + x.ToString().ToLower()).ToArray());
        }

        public string CssClass()
        {
            return CssClass(string.Empty);
        }
    }
}