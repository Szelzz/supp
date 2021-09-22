using Supp.Core.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
