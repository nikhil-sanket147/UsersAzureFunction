using UsersAzureFunction.Models;

namespace UsersAzureFunction.Services;

public interface IUserService
{
    Task<List<UsersModel>> GetUsersAsync();

    Task<UsersModel?> GetUserByIdAsync(int id);

    Task<UsersModel> CreateUserAsync(UsersModel user);

    Task<UsersModel?> UpdateUserAsync(int id, UsersModel user);

    Task<bool> DeleteUserAsync(int id);
}