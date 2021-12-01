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
    public class RegistroController : Controller
    {
        // GET: Registro
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Encargado)]
        public ActionResult Index()
        {
            IEnumerable<RegistroInventario> lista = null;

            try
            {
                IServiceRegistro _ServReg= new ServiceRegistro();

                ViewBag.IdUserReg = listaUsuario();
                lista = _ServReg.GetRegistro();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al tratar de obtener los registros de Inventario: " + ex.Message;

                return RedirectToAction("Default", "Error");
            }

            return View(lista);
        }

        // GET: Registro/Details/5
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Encargado)]
        public ActionResult Details(int? pId)
        {
            IServiceRegistro _ServRegistro = new ServiceRegistro();
            RegistroInventario oRegistro = null;

            try
            {
                if (pId == null)
                {
                    return RedirectToAction("Index");
                }

                oRegistro = _ServRegistro.GetRegistroByID(pId.Value);

                if (oRegistro == null)
                {
                    TempData["Message"] = "No existe la información solicitada.";
                    TempData["Redirect"] = "Registro";
                    TempData["Redirect-Action"] = "Index";

                    return RedirectToAction("Default", "Error");
                }

                ViewBag.IdUserReg = listaUsuario(oRegistro.IdUsuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al tratar de obtener el inventario solicitado: " + ex.Message;
                TempData["Redirect"] = "Inventario";
                TempData["Redirect-Action"] = "Index";

                return RedirectToAction("Default", "Error");
            }

            return View(oRegistro);
        }

        // GET: Registro/Create
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Encargado)]
        public ActionResult Create()
        {
            ViewBag.ListGestion = listaGestiones(null);
            return View();
        }

        private SelectList listaUsuario(int pIdUsuario = 0)
        {
            IServiceUsuario _ServUser = new ServiceUsuario();
            IEnumerable<Usuario> lUsuarios = _ServUser.GetUsuario();
            return new SelectList(lUsuarios, "idUsuario", "Nombre", pIdUsuario);
        }

        private MultiSelectList listaGestiones(ICollection<GestionInventario> pGestion)
        {
            IServiceInventario _ServGestion = new ServiceInventario();
            IEnumerable<GestionInventario> lGestiones = _ServGestion.GetInventario();

            int[] listaGestionSelect = null;

            if (pGestion != null)
            {
                listaGestionSelect = pGestion.Select(g => g.IdGestionInventario).ToArray();
            }

            return new MultiSelectList(lGestiones, "IdGestionInventario", listaGestionSelect);
        }

        // GET: Registro/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? pId)
        {
            IServiceRegistro _ServRegistro = new ServiceRegistro();
            RegistroInventario oRegistro = null;

            try
            {
                if (pId == null)
                {
                    return RedirectToAction("Index");
                }

                oRegistro = _ServRegistro.GetRegistroByID(pId.Value);

                if (oRegistro == null)
                {
                    TempData["Message"] = "No existe la información solicitada.";
                    TempData["Redirect"] = "Registro";
                    TempData["Redirect-Action"] = "Index";

                    return RedirectToAction("Default", "Error");
                }

                ViewBag.ListGestion = listaGestiones(oRegistro.GestionInventario);

                return View(oRegistro);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al tratar de obtener el inventario solicitado: " + ex.Message;
                TempData["Redirect"] = "Registro";
                TempData["Redirect-Action"] = "Index";

                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Registro/Edit/5
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Encargado)]
        public ActionResult Save(RegistroInventario pRegistro, string[] selectGestion)
        {
            IServiceRegistro _ServRegistro = new ServiceRegistro();

            try
            {
                if (ModelState.IsValid)
                {
                    RegistroInventario oRegristro = _ServRegistro.GuardarRegistro(pRegistro, selectGestion);
                }
                else
                {
                    Util.ValidateErrors(this);
                    ViewBag.IdUserReg = listaUsuario(pRegistro.IdUsuario);
                    ViewBag.ListGestion = listaGestiones(pRegistro.GestionInventario);
                    return View("Create", pRegistro);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al registrar el inventario: " + ex.Message;
                TempData["Redirect"] = "Registro";
                TempData["Redirect-Action"] = "Index";

                return RedirectToAction("Default", "Error");
            }
        }
    }
}
