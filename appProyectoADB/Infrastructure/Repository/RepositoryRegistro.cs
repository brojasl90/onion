using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public RegistroInventario GuardarRegistro(RegistroInventario pRegistro, string[] selectInventario)
        {
            int vRetorno = 0;
            RegistroInventario oRegistro = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oRegistro = GetRegistroByID((int)pRegistro.IdRegistroInventario);
                IRepositoryInventario _RepoInvent = new RepositoryInventario();

                if (oRegistro == null)
                {
                    if (selectInventario != null)
                    {
                        pRegistro.GestionInventario = new List<GestionInventario>();

                        foreach (var iProducto in selectInventario)
                        {
                            var oInventarioToAdd = _RepoInvent.GetInventarioByID(int.Parse(iProducto));
                            ctx.GestionInventario.Attach(oInventarioToAdd);
                            pRegistro.GestionInventario.Add(oInventarioToAdd);
                        }
                    }

                    ctx.RegistroInventario.Add(pRegistro);
                    vRetorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.RegistroInventario.Add(pRegistro);
                    ctx.Entry(pRegistro).State = EntityState.Modified;
                    vRetorno = ctx.SaveChanges();

                    var selectedInventarioID = new HashSet<string>(selectInventario);

                    if (selectInventario != null)
                    {
                        ctx.Entry(pRegistro).Collection(i => i.GestionInventario).Load();
                        var newProductoByInventario = ctx.GestionInventario.Where(x => selectedInventarioID.Contains(x.IdGestionInventario.ToString())).ToList();

                        pRegistro.GestionInventario = newProductoByInventario;

                        ctx.Entry(pRegistro).State = EntityState.Modified;
                        vRetorno = ctx.SaveChanges();
                    }
                }
            }

            if (vRetorno >= 0)
            {
                oRegistro = GetRegistroByID((int)pRegistro.IdRegistroInventario);
            }

            return oRegistro;
        }
    }
}
