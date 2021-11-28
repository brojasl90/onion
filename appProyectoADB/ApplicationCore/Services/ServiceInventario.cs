using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceInventario : IServiceInventario
    {
        public IEnumerable<GestionInventario> GetInventario()
        {
            IRepositoryInventario oRI = new RepositoryInventario();
            return oRI.GetInventario();
        }

        public GestionInventario GetInventarioByID(int pId)
        {
            IRepositoryInventario oRI = new RepositoryInventario();
            return oRI.GetInventarioByID(pId);
        }

        public IEnumerable<GestionInventario> GetInventarioPorFecha(string pGestion, DateTime pFecha)
        {
            IRepositoryInventario oRI = new RepositoryInventario();
            return oRI.GetInventarioPorFecha(pGestion, pFecha);
        }

        public IEnumerable<GestionInventario> GetInventarioPorNombreUsuario(string pNombre)
        {
            IRepositoryInventario oRI = new RepositoryInventario();
            return oRI.GetInventarioPorNombreUsuario(pNombre);
        }

        public IEnumerable<GestionInventario> GetInventarioPorTipMovimiento(int pTipo)
        {
            IRepositoryInventario oRI = new RepositoryInventario();
            return oRI.GetInventarioPorTipMovimiento(pTipo);
        }

        public IEnumerable<GestionInventario> GetInventarioPorUsuario(int pId)
        {
            IRepositoryInventario oRI = new RepositoryInventario();
            return oRI.GetInventarioPorUsuario(pId);
        }

        public GestionInventario GuardarInventario(GestionInventario pInventario, string[] selectProducto)
        {
            IRepositoryInventario oRI = new RepositoryInventario();
            return oRI.GuardarInventario(pInventario, selectProducto);
        }
    }
}
