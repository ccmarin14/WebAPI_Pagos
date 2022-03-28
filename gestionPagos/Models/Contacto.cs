using System;
using System.Collections.Generic;

#nullable disable

namespace gestionPagos.Models
{
    public partial class Contacto
    {
        public Contacto()
        {
            PedidoIdProveedorNavigations = new HashSet<Pedido>();
            PedidoIdUsuarioNavigations = new HashSet<Pedido>();
        }

        public int Documento { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public int Tipo { get; set; }

        public virtual TipoContacto TipoNavigation { get; set; }
        public virtual ICollection<Pedido> PedidoIdProveedorNavigations { get; set; }
        public virtual ICollection<Pedido> PedidoIdUsuarioNavigations { get; set; }
    }
}
