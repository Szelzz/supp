using Supp.Core.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Supp.Core.Tags
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }


        public static string NormalizeName(string value)
        {
            var allowedRegex = new Regex("[a-zA-Z0-9\\-]");
            return string.Join("", allowedRegex.Matches(value).Select(v => v.Value));
        }
    }
}
