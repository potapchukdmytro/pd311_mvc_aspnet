using Microsoft.EntityFrameworkCore;
using pd311_mvc_aspnet.Data.Initializer;
using pd311_mvc_aspnet.Data;
using pd311_mvc_aspnet.Repositories.Products;
using pd311_mvc_aspnet.Services.Image;
using pd311_mvc_aspnet.Repositories.Categories;
using pd311_mvc_aspnet.Services.Cart;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICartService, CartService>();

// Add repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Add database context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("name=SQLServerLocal");
});

// Add session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seeding data
app.Seed();

app.Run();
