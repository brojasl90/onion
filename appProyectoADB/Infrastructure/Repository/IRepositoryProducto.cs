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
        IEnumerable<Producto> GetProductoByNombre(string pNombre);
        IEnumerable<Producto> GetProductoByCategoria(int idCategoria);
        void DeleteProducto(int id);
        Producto Save(Producto libro, string[] selectedCategorias, string[] selectedProveedor, string[] selectedBodega);
        IEnumerable<Producto> GetProductoByAgotar();
        void GetProductoCountDate(out string etiquetas, out string valores);
    }
}
