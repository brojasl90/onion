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
        public void DeleteProveedor(int pId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proveedor> GetProveedor()
        {
            IRepositoryProveedor oRepProv = new RepositoryProveedor();
            return oRepProv.GetProveedor();
        }

        public Proveedor GetProveedorByID(int pId)
        {
            IRepositoryProveedor oRepProv = new RepositoryProveedor();
            return oRepProv.GetProveedorByID(pId);
        }

        public Proveedor Save(Proveedor pProv)
        {
            IRepositoryProveedor oRepProv = new RepositoryProveedor();
            return oRepProv.Save(pProv);
        }
    }
}
