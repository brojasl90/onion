using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        Usuario GetUsuarioByID(int pId);

        IEnumerable<Usuario> GetUsuario();

        Usuario GuardarUsuario(Usuario pUsuario);
        IEnumerable<Usuario> GetUsuarioByNombre(string pNombre);

        Usuario GetUsuario(string pNumId, string pPassword);
    }
}
