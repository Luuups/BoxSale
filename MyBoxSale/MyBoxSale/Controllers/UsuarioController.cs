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
    public class UsuarioController : BoxSaleController
    {
        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            try
            {
                using (Entities)
                {
                    var Usuarios = (from usr in Entities.USUARIO
                                    
                                   orderby usr.Apellidos
                                   select new UsuarioView
                                   {
                                       Id=usr.Id,
                                       Nombre=usr.Nombre,
                                       Apellidos=usr.Apellidos,
                                       Telefono=usr.Telefono,
                                       NombreUsuario=usr.NombreUsuario,
                                       Empresa=usr.EMPRESA,
                                       Activo=usr.Activo
                                   }).ToList();
                    return View(Usuarios);
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
                    var reg = Entities.USUARIO.Find(id);
                    Entities.USUARIO.Remove(reg);
                    Entities.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Disabled(int id)
        {
            try
            {
                using (Entities)
                {
                    var reg = Entities.USUARIO.Find(id);
                    if (reg.Activo.HasValue && reg.Activo.Value)
                        reg.Activo = false;
                    else
                        reg.Activo = true;
                    Entities.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                using (Entities)
                {
                    var usuario = (from usr in Entities.USUARIO
                                  where usr.Id.Equals(id)
                                  select new UsuarioView
                                  {
                                      Id = usr.Id,
                                      NombreUsuario = usr.NombreUsuario,
                                      Nombre = usr.Nombre,
                                      Apellidos = usr.Apellidos,
                                      Direccion = usr.Direccion,
                                      Telefono = usr.Telefono,
                                      Password = usr.Password,
                                      Empresa = usr.EMPRESA
                                  }).SingleOrDefault();
                    return View(usuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(USUARIO id)
        {
            try
            {
                using (Entities)
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
