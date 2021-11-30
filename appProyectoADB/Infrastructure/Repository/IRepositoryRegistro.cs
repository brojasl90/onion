using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryRegistro
    {
        IEnumerable<RegistroInventario> GetRegistro();

        IEnumerable<RegistroInventario> GetRegistroPorUsuario(int pId);

        IEnumerable<RegistroInventario> GetRegistroPorFecha(string pGestion, DateTime pFecha);

        RegistroInventario GetRegistroByID(int pId);

        RegistroInventario GuardarRegistro(RegistroInventario pInventario, string[] selectInventario);
    }
}
