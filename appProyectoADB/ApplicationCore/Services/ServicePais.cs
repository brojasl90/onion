using Infrastructure.APIs;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePais : IServicePais
    {
        public IEnumerable<PaisAPI> GetPaisAPIs()
        {
            IRepositoryPais repositoryPais = new RepositoryPais();
            return repositoryPais.GetPaisAPIs();
        }
    }
}
