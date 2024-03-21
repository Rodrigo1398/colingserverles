using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface IAfiliadoo
    {
        string Afiliadoo { get; set; }
        string Profesion { get; set; }
        DateTime FechaAsignacion { get; set; }
        string NroSello { get; set; }
        bool Estado { get; set; }
    }
}
