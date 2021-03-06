using Infrastructure.Models;
using Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryProducto : IRepositoryProducto
    {
        public void DeleteProducto(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> GetProducto()
        {
            try
            {

                IEnumerable<Producto> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Select * from Producto
                    //lista = ctx.Producto.ToList<Producto>();
                    //lista = ctx.Producto.Include(x=>x.Categoria).Include(x => x.Proveedor).ToList();
                    lista = ctx.Producto.Include(x => x.Categoria).Include(x => x.Proveedor).Include(x => x.Proveedor).ToList();
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

        public IEnumerable<Producto> GetProductoByAgotar()
        {
            try
            {

                IEnumerable<Producto> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Select * from Producto
                    //lista = ctx.Producto.ToList<Producto>();
                    lista = ctx.Producto.Where(a => a.CantidadStockMin > a.CantidadDisponible).ToList();
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

        public IEnumerable<Producto> GetProductoByCategoria(int idCategoria)
        {
            IEnumerable<Producto> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Producto.Where(l => l.IdCategoria == idCategoria).ToList();
            }
            return lista;
        }

        /*
public IEnumerable<Producto> GetProductoByCategoria(int idAutor)
{
   IEnumerable<Libro> lista = null;
   using (MyContext ctx = new MyContext())
   {
       ctx.Configuration.LazyLoadingEnabled = false;
       lista = ctx.Libro.Where(l => l.IdAutor == idAutor).ToList();
   }
   return lista;
}
*/

        public Producto GetProductoByID(int id)
        {

            Producto oProducto = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProducto = ctx.Producto.
                    Where(p => p.IdProducto == id).
                     Include(x => x.Categoria).
                     Include(x => x.Bodega).
                     Include(x => x.Proveedor).FirstOrDefault();
            }
            return oProducto;
        }

        public IEnumerable<Producto> GetProductoByNombre(string nombre)
        {
            IEnumerable<Producto> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Producto.ToList().
                    FindAll(l => l.Nombre_Producto.ToLower().Contains(nombre.ToLower()));
            }
            return lista;
        }

        public Producto Save(Producto producto, string[] selectedCategorias, string[] selectedProveedores, string[] selectedBodega)
        {
            int retorno = 0;
            Producto oProducto = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProducto = GetProductoByID((int)producto.IdProducto);
                IRepositoryCategoriaProducto _RepositoryCategoriaProducto = new RepositoryCategoriaProducto();
                IRepositoryProveedor _RepositoryProveedor = new RepositoryProveedor();
                IRepositoryBodega _RepositoryBodega = new RepositoryBodega();

                if (oProducto == null)
                {

                    //Insertar
                    /*
                    if (selectedCategorias != null)
                    {
                        producto.listaCategoria = new List<Categoria>();
                        foreach (var categoria in selectedCategorias)
                        {
                            var categoriaToAdd = _RepositoryCategoriaProducto.GetCategoriaByID(int.Parse(categoria));
                            ctx.Categoria.Attach(categoriaToAdd); //sin esto, EF intentará crear una categoría
                            producto.listaCategoria.Add(categoriaToAdd);// asociar a la categoría existente con el Producto
                        }
                    }
                    */
                    if (selectedProveedores != null)
                    {
                        producto.Proveedor = new List<Proveedor>();
                        foreach (var proveedor in selectedProveedores)
                        {
                            var proveedorToAdd = _RepositoryProveedor.GetProveedorByID(int.Parse(proveedor));
                            ctx.Proveedor.Attach(proveedorToAdd); //sin esto, EF intentará crear un proveedor
                            producto.Proveedor.Add(proveedorToAdd);// asociar a la categoría existente con el Producto
                        }
                    }
                    if (selectedBodega != null)
                    {
                        producto.Bodega = new List<Bodega>();
                        foreach (var bodega in selectedBodega)
                        {
                            var bodegaToAdd = _RepositoryBodega.GetBodegaByID(int.Parse(bodega));
                            ctx.Bodega.Attach(bodegaToAdd); //sin esto, EF intentará crear un proveedor
                            producto.Bodega.Add(bodegaToAdd);// asociar a la bodega existente con el Producto
                        }
                    }
                    producto.Estado = 1;
                    ctx.Producto.Add(producto);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4

                    //Actualizar Producto
                    ctx.Producto.Add(producto);
                    ctx.Entry(producto).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                    //Actualizar Categorias
                    //var selectedCategoriasID = new HashSet<string>(selectedCategorias);              

                    /*
                    if (selectedCategorias != null)
                    {
                        ctx.Entry(producto).Collection(p => p.listaCategoria).Load();
                        var newCategoriaForLibro = ctx.Categoria
                         .Where(x => selectedCategoriasID.Contains(x.IdCategoria.ToString())).ToList();
                        producto.listaCategoria = newCategoriaForLibro;

                        ctx.Entry(producto).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                    */
                    //Actualizar Proveedor
                    var selectedProveedorID = new HashSet<string>(selectedProveedores);
                    if (selectedProveedores != null)
                    {
                        ctx.Entry(producto).Collection(p => p.Proveedor).Load();
                        var newProveedorForProducto = ctx.Proveedor
                         .Where(x => selectedProveedorID.Contains(x.IdProveedor.ToString())).ToList();
                        producto.Proveedor = newProveedorForProducto;

                        ctx.Entry(producto).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                    //Actualizar Bodega
                    var selectedBodegaID = new HashSet<string>(selectedBodega);
                    if (selectedBodega != null)
                    {
                        ctx.Entry(producto).Collection(p => p.Bodega).Load();
                        var newBodegaForProducto = ctx.Bodega
                         .Where(x => selectedBodegaID.Contains(x.IdBodega.ToString())).ToList();
                        producto.Bodega = newBodegaForProducto;

                        ctx.Entry(producto).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
            }

            if (retorno >= 0)
                oProducto = GetProductoByID((int)producto.IdProducto);

            return oProducto;
        }
        public void GetProductoCountDate(out string etiquetas, out string valores)
        {
            String varEtiquetas = "";
            String varValores = "";
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    var resultado = ctx.Producto.GroupBy(x => x.FechaCreacion).
                               Select(o => new {
                                   Count = o.Count(),
                                   FechaCreacion = o.Key
                               });
                    foreach (var item in resultado)
                    {
                        varEtiquetas += String.Format("{0:dd/MM/yyyy}", item.FechaCreacion) + ",";
                        varValores += item.Count + ",";
                    }
                }
                //Ultima coma
                varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1); // ultima coma
                varValores = varValores.Substring(0, varValores.Length - 1);
                //Asignar valores de salida
                etiquetas = varEtiquetas;
                valores = varValores;
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
    }
}
