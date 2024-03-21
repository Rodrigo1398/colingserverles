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
    public class AfiliadoFunction
    {
        private readonly ILogger<AfiliadoFunction> _logger;
        private readonly AfiliadoServices _afiliado;

        public AfiliadoFunction(ILogger<AfiliadoFunction> logger, AfiliadoServices afiliado)
        {
            _logger = logger;
            this._afiliado = afiliado;
        }

        [Function("ListarAfiliados")]
        public async Task<HttpResponseData> ListarAfiliados([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarAfiliados")] HttpRequestData req)
        {
            var ListarAfiliados = await _afiliado.ListarAfiliados();
            if (ListarAfiliados == null) return req.CreateResponse(HttpStatusCode.BadRequest);
            var resp = req.CreateResponse(HttpStatusCode.OK);
            await resp.WriteAsJsonAsync(ListarAfiliados);
            return resp;
        }
        [Function("ListarAfiliadoById")]
        public async Task<HttpResponseData> ListarTipoSocialById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTipoSocialById")] HttpRequestData req)
        {
            var afiliado = await req.ReadFromJsonAsync<Shared.Afiliados>() ?? throw new Exception("Debe ingresar un telefono");
            var afiliadoId = await _afiliado.ListarAfiliadoById(afiliado.Id);
            if (afiliadoId == null) return req.CreateResponse(HttpStatusCode.BadRequest);
            var resp = req.CreateResponse(HttpStatusCode.OK);
            await resp.WriteAsJsonAsync(afiliadoId);
            return resp;
        }
        [Function("InsertarAfiliado")]
        public async Task<HttpResponseData> InsertarAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarAfiliado")] HttpRequestData req)
        {
            try
            {
                var afiliado = await req.ReadFromJsonAsync<Shared.Afiliados>() ?? throw new Exception("Debe ingresar un telefono");

                bool seGuardo = await _afiliado.InsertarAfiliado(afiliado);

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
        [Function("EditarAfiliado")]
        public async Task<HttpResponseData> EditarAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "EditarAfiliado")] HttpRequestData req)
        {
            try
            {
                var afiliado = await req.ReadFromJsonAsync<Shared.Afiliados>() ?? throw new Exception("Debe ingresar un telefono");

                bool seGuardo = await _afiliado.ModificarAfiliado(afiliado, afiliado.Id);

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
        [Function("EliminarAfiliado")]
        public async Task<HttpResponseData> EliminarAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "EliminarAfiliado")] HttpRequestData req)
        {
            try
            {
                var afiliado = await req.ReadFromJsonAsync<TipoSocial>() ?? throw new Exception("Debe ingresar un telefono");

                bool seGuardo = await _afiliado.EliminarAfiliado(afiliado.Id);

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
