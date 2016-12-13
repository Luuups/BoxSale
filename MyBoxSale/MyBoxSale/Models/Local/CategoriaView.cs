using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBoxSale.Models.Local
{
    public class CategoriaView
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
    }
}