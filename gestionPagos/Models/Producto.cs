using System;
using System.Collections.Generic;

#nullable disable

namespace gestionPagos.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Asignacions = new HashSet<Asignacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public decimal Valor { get; set; }

        public virtual ICollection<Asignacion> Asignacions { get; set; }
    }
}
