using eArticles.API.Controllers;
using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using eArticles.API.Persistance;
using eArticles.Tests.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace eArticles.Tests;

public class ArticlesControllerTests
{
    [Fact]
    public async void GetArticleTest()
    {
        //Arrange
        Mock<IArticlesRepository> mockArticleRepo = new();
        Mock<IUsersRepository> mockUserRepo = new();
        Article[] articles = ArticlesProvider.GetArticles().ToArray<Article>();
        Article expectedArticle = articles.First(ar => ar.Id == 1);
        mockArticleRepo.Setup(x => x.GetById(1)).ReturnsAsync(expectedArticle);
        ArticlesController articlesController = new ArticlesController(
            mockArticleRepo.Object,
            mockUserRepo.Object
        );
        //Act
        var result = await articlesController.Get(1) as ObjectResult;
        //Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);

        var resArticleDTO = result.Value as ArticleDto;

        Assert.Equal(expectedArticle.Id.ToString(), resArticleDTO?.Id);
        Assert.Equal(expectedArticle.Title, resArticleDTO?.Title);
        Assert.Equal(expectedArticle.Description, resArticleDTO?.Description);
        Assert.Equal(expectedArticle.Content, resArticleDTO?.Content);
        Assert.Equal(expectedArticle.ContentType.ToString(), resArticleDTO?.ArticleType);
        Assert.Equal(expectedArticle.Published_Date.ToString(), resArticleDTO?.PublishedDate);
    }

    [Fact]
    public async void GetPageTest()
    {
        //Arange
        Mock<IArticlesRepository> mockRepository = new();
        Mock<IUsersRepository> mockUserManager = new();
        Article[] articles = ArticlesProvider.GetArticles().ToArray<Article>();
        const int pageNumber = 2;
        const int pageSize = 3;
        var expectedArticles = articles
            .OrderBy(ar => ar.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        mockRepository.Setup(x => x.GetPage(pageNumber, pageSize, null, "", "", null)).ReturnsAsync(expectedArticles);
        mockRepository.Setup(x => x.GetTotalItems(null)).ReturnsAsync(articles.Length);
        //Act
        ArticlesController articlesController = new ArticlesController(
            mockRepository.Object,
            mockUserManager.Object
        );
        var actionResult = await articlesController.GetPage(pageNumber, pageSize);

        //Assert
        var okObjectResult = actionResult as OkObjectResult;
        Assert.NotNull(okObjectResult);

        var articleDtoList = okObjectResult.Value as PageArticleDto;
        Assert.NotNull(articleDtoList);

        Assert.NotEmpty(articleDtoList.items);
        Assert.Equal(pageSize, articleDtoList.items.Count());
        Assert.Equal(articles.Length, articleDtoList.totalCount);
        Assert.Equal(expectedArticles.First().Id.ToString(), articleDtoList.items.First().Id);
        Assert.Equal(expectedArticles.Last().Id.ToString(), articleDtoList.items.Last().Id);
    }

    [Fact]
    public async void CreateArticleTest()
    {
        Mock<IArticlesRepository> mockRepository = new();
        Mock<IUsersRepository> mockUserManager = new();
        mockUserManager
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .ReturnsAsync(new User() { UserName = "qwe", Email = "asd" });
        Article[] articles = ArticlesProvider.GetArticles().ToArray<Article>();

        var expectedCreatedArticle = articles.First();

        CreateArticleDto createArticleDto =
            new(
                expectedCreatedArticle.Title,
                expectedCreatedArticle.Description,
                expectedCreatedArticle.Content,
                expectedCreatedArticle.ContentType.ToString(),
                expectedCreatedArticle.Tags.Select(t => t.Title).ToList(),
                expectedCreatedArticle.Img_Url
            );

        mockRepository
            .Setup(repo => repo.Create(It.IsAny<Article>(), It.IsAny<string>(), It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(expectedCreatedArticle);

        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "123") };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);

        var controller = new ArticlesController(mockRepository.Object, mockUserManager.Object);

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };

