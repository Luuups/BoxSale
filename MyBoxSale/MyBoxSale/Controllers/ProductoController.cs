using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBoxSale.Models;
using MyBoxSale.Core;
using System.Data.Objects.SqlClient;

namespace MyBoxSale.Controllers
{
    [Authorize(Roles="Admin")]
    public class ProductoController : BoxSaleController
    {
        //
        // GET: /Producto/
        
        public ActionResult Index()
        {
            using (Entities)
            {
                var Producto = (from producto in Entities.PRODUCTO
                                orderby producto.Nombre
                                select new
                                {
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
        }
        public ActionResult NuevoProducto()
        {
            using (Entities)
            {
                ViewBag.Categoria = (from Categoria in Entities.CATEGORIA
                                     orderby Categoria.Nombre
                                     select new SelectListItem
                                     {
                                         Value = SqlFunctions.StringConvert((double)Categoria.Id),
                                         Text = Categoria.Nombre
                                     }).ToList();

                ViewBag.Proveedor = (from proveedor in Entities.PROVEEDOR
                                     where proveedor.Activo == true
                                     orderby proveedor.Nombre
                                     select new SelectListItem
                                     {
                                         Value = SqlFunctions.StringConvert((double)proveedor.Id),
                                         Text = proveedor.Nombre
                                     }).ToList();

                ViewBag.UnidadMedida = (from unidad in Entities.UNIDADMEDIDA
                                        where unidad.Activo == true
                                        orderby unidad.Nombre
                                        select new SelectListItem
                                        {
                                            Value = SqlFunctions.StringConvert((double)unidad.Id),
                                            Text = unidad.Nombre
                                        }).ToList();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevoProducto(PRODUCTO _producto)
        {
            if (ModelState.IsValid)
            {
                using (Entities)
                {
                    Entities.PRODUCTO.Add(_producto);

                    if (_producto.FileImg != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images/Producto"),
                                           Path.GetFileName(_producto.FileImg.FileName));
                        _producto.FileImg.SaveAs(path);
                    }

                    Entities.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }


    }
}
