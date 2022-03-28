using System;
using System.Collections.Generic;

#nullable disable

namespace gestionPagos.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
