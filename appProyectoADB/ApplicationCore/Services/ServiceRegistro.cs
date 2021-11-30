using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceRegistro : IServiceRegistro
    {
        public IEnumerable<RegistroInventario> GetRegistro()
        {
            IRepositoryRegistro oRepoReg = new RepositoryRegistro();
            return oRepoReg.GetRegistro();
        }

        public RegistroInventario GetRegistroByID(int pId)
        {
            IRepositoryRegistro oRepoReg = new RepositoryRegistro();
            return oRepoReg.GetRegistroByID(pId);
        }

        public IEnumerable<RegistroInventario> GetRegistroPorFecha(string pGestion, DateTime pFecha)
        {
            IRepositoryRegistro oRepoReg = new RepositoryRegistro();
            return oRepoReg.GetRegistroPorFecha(pGestion, pFecha);
        }

        public IEnumerable<RegistroInventario> GetRegistroPorUsuario(string pNombre)
        {
            IRepositoryRegistro oRepoReg = new RepositoryRegistro();
            return oRepoReg.GetRegistroPorUsuario(pNombre);
        }

        public RegistroInventario GuardarRegistro(RegistroInventario pRegistro, string[] selectInventario)
        {
            IRepositoryRegistro oRepoReg = new RepositoryRegistro();
            return oRepoReg.GuardarRegistro(pRegistro, selectInventario);
        }
    }
}
