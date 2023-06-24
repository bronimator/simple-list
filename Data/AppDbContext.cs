using Microsoft.EntityFrameworkCore;
using UserListTestApp.Models;

namespace UserListTestApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get;set; }

        public DbSet<UserType> UserTypes { get;set; }
    }
}
