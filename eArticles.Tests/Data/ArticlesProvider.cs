using eArticles.API.Data.Enums;
using eArticles.API.Models;

namespace eArticles.Tests.Data;

public static class ArticlesProvider
{
    public static IEnumerable<Article> GetArticles(){
        return new Article[]
            {
                new()
                {
                    Id = 1,
                    Title = "Best AAA game 2023",
                    Description = "Discovering the best triple A project in 2023",
                    Content =
                        "Pulvinar neque laoreet suspendisse interdum consectetur libero id faucibus. Mi ipsum faucibus vitae aliquet nec ullamcorper sit amet. Id faucibus nisl tincidunt eget nullam. Leo duis ut diam quam nulla porttitor massa. Nec ultrices dui sapien eget mi proin sed libero. Viverra accumsan in nisl nisi. Ut enim blandit volutpat maecenas volutpat blandit aliquam. Tincidunt nunc pulvinar sapien et. Dolor morbi non arcu risus quis varius quam quisque. Tortor consequat id porta nibh venenatis. Porttitor rhoncus dolor purus non enim praesent elementum facilisis leo. Elit ullamcorper dignissim cras tincidunt lobortis feugiat vivamus at. Aliquet bibendum enim facilisis gravida neque convallis a cras semper. Sapien faucibus et molestie ac feugiat sed lectus vestibulum. Duis at consectetur lorem donec massa sapien faucibus. Volutpat consequat mauris nunc congue nisi vitae. Lectus arcu bibendum at varius vel pharetra vel turpis. Arcu felis bibendum ut tristique et. Enim neque volutpat ac tincidunt vitae semper quis lectus.",
                    Article_type = ArticleType.Opinion,
                    Published_Date = new DateTime(2023, 07, 14),
                },
                new()
                {
                    Id = 2,
                    Title = "How to beat Elden Ring ",
                    Description = "Making best build for beating Elden Ring",
                    Content =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tortor dignissim convallis aenean et tortor. Fames ac turpis egestas integer. Sed augue lacus viverra vitae. Habitasse platea dictumst vestibulum rhoncus est pellentesque elit. Vehicula ipsum a arcu cursus. Arcu odio ut sem nulla pharetra diam sit. At risus viverra adipiscing at in. Ut aliquam purus sit amet luctus venenatis lectus magna fringilla. Nec tincidunt praesent semper feugiat nibh sed pulvinar proin gravida. Id faucibus nisl tincidunt eget nullam non nisi est. Convallis a cras semper auctor. Elementum nibh tellus molestie nunc non. Ipsum dolor sit amet consectetur adipiscing elit.",
                    Article_type = ArticleType.Guide,
                    Published_Date = new DateTime(2023, 07, 14),
                },
                new()
                {
                    Id = 3,
                    Title = "New changes in Factorio",
                    Description = "Discovering the best triple A project in 2023",
                    Content =
                        "Amet nisl suscipit adipiscing bibendum est ultricies integer quis. Nunc lobortis mattis aliquam faucibus purus in massa tempor nec. Adipiscing diam donec adipiscing tristique risus nec feugiat in fermentum. Massa enim nec dui nunc mattis. At consectetur lorem donec massa. Viverra mauris in aliquam sem fringilla. Sed nisi lacus sed viverra tellus in. Eu non diam phasellus vestibulum lorem sed risus ultricies. Interdum consectetur libero id faucibus nisl tincidunt eget. Aenean euismod elementum nisi quis eleifend quam adipiscing vitae. Curabitur gravida arcu ac tortor dignissim. Sit amet risus nullam eget felis eget nunc lobortis. Ultrices sagittis orci a scelerisque purus semper eget duis. Pretium lectus quam id leo in vitae turpis massa. Ut placerat orci nulla pellentesque dignissim enim sit. Cursus euismod quis viverra nibh cras pulvinar. Diam ut venenatis tellus in metus vulputate eu scelerisque felis.",
                    Article_type = ArticleType.News,
                    Published_Date = new DateTime(2023, 07, 14),
                },
                new()
                {
                    Id = 4,
                    Title = "New logitech mouse",
                    Description = "Review new logitech mouse",
                    Content =
                        "Amet nisl suscipit adipiscing bibendum est ultricies integer quis. Nunc lobortis mattis aliquam faucibus purus in massa tempor nec. Adipiscing diam donec adipiscing tristique risus nec feugiat in fermentum. Massa enim nec dui nunc mattis. At consectetur lorem donec massa. Viverra mauris in aliquam sem fringilla. Sed nisi lacus sed viverra tellus in. Eu non diam phasellus vestibulum lorem sed risus ultricies. Interdum consectetur libero id faucibus nisl tincidunt eget. Aenean euismod elementum nisi quis eleifend quam adipiscing vitae. Curabitur gravida arcu ac tortor dignissim. Sit amet risus nullam eget felis eget nunc lobortis. Ultrices sagittis orci a scelerisque purus semper eget duis. Pretium lectus quam id leo in vitae turpis massa. Ut placerat orci nulla pellentesque dignissim enim sit. Cursus euismod quis viverra nibh cras pulvinar. Diam ut venenatis tellus in metus vulputate eu scelerisque felis.",
                    Article_type = ArticleType.Review,
                    Published_Date = new DateTime(2023, 06, 21),
                },
                new()
                {
                    Id = 5,
                    Title = "Nintendo news",
                    Description = "Nintendo news ",
                    Content =
                        "Amet nisl suscipit adipiscing bibendum est ultricies integer quis. Nunc lobortis mattis aliquam faucibus purus in massa tempor nec. Adipiscing diam donec adipiscing tristique risus nec feugiat in fermentum. Massa enim nec dui nunc mattis. At consectetur lorem donec massa. Viverra mauris in aliquam sem fringilla. Sed nisi lacus sed viverra tellus in. Eu non diam phasellus vestibulum lorem sed risus ultricies. Interdum consectetur libero id faucibus nisl tincidunt eget. Aenean euismod elementum nisi quis eleifend quam adipiscing vitae. Curabitur gravida arcu ac tortor dignissim. Sit amet risus nullam eget felis eget nunc lobortis. Ultrices sagittis orci a scelerisque purus semper eget duis. Pretium lectus quam id leo in vitae turpis massa. Ut placerat orci nulla pellentesque dignissim enim sit. Cursus euismod quis viverra nibh cras pulvinar. Diam ut venenatis tellus in metus vulputate eu scelerisque felis.",
                    Article_type = ArticleType.News,
                    Published_Date = new DateTime(2023, 07, 14),
                },
                new()
                {
                    Id = 6,
                    Title = "Is Skyrim still relevant in 2023?",
                    Description = "Honest opinion about Skyrim",
                    Content =
                        "Amet nisl suscipit adipiscing bibendum est ultricies integer quis. Nunc lobortis mattis aliquam faucibus purus in massa tempor nec. Adipiscing diam donec adipiscing tristique risus nec feugiat in fermentum. Massa enim nec dui nunc mattis. At consectetur lorem donec massa. Viverra mauris in aliquam sem fringilla. Sed nisi lacus sed viverra tellus in. Eu non diam phasellus vestibulum lorem sed risus ultricies. Interdum consectetur libero id faucibus nisl tincidunt eget. Aenean euismod elementum nisi quis eleifend quam adipiscing vitae. Curabitur gravida arcu ac tortor dignissim. Sit amet risus nullam eget felis eget nunc lobortis. Ultrices sagittis orci a scelerisque purus semper eget duis. Pretium lectus quam id leo in vitae turpis massa. Ut placerat orci nulla pellentesque dignissim enim sit. Cursus euismod quis viverra nibh cras pulvinar. Diam ut venenatis tellus in metus vulputate eu scelerisque felis.",
                    Article_type = ArticleType.Opinion,
                    Published_Date = new DateTime(2023, 05, 02),
                },
                new()
                {
                    Id = 7,
                    Title = "Best Cyberpunk2077 Mods",
                    Description = "Discovering best Cyberpunk2077 mods",
                    Content =
                        "Amet nisl suscipit adipiscing bibendum est ultricies integer quis. Nunc lobortis mattis aliquam faucibus purus in massa tempor nec. Adipiscing diam donec adipiscing tristique risus nec feugiat in fermentum. Massa enim nec dui nunc mattis. At consectetur lorem donec massa. Viverra mauris in aliquam sem fringilla. Sed nisi lacus sed viverra tellus in. Eu non diam phasellus vestibulum lorem sed risus ultricies. Interdum consectetur libero id faucibus nisl tincidunt eget. Aenean euismod elementum nisi quis eleifend quam adipiscing vitae. Curabitur gravida arcu ac tortor dignissim. Sit amet risus nullam eget felis eget nunc lobortis. Ultrices sagittis orci a scelerisque purus semper eget duis. Pretium lectus quam id leo in vitae turpis massa. Ut placerat orci nulla pellentesque dignissim enim sit. Cursus euismod quis viverra nibh cras pulvinar. Diam ut venenatis tellus in metus vulputate eu scelerisque felis.",
                    Article_type = ArticleType.Opinion,
                    Published_Date = new DateTime(2023, 02, 28),
                }
            };
    }
}
