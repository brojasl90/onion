using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceCategoriaProducto
    {
        IEnumerable<Categoria> GetCategoria();
        Categoria GetCategoriaByID(int id);
    }
}
