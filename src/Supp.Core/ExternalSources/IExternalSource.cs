using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core.ExternalSources
{
    public interface IExternalSource
    {
        string Id { get; }

        IEnumerable<ExternalPost> GetNew();
    }
}
