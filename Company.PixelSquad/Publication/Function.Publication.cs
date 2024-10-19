using System.Collections.Generic;
using System.Net;
using DistribuitedServices.Global.Services.Publication;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.PixelSquad.Publication;

public class Publication
{
    private readonly ILogger _logger;
    private IPublicationDistribuitedService _iPublicationDistribuitedService; 

    public Publication(ILoggerFactory loggerFactory, IPublicationDistribuitedService iPublicationDistribuitedService)
    {
        _logger = loggerFactory.CreateLogger<Publication>();
        _iPublicationDistribuitedService = iPublicationDistribuitedService;
    }

    [Function("Post")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post",Route = "post/list/{id}")] HttpRequestData req,
        string id,
        FunctionContext executionContext)
    {

        var data = _iPublicationDistribuitedService.GetAllList();
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        
        // var postJson = $@"
        // {{
        //     ""Id"": {id},
        //     ""UserId"": 1,
        //     ""GameId"": 101,
        //     ""PlatformId"": 202,
        //     ""LanguageId"": 303,
        //     ""HasMicrophone"": true,
        //     ""PlayStyleId"": 2,
        //     ""Description"": ""Looking for teammates to play ranked matches"",
        //     ""CreatedDate"": ""{DateTime.UtcNow:yyyy-MM-ddTHH:mm:ssZ}""
        // }}";
        //
        // // Crear la respuesta con código de estado 200 (OK)
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        //
        // // Escribir el contenido JSON manualmente en el cuerpo de la respuesta
        response.WriteString(data);
        //
        return response;
        
    }
}