﻿using Coling.API.BolsaTrabajo.interfaces;
using Coling.API.BolsaTrabajo.model;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coling.API.BolsaTrabajo.services
{
    public class SolicitudService : ISolicitud
    {
        public readonly MongoClient mongoClient;
        private readonly IMongoDatabase database;
        private readonly string collectionName;

        public SolicitudService(IConfiguration conf)
        {
            var configuration = conf.GetSection("cadenaMongo").Value ?? "";
            this.mongoClient = new MongoClient(configuration);
            database = mongoClient.GetDatabase("ColingDB");
            collectionName = "Solicitud";
        }
        public async Task<bool> Create(Solicitud solicitud)
        {
            try
            {
                if (solicitud==null) return false;
                var soli = database.GetCollection<Solicitud>(collectionName);
                await soli.InsertOneAsync(solicitud);
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

                var filter = Builders<Solicitud>.Filter.Eq("_id", _id);
                var solicitudCollection = database.GetCollection<Solicitud>(collectionName);
                var deleteResult = await solicitudCollection.DeleteOneAsync(filter);

                return deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar la solicitud: {ex.Message}");
                throw;
            }
        }


        public async Task<Solicitud> Get(string id)
        {
            try
            {
                ObjectId _id;
                if (!ObjectId.TryParse(id, out _id))
                {
                    throw new ArgumentException("El ID proporcionado no es válido.");
                }

                var filter = Builders<Solicitud>.Filter.Eq("_id", _id);
                var solicitudCollection = database.GetCollection<Solicitud>(collectionName);
                var solicitud = await solicitudCollection.Find(filter).FirstOrDefaultAsync();

                return solicitud;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la solicitud: {ex.Message}");
                throw;
            }
        }


        public async Task<List<Solicitud>> GetAll()
        {
            try
            {
                var solicitud = database.GetCollection<Solicitud>(collectionName);
                List<Solicitud> solicitudes = await solicitud.Find(Builders<Solicitud>.Filter.Empty).ToListAsync();
                return solicitudes;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(Solicitud solicitud,string id)
        {
            try
            {
                ObjectId _id;
                ObjectId.TryParse(id, out _id);
                var filter = Builders<Solicitud>.Filter.Eq("_id", _id);
                var update = Builders<Solicitud>.Update
                    .Set("PretencionSalarial", solicitud.PretencionSalarial)
                    .Set("Acercade", solicitud.Acercade);

                var updateResult = await database.GetCollection<Solicitud>(collectionName).UpdateOneAsync(filter, update);
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
