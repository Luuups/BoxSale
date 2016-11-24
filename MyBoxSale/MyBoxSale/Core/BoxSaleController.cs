using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBoxSale.Models;

namespace MyBoxSale.Core
{
    public class BoxSaleController:Controller
    {
        protected MyBoxSaleEntities Entities = new MyBoxSaleEntities();
    }
}