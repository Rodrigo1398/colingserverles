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
    public class TelefonoServices : ITelefono
    {
        private readonly Contexto contexto;

        public TelefonoServices(Contexto _contexto)
        {
            this.contexto = _contexto;
        }
        public async Task<bool> EliminarTelefono(int id)
        {
            var tel = await contexto.Telefono.FirstOrDefaultAsync(t => t.Id == id);
            if (tel == null) return false;
            contexto.Telefono.Remove(tel);
            var resp = await contexto.SaveChangesAsync();
            if (resp == 1) return true;
            return false;

        }

        public async Task<bool> InsertarTelefono(Telefono telefono)
        {
            contexto.Telefono.Add(telefono);
            int response = await contexto.SaveChangesAsync();
            if (response == 1) return true;
            return false;
        }

        public async Task<List<Telefono>> ListarTelefonos()
        {
            var resp = await contexto.Telefono.ToListAsync();
            return resp;
        }

        public async Task<Telefono> ListarTelefonoById(int id)
        {
            var telefono = await contexto.Telefono.FirstOrDefaultAsync(t => t.Id == id);
            return telefono;
        }

        public async Task<bool> ModificarTelefono(Telefono telefono, int id)
        {
            Telefono tel = await contexto.Telefono.FirstOrDefaultAsync(t => t.Id==id);
            if(tel==null) return false;
            tel.NumeroTelefono = telefono.NumeroTelefono;
            tel.PersonaId = telefono.PersonaId;
            tel.Estado = telefono.Estado;
            int resp = await contexto.SaveChangesAsync();
            if(resp==1) return true;
            return false;
        }
    }
}
