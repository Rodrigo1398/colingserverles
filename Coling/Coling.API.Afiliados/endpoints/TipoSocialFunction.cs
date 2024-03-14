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
    public class TipoSocialFunction
    {
        private readonly ILogger<TipoSocialFunction> _logger;
        private readonly TipoSocialService _tipoSocial;

        public TipoSocialFunction(ILogger<TipoSocialFunction> logger, TipoSocialService tipoSocial)
        {
            _logger = logger;
            this._tipoSocial = tipoSocial;
        }

        [Function("ListarTiposSociales")]
        public async Task<HttpResponseData> ListarTiposSociales([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTiposSociales")] HttpRequestData req)
        {
            var listaTiposSociales = await _tipoSocial.ListarTiposSociales();
            if (listaTiposSociales == null) return req.CreateResponse(HttpStatusCode.BadRequest);
            var resp = req.CreateResponse(HttpStatusCode.OK);
            await resp.WriteAsJsonAsync(listaTiposSociales);
            return resp;
        }
        [Function("ListarTipoSocialById")]
        public async Task<HttpResponseData> ListarTipoSocialById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTipoSocialById")] HttpRequestData req)
        {
            var tiposo = await req.ReadFromJsonAsync<TipoSocial>() ?? throw new Exception("Debe ingresar un telefono");
            var tiposocial = await _tipoSocial.ListarTipoSocialById(tiposo.Id);
            if (tiposocial == null) return req.CreateResponse(HttpStatusCode.BadRequest);
            var resp = req.CreateResponse(HttpStatusCode.OK);
            await resp.WriteAsJsonAsync(tiposocial);
            return resp;
        }
        [Function("InsertarTipoSocial")]
        public async Task<HttpResponseData> InsertarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarTipoSocial")] HttpRequestData req)
        {
            try
            {
                var tipoSocial = await req.ReadFromJsonAsync<TipoSocial>() ?? throw new Exception("Debe ingresar un telefono");

                bool seGuardo = await _tipoSocial.InsertarTipoSocial(tipoSocial);

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
        [Function("EditarTipoSocial")]
        public async Task<HttpResponseData> EditarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "EditarTipoSocial")] HttpRequestData req)
        {
            try
            {
                var tipoSocial = await req.ReadFromJsonAsync<TipoSocial>() ?? throw new Exception("Debe ingresar un telefono");

                bool seGuardo = await _tipoSocial.ModificarTipoSocial(tipoSocial, tipoSocial.Id);

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
        [Function("EliminarTipoSocial")]
        public async Task<HttpResponseData> EliminarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "EliminarTipoSocial")] HttpRequestData req)
        {
            try
            {
                var tipoSocial = await req.ReadFromJsonAsync<TipoSocial>() ?? throw new Exception("Debe ingresar un telefono");

                bool seGuardo = await _tipoSocial.EliminarTipoSocial(tipoSocial.Id);

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
