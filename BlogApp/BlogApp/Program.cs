using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(options => {
    options.UseSqlite(builder.Configuration["ConnectionStrings:Sql_connection"]);
});

builder.Services.AddScoped<IPostRepository, EfPostRepository>();

var app = builder.Build();

app.UseStaticFiles();

SeedData.TestVerileriniDoldur(app);

app.MapDefaultControllerRoute();

app.Run();
