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
        public Task<bool> Create<T>(string collectionName,T document);
        public Task<bool> Update(string collectionName, Solicitud solicitud);
        public Task<bool> Delete(string id);
        public Task<Solicitud> Get(string id);
        public Task<List<Solicitud>> GetAll();
    }
}
