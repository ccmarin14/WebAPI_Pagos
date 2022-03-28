using System;
using System.Collections.Generic;

#nullable disable

namespace gestionPagos.Models
{
    public partial class Envio
    {
        public int Id { get; set; }
        public string Guia { get; set; }
        public DateTime FechaEstimada { get; set; }
        public int IdPedido { get; set; }
        public DateTime? FechaEntrega { get; set; }

        public virtual Pedido IdPedidoNavigation { get; set; }
    }
}
