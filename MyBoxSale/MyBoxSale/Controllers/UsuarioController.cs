using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyBoxSale.Core;
using MyBoxSale.Filters;
using MyBoxSale.Models;
using MyBoxSale.Models.Local;
using WebMatrix.WebData;

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
                    var Usuarios = (from usr in Entities.USUARIO.Include("USUARIOROLES").Include("USUARIOROLES.ROLES")
                                    where usr.USUARIOROLES.Any(x => x.ROLES.Nombre != "Admin")
                                    select new UsuarioView
                                    {
                                        Id = usr.Id,
                                        Nombre = usr.Nombre,
                                        Apellidos = usr.Apellidos,
                                        Telefono = usr.Telefono,
                                        NombreUsuario = usr.NombreUsuario,
                                        Empresa = usr.EMPRESA,
                                        Activo = usr.Activo
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
        public ActionResult Edit(USUARIO _usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (Entities)
                    {
                        var usuario = Entities.USUARIO.Find(_usuario.Id);
                        usuario.Nombre = _usuario.Nombre;
                        usuario.Apellidos = _usuario.Apellidos;
                        usuario.Direccion = _usuario.Direccion;
                        usuario.Telefono = _usuario.Telefono;
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

        public ActionResult NewUser()
        {
            try
            {
                using (Entities)
                {
                    ViewBag.Empresa = Entities.EMPRESA.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult NewUser(USUARIO _newUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (Entities)
                    {
                        _newUser.EmpresaId = Entities.EMPRESA.FirstOrDefault().Id;
                        Entities.USUARIO.Add(_newUser);
                        Entities.SaveChanges();
                        Roles.AddUsersToRoles(new[] { _newUser.NombreUsuario }, new[] { "Cajero" });
                        WebSecurity.CreateAccount(_newUser.NombreUsuario, _newUser.Password);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return View();
        }
    }
}
