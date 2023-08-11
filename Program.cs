using GamingBlog.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<GamingBlogDbContext>(opts =>{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:MSSQLCONNETION"]);
});

var app = builder.Build();
app.MapControllers();

// app.MapGet("/", () => "Hello World!");

app.Run();
