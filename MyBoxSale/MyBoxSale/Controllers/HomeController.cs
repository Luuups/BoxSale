using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBoxSale.Filters;
using MyBoxSale.Models;
using WebMatrix.WebData;
using MyBoxSale.Core;

namespace MyBoxSale.Controllers
{
    public class HomeController :BoxSaleController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Principal");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [InitializeSimpleMembership]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.NombreUsuario, model.Password, persistCookie: model.RememberMe))
            {
                using (Entities)
                {
                    var usuario = Entities.USUARIO.Single(x => x.NombreUsuario.Equals(model.NombreUsuario));
                    if (usuario.Activo.HasValue && usuario.Activo.Value)
                        return RedirectToLocal(returnUrl);
                    else
                    {
                        ModelState.AddModelError("", "El Usuario se encuentra desactivado.");
                        return View("Index", model);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "El nombre de usuario o contraseña es incorrecto.");
            return View("Index", model);
        }

        #region Auxiliares
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Principal");
            }
        }
        #endregion
    }
}
