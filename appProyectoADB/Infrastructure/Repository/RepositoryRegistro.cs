using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryRegistro : IRepositoryRegistro
    {
        public IEnumerable<RegistroInventario> GetRegistro()
        {
            throw new NotImplementedException();
        }

        public RegistroInventario GetRegistroByID(int pId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegistroInventario> GetRegistroPorFecha(string pGestion, DateTime pFecha)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegistroInventario> GetRegistroPorUsuario(int pId)
        {
            throw new NotImplementedException();
        }

        public RegistroInventario GuardarRegistro(RegistroInventario pInventario, string[] selectInventario)
        {
            throw new NotImplementedException();
        }
    }
}
