using Coling.API.BolsaTrabajo.interfaces;
using Coling.API.BolsaTrabajo.model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.BolsaTrabajo.services
{
    public class SolicitudService : ISolicitud
    {
        public readonly MongoClient mongoClient;
        private readonly IMongoDatabase database;

        public SolicitudService(IConfiguration conf)
        {
            var configuration = conf.GetSection("cadenaMongo").Value ?? "";
            this.mongoClient = new MongoClient(configuration);
            database = mongoClient.GetDatabase("ColingDB");
        }
        public async Task<bool> Create<T>(string collectionName,T solicitud)
        {
            try
            {
                if (solicitud==null) return false;
                var soli = database.GetCollection<T>(collectionName);
                await soli.InsertOneAsync(solicitud);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Solicitud> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Solicitud>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(string collectionName, Solicitud solicitud)
        {
            var filter = Builders<Solicitud>.Filter.Eq("Id", solicitud.Id);
            var update = Builders<Solicitud>.Update
                .Set("PretencionSalarial", solicitud.PretencionSalarial)
                .Set("Acercade", solicitud.Acercade);

            var updateResult = await database.GetCollection<Solicitud>(collectionName).UpdateOneAsync(filter, update);
            return updateResult.ModifiedCount > 0;
        }
    }
}
