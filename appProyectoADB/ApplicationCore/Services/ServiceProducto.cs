using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceProducto : IServiceProducto
    {
        public void DeleteProducto(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> GetProducto()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProducto();
        }

        public IEnumerable<Producto> GetProductoByAgotar()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByAgotar();
        }

        public IEnumerable<Producto> GetProductoByCategoria(int idCategoria)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByCategoria(idCategoria);
        }

        public Producto GetProductoByID(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByID(id);
        }

        public IEnumerable<Producto> GetProductoByNombre(string nombre)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByNombre(nombre);
        }

        public Producto Save(Producto producto, string[] selectedCategorias, string[] selectedProveedor, string[] selectedBodega)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.Save(producto, selectedCategorias, selectedProveedor, selectedBodega);
        }
        public void GetProductoCountDate(out string etiquetas1, out string valores1)
        {
            IRepositoryProducto repository = new RepositoryProducto();

            repository.GetProductoCountDate(out string etiquetas, out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }
    }
}
