using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBoxSale.Controllers
{
    public class NotificacionesController : Controller
    {
        //
        // GET: /Notificaciones/
        public JsonResult ObtenerNotificaciones()
        {
            return Json(new { Success=true }, JsonRequestBehavior.DenyGet);
        }

    }
}
