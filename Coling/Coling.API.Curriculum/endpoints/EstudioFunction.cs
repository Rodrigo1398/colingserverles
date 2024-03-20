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
    public class EstudioFunction
    {
        private readonly ILogger<EstudioFunction> _logger;
        private readonly EstudioRepositorio estudioRepositorio;

        public EstudioFunction(ILogger<EstudioFunction> logger, EstudioRepositorio _estudioRepositorio)
        {
            _logger = logger;
            estudioRepositorio = _estudioRepositorio;
        }

        [Function("InsertarEstudio")]
        public async Task<HttpResponseData> InsertarEstudio([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarEstudio")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var estudio = await req.ReadFromJsonAsync<Estudio>() ?? throw new Exception("Debe ingresar una Estudio");
                estudio.RowKey = Guid.NewGuid().ToString();
                estudio.Timestamp = DateTime.UtcNow;
                bool seGuardo = await estudioRepositorio.Create(estudio);

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
        [Function("GetAllEstudio")]
        public async Task<HttpResponseData> GetAllEstudio([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var estudios = await estudioRepositorio.GetAll();

                resp = req.CreateResponse(HttpStatusCode.OK);

                await resp.WriteAsJsonAsync(estudios);

                return resp;
            }
            catch (Exception)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }

        [Function("GetEstudio")]
        public async Task<HttpResponseData> GetEstudio([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var id = req.Query["id"];

                if (id == null) return req.CreateResponse(HttpStatusCode.BadRequest);

                var estudios = await estudioRepositorio.Get(id);

                resp = req.CreateResponse(HttpStatusCode.OK);

                await resp.WriteAsJsonAsync(estudios);

                return resp;
            }
            catch (Exception)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }
        [Function("UpdateEstudio")]
        public async Task<HttpResponseData> UpdateEstudio([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var estudio = await req.ReadFromJsonAsync<Estudio>() ?? throw new Exception("Debe ingresar una Estudio");
                estudio.RowKey = Guid.NewGuid().ToString();
                estudio.Timestamp = DateTime.UtcNow;
                bool seGuardo = await estudioRepositorio.Update(estudio);

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
        [Function("DeleteEstudio")]
        public async Task<HttpResponseData> DeleteEstudio([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var partitionkey = req.Query["partitionkey"];
                var rowkey = req.Query["rowkey"];

                if (partitionkey == null || rowkey == null) return req.CreateResponse(HttpStatusCode.BadRequest);

                var estudio = await estudioRepositorio.Delete(partitionkey, rowkey);

                resp = req.CreateResponse(HttpStatusCode.OK);

                await resp.WriteAsJsonAsync(estudio);

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
