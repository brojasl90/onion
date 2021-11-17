using ApplicationCore.Services;
using Infrastructure.APIs;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class InventarioController : Controller
    {
        // GET: Inventario
        public ActionResult Index()
        {
            IEnumerable<GestionInventario> lista = null;

            try
            {
                IServiceInventario _ServInventario = new ServiceInventario();
                IServiceMovimiento _ServiceMovimiento = new ServiceMovimiento();
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();

                ViewBag.LtsMovimientos = _ServiceMovimiento.GetTipoMovimiento();
                ViewBag.LtsUsuarios = _ServiceUsuario.GetUsuario();
                lista = _ServInventario.GetInventario();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al tratar de obtener lista de Inventario: " + ex.Message;

                return RedirectToAction("Default", "Error");
            }

            return View(lista);
        }

        // GET: Inventario/Details/5
        public ActionResult Details(int? pId)
        {
            IServiceInventario _ServInventario = new ServiceInventario();
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            GestionInventario oInventario = null;

            try
            {
                if (pId == null)
                {
                    return RedirectToAction("Index");
                }

                oInventario = _ServInventario.GetInventarioByID(pId.Value);

                if (oInventario == null)
                {
                    TempData["Message"] = "No existe la información solicitada.";
                    TempData["Redirect"] = "Inventario";
                    TempData["Redirect-Action"] = "Index";

                    return RedirectToAction("Default", "Error");
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al tratar de obtener el inventario solicitado: " + ex.Message;
                TempData["Redirect"] = "Inventario";
                TempData["Redirect-Action"] = "Index";

                return RedirectToAction("Default", "Error");
            }

            ViewBag.LtsUsuarios = _ServiceUsuario.GetUsuario();
            return View(oInventario);
        }

        public ActionResult BusquedaXNombre(string pNombre)
        {
            IEnumerable<GestionInventario> listaInventarios = null;
            IServiceInventario _ServInventario = new ServiceInventario();

            if (string.IsNullOrEmpty(pNombre))
            {
                listaInventarios = _ServInventario.GetInventario();
            }
            else
            {
                listaInventarios = _ServInventario.GetInventarioPorNombreUsuario(pNombre);
            }

            return PartialView("_PartialViewInventIndex", listaInventarios);
        }



        // GET: Inventario/Create
        public ActionResult Create()
        {
            ViewBag.IdTipMov = listaTipoMovimiento();
            ViewBag.IdUserReg = listaUsuario();
            ViewBag.IdUserGes = listaUsuario();
            ViewBag.IdProd = listaProductos(null);
            
            return View();
        }

        private SelectList listaTipoMovimiento(int pIdMovimiento = 0)
        {
            IServiceMovimiento _ServMovi = new ServiceMovimiento();
            IEnumerable<TipoMovimiento> lTipoMovimientos = _ServMovi.GetTipoMovimiento();
            return new SelectList(lTipoMovimientos, "IdTipMovimiento", "Descripcion", pIdMovimiento); 
        }

        private SelectList listaUsuario(int pIdUsuario= 0)
        {
            IServiceUsuario _ServUser = new ServiceUsuario();
            IEnumerable<Usuario> lUsuarios = _ServUser.GetUsuario();
            return new SelectList(lUsuarios, "idUsuario", "Nombre", pIdUsuario);
        }

        private MultiSelectList listaProductos(ICollection<Producto> pProductos)
        {
            IServiceProducto _ServProd = new ServiceProducto();
            IEnumerable<Producto> lproductos= _ServProd.GetProducto();

            int[] listaProductosSelect = null;

            if (pProductos != null)
            {
                listaProductosSelect = pProductos.Select(c => c.IdProducto).ToArray();
            }

            return new MultiSelectList(lproductos, "IdProducto", "Nombre_Producto", listaProductosSelect);
        }

        // GET: Inventario/Edit/5
        public ActionResult Edit(int? pId)
        {
            IServiceInventario _ServInventario = new ServiceInventario();
            GestionInventario oInventario = null;

            try
            {
                if (pId == null)
                {
                    return RedirectToAction("Index");
                }

                oInventario = _ServInventario.GetInventarioByID(pId.Value);

                if (oInventario == null)
                {
                    TempData["Message"] = "No existe la información solicitada.";
                    TempData["Redirect"] = "Inventario";
                    TempData["Redirect-Action"] = "Index";

                    return RedirectToAction("Default", "Error");
                }

                ViewBag.IdTipMov = listaTipoMovimiento(oInventario.IdTipMovimiento);
                ViewBag.IdUserReg = listaUsuario(oInventario.IdUsuario);
                ViewBag.IdUserGes = listaUsuario(oInventario.UsuarioGestion);
                ViewBag.IdProd = listaProductos(oInventario.Producto);

                return View(oInventario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al tratar de obtener el inventario solicitado: " + ex.Message;
                TempData["Redirect"] = "Inventario";
                TempData["Redirect-Action"] = "Index";

                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Inventario/Edit/5
        [HttpPost]
        public ActionResult Save(GestionInventario pInventario, string[] selectProducto)
        {
            IServiceInventario _ServInventario = new ServiceInventario();

            try
            {
                if (ModelState.IsValid)
                {
                    GestionInventario oInventario = _ServInventario.GuardarInventario(pInventario, selectProducto);
                }
                else
                {
                    Util.ValidateErrors(this);
                    ViewBag.IdTipMov = listaTipoMovimiento(pInventario.IdTipMovimiento);
                    ViewBag.IdUserReg = listaUsuario(pInventario.IdUsuario);
                    ViewBag.IdUserGes = listaUsuario(pInventario.UsuarioGestion);
                    ViewBag.IdProd = listaProductos(pInventario.Producto);
                    return View("Create", pInventario);
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al registrar el inventario: " + ex.Message;
                TempData["Redirect"] = "Inventario";
                TempData["Redirect-Action"] = "Index";

                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Inventario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inventario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // Obtener la cantidad de Salidas del inventario segun tipoGestion
        public ActionResult _TotalSalidas()
        {
            IEnumerable<GestionInventario> lista = null;
            int numSalidas = 0;
            try
            {
                IServiceInventario _SeviceInventario= new ServiceInventario();
                lista = _SeviceInventario.GetInventario();

                foreach (var item in lista)
                {
                    if (item.TipoGestion.Equals("Salida"))
                    {
                        numSalidas += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Default", "Error");

            }
            return Content(numSalidas.ToString());
        }

        // Obtener la cantidad de Salidas del inventario segun tipoGestion
        public ActionResult _TotalEntradas()
        {
            IEnumerable<GestionInventario> lista = null;
            int numEntradas = 0;
            try
            {
                IServiceInventario _SeviceInventario = new ServiceInventario();
                lista = _SeviceInventario.GetInventario();

                foreach (var item in lista)
                {
                    if (item.TipoGestion.Equals("Entrada"))
                    {
                        numEntradas += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Default", "Error");

            }
            return Content(numEntradas.ToString());
        }
    }
}
