using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Models;
using pd311_mvc_aspnet.Models.Identity;

namespace pd311_mvc_aspnet.Data
{
    public class AppDbContext(DbContextOptions options) 
        : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
