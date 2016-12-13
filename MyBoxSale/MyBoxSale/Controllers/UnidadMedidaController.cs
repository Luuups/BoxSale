using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBoxSale.Models.Local;
using MyBoxSale.Core;
using MyBoxSale.Models;

namespace MyBoxSale.Controllers
{
    [Authorize(Roles="Admin")]
    public class UnidadMedidaController : BoxSaleController
    {
        //
        // GET: /UnidadMedida/

        public ActionResult Index()
        {
            using (Entities)
            {
                var unidades = (from unidad in Entities.UNIDADMEDIDA
                               orderby unidad.Nombre
                               select new UnidadView
                               {
                                   Id=unidad.Id,
                                   Nombre=unidad.Nombre,
                                   Abreviado=unidad.Abreviado,
                                   Activo=unidad.Activo
                               }).ToList();

                return View(unidades);
            }
        }

        public ActionResult NuevaUnidad()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevaUnidad(UNIDADMEDIDA _unidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (Entities)
                    {
                        _unidad.Activo = true;
                        Entities.UNIDADMEDIDA.Add(_unidad);
                        Entities.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return View();
        }

        public ActionResult Editar(int id)
        {
            try
            {
                using (Entities)
                {
                    var unidad = Entities.UNIDADMEDIDA.Find(id);
                    return View(unidad);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(UNIDADMEDIDA _unidad)
        {
            try
            {
                using (Entities)
                {
                    var unidad = Entities.UNIDADMEDIDA.Find(_unidad.Id);
                    unidad.Nombre = _unidad.Nombre;
                    unidad.Abreviado = _unidad.Abreviado;
                    Entities.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Eliminar(int id)
        {
            try
            {
                using (Entities)
                {
                    var unidad = Entities.UNIDADMEDIDA.Find(id);
                    Entities.UNIDADMEDIDA.Remove(unidad);
                    Entities.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Desactivar(int id)
        {
            try
            {
                using (Entities)
                {
                    var unidad = Entities.UNIDADMEDIDA.Find(id);
                    if (unidad.Activo.HasValue && unidad.Activo.Value)
                        unidad.Activo = false;
                    else
                        unidad.Activo = true;
                    Entities.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
