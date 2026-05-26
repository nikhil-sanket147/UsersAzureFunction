using Microsoft.EntityFrameworkCore;
using UsersAzureFunction.Data;
using UsersAzureFunction.Models;

namespace UsersAzureFunction.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UsersModel>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<UsersModel?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<UsersModel> CreateUserAsync(UsersModel user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<UsersModel?> UpdateUserAsync(int id, UsersModel updatedUser)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return null;

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        user.Age = updatedUser.Age;

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return false;

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return true;
    }
}