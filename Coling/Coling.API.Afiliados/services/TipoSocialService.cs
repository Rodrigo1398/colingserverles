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
    public class TipoSocialService : ITipoSocial
    {
        private readonly Contexto contexto;

        public TipoSocialService(Contexto _contexto)
        {
            this.contexto = _contexto;
        }
        public async Task<bool> EliminarTipoSocial(int id)
        {
            var tiposocial = await contexto.TipoSocial.FirstOrDefaultAsync(t => t.Id == id);
            if (tiposocial == null) return false;
            contexto.TipoSocial.Remove(tiposocial);
            var resp = await contexto.SaveChangesAsync();
            if (resp == 1) return true;
            return false;
        }

        public async Task<bool> InsertarTipoSocial(TipoSocial tiposocial)
        {
            contexto.TipoSocial.Add(tiposocial);
            int response = await contexto.SaveChangesAsync();
            if (response == 1) return true;
            return false;
        }

        public async Task<TipoSocial> ListarTipoSocialById(int id)
        {
            var tiposocial = await contexto.TipoSocial.FirstOrDefaultAsync(t => t.Id == id);
            return tiposocial;
        }

        public async Task<List<TipoSocial>> ListarTiposSociales()
        {
            var resp = await contexto.TipoSocial.ToListAsync();
            return resp;
        }

        public async Task<bool> ModificarTipoSocial(TipoSocial tiposocial, int id)
        {
            TipoSocial tiposo = await contexto.TipoSocial.FirstOrDefaultAsync(t => t.Id == id);
            if (tiposo == null) return false;
            tiposo.NombreSocial = tiposocial.NombreSocial;
            tiposo.Estado = tiposocial.Estado;
            int resp = await contexto.SaveChangesAsync();
            if (resp == 1) return true;
            return false;
        }
    }
}