        //Act
        var result = await controller.Create(createArticleDto);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var createdArticleDto = Assert.IsType<ArticleDto>(okResult.Value);

        Assert.Equal(expectedCreatedArticle.Id.ToString(), createdArticleDto.Id);

        mockRepository.Verify(
            repo => repo.Create(It.IsAny<Article>(), It.IsAny<string>(), It.IsAny<IEnumerable<string>>()),
            Times.Once
        );
    }

    [Fact]
    public async void Create_InvalidArticleDto_ReturnsBadRequest()
    {
        //Arrange
        Mock<IArticlesRepository> mockRepository = new();
        Mock<IUsersRepository> mockUserManager = new();
        ArticlesController controller = new(mockRepository.Object, mockUserManager.Object);
        controller.ModelState.AddModelError("Title", "Title is required");
        var invalidArticleDto = new CreateArticleDto("", "", "", "");

        //Act
        var result = await controller.Create(invalidArticleDto);

        //Assert

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);
        mockRepository.Verify(
            repo => repo.Create(It.IsAny<Article>(), It.IsAny<string>(), It.IsAny<IEnumerable<string>>()),
            Times.Never
        );
    }

    [Fact]
    public async void UpdateArticleTest()
    {
        Mock<IArticlesRepository> mockRepository = new();
        Mock<IUsersRepository> mockUserManager = new();
        Article[] articles = ArticlesProvider.GetArticles().ToArray();
        User[] users = ArticlesProvider.GetUsers().ToArray();
        var expectedCreatedArticle = articles.First();
        mockUserManager
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .ReturnsAsync(expectedCreatedArticle.User);
        UpdateArticleDto updateArticleDto =
            new(
                expectedCreatedArticle.Title,
                expectedCreatedArticle.Description,
                expectedCreatedArticle.Content,
                expectedCreatedArticle.ContentType.ToString(),
                expectedCreatedArticle.Tags.Select(t => t.Title).ToList(),
                expectedCreatedArticle.Img_Url
            );

        mockRepository
            .Setup(repo => repo.Update(It.IsAny<Article>(), It.IsAny<List<string>>()))
            .ReturnsAsync(expectedCreatedArticle);
        mockRepository
            .Setup(repo => repo.GetById(expectedCreatedArticle.Id))
            .ReturnsAsync(expectedCreatedArticle);

        var controller = new ArticlesController(mockRepository.Object, mockUserManager.Object);
        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "123") };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };
        //Act
        var result = await controller.Update(expectedCreatedArticle.Id, updateArticleDto);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var updatedArticleDto = Assert.IsType<ArticleDto>(okResult.Value);

        Assert.Equal(expectedCreatedArticle.Id.ToString(), updatedArticleDto.Id);

        mockRepository.Verify(
            repo => repo.Update(It.IsAny<Article>(), It.IsAny<IEnumerable<string>>()),
            Times.Once
        );
    }

    [Fact]
    public async void DeleteArticleTest()
    {
        //Arrange
        User[] users = ArticlesProvider.GetUsers().ToArray();
        Mock<IArticlesRepository> mockRepository = new();
        Mock<IUsersRepository> mockUserManager = new();
        Article[] articles = ArticlesProvider.GetArticles().ToArray<Article>();
        var expectedDeletedArticle = articles.First();
        mockUserManager
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .ReturnsAsync(expectedDeletedArticle.User);

        mockRepository
            .Setup(repo => repo.Delete(It.IsAny<int>()))
            .ReturnsAsync(expectedDeletedArticle);
        mockRepository
            .Setup(repo => repo.GetById(expectedDeletedArticle.Id))
            .ReturnsAsync(expectedDeletedArticle);
        ArticlesController controller = new(mockRepository.Object, mockUserManager.Object);

        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "123") };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };
        //Act
        var result = await controller.Delete(expectedDeletedArticle.Id);
        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        int deletedArticleId = int.Parse(Assert.IsType<string>(okResult.Value).Split(' ').Last());
        Assert.Equal(deletedArticleId, expectedDeletedArticle.Id);
        mockRepository.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
    }
}
