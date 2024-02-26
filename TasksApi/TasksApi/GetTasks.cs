using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Ritekode.TasksApi
{
    public class GetTasks
    {
        private readonly ILogger<GetTasks> _logger;

        public GetTasks(ILogger<GetTasks> logger)
        {
            _logger = logger;
        }

        [OpenApiOperation(operationId: "getName", tags: new[] { "name" }, Summary = "Gets the name", Description = "This gets the name.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Summary = "The name", Description = "The name", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Summary = "The response", Description = "This returns the response")]
        // Add these four attribute classes above
        [Function("GetTasks")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string response = string.IsNullOrEmpty(req.Query["name"]) ? "Welcome to Azure!" : "Welcome " + req.Query["name"].ToString();
            return new OkObjectResult(response);
        }
    }
}
