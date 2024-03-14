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
    public class DireccionServices : IDireccion
    {
        private readonly Contexto contexto;

        public DireccionServices(Contexto _contexto)
        {
            this.contexto = _contexto;
        }
        public async Task<bool> EliminarDireccion(int id)
        {
            var direccion = await contexto.Direccion.FirstOrDefaultAsync(t => t.Id == id);
            if (direccion == null) return false;
            contexto.Direccion.Remove(direccion);
            var resp = await contexto.SaveChangesAsync();
            if (resp == 1) return true;
            return false;
        }

        public async Task<bool> InsertarDireccion(Direccion direccion)
        {
            contexto.Direccion.Add(direccion);
            int response = await contexto.SaveChangesAsync();
            if (response == 1) return true;
            return false;
        }

        public async Task<Direccion> ListarDireccionById(int id)
        {
            var direccion = await contexto.Direccion.FirstOrDefaultAsync(t => t.Id == id);
            return direccion;
        }

        public async Task<List<Direccion>> ListarDirecciones()
        {
            var resp = await contexto.Direccion.ToListAsync();
            return resp;
        }

        public async Task<bool> ModificarDireccion(Direccion direccion, int id)
        {
            Direccion direc = await contexto.Direccion.FirstOrDefaultAsync(t => t.Id == id);
            if (direc == null) return false;
            direc.Descripcion = direccion.Descripcion;
            direc.PersonaId = direccion.PersonaId;
            direc.Estado = direccion.Estado;
            int resp = await contexto.SaveChangesAsync();
            if (resp == 1) return true;
            return false;
        }
    }
}
