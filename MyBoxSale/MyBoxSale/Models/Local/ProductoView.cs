using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBoxSale.Models.Local
{
    public class ProductoView
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double? PrecioCompra { get; set; }
        public double? PrecioMostrador { get; set; }
        public bool? Activo { get; set; }
        public CATEGORIA CATEGORIA { get; set; }
        public UNIDADMEDIDA UNIDADMEDIDA { get; set; }
        public double? StockMin { get; set; }
        public double? Existencia { get; set; }
        public PROVEEDOR PROVEEDOR { get; set; }
        public string Imagen { get; set; }
    }
}