using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.interfaces
{
    public interface IPersona
    {
        public Task<bool> InsertarPersona(Persona persona);
        public Task<bool> ModificarPersona(Persona persona,int id);
        public Task<bool> EliminarPersona(int id);
        public Task<List<Persona>> ListarPersonas();
        public Task<Persona> ListarPersonaById(int id);

    }
}
