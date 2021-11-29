using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Supp.Core;
using Supp.Core.Posts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Supp.Web.HtmlHelpers
{
    public static class PostHelper
    {
        private static string GetEnumName<T>(T enumValue)
            where T : Enum
        {
            var member = typeof(T).GetMember(enumValue.ToString());
            var attribute = member[0].GetCustomAttribute<DisplayAttribute>();
            if (attribute != null)
                return attribute.Name;

            return enumValue.ToString();
        }

        public static IHtmlContent PostTypeToIcon(this IHtmlHelper htmlHelper, PostType postType)
        {
            var name = GetEnumName(postType);
            return postType switch
            {
                PostType.Error => new HtmlString($"<i title=\"{name}\" class=\"fas fa-fw fa-bug\"></i>"),
                PostType.Enhancement => new HtmlString($"<i title=\"{name}\" class=\"fas fa-fw fa-angle-double-up\"></i>"),
                PostType.Task => new HtmlString($"<i title=\"{name}\" class=\"fas fa-fw fa-clipboard-check\"></i>"),
                _ => new HtmlString(null),
            };
        }

        public static IHtmlContent PostPriorityToIcon(this IHtmlHelper htmlHelper, PostPriority postPriority)
        {
            var name = GetEnumName(postPriority);
            return postPriority switch
            {
                PostPriority.Unset => new HtmlString($"<i title=\"{name}\" class=\"far fa-fw fa-square\"></i>"),
                PostPriority.Unimportant => new HtmlString($"<i title=\"{name}\" class=\"fas fa-fw fa-arrow-down\"></i>"),
                PostPriority.Normal => new HtmlString($"<i title=\"{name}\" class=\"fas fa-fw fa-check\"></i>"),
                PostPriority.Important => new HtmlString($"<i title=\"{name}\" class=\"fas fa-fw fa-exclamation\"></i>"),
                _ => new HtmlString(null),
            };
        }

    }
}
