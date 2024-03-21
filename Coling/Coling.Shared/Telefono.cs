﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Telefono
    {
        [Key]
        public int Id { get; set; }
        public string? NumeroTelefono { get; set; }
        public bool Estado { get; set; }

        public int PersonaId { get; set; }

        [ForeignKey("PersonaId")]
        public virtual Persona? Persona { get; set; }
    }
}
