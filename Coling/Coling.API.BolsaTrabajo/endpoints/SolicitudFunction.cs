using Coling.API.BolsaTrabajo.interfaces;
using Coling.API.BolsaTrabajo.model;
using Coling.API.BolsaTrabajo.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
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
        [OpenApiOperation("InsertarSolicitud", Description = "Sirve para Insertar Solicitud")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Solicitud))]
        public async Task<HttpResponseData> InsertarSolicitud([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var solicitud = await req.ReadFromJsonAsync<Solicitud>() ?? throw new Exception("Debe ingresar una Solicitud");
                bool seGuardo = await solicitudService.Create(solicitud);
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
        [OpenApiOperation("Actualiza una Solicitud", Description = "Sirve para Actualizar una Solicitud")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Solicitud))]
        public async Task<HttpResponseData> EditarSolicitud([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData resp;
            string? id = req.Query["id"];

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    resp = req.CreateResponse(HttpStatusCode.BadRequest);
                    return resp;
                }

                var solicitud = await req.ReadFromJsonAsync<Solicitud>();

                if (solicitud == null)
                {
                    resp = req.CreateResponse(HttpStatusCode.BadRequest);
                    return resp;
                }

                bool seEdito = await solicitudService.Update(solicitud, id);

                if (!seEdito)
                {
                    resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                    return resp;
                }

                resp = req.CreateResponse(HttpStatusCode.OK);
                return resp;
            }
            catch (Exception ex)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }

        [Function("GetAllSolicitud")]
        [OpenApiOperation("ListarSolicitudes", Description = "Sirve para Listar Solicitudes")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Solicitud>),
            Description = "Muestra las Solicitudes de la base de datos")]
        public async Task<HttpResponseData> GetAllSolicitud([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                
                List<Solicitud> solicitudes = await solicitudService.GetAll();

                resp = req.CreateResponse(HttpStatusCode.OK);

                await resp.WriteAsJsonAsync(solicitudes);

                return resp;
            }
            catch (Exception)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }
        [Function("GetByIdSolicitud")]
        [OpenApiOperation("Lista una Solicitud por Id", Description = "Sirve para listar una Solicitud")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "el id de la Solicitud")]
        public async Task<HttpResponseData> GetByIdSolicitud([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                string? id = req.Query["id"];
                if (string.IsNullOrEmpty(id))
                {
                    resp = req.CreateResponse(HttpStatusCode.BadRequest);
                    return resp;
                }
                Solicitud solicitud = await solicitudService.Get(id);

                resp = req.CreateResponse(HttpStatusCode.OK);

                await resp.WriteAsJsonAsync(solicitud);

                return resp;
            }
            catch (Exception)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }
        [Function("DeleteSolicitud")]
        [OpenApiOperation("Elimina una Solicitud", Description = "Sirve para Eliminar una Solicitud")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "el id de la Solicitud")]
        public async Task<HttpResponseData> DeleteSolicitud([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                string? id = req.Query["id"];
                if (string.IsNullOrEmpty(id))
                {
                    resp = req.CreateResponse(HttpStatusCode.BadRequest);
                    return resp;
                }
                var sw = await solicitudService.Delete(id);



                resp = req.CreateResponse(HttpStatusCode.OK);

                bool seEdito = await solicitudService.Delete(id);

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
