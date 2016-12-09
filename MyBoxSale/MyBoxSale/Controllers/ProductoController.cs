using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBoxSale.Models;
using MyBoxSale.Core;
using System.Data.Objects.SqlClient;
using MyBoxSale.Models.Local;

namespace MyBoxSale.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductoController : BoxSaleController
    {
        //
        // GET: /Producto/

        public ActionResult Index()
        {
            try
            {
                using (Entities)
                {
                    var Producto = (from producto in Entities.PRODUCTO
                                    orderby producto.Nombre
                                    select new ProductoView
                                    {
                                        Id = producto.Id,
                                        SKU=producto.SKU,
                                        Nombre = producto.Nombre,
                                        PrecioCompra = producto.PrecioCompra,
                                        PrecioMostrador = producto.PrecioMostrador,
                                        Activo = producto.Activo,
                                        CATEGORIA = producto.CATEGORIA,
                                        UNIDADMEDIDA = producto.UNIDADMEDIDA,
                                        StockMin = producto.StockMin,
                                        Existencia = producto.Existencia,
                                        PROVEEDOR = producto.PROVEEDOR,
                                        Imagen = producto.Imagen
                                    }).ToList();
                    return View(Producto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult NuevoProducto()
        {
            using (Entities)
            {
                CargaSelects();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevoProducto(PRODUCTO _producto)
        {
            if (ModelState.IsValid)
            {
                using (Entities)
                {

                    if (_producto.FileImg != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images/Productos"),
                                           Path.GetFileName(_producto.FileImg.FileName));
                        _producto.FileImg.SaveAs(path);
                        _producto.Imagen = _producto.FileImg.FileName;
                    }

                    Entities.PRODUCTO.Add(_producto);

                    Entities.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (Entities)
                {
                    CargaSelects();
                    return View(Entities.PRODUCTO.Find(id));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (Entities)
                {
                    Entities.PRODUCTO.Remove(Entities.PRODUCTO.Find(id));
                    Entities.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PRODUCTO _producto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (Entities)
                    {
                        var updateProducto = Entities.PRODUCTO.Find(_producto.Id);
                        if (updateProducto == null)
                            throw new Exception("No se Encontro el Producto");
                        updateProducto.SKU = _producto.SKU;
                        updateProducto.Nombre = _producto.Nombre;
                        updateProducto.PrecioCompra = _producto.PrecioCompra;
                        updateProducto.PrecioMostrador = _producto.PrecioMostrador;
                        updateProducto.ProveedorId = _producto.ProveedorId;
                        updateProducto.UnidadId = _producto.UnidadId;
                        updateProducto.StockMin = _producto.StockMin;
                        updateProducto.CategoriaId = _producto.CategoriaId;

                        if (_producto.FileImg != null)
                        {
                            string path = Path.Combine(Server.MapPath("~/Images/Productos"),
                                               Path.GetFileName(_producto.FileImg.FileName));
                            _producto.FileImg.SaveAs(path);
                            updateProducto.Imagen = _producto.FileImg.FileName;
                        }

                        Entities.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return View();
        }

        private void CargaSelects()
        {
            var preview = (from Categoria in Entities.CATEGORIA
                           orderby Categoria.Nombre
                           select new SelectListItem
                           {
                               Value = SqlFunctions.StringConvert((double)Categoria.Id).Trim(),
                               Text = Categoria.Nombre
                           }).ToList();
            preview.Add(new SelectListItem { Selected = true, Text = "Selecciona un Categoria", Value = "" });
            ViewBag.Categoria = preview;
            preview = (from proveedor in Entities.PROVEEDOR
                       where proveedor.Activo == true
                       orderby proveedor.Nombre
                       select new SelectListItem
                       {
                           Value = SqlFunctions.StringConvert((double)proveedor.Id).Trim(),
                           Text = proveedor.Nombre
                       }).ToList();
            preview.Add(new SelectListItem { Selected = true, Text = "Selecciona un Proveedor", Value = "" });
            ViewBag.Proveedor = preview;
            preview=(from unidad in Entities.UNIDADMEDIDA
                                        where unidad.Activo == true
                                        orderby unidad.Nombre
                                        select new SelectListItem
                                        {
                                            Value = SqlFunctions.StringConvert((double)unidad.Id).Trim(),
                                            Text = unidad.Nombre
                                        }).ToList();
            preview.Add(new SelectListItem { Selected = true, Text = "Selecciona una Unidad", Value = "" });
            ViewBag.UnidadMedida = preview;
                
        }
    }
}
