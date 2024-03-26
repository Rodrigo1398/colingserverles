using Coling.API.BolsaTrabajo.model;
using Coling.API.BolsaTrabajo.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.BolsaTrabajo.endpoints
{
    public class SolicitudFunction
    {
        private readonly ILogger<SolicitudFunction> _logger;
        private readonly SolicitudService solicitudService;

        public SolicitudFunction(ILogger<SolicitudFunction> logger, SolicitudService _solicitudService)
        {
            _logger = logger;
            solicitudService = _solicitudService;
        }

        [Function("InsertarSolicitud")]
        public async Task<HttpResponseData> InsertarSolicitud([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var solicitud = await req.ReadFromJsonAsync<Solicitud>() ?? throw new Exception("Debe ingresar una Solicitud");
                bool seGuardo = await solicitudService.Create("Solicitud",solicitud);
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
        [Function("EditarSolicitud")]
        public async Task<HttpResponseData> EditarSolicitud([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var solicitud = await req.ReadFromJsonAsync<Solicitud>() ?? throw new Exception("Debe ingresar una Solicitud válida");
                bool seEdito = await solicitudService.Update("Solicitud", solicitud);

                if (!seEdito)
                {
                    resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                    return resp;
                }

                resp = req.CreateResponse(HttpStatusCode.OK);
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
