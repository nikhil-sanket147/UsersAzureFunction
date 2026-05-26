//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.Functions.Worker;
//using Microsoft.Extensions.Logging;

//namespace UsersAzureFunction;

////this class contains azure fuction logic
//public class Function1
//{
//    //logger injection 
//    private readonly ILogger<Function1> _logger;


//    //dependency injection
//    public Function1(ILogger<Function1> logger)
//    {
//        _logger = logger;
//    }


//    //function attribute - registers this method as an azure function - Function1
//    [Function("Function1")]

//    //HTTP Trigger

//    // Request object - HttpRequest req
//    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
//    {
//        _logger.LogInformation("C# HTTP trigger function processed a request.");

//        //response
//        return new OkObjectResult("Welcome to Azure Functions!");
//    }
//}




using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace UsersAzureFunction;

public class Function1
{
    private readonly ILogger<Function1> _logger;

    public Function1(ILogger<Function1> logger)
    {
        _logger = logger;
    }

    [Function("Function1")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req
        )
    {
        _logger.LogInformation("function executed");

        var response = req.CreateResponse(HttpStatusCode.OK);

        await response.WriteStringAsync("welcome to azure functions");

        return response;
    }

    [Function("GetUsers")]
    public async Task<HttpResponseData> GetUsers(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req
        )
    {
        var response = req.CreateResponse(HttpStatusCode.OK);

        await response.WriteStringAsync("get response");

        return response;
    }
}