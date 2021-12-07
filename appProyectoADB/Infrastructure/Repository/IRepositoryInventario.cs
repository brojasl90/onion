using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryInventario
    {
        IEnumerable<GestionInventario> GetInventario();

        IEnumerable<GestionInventario> GetInventarioPorTipMovimiento(int pTipo);

        IEnumerable<GestionInventario> GetInventarioPorUsuario(int pId);
        IEnumerable<GestionInventario> GetInventarioPorNombreUsuario(string pNombre);

        IEnumerable<GestionInventario> GetInventarioPorFecha(string pGestion, DateTime pFecha);
        IEnumerable<GestionInventario> GetInventarioReporte(string pGestion);
        IEnumerable<GestionInventario> GetInventarioReportePorFecha(string pGestion, DateTime pFecha);

        GestionInventario GetInventarioByID(int pId);

        GestionInventario GuardarInventario(GestionInventario pInventario, string[] selectProducto);

    }
}
