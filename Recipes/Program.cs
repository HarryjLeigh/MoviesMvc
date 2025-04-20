using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RecipesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RecipesConnection") ??
                         throw new InvalidOperationException("Connection string 'RecipesConnection' not found.")));

builder.Services.AddDbContext<RatingsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RatingsConnection") ??
                         throw new InvalidOperationException("Connection string 'RatingsConnection' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

var scope = app.Services.CreateScope();

using (scope)
{
    var services = scope.ServiceProvider;
    SeedData.InitializeRecipes(services);
    SeedData.InitializeRatings(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        "default",
        "{controller=Recipe}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
        "default",
        "{controller=Ratings}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();