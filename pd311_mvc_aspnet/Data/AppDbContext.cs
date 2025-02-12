using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Models;

namespace pd311_mvc_aspnet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
