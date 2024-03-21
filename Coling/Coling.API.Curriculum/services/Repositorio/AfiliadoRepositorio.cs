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
    public class AfiliadoRepositorio : IAfiliadoRepositorio
    {
        public readonly string cadenaConexion;
        public readonly string tablaNombre;
        public readonly IConfiguration configuration;
        public AfiliadoRepositorio(IConfiguration conf)
        {
            this.configuration = conf;
            this.cadenaConexion = configuration.GetSection("cadenaConexion").Value ?? "";
            this.tablaNombre = "Afiliado";
        }
        public async Task<bool> Create(Afiliado afiliado)
        {
            try
            {
                var tablaClient = new TableClient(cadenaConexion, tablaNombre);
                await tablaClient.UpsertEntityAsync(afiliado);
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

        public async Task<Afiliado> Get(string id)
        {
            try
            {
                var tablaClient = new TableClient(cadenaConexion, tablaNombre);
                var filtro = $"PartitionKey eq 'Afiliado' and RowKey eq '{id}'";
                await foreach (Afiliado afiliado in tablaClient.QueryAsync<Afiliado>(filter: filtro))
                {
                    return afiliado;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Afiliado>> GetAll()
        {
            List<Afiliado> afiliados = new List<Afiliado>();
            var tablaClient = new TableClient(cadenaConexion, tablaNombre);
            await foreach (Afiliado afiliado in tablaClient.QueryAsync<Afiliado>(filter: $"PartitionKey eq 'Afiliado'"))
            {
                afiliados.Add(afiliado);
            }
            return afiliados;
        }

        public async Task<bool> Update(Afiliado afiliado)
        {
            try
            {
                var tablaClient = new TableClient(cadenaConexion, tablaNombre);
                await tablaClient.UpdateEntityAsync(afiliado, afiliado.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
