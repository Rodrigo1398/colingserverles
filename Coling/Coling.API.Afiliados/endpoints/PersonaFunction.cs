using Coling.API.Afiliados.interfaces;
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
    public class PersonaFunction
    {
        private readonly ILogger<PersonaFunction> _logger;
        private readonly PersonaServices _persona;

        public PersonaFunction(ILogger<PersonaFunction> logger, PersonaServices persona)
        {
            _logger = logger;
            _persona = persona;
        }

        [Function("ListarPersonas")]
        public async Task<HttpResponseData> ListarPersonas([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPersonas")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var listaPersonas = await _persona.ListarPersonas();
            var resp = req.CreateResponse(HttpStatusCode.OK);
            await resp.WriteAsJsonAsync(listaPersonas);
            return resp;
        }

        [Function("InsertarPersona")]
        public async Task<HttpResponseData> InsertarPersona([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarPersona")] HttpRequestData req)
        {
            try
            {
                var per = await req.ReadFromJsonAsync<Persona>() ?? throw new Exception("Debe ingresar una persona con sus datos");

                bool seGuardo = await _persona.InsertarPersona(per);

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
