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
    public class PersonaTipoSocialService : IPersonaTipoSocial
    {
        private readonly Contexto contexto;

        public PersonaTipoSocialService(Contexto _contexto)
        {
            this.contexto = _contexto;
        }
        public async Task<bool> EliminarPersonaTipoSocial(int id)
        {
            var personatiposocial = await contexto.PersonaTipoSocial.FirstOrDefaultAsync(t => t.Id == id);
            if (personatiposocial == null) return false;
            contexto.PersonaTipoSocial.Remove(personatiposocial);
            var resp = await contexto.SaveChangesAsync();
            if (resp == 1) return true;
            return false;
        }

        public async Task<bool> InsertarPersonaTipoSocial(PersonaTipoSocial personatiposocial)
        {
            contexto.PersonaTipoSocial.Add(personatiposocial);
            int response = await contexto.SaveChangesAsync();
            if (response == 1) return true;
            return false;
        }

        public async Task<List<PersonaTipoSocial>> ListarPersonasTipoSociales()
        {
            var resp = await contexto.PersonaTipoSocial.ToListAsync();
            return resp;
        }

        public async Task<PersonaTipoSocial> ListarPersonaTipoSocialById(int id)
        {
            var personatiposocial = await contexto.PersonaTipoSocial.FirstOrDefaultAsync(t => t.Id == id);
            return personatiposocial;
        }

        public async Task<bool> ModificarPersonaTipoSocial(PersonaTipoSocial personatiposocial, int id)
        {
            PersonaTipoSocial personatiposo = await contexto.PersonaTipoSocial.FirstOrDefaultAsync(t => t.Id == id);
            if (personatiposo == null) return false;
            personatiposo.TipoSocialId = personatiposocial.TipoSocialId;
            personatiposo.Estado = personatiposocial.Estado;
            int resp = await contexto.SaveChangesAsync();
            if (resp == 1) return true;
            return false;
        }
    }
}
