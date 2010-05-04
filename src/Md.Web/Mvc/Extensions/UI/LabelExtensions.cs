using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace Md.Infrastructure.Mvc.Extensions.UI
{
    public static class LabelExtensions
    {
        public static MvcHtmlString RequiredLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return RequiredLabel(html, ExpressionHelper.GetExpressionText(expression));
        }

        public static MvcHtmlString RequiredLabel(this HtmlHelper html, string expression)
        {
            var metadata = ModelMetadata.FromStringExpression(expression, html.ViewData);
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? expression.Split('.').Last();
            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            var labelTag = new TagBuilder("label");
            labelTag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(expression));

            var abbrTag = new TagBuilder("abbr");
            abbrTag.MergeAttribute("title", "Required field");
            abbrTag.InnerHtml = "*";
            labelTag.InnerHtml = string.Format("{0}{1}", labelText, abbrTag);

            //<label for="email">Your email address <abbr title="Required field">*</abbr></label>

            return MvcHtmlString.Create(labelTag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return LabelHelper(html, ModelMetadata.FromLambdaExpression(expression, html.ViewData), ExpressionHelper.GetExpressionText(expression), new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString LabelForModel(this HtmlHelper html, object htmlAttributes)
        {
            return LabelHelper(html, html.ViewData.ModelMetadata, String.Empty, new RouteValueDictionary(htmlAttributes));
        }

        internal static MvcHtmlString LabelHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, IDictionary<string, object> htmlAttributes)
        {
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            var tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(labelText);
            
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}
