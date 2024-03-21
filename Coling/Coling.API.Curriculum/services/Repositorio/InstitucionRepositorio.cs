using Azure;
using Azure.Data.Tables;
using Coling.API.Curriculum.interfaces.Repositorio;
using Coling.API.Curriculum.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.services.Repositorio
{
    public class InstitucionRepositorio : IInstitucionRepositorio
    {
        public readonly string cadenaConexion;
        public readonly string tablaNombre;
        public readonly IConfiguration configuration;
        public InstitucionRepositorio(IConfiguration conf)
        {
            this.configuration = conf;
            this.cadenaConexion = configuration.GetSection("cadenaConexion").Value ?? "";
            this.tablaNombre = "Institucion";
        }
        public async Task<bool> Create(Institucion institucion)
        {
            try
            {
                var tablaClient = new TableClient(cadenaConexion,tablaNombre);
                await tablaClient.UpsertEntityAsync(institucion);
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

        public async Task<Institucion> Get(string id)
        {
            try
            {
                var tablaClient = new TableClient(cadenaConexion, tablaNombre);
                var filtro = $"PartitionKey eq 'Educacion' and RowKey eq '{id}'";
                await foreach (Institucion institucion in tablaClient.QueryAsync<Institucion>(filter: filtro))
                {
                    return institucion;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Institucion>> GetAll()
        {
            List<Institucion> instituciones = new List<Institucion>();
            var tablaClient = new TableClient(cadenaConexion, tablaNombre);
            await foreach (Institucion institucion in tablaClient.QueryAsync<Institucion>(filter: $"PartitionKey eq 'Educacion'"))
            {
                instituciones.Add(institucion);
            }
            return instituciones;
        }

        public async Task<bool> Update(Institucion institucion)
        {
            try
            {
                var tablaClient = new TableClient(cadenaConexion, tablaNombre);
                await tablaClient.UpdateEntityAsync(institucion, institucion.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
