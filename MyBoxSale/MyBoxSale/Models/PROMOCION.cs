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
    
    public partial class PROMOCION
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> TipoId { get; set; }
        public Nullable<int> ProductoId { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        public virtual PRODUCTO PRODUCTO { get; set; }
        public virtual TIPOPROMOCION TIPOPROMOCION { get; set; }
    }
}