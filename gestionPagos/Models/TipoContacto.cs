using System;
using System.Collections.Generic;

#nullable disable

namespace gestionPagos.Models
{
    public partial class TipoContacto
    {
        public TipoContacto()
        {
            Contactos = new HashSet<Contacto>();
        }

        public int Id { get; set; }
        public string Tipo { get; set; }

        public virtual ICollection<Contacto> Contactos { get; set; }
    }
}
