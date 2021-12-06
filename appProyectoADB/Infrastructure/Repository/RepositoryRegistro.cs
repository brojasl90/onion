using Infrastructure.Models;
using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryRegistro : IRepositoryRegistro
    {
        public IEnumerable<RegistroInventario> GetRegistro()
        {
            IEnumerable<RegistroInventario> lista = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.RegistroInventario.Include(x => x.GestionInventario) .ToList();
                //lista = ctx.GestionInventario.Include("Producto").ToList();
            }

            return lista;
        }

        public RegistroInventario GetRegistroByID(int pId)
        {
            RegistroInventario oRegistro = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oRegistro = ctx.RegistroInventario.Where(i => i.IdRegistroInventario == pId).Include(p => p.GestionInventario).FirstOrDefault();
            }

            return oRegistro;
        }

        public IEnumerable<RegistroInventario> GetRegistroPorFecha(string pGestion, DateTime pFecha)
        {
            IEnumerable<RegistroInventario> lista = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.RegistroInventario.Where(x => x.TipoGestion.Equals(pGestion) && x.FechaIngreso == pFecha.Date).ToList();
                //lista = ctx.GestionInventario.Include("Producto").ToList();
            }

            return lista;
        }

        public IEnumerable<RegistroInventario> GetRegistroPorUsuario(string pNombre)
        {
            IEnumerable<RegistroInventario> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                Usuario idUser = ctx.Usuario.Where(u => u.Nombre.ToLower().Contains(pNombre.ToLower())).FirstOrDefault();
                if (idUser != null)
                {
                    lista = ctx.RegistroInventario.ToList().FindAll(i => i.IdUsuario == idUser.IdUsuario);
                }
                else
                {
                    lista = ctx.RegistroInventario.ToList().FindAll(i => i.IdUsuario == 0);
                }
            }
            return lista;
        }

        public RegistroInventario GuardarRegistro(RegistroInventario pRegistro)
        {
            int vRetorno = 0;
            RegistroInventario oRegistro = null;
            IRepositoryProducto _RepoProd = new RepositoryProducto();
            try
            {
                using (MyContext ctx = new MyContext())
                {

                    using (var transaccion = ctx.Database.BeginTransaction())
                    {
                        ctx.RegistroInventario.Add(pRegistro);
                        vRetorno = ctx.SaveChanges();

                        foreach (var detalle in pRegistro.GestionInventario)
                        {
                            detalle.IdRegistro = pRegistro.IdRegistroInventario;
                        }

                        transaccion.Commit();
                    }
                }

                if (vRetorno >= 0)
                {
                    oRegistro = GetRegistroByID((int)pRegistro.IdRegistroInventario);
                }

                return oRegistro;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (DbEntityValidationException eve)
            {
                string mensaje = "";
                Log.Error(eve, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);

                foreach (var even in eve.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        even.Entry.Entity.GetType().Name, even.Entry.State);
                    foreach (var ve in even.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
        }
    }
}
