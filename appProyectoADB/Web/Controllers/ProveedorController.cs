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
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());

                // Redirecciona a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }

        public ActionResult Details (int? id)
        {
            ServiceProveedor _ServProv = new ServiceProveedor();
            Proveedor oProv = null;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                oProv = _ServProv.GetProveedorByID(id.Value);

                if (oProv == null)
                {
                    return RedirectToAction("Index");
                }

                return View(oProv);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");

                // Redirecciona a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}