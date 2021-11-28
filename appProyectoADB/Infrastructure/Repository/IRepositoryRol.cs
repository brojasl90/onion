using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryRol
    {
        Rol GetRolByID(int pId);

        IEnumerable<Rol> GetRol();

        Rol GuardarRol(Rol pRol);
    }
}
