using ApplicationCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
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
            return View(lista);
        }
        public ActionResult Details(int? id)
        {
            ServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                producto = _ServiceProducto.GetProductoByID(id.Value);
                if (producto == null)
                {
                    return RedirectToAction("Index");
                }
                return View(producto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Redirecciona a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Libro/Create
        public ActionResult Create()
        {
            //Lista de autores
            //ViewBag.IdAutor = listaAutores();
            ViewBag.IdCategoria = listaCategorias();
            return View();
        }

        public ActionResult _TotalProductos()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                IServiceProducto _SeviceProducto = new ServiceProducto();
                lista = _SeviceProducto.GetProducto();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Default", "Error");

            }
            return Content(lista.Count() + "");
        }
        private MultiSelectList listaCategoria(ICollection<Categoria> categorias)
        {
            //Lista de Categorias
            IServiceCategoriaProducto _ServiceCategoria = new ServiceCategoriaProducto();
            IEnumerable<Categoria> listaCategorias = _ServiceCategoria.GetCategoria();
            int[] listaCategoriasSelect = null;

            if (categorias != null)
            {

                listaCategoriasSelect = categorias.Select(c => c.IdCategoria).ToArray();
            }

            return new MultiSelectList(listaCategorias, "IdCategoria", "Dsc_Categoria", listaCategoriasSelect);

        }
        private SelectList listaCategorias(int idCategoria = 0)
        {
            //Lista de Categorias
            IServiceCategoriaProducto _ServiceCategoriaProducto = new ServiceCategoriaProducto();
            IEnumerable<Categoria> listaCategorias = _ServiceCategoriaProducto.GetCategoria();
            return new SelectList(listaCategorias, "IdCategoria", "Dsc_Categoria", idCategoria);
        }
        // POST: Libro/Edit/5
        [HttpPost]
        public ActionResult Save(Producto producto, string[] selectedCategorias)
        {
            MemoryStream target = new MemoryStream();
            IServiceProducto _ServiceProducto = new ServiceProducto();
            try
            {
                if (ModelState.IsValid)
                {
                    Producto oProductoI = _ServiceProducto.Save(producto, selectedCategorias);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    ViewBag.IdCategoria = listaCategorias(producto.IdCategoria);
                    return View("Create", producto);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}