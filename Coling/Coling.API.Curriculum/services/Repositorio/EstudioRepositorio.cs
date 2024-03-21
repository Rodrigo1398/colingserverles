using Azure.Data.Tables;
using Coling.API.Curriculum.interfaces.Repositorio;
using Coling.API.Curriculum.Model;
using Coling.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.services.Repositorio
{
    public class EstudioRepositorio : IEstudioRepositorio
    {
        public readonly string cadenaConexion;
        public readonly string tablaNombre;
        public readonly IConfiguration configuration;
        public EstudioRepositorio(IConfiguration conf)
        {
            this.configuration = conf;
            this.cadenaConexion = configuration.GetSection("cadenaConexion").Value ?? "";
            this.tablaNombre = "Estudio";
        }
        public async Task<bool> Create(Estudio estudio)
        {
            try
            {
                var tablaClient = new TableClient(cadenaConexion, tablaNombre);
                await tablaClient.UpsertEntityAsync(estudio);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(string partitionkey, string rowkey)
        {
            try
            {
                var tablaClient = new TableClient(cadenaConexion, tablaNombre);
                await tablaClient.DeleteEntityAsync(partitionkey, rowkey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Estudio> Get(string id)
        {
            try
            {
                var tablaClient = new TableClient(cadenaConexion, tablaNombre);
                var filtro = $"PartitionKey eq 'Estudio' and RowKey eq '{id}'";
                await foreach (Estudio estudio in tablaClient.QueryAsync<Estudio>(filter: filtro))
                {
                    return estudio;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Estudio>> GetAll()
        {
            List<Estudio> estudios = new List<Estudio>();
            var tablaClient = new TableClient(cadenaConexion, tablaNombre);
            await foreach (Estudio estudio in tablaClient.QueryAsync<Estudio>(filter: $"PartitionKey eq 'Estudio'"))
            {
                estudios.Add(estudio);
            }
            return estudios;
        }

        public async Task<bool> Update(Estudio estudio)
        {
            try
            {
                var tablaClient = new TableClient(cadenaConexion, tablaNombre);
                await tablaClient.UpdateEntityAsync(estudio, estudio.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
