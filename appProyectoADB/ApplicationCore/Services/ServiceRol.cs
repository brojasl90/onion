using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceRol : IServiceRol
    {
        public IEnumerable<Rol> GetRol()
        {
            IRepositoryRol oRepRol = new RepositoryRol();
            return oRepRol.GetRol();
        }

        public Rol GetRolByID(int pId)
        {
            IRepositoryRol oRepRol = new RepositoryRol();
            return oRepRol.GetRolByID(pId);
        }

        public Rol GuardarRol(Rol pRol)
        {
            IRepositoryRol oRepRol = new RepositoryRol();
            return oRepRol.GuardarRol(pRol);
        }
    }
}
