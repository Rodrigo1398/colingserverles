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
    public class OfertaLaboralFunction
    {
        private readonly ILogger<OfertaLaboralFunction> _logger;
        private readonly OfertaLaboralService ofertaLaboralService;

        public OfertaLaboralFunction(ILogger<OfertaLaboralFunction> logger, OfertaLaboralService _ofertaLaboralService)
        {
            _logger = logger;
            ofertaLaboralService = _ofertaLaboralService;
        }

        [Function("InsertarOfertaLaboral")]
        [OpenApiOperation("InsertarOfertaLaboral", Description = "Sirve para Insertar OfertaLaboral")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(OfertaLaboral))]
        public async Task<HttpResponseData> InsertarOfertaLaboral([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {
                var ofertaLaboral = await req.ReadFromJsonAsync<OfertaLaboral>() ?? throw new Exception("Debe ingresar una OfertaLaboral");
                bool seGuardo = await ofertaLaboralService.Create(ofertaLaboral);
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
        [Function("EditarOfertaLaboral")]
        [OpenApiOperation("Actualiza una Oferta Laboral", Description = "Sirve para Actualizar una Oferta Laboral")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(OfertaLaboral))]
        public async Task<HttpResponseData> EditarOfertaLaboral([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
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

                var ofertaLaboral = await req.ReadFromJsonAsync<OfertaLaboral>();

                if (ofertaLaboral == null)
                {
                    resp = req.CreateResponse(HttpStatusCode.BadRequest);
                    return resp;
                }

                bool seEdito = await ofertaLaboralService.Update(ofertaLaboral, id);

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

        [Function("GetAllOfertaLaboral")]
        [OpenApiOperation("ListarOfertasLaborales", Description = "Sirve para Listar Ofertas Laborales")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<OfertaLaboral>),
            Description = "Muestra las Ofertas Laborales de la base de datos")]
        public async Task<HttpResponseData> GetAllOfertaLaboral([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData resp;
            try
            {

                List<OfertaLaboral> ofertasLaborales = await ofertaLaboralService.GetAll();

                resp = req.CreateResponse(HttpStatusCode.OK);

                await resp.WriteAsJsonAsync(ofertasLaborales);

                return resp;
            }
            catch (Exception)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }
        [Function("GetByIdOfertaLaboral")]
        [OpenApiOperation("Lista una Oferta Laboral por Id", Description = "Sirve para listar una Oferta Laboral")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "el id de la oferta laboral")]
        public async Task<HttpResponseData> GetByIdOfertaLaboral([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
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
                OfertaLaboral ofertaLaboral = await ofertaLaboralService.Get(id);

                resp = req.CreateResponse(HttpStatusCode.OK);

                await resp.WriteAsJsonAsync(ofertaLaboral);

                return resp;
            }
            catch (Exception)
            {
                resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                return resp;
            }
        }
        [Function("DeleteOfertaLaboral")]
        [OpenApiOperation("Elimina una Oferta Laboral", Description = "Sirve para Eliminar una Oferta Laboral")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "el id de la oferta laboral")]
        public async Task<HttpResponseData> DeleteOfertaLaboral([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
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
                var sw = await ofertaLaboralService.Delete(id);



                resp = req.CreateResponse(HttpStatusCode.OK);

                bool seEdito = await ofertaLaboralService.Delete(id);

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
