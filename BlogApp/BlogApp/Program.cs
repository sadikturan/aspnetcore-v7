using BlogApp.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogContext>(options => {
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("mysql_connection");
    // options.UseSqlite(connectionString);

    var version = new MySqlServerVersion(new Version(8,0,30));
    options.UseMySql(connectionString, version);
});

var app = builder.Build();

SeedData.TestVerileriniDoldur(app);

app.MapGet("/", () => "Hello World!");

app.Run();
