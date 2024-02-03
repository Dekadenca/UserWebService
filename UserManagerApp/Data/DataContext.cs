using Microsoft.EntityFrameworkCore;
using UserManagerApp.Models;

namespace UserManagerApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        // Set database tables into DbContext for later use
        public DbSet<User> Users { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
    }
}
