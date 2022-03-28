using System;
using System.Collections.Generic;

#nullable disable

namespace gestionPagos.Models
{
    public partial class Factura
    {
        public DateTime Fecha { get; set; }
        public int IdPedido { get; set; }
        public decimal Total { get; set; }
        public int Consecutivo { get; set; }

        public virtual Pedido IdPedidoNavigation { get; set; }
    }
}
