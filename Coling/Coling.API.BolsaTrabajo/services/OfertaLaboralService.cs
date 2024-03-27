using Coling.API.BolsaTrabajo.interfaces;
using Coling.API.BolsaTrabajo.model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.BolsaTrabajo.services
{
    public class OfertaLaboralService:IOfertaLaboral
    {
        public readonly MongoClient mongoClient;
        private readonly IMongoDatabase database;
        private readonly string collectionName;

        public OfertaLaboralService(IConfiguration conf)
        {
            var configuration = conf.GetSection("cadenaMongo").Value ?? "";
            this.mongoClient = new MongoClient(configuration);
            database = mongoClient.GetDatabase("ColingDB");
            collectionName = "OfertaLaboral";
        }
        public async Task<bool> Create(OfertaLaboral ofertaLaboral)
        {
            try
            {
                if (ofertaLaboral == null) return false;
                var oferta = database.GetCollection<OfertaLaboral>(collectionName);
                await oferta.InsertOneAsync(ofertaLaboral);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                ObjectId _id;
                if (!ObjectId.TryParse(id, out _id))
                {
                    throw new ArgumentException("El ID proporcionado no es válido.");
                }

                var filter = Builders<OfertaLaboral>.Filter.Eq("_id", _id);
                var oferta = database.GetCollection<OfertaLaboral>(collectionName);
                var deleteResult = await oferta.DeleteOneAsync(filter);

                return deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar la solicitud: {ex.Message}");
                throw;
            }
        }


        public async Task<OfertaLaboral> Get(string id)
        {
            try
            {
                ObjectId _id;
                if (!ObjectId.TryParse(id, out _id))
                {
                    throw new ArgumentException("El ID proporcionado no es válido.");
                }

                var filter = Builders<OfertaLaboral>.Filter.Eq("_id", _id);
                var oferta = database.GetCollection<OfertaLaboral>(collectionName);
                OfertaLaboral ofertaLaboral = await oferta.Find(filter).FirstOrDefaultAsync();

                return ofertaLaboral;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la solicitud: {ex.Message}");
                throw;
            }
        }


        public async Task<List<OfertaLaboral>> GetAll()
        {
            try
            {
                var oferta = database.GetCollection<OfertaLaboral>(collectionName);
                List<OfertaLaboral> ofertas = await oferta.Find(Builders<OfertaLaboral>.Filter.Empty).ToListAsync();
                return ofertas;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(OfertaLaboral ofertaLaboral, string id)
        {
            try
            {
                ObjectId _id;
                ObjectId.TryParse(id, out _id);
                var filter = Builders<OfertaLaboral>.Filter.Eq("_id", _id);
                var update = Builders<OfertaLaboral>.Update
                    .Set("Area", ofertaLaboral.Area)
                    .Set("TipoContrato", ofertaLaboral.TipoContrato);

                var updateResult = await database.GetCollection<OfertaLaboral>(collectionName).UpdateOneAsync(filter, update);
                return updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar: {ex.Message}");
                throw;
            }
        }
    }
}
