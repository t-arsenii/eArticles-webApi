using eArticles.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Data;

public static class SeedDataExtension
{
    public static async Task SeedInitialData(this WebApplication app)
    {
        var serviceProvider = app.Services.CreateScope().ServiceProvider;
        eArticlesDbContext? dbContext = serviceProvider.GetService<eArticlesDbContext>();
        UserManager<User>? userManager = serviceProvider.GetService<UserManager<User>>();
        RoleManager<IdentityRole<Guid>>? roleManager = serviceProvider.GetService<RoleManager<IdentityRole<Guid>>>();
        IWebHostEnvironment? _environment = serviceProvider.GetService<IWebHostEnvironment>();
        dbContext!.Database.Migrate();

        if (!await roleManager.RoleExistsAsync("User"))
            await roleManager.CreateAsync(new IdentityRole<Guid>("User"));
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));

        if (!dbContext.Articles.Any() && !dbContext.ArticleTags.Any() && !dbContext.Tags.Any() && !dbContext.ContentTypes.Any())
        {
            ContentType[] articleTypes = new ContentType[]
            {
                new ContentType() {Title = "News"},
                new ContentType() {Title = "Guide"},
                new ContentType() {Title = "Opinion"},
            };
            Category[] articleCategories = new Category[]
            {
                new Category() {Title = "Anime"},
                new Category() {Title = "Games"},
                new Category() {Title = "Movies"},
            };
            User[] users = new User[]
            {
                new User() { UserName = "Primeagen", Email = "newuser1@example.com", FirstName = "Jan", LastName="Kowalski",PhoneNumber="48123123123" },
                new User() { UserName = "Godot", Email = "newuser2@example.com", FirstName = "Valerii", LastName="Havryliuk", PhoneNumber="48321321312" },
                new User() { UserName = "Spike", Email = "newuser3@example.com", FirstName = "Arkadiy", LastName="Perekrest", PhoneNumber="48999999999"  }
            };
            foreach (var user in users)
            {
                await userManager!.CreateAsync(user, "1234567890");
                await userManager!.AddToRoleAsync(user, "User");
            }

            User? user1 = await userManager!.FindByNameAsync("Primeagen");
            User? user2 = await userManager!.FindByNameAsync("Godot");
            User? user3 = await userManager!.FindByNameAsync("Spike");

            Article[] articles = new Article[]
            {
                new()
                {
                    Title = "Best AAA game 2023",
                    Description = "Discovering the best triple A project in 2023",
                    Content =
                        "Pulvinar neque laoreet suspendisse interdum consectetur libero id faucibus. Mi ipsum faucibus vitae aliquet nec ullamcorper sit amet. Id faucibus nisl tincidunt eget nullam. Leo duis ut diam quam nulla porttitor massa. Nec ultrices dui sapien eget mi proin sed libero. Viverra accumsan in nisl nisi. Ut enim blandit volutpat maecenas volutpat blandit aliquam. Tincidunt nunc pulvinar sapien et. Dolor morbi non arcu risus quis varius quam quisque. Tortor consequat id porta nibh venenatis. Porttitor rhoncus dolor purus non enim praesent elementum facilisis leo. Elit ullamcorper dignissim cras tincidunt lobortis feugiat vivamus at. Aliquet bibendum enim facilisis gravida neque convallis a cras semper. Sapien faucibus et molestie ac feugiat sed lectus vestibulum. Duis at consectetur lorem donec massa sapien faucibus. Volutpat consequat mauris nunc congue nisi vitae. Lectus arcu bibendum at varius vel pharetra vel turpis. Arcu felis bibendum ut tristique et. Enim neque volutpat ac tincidunt vitae semper quis lectus.",
                    ContentType = articleTypes[2],
                    Category = articleCategories[0],
                    Published_Date = new DateTime(2023, 07, 14),
                    ImagePath = Path.Join(_environment.WebRootPath, "images", "testImage1.png"),
                    UserId = user1!.Id
                },
                new()
                {
                    Title = "How to beat Elden Ring with default gear",
                    Description = "Making best build for beating Elden Ring",
                    Content =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tortor dignissim convallis aenean et tortor. Fames ac turpis egestas integer. Sed augue lacus viverra vitae. Habitasse platea dictumst vestibulum rhoncus est pellentesque elit. Vehicula ipsum a arcu cursus. Arcu odio ut sem nulla pharetra diam sit. At risus viverra adipiscing at in. Ut aliquam purus sit amet luctus venenatis lectus magna fringilla. Nec tincidunt praesent semper feugiat nibh sed pulvinar proin gravida. Id faucibus nisl tincidunt eget nullam non nisi est. Convallis a cras semper auctor. Elementum nibh tellus molestie nunc non. Ipsum dolor sit amet consectetur adipiscing elit.",
                    ContentType = articleTypes[1],
                    Category = articleCategories[1],
                    Published_Date = new DateTime(2023, 07, 14),
                    ImagePath = Path.Join(_environment.WebRootPath, "images", "testImage2.jpg"),
                    UserId = user1!.Id
                },
                new()
                {
                    Title = "New changes in Factorio",
                    Description = "Discovering new changes in recent update",
                    Content =
                        "Amet nisl suscipit adipiscing bibendum est ultricies integer quis. Nunc lobortis mattis aliquam faucibus purus in massa tempor nec. Adipiscing diam donec adipiscing tristique risus nec feugiat in fermentum. Massa enim nec dui nunc mattis. At consectetur lorem donec massa. Viverra mauris in aliquam sem fringilla. Sed nisi lacus sed viverra tellus in. Eu non diam phasellus vestibulum lorem sed risus ultricies. Interdum consectetur libero id faucibus nisl tincidunt eget. Aenean euismod elementum nisi quis eleifend quam adipiscing vitae. Curabitur gravida arcu ac tortor dignissim. Sit amet risus nullam eget felis eget nunc lobortis. Ultrices sagittis orci a scelerisque purus semper eget duis. Pretium lectus quam id leo in vitae turpis massa. Ut placerat orci nulla pellentesque dignissim enim sit. Cursus euismod quis viverra nibh cras pulvinar. Diam ut venenatis tellus in metus vulputate eu scelerisque felis.",
                    ContentType = articleTypes[0],
                    Category = articleCategories[2],
                    Published_Date = new DateTime(2023, 07, 14),
                    ImagePath = Path.Join(_environment.WebRootPath, "images", "testImage3.jpg"),
                    UserId = user2!.Id
                }
            };
            Tag[] tags = new Tag[]
            {
                new() { Title = "PC" },
                new() { Title = "Nintendo" },
                new() { Title = "Playstation" }
            };
            ArticleTag[] articleTags = new ArticleTag[]
            {
                new ArticleTag() { Article = articles[0], Tag = tags[0] },
                new ArticleTag() { Article = articles[0], Tag = tags[1] },
                new ArticleTag() { Article = articles[0], Tag = tags[2] },
                new ArticleTag() { Article = articles[1], Tag = tags[1] },
                new ArticleTag() { Article = articles[2], Tag = tags[0] }
            };
            dbContext.Articles.AddRange(articles);
            dbContext.Tags.AddRange(tags);
            dbContext.ArticleTags.AddRange(articleTags);

            dbContext.SaveChanges();
        }
    }
}
