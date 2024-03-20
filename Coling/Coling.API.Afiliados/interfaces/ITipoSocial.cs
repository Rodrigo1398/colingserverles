using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.interfaces
{
    public interface ITipoSocial
    {
        public Task<bool> InsertarTipoSocial(TipoSocial tiposocial);
        public Task<bool> ModificarTipoSocial(TipoSocial tiposocial, int id);
        public Task<bool> EliminarTipoSocial(int id);
        public Task<List<TipoSocial>> ListarTiposSociales();
        public Task<TipoSocial> ListarTipoSocialById(int id);
    }
}
