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
    public class RepositoryRol : IRepositoryRol
    {
        public IEnumerable<Rol> GetRol()
        {
            IEnumerable<Rol> lista = null;

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Rol.ToList<Rol>();
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
                throw new Exception(mensaje);
            }
        }

        public Rol GetRolByID(int pId)
        {
            Rol oRol = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oRol = ctx.Rol.Find(pId);
            }
            return oRol;
        }

        public Rol GuardarRol(Rol pRol)
        {
            Rol oRol = null;
            int retorno = 0;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oRol = GetRolByID((int)pRol.IdRol);

                if (oRol == null)
                {
                    ctx.Rol.Add(pRol);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.Rol.Add(pRol);
                    ctx.Entry(pRol).State = System.Data.Entity.EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }

            if (retorno >= 0)
            {
                oRol = GetRolByID((int)pRol.IdRol);
            }

            return oRol;
        }
    }
}
