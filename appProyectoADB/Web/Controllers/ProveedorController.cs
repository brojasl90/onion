using ApplicationCore.Services;
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
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            IEnumerable<Proveedor> lista = null;
            try
            {
                IServiceProveedor _SeviceProveedor = new ServiceProveedor();
                lista = _SeviceProveedor.GetProveedor();
                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());

                // Redirecciona a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult Details (int? pId)
        {
            IServiceProveedor _ServProv = new ServiceProveedor();
            Proveedor oProv = null;

            try
            {
                if (pId == null)
                {
                    return RedirectToAction("Index");
                }

                oProv = _ServProv.GetProveedorByID(pId.Value);

                if (oProv == null)
                {
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.EstadoActual = oProv.Estado == 1 ? "Activo" : "Inactivo";
                return View(oProv);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                /*return RedirectToAction("Index");*/

                // Redirecciona a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult BusquedaXNombre(string pNombre)
        {
            IEnumerable<Proveedor> listaProveedores = null;
            IServiceProveedor _ServProveedor = new ServiceProveedor();

            if (string.IsNullOrEmpty(pNombre))
            {
                listaProveedores = _ServProveedor.GetProveedor();
            }
            else
            {
                listaProveedores = _ServProveedor.GetProveedorByNombre(pNombre);
            }

            return PartialView("_PartialViewProvIndex", listaProveedores);
        }

        public ActionResult Create ()
        {
                return View();
        }

        public ActionResult Edit( int? pId)
        {
            IServiceProveedor _ServProv = new ServiceProveedor();
            Proveedor oProv = null;

            try
            {
                if (pId == null)
                {
                    return RedirectToAction("Index");
                }

                oProv = _ServProv.GetProveedorByID(pId.Value);

                if (oProv == null)
                {
                    return RedirectToAction("Default", "Error");
                }

                return View(oProv);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                /*TempData["Message"] = "Error al procesar los datos: " + ex.Message;
                TempData["Redirect"] = "Proveedor";
                TempData["Redirect-Action"] = "Index";*/

                // Redirecciona a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        [HttpPost]
        public ActionResult Save(Proveedor pProv)
        {
            IServiceProveedor _ServProv = new ServiceProveedor();

            try
            {
                if (ModelState.IsValid)
                {
                    Proveedor oProv = _ServProv.Save(pProv);
                }
                else
                {
                    Util.ValidateErrors(this);
                    return View("Create", pProv);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
               /* TempData["Message"] = "Error al procesar los datos: " + ex.Message;
                TempData["Redirect"] = "Proveedor";
                TempData["Redirect-Action"] = "Index";*/

                return RedirectToAction("Default", "Error");
            }
        }
    }
}