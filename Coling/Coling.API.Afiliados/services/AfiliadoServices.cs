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
    public class AfiliadoServices : IAfiliado
    {
        private readonly Contexto contexto;

        public AfiliadoServices(Contexto _contexto)
        {
            this.contexto = _contexto;
        }
        public async Task<bool> EliminarAfiliado(int id)
        {
            var afiliado = await contexto.Afiliado.FirstOrDefaultAsync(t => t.Id == id);
            if (afiliado == null) return false;
            contexto.Afiliado.Remove(afiliado);
            var resp = await contexto.SaveChangesAsync();
            if (resp == 1) return true;
            return false;
        }

        public async Task<bool> InsertarAfiliado(Afiliado afiliado)
        {
            contexto.Afiliado.Add(afiliado);
            int response = await contexto.SaveChangesAsync();
            if (response == 1) return true;
            return false;
        }

        public async Task<Afiliado> ListarAfiliadoById(int id)
        {
            var afiliado = await contexto.Afiliado.FirstOrDefaultAsync(t => t.Id == id);
            return afiliado;
        }

        public async Task<List<Afiliado>> ListarAfiliados()
        {
            var resp = await contexto.Afiliado.ToListAsync();
            return resp;
        }

        public async Task<bool> ModificarAfiliado(Afiliado afiliado, int id)
        {
            Afiliado afi = await contexto.Afiliado.FirstOrDefaultAsync(t => t.Id == id);
            if (afi == null) return false;
            afi.NroTituloProvicional = afiliado.NroTituloProvicional;
            afi.FechaAfiliacion = afiliado.FechaAfiliacion;
            afi.Estado = afiliado.Estado;
            int resp = await contexto.SaveChangesAsync();
            if (resp == 1) return true;
            return false;
        }
    }
}
