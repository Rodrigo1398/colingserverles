using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.interfaces
{
    public interface IPersonaTipoSocial
    {
        public Task<bool> InsertarPersonaTipoSocial(PersonaTipoSocial personatiposocial);
        public Task<bool> ModificarPersonaTipoSocial(PersonaTipoSocial personatiposocial, int id);
        public Task<bool> EliminarPersonaTipoSocial(int id);
        public Task<List<PersonaTipoSocial>> ListarPersonasTipoSociales();
        public Task<PersonaTipoSocial> ListarPersonaTipoSocialById(int id);
    }
}
