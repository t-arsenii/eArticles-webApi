using GamingBlog.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<GamingBlogDbContext>(opts =>{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:MSSQLCONNETION"]);
});

var app = builder.Build();
app.MapControllers();
bool cmdLineInit = (app.Configuration["INITDB"] ?? "false") == "true";
if(app.Environment.IsDevelopment() && cmdLineInit)
{
    Console.WriteLine("Seeding DB");
    app.SeedInitialData();
}
app.Run();
