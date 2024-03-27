using Coling.API.BolsaTrabajo.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.BolsaTrabajo.interfaces
{
    public interface IOfertaLaboral
    {
        public Task<bool> Create(OfertaLaboral ofertaLaboral);
        public Task<bool> Update(OfertaLaboral ofertaLaboral, string id);
        public Task<bool> Delete(string id);
        public Task<OfertaLaboral> Get(string id);
        public Task<List<OfertaLaboral>> GetAll();
    }
}
