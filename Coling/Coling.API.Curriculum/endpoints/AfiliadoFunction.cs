using Coling.API.Curriculum.interfaces.Repositorio;
using Coling.API.Curriculum.Model;
using Coling.API.Curriculum.services.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Curriculum.endpoints
{
    public class AfiliadoFunction
    {
        private readonly ILogger<AfiliadoFunction> _logger;
        private readonly AfiliadoRepositorio afiliadoRepositorio;

        public AfiliadoFunction(ILogger<AfiliadoFunction> logger, AfiliadoRepositorio _afiliadoRepositorio)
        {
            _logger = logger;
            afiliadoRepositorio = _afiliadoRepositorio;
        }

        [Function("InsertarAfiliado")]
        public async Task<HttpResponseData> InsertarAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarAfiliado")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var Afiliado = await req.ReadFromJsonAsync<Afiliado>() ?? throw new Exception("Debe ingresar una Afiliado");
                Afiliado.RowKey = Guid.NewGuid().ToString();
                Afiliado.Timestamp = DateTime.UtcNow;
                bool seGuardo = await afiliadoRepositorio.Create(Afiliado);

                if (!seGuardo) return req.CreateResponse(HttpStatusCode.BadRequest);

                resp = req.CreateResponse(HttpStatusCode.OK);

                return resp;
            }
            catch (Exception)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }
        [Function("GetAllAfiliado")]
        public async Task<HttpResponseData> GetAllAfiliado([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var afiliado = await afiliadoRepositorio.GetAll();

                resp = req.CreateResponse(HttpStatusCode.OK);

                await resp.WriteAsJsonAsync(afiliado);

                return resp;
            }
            catch (Exception)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }

        [Function("GetAfiliado")]
        public async Task<HttpResponseData> GetAfiliado([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var id = req.Query["id"];

                if (id == null) return req.CreateResponse(HttpStatusCode.BadRequest);

                var afiliado = await afiliadoRepositorio.Get(id);

                resp = req.CreateResponse(HttpStatusCode.OK);

                await resp.WriteAsJsonAsync(afiliado);

                return resp;
            }
            catch (Exception)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }
        [Function("UpdateAfiliado")]
        public async Task<HttpResponseData> UpdateAfiliado([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var afiliado = await req.ReadFromJsonAsync<Afiliado>() ?? throw new Exception("Debe ingresar una Afiliado");
                afiliado.RowKey = Guid.NewGuid().ToString();
                afiliado.Timestamp = DateTime.UtcNow;
                bool seGuardo = await afiliadoRepositorio.Update(afiliado);

                if (!seGuardo) return req.CreateResponse(HttpStatusCode.BadRequest);

                resp = req.CreateResponse(HttpStatusCode.OK);

                return resp;
            }
            catch (Exception)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }
        [Function("DeleteAfiliado")]
        public async Task<HttpResponseData> DeleteAfiliado([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var partitionkey = req.Query["partitionkey"];
                var rowkey = req.Query["rowkey"];

                if (partitionkey == null || rowkey == null) return req.CreateResponse(HttpStatusCode.BadRequest);

                var afiliado = await afiliadoRepositorio.Delete(partitionkey, rowkey);

                resp = req.CreateResponse(HttpStatusCode.OK);

                await resp.WriteAsJsonAsync(afiliado);

                return resp;
            }
            catch (Exception)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }
    }
}
