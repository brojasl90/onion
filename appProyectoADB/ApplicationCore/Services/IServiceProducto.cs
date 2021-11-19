using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceProducto
    {
        IEnumerable<Producto> GetProducto();
        Producto GetProductoByID(int id);
        IEnumerable<Producto> GetProductoByNombre(string pNombre);
        IEnumerable<Producto> GetProductoByCategoria(int idCategoria);
        void DeleteProducto(int id);
        Producto Save(Producto producto, string[] selectedCategorias, string[] selectedProveedor, string[] selectedBodega);
        IEnumerable<Producto> GetProductoByAgotar();
    }
}
