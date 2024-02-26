using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Ritekode.TasksApi
{
    public class GetTasks
    {
        private readonly ILogger<GetTasks> _logger;

        public GetTasks(ILogger<GetTasks> logger)
        {
            _logger = logger;
        }

        [Function("GetTasks")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string response = string.IsNullOrEmpty(req.Query["name"]) ? "Welcome to Azure!" : "Welcome " + req.Query["name"].ToString();
            return new OkObjectResult(response);
        }
    }
}
