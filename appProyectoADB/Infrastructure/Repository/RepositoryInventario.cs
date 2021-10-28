using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryInventario : IRepositoryInventario
    {
        public IEnumerable<GestionInventario> GetInventario()
        {
            throw new NotImplementedException();
        }

        public GestionInventario GetInventarioByID(int pId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GestionInventario> GetInventarioPorFecha(DateTime pFecha)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GestionInventario> GetInventarioPorTipMovimiento(int pTipo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GestionInventario> GetInventarioPorUsuario(int pId)
        {
            throw new NotImplementedException();
        }

        public GestionInventario GuardarInventario(GestionInventario pInventario)
        {
            throw new NotImplementedException();
        }
    }
}
