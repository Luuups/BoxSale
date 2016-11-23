using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBoxSale.Models;

namespace MyBoxSale.Controllers
{
    [Authorize(Roles="Admin")]
    public class ProductoController : Controller
    {
        //
        // GET: /Producto/
        private MyBoxSaleEntities db = new MyBoxSaleEntities();
        public ActionResult Index()
        {
            var Producto = (from producto in db.PRODUCTO orderby producto.Nombre
                           select new {
                               producto.Id,
                               producto.Nombre,
                               producto.PrecioCompra,
                               producto.PrecioMostrador,
                               producto.Activo,
                               producto.CATEGORIA,
                               producto.UNIDADMEDIDA,
                               producto.StockMin,
                               producto.Existencia,
                               producto.PROVEEDOR,
                               producto.Imagen
                           }).ToList();
            return View(Producto);
        }
        public ActionResult NuevoProducto()
        {
            ViewBag.Categoria = (from Categoria in db.CATEGORIA orderby Categoria.Nombre
                                    select new SelectListItem
                                    {
                                     Value=Convert.ToString(Categoria.Id),
                                     Text=Categoria.Nombre
                                    }).ToList();

            ViewBag.Proveedor = (from proveedor in db.PROVEEDOR
                                 where proveedor.Activo == true
                                 orderby proveedor.Nombre
                                 select new SelectListItem
                                 {
                                     Value = Convert.ToString(proveedor.Id),
                                     Text = proveedor.Nombre
                                 }).ToList();

            ViewBag.UnidadMedida = (from unidad in db.UNIDADMEDIDA
                                    where unidad.Activo == true
                                    orderby unidad.Nombre
                                    select new SelectListItem
                                    {
                                        Value = Convert.ToString(unidad.Id),
                                        Text = unidad.Nombre
                                    }).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevoProducto(PRODUCTO _producto)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCTO.Add(_producto);

                if (_producto.FileImg != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Images/Producto"),
                                       Path.GetFileName(_producto.FileImg.FileName));
                    _producto.FileImg.SaveAs(path);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }


    }
}
