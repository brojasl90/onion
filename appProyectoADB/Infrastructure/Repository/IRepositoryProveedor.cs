using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryProveedor
    {
        IEnumerable<Proveedor> GetProveedor();
        Proveedor GetProveedorByID(int pId);
        IEnumerable<Proveedor> GetProveedorByNombre(string pNombre);
        Proveedor Save(Proveedor pProv);
        void DeleteProveedor(int pId);
    }
}
