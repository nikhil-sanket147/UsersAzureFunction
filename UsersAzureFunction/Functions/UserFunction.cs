using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Text.Json;
using UsersAzureFunction.Models;
using UsersAzureFunction.Services;

namespace UsersAzureFunction.Functions;

public class UserFunction
{
    private readonly IUserService _userService;

    public UserFunction(IUserService userService)
    {
        _userService = userService;
    }

    [Function("GetUsers")]
    public async Task<HttpResponseData> GetUsers(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")]
        HttpRequestData req)
    {
        var users = await _userService.GetUsersAsync();

        var response = req.CreateResponse(HttpStatusCode.OK);

        await response.WriteAsJsonAsync(users);

        return response;
    }

    [Function("GetUserById")]
    public async Task<HttpResponseData> GetUserById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id:int}")]
        HttpRequestData req,
        int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        var response = req.CreateResponse();

        if (user == null)
        {
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        await response.WriteAsJsonAsync(user);

        return response;
    }

    [Function("CreateUser")]
    public async Task<HttpResponseData> CreateUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")]
        HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        var user = JsonSerializer.Deserialize<UsersModel>(requestBody);

        var createdUser = await _userService.CreateUserAsync(user!);

        var response = req.CreateResponse(HttpStatusCode.Created);

        await response.WriteAsJsonAsync(createdUser);

        return response;
    }

    [Function("UpdateUser")]
    public async Task<HttpResponseData> UpdateUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users/{id:int}")]
        HttpRequestData req,
        int id)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        var updatedUser = JsonSerializer.Deserialize<UsersModel>(requestBody);

        var result = await _userService.UpdateUserAsync(id, updatedUser!);

        var response = req.CreateResponse();

        if (result == null)
        {
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        await response.WriteAsJsonAsync(result);

        return response;
    }

    [Function("DeleteUser")]
    public async Task<HttpResponseData> DeleteUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "users/{id:int}")]
        HttpRequestData req,
        int id)
    {
        var deleted = await _userService.DeleteUserAsync(id);

        var response = req.CreateResponse();

        response.StatusCode = deleted
            ? HttpStatusCode.OK
            : HttpStatusCode.NotFound;

        return response;
    }
}