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
    public class ListDetailApi
    {
        private readonly ILogger<ListDetailApi> _logger;

        public ListDetailApi(ILogger<ListDetailApi> logger)
        {
            _logger = logger;
        }

        [OpenApiOperation(operationId: "getTasks", tags: new[] { "lists" }, Summary = "Get tasks", Description = "Get tasks in a task list.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "listId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Summary = "Id of the task list", Description = "Id of the task list.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/json", bodyType: typeof(ApiResponse<List<TodoTask>>), Summary = "List of tasks.", Description = "This returns the list of tasks in a list.")]
        [Function("ListDetailApi")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "lists/{listId}")] HttpRequest req, string listId)
        {
            _logger.LogInformation("Getting tasks for listId: " + listId);
            List<TodoTask> tasks = [
                new TodoTask("taskId-1", "This is task 1"),
                new TodoTask("taskId-2", "This is task 2")
            ];
            return new OkObjectResult(new ApiResponse<List<TodoTask>>(tasks));
        }
    }
}
