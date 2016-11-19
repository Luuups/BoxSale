using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyBoxSale.Filters;
using MyBoxSale.Models;
using WebMatrix.WebData;

namespace MyBoxSale.Controllers
{
    public class EmpresaController : Controller
    {
        //
        // GET: /Empresa/
        private MyBoxSaleEntities Be = new MyBoxSaleEntities();
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (Be.EMPRESA.Count() != 0)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreaEmpresa(EMPRESA _empresa)
        {
            if (ModelState.IsValid)
            {
                var empresa=Be.EMPRESA.Add(new EMPRESA
                {
                    Nombre = _empresa.Nombre,
                    Direccion = _empresa.Direccion,
                    Telefono = _empresa.Telefono,
                    FechaCreacion = DateTime.Now,
                    Activo=true
                });

                Be.SaveChanges();
                return RedirectToAction("CreaRolesUsuarios",  new { id=empresa.Id });
            }
            return View();
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
                _usuario.FechaAlta = DateTime.Now;
                _usuario.Activo=true;

                Be.USUARIO.Add(_usuario);
                Be.SaveChanges();

            if (!Roles.Provider.RoleExists("Admin"))
            {
                Roles.CreateRole("Admin");
            }
            if (!Roles.Provider.RoleExists("User"))
            {
                Roles.CreateRole("User");
            }
                Roles.AddUsersToRoles(new[] { _usuario.NombreUsuario }, new[] { "Admin" });
            }

            WebSecurity.CreateAccount(_usuario.NombreUsuario, _usuario.Password);

            return RedirectToAction("Index","Home");
        }

    }
}
