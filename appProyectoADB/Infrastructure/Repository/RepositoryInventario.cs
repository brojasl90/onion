using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryInventario : IRepositoryInventario
    {
        public IEnumerable<GestionInventario> GetInventario()
        {
            IEnumerable<GestionInventario> lista = null;

            using(MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.GestionInventario.Include(x=>x.Producto).ToList();
                //lista = ctx.GestionInventario.Include("Producto").ToList();
            }

            return lista;
        }

        public GestionInventario GetInventarioByID(int pId)
        {
            GestionInventario oInventario = null;

            using(MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oInventario = ctx.GestionInventario.Where(i => i.IdGestionInventario == pId).Include(p => p.Producto).FirstOrDefault();
            }

            return oInventario;
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
