using Supp.Core.Authorization;
using Supp.Core.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Web.HtmlHelpers
{
    public class SerializationHelpers
    {
        public static string TagsToJson(IEnumerable<Tag> tags)
        {
            return "[" + string.Join(",", tags.Select(t => $"'{t.Name}'")) + "]";
        }

        public static string GetProjectRoles()
        {
            var json = new StringBuilder("{");
            foreach (var role in RoleHelper.ProjectRoles)
            {
                var member = typeof(Role).GetMember(role.ToString()).First();
                var name = member.GetCustomAttribute<DisplayAttribute>().Name;
                json.Append($"'{role}': '{name}',");
            }
            json.Remove(json.Length - 1, 1);
            json.Append('}');
            return json.ToString();
        }

        public static Dictionary<string, string> EnumToNamedDictionary(Type enumType)
        {
            var dict = new Dictionary<string, string>();
            foreach (var enumName in Enum.GetNames(enumType))
            {
                var member = enumType.GetMember(enumName)[0];
                var name = member.GetCustomAttribute<DisplayAttribute>()?.Name ?? enumName;
                dict.Add(enumName, name);
            }
            return dict;
        }

    }
}
