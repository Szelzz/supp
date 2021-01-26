using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Supp.Core;
using Supp.Core.Posts;

namespace Supp.Web.HtmlHelpers
{
    public static class PostHelper
    {
        public static IHtmlContent PostTypeToIcon(this IHtmlHelper htmlHelper, PostType postType)
        {
            return postType switch
            {
                PostType.Error => new HtmlString("error-type-icon"),
                PostType.Enhancement => new HtmlString("enhancement-type-icon"),
                PostType.Task => new HtmlString("task-type-icon"),
                _ => new HtmlString(null),
            };
        }

        public static IHtmlContent PostPriorityToIcon(this IHtmlHelper htmlHelper, PostPriority postPriority)
        {
            return postPriority switch
            {
                PostPriority.Unset => new HtmlString("unset-icon"),
                PostPriority.Unimportant => new HtmlString("unimportant-icon"),
                PostPriority.Normal => new HtmlString("normal-icon"),
                PostPriority.Important => new HtmlString("important-icon"),
                _ => new HtmlString(null),
            };
        }
    }
}
