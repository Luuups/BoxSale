//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyBoxSale.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DETALLEVENTA
    {
        public int Id { get; set; }
        public Nullable<int> VentaId { get; set; }
        public Nullable<int> ProductoId { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<double> Subtotal { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        public virtual PRODUCTO PRODUCTO { get; set; }
        public virtual VENTA VENTA { get; set; }
    }
}