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
    public class BodegaController : Controller
    {
        // GET: Bodega
        public ActionResult Index()
        {
            IEnumerable<Bodega> lista = null;
            try
            {
                IServiceBodega _SeviceBodega = new ServiceBodega();
                lista = _SeviceBodega.GetBodega();
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
        // Detalle: Bodega
        public ActionResult Details(int? pId)
        {
            IServiceBodega _SeviceBodega = new ServiceBodega();
            Bodega oBodega = null;

            try
            {
                if (pId == null)
                {
                    return RedirectToAction("Index");
                }

                oBodega = _SeviceBodega.GetBodegaByID(pId.Value);

                if (oBodega == null)
                {
                    return RedirectToAction("Default", "Error");
                }

                return View(oBodega);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                /*return RedirectToAction("Index");*/

                // Redirecciona a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Edit(int? pId)
        {
            IServiceBodega _SeviceBodega = new ServiceBodega();
            Bodega oBodega = null;

            try
            {
                if (pId == null)
                {
                    return RedirectToAction("Index");
                }

                oBodega = _SeviceBodega.GetBodegaByID(pId.Value);

                if (oBodega == null)
                {
                    return RedirectToAction("Default", "Error");
                }

                return View(oBodega);
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
        public ActionResult Save(Bodega pBodega)
        {
            IServiceBodega _SeviceBodega = new ServiceBodega();

            try
            {
                if (ModelState.IsValid)
                {
                    Bodega oBodega = _SeviceBodega.Save(pBodega);
                }
                else
                {
                    Util.ValidateErrors(this);
                    return View("Create", pBodega);
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