using Coling.API.Afiliados.services;
using Coling.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Afiliados.endpoints
{
    public class DireccionFunction
    {
        private readonly ILogger<DireccionFunction> _logger;
        private readonly DireccionServices _direccion;

        public DireccionFunction(ILogger<DireccionFunction> logger,DireccionServices direccion)
        {
            _logger = logger;
            this._direccion = direccion;
        }

        [Function("ListarDirecciones")]
        public async Task<HttpResponseData> ListarDirecciones([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarDirecciones")] HttpRequestData req)
        {
            var listaTelefonos = await _direccion.ListarDirecciones();
            if (listaTelefonos == null) return req.CreateResponse(HttpStatusCode.BadRequest);
            var resp = req.CreateResponse(HttpStatusCode.OK);
            await resp.WriteAsJsonAsync(listaTelefonos);
            return resp;
        }
        [Function("ListarDireccionById")]
        public async Task<HttpResponseData> ListarDireccionById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarDireccionById")] HttpRequestData req)
        {
            var direc = await req.ReadFromJsonAsync<Direccion>() ?? throw new Exception("Debe ingresar un telefono");
            var direccion = await _direccion.ListarDireccionById(direc.Id);
            if (direccion == null) return req.CreateResponse(HttpStatusCode.BadRequest);
            var resp = req.CreateResponse(HttpStatusCode.OK);
            await resp.WriteAsJsonAsync(direccion);
            return resp;
        }
        [Function("InsertarDireccion")]
        public async Task<HttpResponseData> InsertarDireccion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarDireccion")] HttpRequestData req)
        {
            try
            {
                var direccion = await req.ReadFromJsonAsync<Direccion>() ?? throw new Exception("Debe ingresar un telefono");

                bool seGuardo = await _direccion.InsertarDireccion(direccion);

                if (!seGuardo) return req.CreateResponse(HttpStatusCode.BadRequest);

                var respuesta = req.CreateResponse(HttpStatusCode.OK);

                return respuesta;
            }
            catch (Exception ex)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(ex.Message);
                return error;
            }
        }
        [Function("EditarDireccion")]
        public async Task<HttpResponseData> EditarDireccion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "EditarDireccion")] HttpRequestData req)
        {
            try
            {
                var direccion = await req.ReadFromJsonAsync<Direccion>() ?? throw new Exception("Debe ingresar una direccion");

                bool seGuardo = await _direccion.ModificarDireccion(direccion, direccion.Id);

                if (!seGuardo) return req.CreateResponse(HttpStatusCode.BadRequest);

                var respuesta = req.CreateResponse(HttpStatusCode.OK);

                return respuesta;
            }
            catch (Exception ex)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(ex.Message);
                return error;
            }
        }
        [Function("EliminarDireccion")]
        public async Task<HttpResponseData> EliminarDireccion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "EliminarDireccion")] HttpRequestData req)
        {
            try
            {
                var direccion = await req.ReadFromJsonAsync<Direccion>() ?? throw new Exception("Debe ingresar una direccion");

                bool seGuardo = await _direccion.EliminarDireccion(direccion.Id);

                if (!seGuardo) return req.CreateResponse(HttpStatusCode.BadRequest);

                var respuesta = req.CreateResponse(HttpStatusCode.OK);

                return respuesta;
            }
            catch (Exception ex)
            {
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                await error.WriteAsJsonAsync(ex.Message);
                return error;
            }
        }
    }
}
