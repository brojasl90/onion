using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryUsuario
    {
        Usuario GetTipoUsuarioByID(int pId);

        IEnumerable<Usuario> GetUsuario();

        Usuario GuardarUsuario(Usuario pUsuario);

        IEnumerable<Usuario> GetUsuarioByNombre(string pNombre);
    }
}
