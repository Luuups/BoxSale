using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyBoxSale.Filters;
using MyBoxSale.Models;
using WebMatrix.WebData;
using MyBoxSale.Core;

namespace MyBoxSale.Controllers
{
    public class EmpresaController : BoxSaleController
    {
        //
        // GET: /Empresa/
        
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                using (Entities)
                {
                    if (Entities.EMPRESA.Count() != 0)
                        return RedirectToAction("Index", "Home");
                    else
                        return View();
                }
            }
            catch (Exception ex)
            {
               throw new Exception("Error:" + ex.Message);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreaEmpresa(EMPRESA _empresa)
        {
            if (ModelState.IsValid)
            {
                using (Entities)
                {
                    var ConsultEmpresa = Entities.EMPRESA.Any(x => x.Telefono.Equals(_empresa.Nombre, StringComparison.CurrentCultureIgnoreCase));
                    if (!ConsultEmpresa)
                    {
                        var empresa = Entities.EMPRESA.Add(new EMPRESA
                        {
                            Nombre = _empresa.Nombre,
                            Direccion = _empresa.Direccion,
                            Telefono = _empresa.Telefono,
                            FechaCreacion = DateTime.Now,
                            Activo = true
                        });

                        Entities.SaveChanges();
                        return RedirectToAction("CreaRolesUsuarios", new { id = empresa.Id });
                    }
                    else
                    {
                        ModelState.AddModelError("Empresa", "La Empresa ya existe.");
                    }
                }
            }
            return View("Index",_empresa);
        }

        [AllowAnonymous]
        public ActionResult CreaRolesUsuarios(int id)
        {
            return View(new USUARIO { EmpresaId = id});
        }

        [HttpPost]
        [InitializeSimpleMembership]
        [AllowAnonymous]
        public ActionResult CreaRolesUsuarios(USUARIO _usuario)
        {
            if (ModelState.IsValid)
            {
                using(Entities)
                {
                _usuario.FechaAlta = DateTime.Now;
                _usuario.Activo=true;

                Entities.USUARIO.Add(_usuario);
                Entities.SaveChanges();

                if (!Roles.Provider.RoleExists("Admin"))
                {
                    Roles.CreateRole("Admin");
                }
                if (!Roles.Provider.RoleExists("Cajero"))
                {
                    Roles.CreateRole("Cajero");
                }
                Roles.AddUsersToRoles(new[] { _usuario.NombreUsuario }, new[] { "Admin" });
                }
            }

            WebSecurity.CreateAccount(_usuario.NombreUsuario, _usuario.Password);

            return RedirectToAction("Index","Home");
        }

    }
}
