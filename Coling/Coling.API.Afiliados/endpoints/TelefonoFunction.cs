using Coling.API.Afiliados.services;
using Coling.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Afiliados.endpoints
{
    public class TelefonoFunction
    {
        private readonly ILogger<TelefonoFunction> _logger;
        private readonly TelefonoServices _telefono;

        public TelefonoFunction(ILogger<TelefonoFunction> logger,TelefonoServices telefono)
        {
            _logger = logger;
            this._telefono = telefono;
        }

        [Function("ListarTelefonos")]
        public async Task<HttpResponseData> ListarTelefonos([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarTelefonos")] HttpRequestData req)
        {
            var listaTelefonos = await _telefono.ListarTelefonos();
            if(listaTelefonos==null) return req.CreateResponse(HttpStatusCode.BadRequest);
            var resp = req.CreateResponse(HttpStatusCode.OK);
            await resp.WriteAsJsonAsync(listaTelefonos);
            return resp;
        }
        [Function("ListarTelefonoById")]
        public async Task<HttpResponseData> ListarTelefonoById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTelefonoById")] HttpRequestData req)
        {
            var tel = await req.ReadFromJsonAsync<Telefono>() ?? throw new Exception("Debe ingresar un telefono");
            var telefono = await _telefono.ListarTelefonoById(tel.Id);
            if (telefono == null) return req.CreateResponse(HttpStatusCode.BadRequest);
            var resp = req.CreateResponse(HttpStatusCode.OK);
            await resp.WriteAsJsonAsync(telefono);
            return resp;
        }
        [Function("InsertarTelefono")]
        public async Task<HttpResponseData> InsertarTelefono([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarTelefono")] HttpRequestData req)
        {
            try
            {
                var telefono = await req.ReadFromJsonAsync<Telefono>() ?? throw new Exception("Debe ingresar un telefono");

                bool seGuardo = await _telefono.InsertarTelefono(telefono);

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
        [Function("EditarTelefono")]
        public async Task<HttpResponseData> EditarTelefono([HttpTrigger(AuthorizationLevel.Function, "post", Route = "EditarTelefono")] HttpRequestData req)
        {
            try
            {
                var telefono = await req.ReadFromJsonAsync<Telefono>() ?? throw new Exception("Debe ingresar un telefono");

                bool seGuardo = await _telefono.ModificarTelefono(telefono,telefono.Id);

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
        [Function("EliminarTelefono")]
        public async Task<HttpResponseData> EliminarTelefono([HttpTrigger(AuthorizationLevel.Function, "post", Route = "EliminarTelefono")] HttpRequestData req)
        {
            try
            {
                var telefono = await req.ReadFromJsonAsync<Telefono>() ?? throw new Exception("Debe ingresar un telefono");

                bool seGuardo = await _telefono.EliminarTelefono(telefono.Id);

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
