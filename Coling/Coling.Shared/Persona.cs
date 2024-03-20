﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Persona
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Foto { get; set; }
        public bool? Estado { get; set; }
        //public virtual ICollection<Telefono>? Telefono { get; set; }

    }
}
