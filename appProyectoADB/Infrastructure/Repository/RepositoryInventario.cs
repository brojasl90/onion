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
                lista = ctx.GestionInventario.Include(x=>x.TipoMovimiento).ToList();
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
                oInventario = ctx.GestionInventario.Where(i => i.IdGestionInventario == pId).Include(m => m.TipoMovimiento).Include(p => p.Producto).FirstOrDefault();
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

        public GestionInventario GuardarInventario(GestionInventario pInventario, string[] selectProducto)
        {
            int vRetorno = 0;
            GestionInventario oInventario = null;

            using(MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oInventario = GetInventarioByID((int)pInventario.IdGestionInventario);
                IRepositoryProducto _RepoProd = new RepositoryProducto();

                if (oInventario == null)
                {
                    if (selectProducto != null)
                    {
                        pInventario.Producto = new List<Producto>();

                        foreach (var iProducto in selectProducto)
                        {
                            var oProductoToAdd = _RepoProd.GetProductoByID(int.Parse(iProducto));
                            ctx.Producto.Attach(oProductoToAdd);
                            pInventario.Producto.Add(oProductoToAdd);
                        }
                    }

                    ctx.GestionInventario.Add(pInventario);
                    vRetorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.GestionInventario.Add(pInventario);
                    ctx.Entry(pInventario).State = EntityState.Modified;
                    vRetorno = ctx.SaveChanges();

                    var selectedProductosID = new HashSet<string>(selectProducto);

                    if (selectProducto != null)
                    {
                        ctx.Entry(pInventario).Collection(i => i.Producto).Load();
                        var newProductoByInventario = ctx.Producto.Where(x => selectedProductosID.Contains(x.IdProducto.ToString())).ToList();

                        pInventario.Producto = newProductoByInventario;

                        ctx.Entry(pInventario).State = EntityState.Modified;
                        vRetorno = ctx.SaveChanges();
                    }
                }
            }

            if (vRetorno >= 0)
            {
                oInventario = GetInventarioByID((int)pInventario.IdGestionInventario);
            }

            return oInventario;
        }
    }
}
