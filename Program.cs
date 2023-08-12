using GamingBlog.API.Data;
using Microsoft.EntityFrameworkCore;
using GamingBlog.API.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<GamingBlogDbContext>(opts =>{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:MSSQLCONNETION"]);
});
builder.Services.AddScoped<IArticlesRepository, DbMSSqlRepository>();

var app = builder.Build();
app.MapControllers();
bool cmdLineInit = (app.Configuration["INITDB"] ?? "false") == "true";
if(app.Environment.IsDevelopment() && cmdLineInit)
{
    Console.WriteLine("Seeding DB");
    app.SeedInitialData();
}
app.Run();
