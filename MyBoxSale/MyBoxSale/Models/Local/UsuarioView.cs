using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBoxSale.Models.Local
{
    public class UsuarioView
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public EMPRESA Empresa { get; set; }
        public bool? Activo { get; set; }
    }
}