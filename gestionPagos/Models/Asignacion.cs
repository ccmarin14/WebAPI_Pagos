using System;
using System.Collections.Generic;

#nullable disable

namespace gestionPagos.Models
{
    public partial class Asignacion
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int IdPedido { get; set; }
        public decimal Costo { get; set; }

        public virtual Pedido IdPedidoNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
