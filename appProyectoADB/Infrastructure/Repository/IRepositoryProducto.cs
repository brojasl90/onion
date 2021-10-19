using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryProducto
    {
        IEnumerable<Producto> GetProducto();
        Producto GetProductoByID(int id);

    }
}
