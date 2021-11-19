
using Infrastructure.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryPais
    {
        IEnumerable<PaisAPI> GetPaisAPIs();
    }
}
