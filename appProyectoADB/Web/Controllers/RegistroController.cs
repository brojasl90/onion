﻿using ApplicationCore.Services;
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
using Web.ViewModel;

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
            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }
            ViewBag.ListGestion = listaGestiones(null);
            ViewBag.DetalleOrden = Movimiento.Instancia.Items;
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
        public ActionResult Save(RegistroInventario pRegistro)
        {
            IServiceRegistro _ServRegistro = new ServiceRegistro();

            try
            {
                // Si no existe la sesión no hay nada
                if (Movimiento.Instancia.Items.Count() <= 0)
                {
                    // Validaciones de datos requeridos.
                    TempData["NotificationMessage"] = SweetAlertHelper.Mensaje("Orden", "Seleccione los libros a ordenar", SweetAlertMessageType.warning);
                    return RedirectToAction("Index");
                }
                else
                {

                    var listaDetalle = Movimiento.Instancia.Items;

                    foreach (var item in listaDetalle)
                    {
                        GestionInventario lineaInventario = new GestionInventario();
                        lineaInventario.IdUsuario = item.IdUsuario;
                        lineaInventario.TipoGestion = item.TipoGestion;
                        lineaInventario.Observaciones = item.Observaciones;
                        pRegistro.GestionInventario.Add(lineaInventario);
                    }
                }

                RegistroInventario ordenSave = _ServRegistro.GuardarRegistro(pRegistro);

                // Limpia el Carrito de compras
                Movimiento.Instancia.eliminarCarrito();
                TempData["NotificationMessage"] = SweetAlertHelper.Mensaje("Orden", "Orden guardada satisfactoriamente!", SweetAlertMessageType.success);
                // Reporte orden
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

        public ActionResult BusquedaXNombre(string pNombre)
        {
            IEnumerable<RegistroInventario> listaRegistros = null;
            IServiceRegistro _ServRegistro = new ServiceRegistro();

            if (string.IsNullOrEmpty(pNombre))
            {
                listaRegistros = _ServRegistro.GetRegistro();
            }
            else
            {
                listaRegistros = _ServRegistro.GetRegistroPorUsuario(pNombre);
            }
            ViewBag.LtsUsuarios = listaUsuario();
            return PartialView("_PartialViewRegIndex", listaRegistros);
        }

        public ActionResult _VistaProducto()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                IServiceProducto _SeviceProducto = new ServiceProducto();
                lista = _SeviceProducto.GetProducto();
                ViewBag.title = "Mantenimiento de productos";
                //Lista Categorias
                IServiceCategoriaProducto _ServiceCategoriaProducto = new ServiceCategoriaProducto();
                ViewBag.listaCategorias = _ServiceCategoriaProducto.GetCategoria();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redirecciona a la captura del Error
                return RedirectToAction("Default", "Error");

            }
            return PartialView(lista);
        }

        //Actualizar Vista parcial detalle carrito
        private ActionResult DetalleCarrito()
        {

            return PartialView("_DetalleOrden", Movimiento.Instancia.Items);
        }
        //Actualizar cantidad de libros en el carrito
        public ActionResult actualizarCantidad(int idLibro, int cantidad)
        {
            ViewBag.DetalleOrden = Movimiento.Instancia.Items;
            TempData["NotiCarrito"] = Movimiento.Instancia.SetItemCantidad(idLibro, cantidad);
            TempData.Keep();
            return PartialView("_DetalleOrden", Movimiento.Instancia.Items);

        }
        //Ordenar un libro y agregarlo al carrito
        public ActionResult ordenarProducto(int? pIdPro)
        {
            int cantidadLibros = Movimiento.Instancia.Items.Count();
            ViewBag.NotiCarrito = Movimiento.Instancia.AgregarItem((int)pIdPro);
            return PartialView("_OrdenCantidad");

        }

        //Actualizar solo la cantidad de libros que se muestra en el menú
        public ActionResult actualizarOrdenCantidad()
        {
            if (TempData.ContainsKey("NotiCarrito"))
            {
                ViewBag.NotiCarrito = TempData["NotiCarrito"];
            }
            int cantidadLibros = Movimiento.Instancia.Items.Count();
            return PartialView("_OrdenCantidad");

        }
        public ActionResult eliminarProducto(int? idLibro)
        {
            ViewBag.NotificationMessage = Movimiento.Instancia.EliminarItem((int)idLibro);
            return PartialView("_DetalleOrden", Movimiento.Instancia.Items);
        }
    }
}
