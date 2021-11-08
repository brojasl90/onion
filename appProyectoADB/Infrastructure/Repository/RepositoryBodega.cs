using Infrastructure.Models;
using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryBodega : IRepositoryBodega
    {
        public void DeleteBodega(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bodega> GetBodega()
        {
            try
            {

                IEnumerable<Bodega> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Select * from Bodega
                    lista = ctx.Bodega.ToList<Bodega>();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Bodega GetBodegaByID(int id)
        {
            Bodega oBodega = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oBodega = ctx.Bodega.Find(id);
            }
            return oBodega;
        }

        public Bodega Save(Bodega pBodega)
        {
            Bodega oBodega = null;
            int retorno = 0;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oBodega = GetBodegaByID((int)pBodega.IdBodega);

                if (oBodega == null)
                {
                    ctx.Bodega.Add(pBodega);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.Bodega.Add(pBodega);
                    ctx.Entry(pBodega).State = System.Data.Entity.EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }

            if (retorno >= 0)
            {
                oBodega = GetBodegaByID((int)pBodega.IdBodega);
            }

            return oBodega;
        }
    }
}
