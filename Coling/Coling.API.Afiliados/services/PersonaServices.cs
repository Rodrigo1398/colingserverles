using Coling.API.Afiliados.interfaces;
using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.services
{
    public class PersonaServices : IPersona
    {
        private readonly Contexto contexto;

        public PersonaServices(Contexto _contexto) {
            this.contexto = _contexto;
        }
        public Task<bool> EliminarPersona(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertarPersona(Persona persona)
        {
            contexto.Persona.Add(persona);
            int response = await contexto.SaveChangesAsync();
            if (response == 1) return true;
            return false;
        }

        public Task<Persona> ListarPersonaById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Persona>> ListarPersonas()
        {
            var listaPersonas = await contexto.Persona.ToListAsync();
            return listaPersonas;
        }

        public Task<bool> ModificarPersona(Persona persona, int id)
        {
            throw new NotImplementedException();
        }
    }
}
