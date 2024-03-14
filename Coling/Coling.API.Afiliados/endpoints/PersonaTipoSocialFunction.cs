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
    public class PersonaTipoSocialFunction
    {
        private readonly ILogger<PersonaTipoSocialFunction> _logger;
        private readonly PersonaTipoSocialService _personatipoSocial;

        public PersonaTipoSocialFunction(ILogger<PersonaTipoSocialFunction> logger, PersonaTipoSocialService personatipoSocial)
        {
            _logger = logger;
            this._personatipoSocial = personatipoSocial;
        }

        [Function("ListarPersonasTiposSociales")]
        public async Task<HttpResponseData> ListarPersonasTiposSociales([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPersonasTiposSociales")] HttpRequestData req)
        {
            var listaPersonaTiposSociales = await _personatipoSocial.ListarPersonasTipoSociales();
            if (listaPersonaTiposSociales == null) return req.CreateResponse(HttpStatusCode.BadRequest);
            var resp = req.CreateResponse(HttpStatusCode.OK);
            await resp.WriteAsJsonAsync(listaPersonaTiposSociales);
            return resp;
        }
        [Function("ListarPersonaTipoSocialById")]
        public async Task<HttpResponseData> ListarPersonaTipoSocialById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPersonaTipoSocialById")] HttpRequestData req)
        {
            var tiposo = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar un telefono");
            var tiposocial = await _personatipoSocial.ListarPersonaTipoSocialById(tiposo.Id);
            if (tiposocial == null) return req.CreateResponse(HttpStatusCode.BadRequest);
            var resp = req.CreateResponse(HttpStatusCode.OK);
            await resp.WriteAsJsonAsync(tiposocial);
            return resp;
        }
        [Function("InsertarPersonaTipoSocial")]
        public async Task<HttpResponseData> InsertarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarPersonaTipoSocial")] HttpRequestData req)
        {
            try
            {
                var tipoSocial = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar un telefono");

                bool seGuardo = await _personatipoSocial.InsertarPersonaTipoSocial(tipoSocial);

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
        [Function("EditarPersonaTipoSocial")]
        public async Task<HttpResponseData> EditarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "EditarPersonaTipoSocial")] HttpRequestData req)
        {
            try
            {
                var tipoSocial = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar un telefono");

                bool seGuardo = await _personatipoSocial.ModificarPersonaTipoSocial(tipoSocial, tipoSocial.Id);

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
        [Function("EliminarPersonaTipoSocial")]
        public async Task<HttpResponseData> EliminarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "EliminarPersonaTipoSocial")] HttpRequestData req)
        {
            try
            {
                var tipoSocial = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar un telefono");

                bool seGuardo = await _personatipoSocial.EliminarPersonaTipoSocial(tipoSocial.Id);

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
