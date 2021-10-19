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
        Proveedor GetProveedorByID(int id);
    }
}
