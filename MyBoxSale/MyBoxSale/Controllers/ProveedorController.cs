using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBoxSale.Core;
using MyBoxSale.Models;
using MyBoxSale.Models.Local;
namespace MyBoxSale.Controllers
{
    [Authorize(Roles="Admin")]
    public class ProveedorController : BoxSaleController
    {
        //
        // GET: /Proveedor/

        public ActionResult Index()
        {
            using(Entities)
            {
                var query = (from prove in Entities.PROVEEDOR
                             orderby prove.Nombre
                             select new ProveedorView
                             {
                                 Id=prove.Id,
                                 Nombre=prove.Nombre,
                                 Telefono=prove.Telefono,
                                 ContactoProveedor=prove.ContactoProveedor,
                                 Activo=prove.Activo

                             }).ToList();
                return View(query);
            }
        }

        public ActionResult NuevoProveedor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevoProveedor(PROVEEDOR _proveedor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (Entities)
                    {
                        _proveedor.Activo = true;
                        Entities.PROVEEDOR.Add(_proveedor);
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
                    var proveedor = Entities.PROVEEDOR.Find(id);
                    if (proveedor != null)
                        return View(proveedor);
                    else
                        throw new Exception("No se encuentra el registro especificado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(PROVEEDOR _proveedor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var proveedor = Entities.PROVEEDOR.Find(_proveedor.Id);
                    proveedor.Nombre = _proveedor.Nombre;
                    proveedor.Telefono = _proveedor.Telefono;
                    proveedor.Direccion = _proveedor.Direccion;
                    proveedor.ContactoProveedor = _proveedor.ContactoProveedor;
                    Entities.SaveChanges();
                    return RedirectToAction("Index");
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
                if (id != null)
                {
                    using (Entities)
                    {
                        Entities.PROVEEDOR.Remove(Entities.PROVEEDOR.Find(id));
                        Entities.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                    throw new Exception("Registro no encontrado");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.HResult == -2146233087)
                    throw new Exception("El proveedor selecionado no se puede eliminar ya que tiene productos vinculados");
                throw new Exception(ex.Message);
            }
            
        }

        public ActionResult Desactivar(int id)
        {
            try
            {
                if (id != null)
                {
                    using (Entities)
                    {
                        var proveedor = Entities.PROVEEDOR.Find(id);
                        if (proveedor.Activo.HasValue && proveedor.Activo.Value)
                            proveedor.Activo = false;
                        else
                            proveedor.Activo = true;

                        Entities.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                    throw new Exception("Registro no encontrado");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
