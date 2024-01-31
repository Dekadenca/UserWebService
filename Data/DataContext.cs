using Microsoft.EntityFrameworkCore;
using UserManagerApp.Models;

namespace UserManagerApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
    }
}
