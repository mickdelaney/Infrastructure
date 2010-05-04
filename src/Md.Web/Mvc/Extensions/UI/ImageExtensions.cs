using System.Collections.Generic;
using System.Web.Mvc;

namespace Md.Infrastructure.Mvc.Extensions.UI
{
    public static class ImageExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string alt)
        {
            var tb = new TagBuilder("img");
            tb.Attributes.Add("src", helper.Encode(src));
            tb.Attributes.Add("alt", helper.Encode(alt));
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString ImageLink(this HtmlHelper helper, string imgSrc, string url)
        {
            return ImageLink(helper, imgSrc, url, null, null);
        }

        public static MvcHtmlString ImageLink(this HtmlHelper htmlHelper, string imgSrc, string url,
                                              IDictionary<object, string> htmlAttributes, IDictionary<object, string> imgHtmlAttributes)
        {

            var imgTag = new TagBuilder("img");
            imgTag.MergeAttribute("src", imgSrc);

            if (imgHtmlAttributes != null)
                imgTag.MergeAttributes(imgHtmlAttributes, true);

            var imglink = new TagBuilder("a");
            imglink.MergeAttribute("href", url);
            imglink.InnerHtml = imgTag.ToString();

            if (htmlAttributes != null)
                imglink.MergeAttributes(htmlAttributes, true);

            return MvcHtmlString.Create(imglink.ToString(TagRenderMode.Normal));
        }

    }
}
