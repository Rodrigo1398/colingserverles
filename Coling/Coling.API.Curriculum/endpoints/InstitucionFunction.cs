using Coling.API.Curriculum.Model;
using Coling.API.Curriculum.services.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Net;

namespace Coling.API.Curriculum.endpoints
{
    public class InstitucionFunction
    {
        private readonly ILogger<InstitucionFunction> _logger;
        private readonly InstitucionRepositorio institucionRepositorio;

        public InstitucionFunction(ILogger<InstitucionFunction> logger, InstitucionRepositorio _institucionRepositorio)
        {
            _logger = logger;
            institucionRepositorio = _institucionRepositorio;
        }

        [Function("InsertarInstitucion")]
        public async Task<HttpResponseData> InsertarInstitucion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarInstitucion")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var insitucion = await req.ReadFromJsonAsync<Institucion>() ?? throw new Exception("Debe ingresar una institucion");
                insitucion.RowKey = Guid.NewGuid().ToString();
                insitucion.Timestamp = DateTime.UtcNow;
                bool seGuardo = await institucionRepositorio.Create(insitucion);

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
        [Function("GetAll")]
        public async Task<HttpResponseData> GetAll([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var instituciones = await institucionRepositorio.GetAll();

                resp = req.CreateResponse(HttpStatusCode.OK);

                await resp.WriteAsJsonAsync(instituciones);

                return resp;
            }
            catch (Exception)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }

        [Function("Get")]
        public async Task<HttpResponseData> Get([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var id = req.Query["id"];
                
                if(id==null) return req.CreateResponse(HttpStatusCode.BadRequest);

                var instituciones = await institucionRepositorio.Get(id);

                resp = req.CreateResponse(HttpStatusCode.OK);

                await resp.WriteAsJsonAsync(instituciones);

                return resp;
            }
            catch (Exception)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }
        [Function("UpdateInstitucion")]
        public async Task<HttpResponseData> UpdateInstitucion([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var insitucion = await req.ReadFromJsonAsync<Institucion>() ?? throw new Exception("Debe ingresar una institucion");
                insitucion.RowKey = Guid.NewGuid().ToString();
                insitucion.Timestamp = DateTime.UtcNow;
                bool seGuardo = await institucionRepositorio.Update(insitucion);

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
        [Function("Delete")]
        public async Task<HttpResponseData> Delete([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var partitionkey = req.Query["partitionkey"];  
                var rowkey = req.Query["rowkey"];

                if (partitionkey == null|| rowkey == null) return req.CreateResponse(HttpStatusCode.BadRequest);

                var instituciones = await institucionRepositorio.Delete(partitionkey,rowkey);

                resp = req.CreateResponse(HttpStatusCode.OK);

                await resp.WriteAsJsonAsync(instituciones);

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
