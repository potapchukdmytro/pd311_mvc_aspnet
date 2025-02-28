using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Models;
using pd311_mvc_aspnet.Models.Identity;

namespace pd311_mvc_aspnet.Data.Initializer
{
    public static class Seeder
    {
        public static void Seed(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var context = scope.ServiceProvider.GetService<AppDbContext>()
                                   ?? throw new ArgumentNullException("Db context is null");
            var userManger = scope.ServiceProvider.GetService<UserManager<AppUser>>()
                                    ?? throw new ArgumentNullException("UserManager is null");
            var roleManger = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>()
                                    ?? throw new ArgumentNullException("RoleManager is null");

            context.Database.Migrate();

            // Roles and Users
            if(!roleManger.Roles.Any())
            {
                var roles = new List<IdentityRole>
                {
                    new(){ Name = Settings.RoleAdmin },
                    new(){ Name = Settings.RoleUser }
                };

                roleManger.CreateAsync(roles[0]).Wait();
                roleManger.CreateAsync(roles[1]).Wait();
            }

            if(!userManger.Users.Any())
            {
                var admin = new AppUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin",
                    FirstName = "admin",
                    LastName = "admin",
                    EmailConfirmed = true
                };

                var user = new AppUser
                {
                    Email = "user@gmail.com",
                    UserName = "user",
                    FirstName = "user",
                    LastName = "user",
                    EmailConfirmed = true
                };

                userManger.CreateAsync(admin, "qwerty").Wait();
                userManger.CreateAsync(user, "qwerty").Wait();

                userManger.AddToRoleAsync(admin, Settings.RoleAdmin).Wait();
                userManger.AddToRoleAsync(user, Settings.RoleUser).Wait();
            }

            // Categories and Products
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                    {
                        new() { Id = Guid.NewGuid().ToString(), Name = "Процесори" },
                        new() { Id = Guid.NewGuid().ToString(), Name = "Материнські плати" },
                        new() { Id = Guid.NewGuid().ToString(), Name = "Блоки живлення" },
                        new() { Id = Guid.NewGuid().ToString(), Name = "Оперативна пам'ять" },
                        new() { Id = Guid.NewGuid().ToString(), Name = "Відеокарти" }
                    };

                context.Categories.AddRange(categories);
                context.SaveChanges();

                var products = new List<Product>
                    {
                        new() { Id = Guid.NewGuid().ToString(), Name = "Asus PRIME B650M-K (sAM5, AMD B650, PCI-Ex16)", Amount = 5, Price = 4799, CategoryId = categories[1].Id },
                        new() { Id = Guid.NewGuid().ToString(), Name = "AMD Ryzen 5 5600 3.5-4.4GHz", Amount = 5, Price = 4499, CategoryId = categories[0].Id }
                    };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
