using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceRegistro
    {
        IEnumerable<RegistroInventario> GetRegistro();

        IEnumerable<RegistroInventario> GetRegistroPorUsuario(string pNombre);

        IEnumerable<RegistroInventario> GetRegistroPorFecha(string pGestion, DateTime pFecha);

        RegistroInventario GetRegistroByID(int pId);

        RegistroInventario GuardarRegistro(RegistroInventario pRegistro, string[] selectInventario);
    }
}
