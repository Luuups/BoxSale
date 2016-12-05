//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyBoxSale.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VENTA
    {
        public VENTA()
        {
            this.DETALLEVENTA = new HashSet<DETALLEVENTA>();
            this.PAGO = new HashSet<PAGO>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> FechaVenta { get; set; }
        public Nullable<double> SubTotal { get; set; }
        public Nullable<double> PorcentajeInteres { get; set; }
        public Nullable<double> Interes { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<double> Cambio { get; set; }
        public Nullable<int> UsuarioId { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        public virtual ICollection<DETALLEVENTA> DETALLEVENTA { get; set; }
        public virtual ICollection<PAGO> PAGO { get; set; }
        public virtual USUARIO USUARIO { get; set; }
    }
}
