using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public Usuario GetUsuarioByID(int pId)
        {
            IRepositoryUsuario oRepUss = new RepositoryUsuario();
            return oRepUss.GetTipoUsuarioByID(pId);
        }

        public IEnumerable<Usuario> GetUsuario()
        {
            IRepositoryUsuario oRepUss = new RepositoryUsuario();
            return oRepUss.GetUsuario();
        }

        public Usuario GuardarUsuario(Usuario pUsuario)
        {
            IRepositoryUsuario oRepUss = new RepositoryUsuario();
            return oRepUss.GuardarUsuario(pUsuario);
        }
    }
}
