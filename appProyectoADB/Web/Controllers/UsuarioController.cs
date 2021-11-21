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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<Usuario> lista = null;

            try
            {
                IServiceUsuario _ServUsuario = new ServiceUsuario();
                lista = _ServUsuario.GetUsuario();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al tratar de obtener lista de Usuarios: " + ex.Message;

                return RedirectToAction("Default", "Error");
            }

            return View(lista);
        }

        // GET: Usuario/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int? pId)
        {
            IServiceUsuario _ServUsuario = new ServiceUsuario();
            Usuario oUsuario
                = null;

            try
            {
                if (pId == null)
                {
                    return RedirectToAction("Index");
                }

                oUsuario = _ServUsuario.GetUsuarioByID(pId.Value);

                if (oUsuario == null)
                {
                    TempData["Message"] = "No existe la información solicitada.";
                    TempData["Redirect"] = "Usuario";
                    TempData["Redirect-Action"] = "Index";

                    return RedirectToAction("Default", "Error");
                }

                ViewBag.EstadoActual = oUsuario.Estado == 1 ? "Activo" : "Inactivo";
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al tratar de obtener el usuario solicitado: " + ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "Index";

                return RedirectToAction("Default", "Error");
            }

            return View(oUsuario);
        }

        public ActionResult BusquedaXNombre(string pNombre)
        {
            IEnumerable<Usuario> listaUsuarios = null;
            IServiceUsuario _ServUsuario = new ServiceUsuario();

            if (string.IsNullOrEmpty(pNombre))
            {
                listaUsuarios = _ServUsuario.GetUsuario();
            }
            else
            {
                listaUsuarios = _ServUsuario.GetUsuarioByNombre(pNombre);
            }

            return PartialView("_PartialViewUserIndex", listaUsuarios);
        }

        private SelectList listaTipoRol(int pIdRol = 0)
        {
            IServiceRol _ServRol = new ServiceRol();
            IEnumerable<Rol> lTipoRoles = _ServRol.GetRol();
            return new SelectList(lTipoRoles, "IdRol", "Descripcion", pIdRol);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.TipoRoles = listaTipoRol();
            return View();
        }

        // GET: Usuario/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? pId)
        {
            IServiceUsuario _ServUsuario = new ServiceUsuario();
            Usuario oUsuario = null;

            try
            {
                if (pId == null)
                {
                    return RedirectToAction("Index");
                }

                oUsuario = _ServUsuario.GetUsuarioByID(pId.Value);

                if (oUsuario == null)
                {
                    TempData["Message"] = "No existe la información solicitada.";
                    TempData["Redirect"] = "Usuario";
                    TempData["Redirect-Action"] = "Index";

                    return RedirectToAction("Default", "Error");
                }
                                
                ViewBag.TipoRoles = listaTipoRol(oUsuario.IdRol);

                return View(oUsuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al tratar de obtener el usuario solicitado: " + ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "Index";

                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Save(Usuario pUsuario)
        {
            IServiceUsuario _ServUsuario = new ServiceUsuario();

            try
            {
                if (ModelState.IsValid)
                {
                    Usuario oUsuario = _ServUsuario.GuardarUsuario(pUsuario);
                }
                else
                {
                    Util.ValidateErrors(this);
                    ViewBag.TipoRoles = listaTipoRol(pUsuario.IdRol);
                    return View("Create", pUsuario);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al registrar el Usuario: " + ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "Index";

                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
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
    }
}
