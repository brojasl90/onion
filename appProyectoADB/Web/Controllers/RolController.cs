using ApplicationCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Enum;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<Rol> lista = null;
            try
            {
                IServiceRol _SeviceRol = new ServiceRol();
                lista = _SeviceRol.GetRol();
                ViewBag.EstadoRol = new List<string> { "Activo", "Inactivo" };

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

        // GET: Rol/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int? pId)
        {
            IServiceRol _ServRol = new ServiceRol();
            Rol oRol = null;

            try
            {
                if (pId == null)
                {
                    return RedirectToAction("Index");
                }

                oRol = _ServRol.GetRolByID(pId.Value);

                if (oRol == null)
                {
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.EstadoActual = oRol.Estado == 1 ? "Activo" : "Inactivo";

                return View(oRol);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                /*return RedirectToAction("Index");*/

                // Redirecciona a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Rol/Create
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            return View();
        }

        // GET: Rol/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? pId)
        {
            IServiceRol _ServRol = new ServiceRol();
            Rol oRol = null;

            try
            {
                if (pId == null)
                {
                    return RedirectToAction("Index");
                }

                oRol = _ServRol.GetRolByID(pId.Value);

                if (oRol == null)
                {
                    return RedirectToAction("Default", "Error");
                }


                return View(oRol);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos: " + ex.Message;
                TempData["Redirect"] = "Rol";
                TempData["Redirect-Action"] = "Index";

                // Redirecciona a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Rol/Edit/5
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(Rol pRol)
        {
            IServiceRol _ServRol = new ServiceRol();

            try
            {
                if (ModelState.IsValid)
                {
                    Rol oRol= _ServRol.GuardarRol(pRol);
                }
                else
                {
                    Util.ValidateErrors(this);
                    return View("Create", pRol);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos: " + ex.Message;
                TempData["Redirect"] = "Proveedor";
                TempData["Redirect-Action"] = "Index";

                return RedirectToAction("Default", "Error");
            }
        }
    }
}
