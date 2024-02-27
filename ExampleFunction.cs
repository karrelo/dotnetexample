// using dotnetcqstemplate.Domain.Core;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Azure.WebJobs;
// using Microsoft.Azure.WebJobs.Extensions.Http;

// namespace dotnetcqstemplate;

// public class ExampleFunctions
// {
//     private readonly ICommandProvider _commandProvider;
//     private readonly IQueryProvider _queryProvider;

//     [FunctionName(nameof(ExampleShit))]
//     public async Task<IActionResult> ExampleShit([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "bff/example/{id}")] HttpRequest req, string id)
//     {
//         return OkObjectResult({ "": ""});
//     }
// }