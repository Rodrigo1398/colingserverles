using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface IEstudio
    {
        string Estudioo { get; set; }
        string Afiliado { get; set; }
        string Grado { get; set; }
        string Titulo { get; set; }
        string Institucion { get; set; }
        int Anio { get; set; }
        bool Estado { get; set; }
    }
}
