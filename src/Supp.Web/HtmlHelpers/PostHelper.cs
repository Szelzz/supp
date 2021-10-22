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
                PostType.Error => new HtmlString("<i class=\"fas fa-fw fa-bug\"></i>"),
                PostType.Enhancement => new HtmlString("<i class=\"fas fa-fw fa-angle-double-up\"></i>"),
                PostType.Task => new HtmlString("<i class=\"fas fa-fw fa-clipboard-check\"></i>"),
                _ => new HtmlString(null),
            };
        }

        public static IHtmlContent PostPriorityToIcon(this IHtmlHelper htmlHelper, PostPriority postPriority)
        {
            return postPriority switch
            {
                PostPriority.Unset => new HtmlString("<i class=\"far fa-fw fa-square\"></i>"),
                PostPriority.Unimportant => new HtmlString("<i class=\"fas fa-fw fa-arrow-down\"></i>"),
                PostPriority.Normal => new HtmlString("<i class=\"fas fa-fw fa-check\"></i>"),
                PostPriority.Important => new HtmlString("<i class=\"fas fa-fw fa-exclamation\"></i>"),
                _ => new HtmlString(null),
            };
        }

    }
}
