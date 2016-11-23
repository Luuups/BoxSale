using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBoxSale.Models.Local
{
    public class Notificacion
    {
        public double MontoVentasAyer { get; private set; }
        public PRODUCTO MasVendido { get;  private set; }
        public PRODUCTO MenosVendido { get;  private set; }
        public double GananciaMesAnterior { get; private set; }

        public Notificacion getNotificacion()
        {
            Notificacion result = new Notificacion();
            using (MyBoxSaleEntities db = new MyBoxSaleEntities())
            {
                var ConsultaVentas= (from total in db.VENTA
                                       where total.FechaVenta.Equals(DateTime.Now.AddDays(-1))
                                         select total.Total).ToList();

                this.MontoVentasAyer = ConsultaVentas.Sum(x => x.Value);

                var ProductosMasVendidos = (from producto in db.PRODUCTO.DefaultIfEmpty()
                                           join listaventa in db.DETALLEVENTA.DefaultIfEmpty()
                                           on producto.Id equals listaventa.ProductoId
                                           group producto by new { producto.Id,producto} into gpr
                                           select gpr.Max(x=>gpr.Count()));

                //var max =ProductosMasVendidos;
                //this.MenosVendido = ProductosMasVendidos.Min(x => x.Cantidad);
                                      

            }
            return result;
        }

    }
}