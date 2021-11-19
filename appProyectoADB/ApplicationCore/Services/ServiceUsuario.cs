using ApplicationCore.Utils;
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
            Usuario oUsuario = oRepUss.GetTipoUsuarioByID(pId);
            oUsuario.Clave = Cryptography.DecrypthAES(oUsuario.Clave);
            return oUsuario;
        }

        public IEnumerable<Usuario> GetUsuario()
        {
            IRepositoryUsuario oRepUss = new RepositoryUsuario();
            return oRepUss.GetUsuario();
        }

        public Usuario GuardarUsuario(Usuario pUsuario)
        {
            IRepositoryUsuario oRepUss = new RepositoryUsuario();
            pUsuario.Clave = Cryptography.EncrypthAES(pUsuario.Clave);
            return oRepUss.GuardarUsuario(pUsuario);
        }

        public IEnumerable<Usuario> GetUsuarioByNombre(string pNombre)
        {
            IRepositoryUsuario oRepUss = new RepositoryUsuario();
            return oRepUss.GetUsuarioByNombre(pNombre);
        }

        public Usuario GetUsuario(string pEmail, string pPassword)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuario(pEmail, Cryptography.EncrypthAES(pPassword));
        }
    }
}
