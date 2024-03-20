using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.interfaces
{
    public interface IAfiliado
    {
        public Task<bool> InsertarAfiliado(Shared.Afiliados afiliado);
        public Task<bool> ModificarAfiliado(Shared.Afiliados afiliado, int id);
        public Task<bool> EliminarAfiliado(int id);
        public Task<List<Shared.Afiliados>> ListarAfiliados();
        public Task<Shared.Afiliados> ListarAfiliadoById(int id);
    }
}
