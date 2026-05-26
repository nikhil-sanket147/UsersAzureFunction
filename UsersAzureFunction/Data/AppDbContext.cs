using Microsoft.EntityFrameworkCore;
using UsersAzureFunction.Models;

namespace UsersAzureFunction.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<UsersModel> Users => Set<UsersModel>();
}