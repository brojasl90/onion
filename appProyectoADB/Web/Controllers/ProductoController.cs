using ApplicationCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    public class ProductoController : Controller
    {
        // GET: Producto
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Encargado, (int)Roles.Vendedor)]
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

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Encargado, (int)Roles.Vendedor)]
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

        public ActionResult BusquedaXNombre(string pNombre)
        {
            IEnumerable<Producto> listaProductos = null;
            IServiceProducto _ServProducto = new ServiceProducto();

            if (string.IsNullOrEmpty(pNombre))
            {
                listaProductos = _ServProducto.GetProducto();
            }
            else
            {
                listaProductos = _ServProducto.GetProductoByNombre(pNombre);
            }

            return PartialView("_PartialViewProdIndex", listaProductos);
        }

        // GET: Libro/Create
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            //Lista de Categorias           
            ViewBag.IdCategoria = listaCategorias();
            ViewBag.IdProveedor = listaProveedores(null);
            ViewBag.IdBodega = listaBodega(null);
            //ViewBag.IdBodega = listaBodegaEnum(null);
            return View();
        }
        /// <summary>
        /// Editar producto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            IServiceProducto _ServProv = new ServiceProducto();
            Producto oProd = null;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                oProd = _ServProv.GetProductoByID(id.Value);

                if (oProd == null)
                {
                    TempData["Message"] = "No existe el Producto solicitado";
                    TempData["Redirect"] = "Producto";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    
                    return RedirectToAction("Default", "Error");
                }
                ViewBag.IdCategoria = listaCategorias(oProd.IdCategoria);
                ViewBag.IdProveedor = listaProveedores(oProd.Proveedor);
                ViewBag.IdBodega = listaBodega(oProd.Bodega);
                ViewBag.ValorPrecio = (int)oProd.Precio;
                return View(oProd);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos: " + ex.Message;
                TempData["Redirect"] = "Producto";
                TempData["Redirect-Action"] = "Index";

                // Redirecciona a la captura del Error
                return RedirectToAction("Default", "Error");
            }
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
            return Content(lista.Count().ToString());
        }

        public ActionResult _ProductosPorAgotar()
        {
            IEnumerable<Producto> lista = null;
            try
            {
                IServiceProducto _SeviceProducto = new ServiceProducto();
                lista = _SeviceProducto.GetProductoByAgotar();

                IServiceCategoriaProducto _ServiceCategoriaProducto = new ServiceCategoriaProducto();
                ViewBag.listaCategorias = _ServiceCategoriaProducto.GetCategoria();

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                return RedirectToAction("Default", "Error");
            }

            return PartialView(lista);
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
        //Lista Ubicacion
        private SelectList listaUbicacion(int idUbicacion = 0)
        {
            //Lista de Ubicacion
            IServiceUbicacion _ServiceUbicacion = new ServiceUbicacion();
            IEnumerable<Ubicacion> listaUbicacion = _ServiceUbicacion.GetUbicacion();
            return new SelectList(listaUbicacion, "IdUbicacion", "Descripcion", idUbicacion);
        }
        //Lista Categorias
        private SelectList listaCategorias(int idCategoria = 0)
        {
            //Lista de Categorias
            IServiceCategoriaProducto _ServiceCategoriaProducto = new ServiceCategoriaProducto();
            IEnumerable<Categoria> listaCategorias = _ServiceCategoriaProducto.GetCategoria();
            return new SelectList(listaCategorias, "IdCategoria", "Dsc_Categoria", idCategoria);
        }
        //Lista de proveedores
        private MultiSelectList listaProveedores(ICollection<Proveedor> proveedores)
        {
            //Lista de Proveedores
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            IEnumerable<Proveedor> listaProveedores = _ServiceProveedor.GetProveedor();
            int[] listaProveedoresSelect = null;

            if (proveedores != null)
            {

                listaProveedoresSelect = proveedores.Select(c => c.IdProveedor).ToArray();
            }

            return new MultiSelectList(listaProveedores, "IdProveedor", "Nombre_Proveedor", listaProveedoresSelect);
        }
        //Lista de bodegas
        private MultiSelectList listaBodega(ICollection<Bodega> bodegas)
        {
            //Lista de Proveedores
            IServiceBodega _ServiceBodega = new ServiceBodega();
            IEnumerable<Bodega> listaBodegas = _ServiceBodega.GetBodega();
            int[] listaBodegasSelect = null;

            if (bodegas != null)
            {

                listaBodegasSelect = bodegas.Select(c => c.IdBodega).ToArray();
            }

            return new MultiSelectList(listaBodegas, "IdBodega", "Descripcion", "Estante", listaBodegasSelect);
        }
        //Lista de bodegas Enum
        private MultiSelectList listaBodegaEnum(ICollection<Bodega> bodegas)
        {
            IServiceBodega _ServiceBodega = new ServiceBodega();
            IEnumerable<Bodega> listaBodegas = _ServiceBodega.GetBodega();

            var enumData = from EUbicacion e in System.Enum.GetValues(typeof(EUbicacion))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString()
                           };
            return new MultiSelectList(listaBodegas, "IdBodega", "IdUbicacion", "Descripcion", enumData);
        }
        // POST: Libro/Edit/5
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(Producto producto, string[] selectedCategorias, string[] selectedProveedores, string[] selectedBodega)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            try
            {
                if (ModelState.IsValid)
                {
                    Producto oProductoI = _ServiceProducto.Save(producto, selectedCategorias, selectedProveedores, selectedBodega);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    ViewBag.IdCategoria = listaCategorias(producto.IdCategoria);
                    ViewBag.IdProveedor = listaProveedores(producto.Proveedor);
                    //ViewBag.IdBodega = listaBodega(producto.Bodega);
                    ViewBag.IdBodega = listaBodega(producto.Bodega);
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
        public ActionResult graficoProducto()
        {
            //Documentación chartjs https://www.chartjs.org/docs/latest/
            IServiceProducto _ServiceProducto = new ServiceProducto();
            ViewModelGrafico grafico = new ViewModelGrafico();
            string etiquetas = "";
            string valores = "";
            _ServiceProducto.GetProductoCountDate(out etiquetas, out valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.titulo = "Ordenes por fecha";
            grafico.tituloEtiquetas = "Cantidad de Salidas";

            grafico.tipo = "polarArea";
            ViewBag.grafico = grafico;
            return View();
        }
    }
}