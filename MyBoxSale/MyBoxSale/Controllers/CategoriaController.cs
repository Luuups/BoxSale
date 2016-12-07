using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBoxSale.Core;
using MyBoxSale.Models.Local;
using MyBoxSale.Models;

namespace MyBoxSale.Controllers
{
    [Authorize(Roles="Admin")]
    public class CategoriaController : BoxSaleController
    {
        //
        // GET: /Categoria/

        public ActionResult Index()
        {
            using (Entities)
            {
                var Categorias = (from cat in Entities.CATEGORIA
                                 orderby cat.Nombre
                                 select new CategoriaView
                                 {
                                     Id=cat.Id,
                                     Nombre=cat.Nombre,
                                     Activo=cat.Activo
                                 }).ToList();
                
                return View(Categorias);
            }
        }

        public ActionResult NuevaCategoria()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevaCategoria(CATEGORIA _categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (Entities)
                    {
                        _categoria.Activo = true;
                        Entities.CATEGORIA.Add(_categoria);
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
                    var categoria = Entities.CATEGORIA.Find(id);
                    return View(categoria);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(CATEGORIA _categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (Entities)
                    {
                        var categoria = Entities.CATEGORIA.Find(_categoria.Id);
                        categoria.Nombre = _categoria.Nombre;
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

        public ActionResult Eliminar(int id)
        {
            try
            {
                using(Entities)
                {
                    var categoria = Entities.CATEGORIA.Find(id);
                    Entities.CATEGORIA.Remove(categoria);
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
                    var cat = Entities.CATEGORIA.Find(id);
                    if (cat.Activo.HasValue && cat.Activo.Value)
                        cat.Activo = false;
                    else
                        cat.Activo = true;
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
