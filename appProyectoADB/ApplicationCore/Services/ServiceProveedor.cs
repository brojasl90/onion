using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProveedor : IServiceProveedor
    {
        public IEnumerable<Proveedor> GetProveedor()
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.GetProveedor();
        }

        public Proveedor GetProveedorByID(int id)
        {
            IRepositoryProveedor oRepProv = new RepositoryProveedor();
            return oRepProv.GetProveedorByID(id);
        }
    }
}
