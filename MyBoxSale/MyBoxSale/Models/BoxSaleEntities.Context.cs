﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyBoxSaleEntities : DbContext
    {
        public MyBoxSaleEntities()
            : base("name=MyBoxSaleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CATEGORIA> CATEGORIA { get; set; }
        public DbSet<DETALLEVENTA> DETALLEVENTA { get; set; }
        public DbSet<EMPRESA> EMPRESA { get; set; }
        public DbSet<PAGO> PAGO { get; set; }
        public DbSet<PRODUCTO> PRODUCTO { get; set; }
        public DbSet<PROMOCION> PROMOCION { get; set; }
        public DbSet<PROVEEDOR> PROVEEDOR { get; set; }
        public DbSet<ROLES> ROLES { get; set; }
        public DbSet<TIPOPAGO> TIPOPAGO { get; set; }
        public DbSet<TIPOPROMOCION> TIPOPROMOCION { get; set; }
        public DbSet<UNIDADMEDIDA> UNIDADMEDIDA { get; set; }
        public DbSet<USUARIO> USUARIO { get; set; }
        public DbSet<USUARIOROLES> USUARIOROLES { get; set; }
        public DbSet<VENTA> VENTA { get; set; }
        public DbSet<VENTAPROMOCION> VENTAPROMOCION { get; set; }
        public DbSet<webpages_Membership> webpages_Membership { get; set; }
        public DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
    }
}
