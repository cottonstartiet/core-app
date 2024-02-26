using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TasksApi;

namespace Ritekode.TasksApi
{
    public class GetTasks
    {
        private readonly ILogger<GetTasks> _logger;

        public GetTasks(ILogger<GetTasks> logger)
        {
            _logger = logger;
        }

        [OpenApiOperation(operationId: "getTasks", tags: new[] { "tasks" }, Summary = "Gets tasks in a task list", Description = "Gets tasks in a task list.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "listId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "The name", Description = "The name", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/json", bodyType: typeof(List<TodoTask>), Summary = "List of tasks.", Description = "This returns the list of tasks.")]
        // Add these four attribute classes above
        [Function("Lists")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "lists/{listId}")] HttpRequest req,
            string listId)
        {
            List<TodoTask> tasks = [new TodoTask(1, "This is task 1"), new TodoTask(2, "This is task 2")];
            _logger.LogInformation("Returning list with tasks: " + tasks.Count);
            return (ActionResult)new OkObjectResult(tasks);
        }
    }
}
