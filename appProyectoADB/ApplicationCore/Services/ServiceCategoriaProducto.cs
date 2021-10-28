using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCategoriaProducto : IServiceCategoriaProducto
    {
        public IEnumerable<Categoria> GetCategoria()
        {
            IRepositoryCategoriaProducto repository = new RepositoryCategoriaProducto();
            return repository.GetCategoria();
        }

        public Categoria GetCategoriaByID(int id)
        {
            IRepositoryCategoriaProducto repository = new RepositoryCategoriaProducto();
            return repository.GetCategoriaByID(id);
        }
    }
}
