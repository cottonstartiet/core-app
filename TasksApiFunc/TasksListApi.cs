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
    public class TasksListApi
    {
        private readonly ILogger<TasksListApi> _logger;

        public TasksListApi(ILogger<TasksListApi> logger)
        {
            _logger = logger;
        }

        [OpenApiOperation(operationId: "getLists", tags: new[] { "lists" }, Summary = "Get lists", Description = "Get lists for the user.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/json", bodyType: typeof(ApiResponse<List<TodoTaskList>>), Summary = "List of lists for the user.", Description = "This returns the list of tasks.")]
        [Function("lists")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "lists")] HttpRequest req,
            string listId)
        {
            List<TodoTaskList> lists = [
                new TodoTaskList("listId-1", "List 1 - Office work"),
                new TodoTaskList("listId-2", "List 2 - Home work")
                ];
            _logger.LogInformation("Number of lists: " + lists.Count);
            return new OkObjectResult(new ApiResponse<List<TodoTaskList>>(lists));
        }
    }
}
