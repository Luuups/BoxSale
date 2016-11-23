using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBoxSale.Models;

namespace MyBoxSale.Controllers
{
    [Authorize(Roles="Admin,Cajero")]
    public class VentasController : Controller
    {
        //
        // GET: /Ventas/

        public ActionResult Index()
        {
            return View();
        }

    }
}
