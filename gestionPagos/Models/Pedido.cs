using System;
using System.Collections.Generic;

#nullable disable

namespace gestionPagos.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            Asignacions = new HashSet<Asignacion>();
            Facturas = new HashSet<Factura>();
        }

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdProveedor { get; set; }
        public int IdEstado { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; }
        public virtual Contacto IdProveedorNavigation { get; set; }
        public virtual Contacto IdUsuarioNavigation { get; set; }
        public virtual ICollection<Asignacion> Asignacions { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
