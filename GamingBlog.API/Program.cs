using GamingBlog.API.Data;
using Microsoft.EntityFrameworkCore;
using GamingBlog.API.Services.Repositories;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Identity;
using GamingBlog.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GamingBlog.API.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddDbContext<GamingBlogDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:MSSQLCONNETION"]);
});
builder.Services.AddScoped<IArticlesRepository, ArticlesRepository>();
builder.Services
    .AddIdentityCore<User>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<GamingBlogDbContext>();

builder.Services.AddScoped<JwtService>();

var Configuration = builder.Configuration;
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        (options) =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = Configuration["Jwt:Audience"],
                ValidIssuer = Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]!)
                )
            };
        }
    );
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
bool cmdLineInit = (app.Configuration["INITDB"] ?? "false") == "true";
if (app.Environment.IsDevelopment() && cmdLineInit)
{
    Console.WriteLine("Seeding DB");
    app.SeedInitialData();
}
app.Run();
