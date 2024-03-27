using Coling.API.BolsaTrabajo.model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.BolsaTrabajo.interfaces
{
    public interface ISolicitud
    {
        public Task<bool> Create(Solicitud solicitud);
        public Task<bool> Update(Solicitud solicitud,string id);
        public Task<bool> Delete(string id);
        public Task<Solicitud> Get(string id);
        public Task<List<Solicitud>> GetAll();
    }
}
