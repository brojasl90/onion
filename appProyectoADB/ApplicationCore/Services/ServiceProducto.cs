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
        public IEnumerable<Producto> GetProducto()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProducto();
        }

        public Producto GetProductoByID(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByID(id);
        }
    }
}
