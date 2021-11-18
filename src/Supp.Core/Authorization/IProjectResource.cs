using Supp.Core.Projects;
using Supp.Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.Authorization
{
    public interface IProjectResource
    {
        public Project GetProject();
        /// <summary>
        /// Can return null
        /// </summary>
        public string GetAuthorId();
    }
}
