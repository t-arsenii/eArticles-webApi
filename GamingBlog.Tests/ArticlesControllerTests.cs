using GamingBlog.API.Controllers;
using GamingBlog.API.Data.Dtos;
using GamingBlog.API.Models;
using GamingBlog.API.Services.Repositories;
using GamingBlog.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using GamingBlog.API.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace GamingBlog.Tests;

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
        mockArticleRepo.Setup(x => x.Get(1)).ReturnsAsync(expectedArticle);
        ArticlesController articlesController = new ArticlesController(
            mockArticleRepo.Object,
            mockUserRepo.Object
        );
        //Act
        var result = await articlesController.Get(1) as ObjectResult;
        //Assert
        Assert.NotNull(result); // Ensure that the result is not null
        Assert.Equal(200, result.StatusCode); // Check for HTTP status code OK

        var resArticleDTO = result.Value as ArticleDto;

        Assert.Equal(expectedArticle.Id, resArticleDTO?.Id);
        Assert.Equal(expectedArticle.Title, resArticleDTO?.Title);
        Assert.Equal(expectedArticle.Description, resArticleDTO?.Description);
        Assert.Equal(expectedArticle.Content, resArticleDTO?.Content);
        Assert.Equal(expectedArticle.Article_type.ToString(), resArticleDTO?.Article_Type);
        Assert.Equal(expectedArticle.Published_Date.ToString(), resArticleDTO?.Published_Date);
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
        mockRepository.Setup(x => x.GetPage(pageNumber, pageSize)).ReturnsAsync(expectedArticles);
        //Act
        ArticlesController articlesController = new ArticlesController(
            mockRepository.Object,
            mockUserManager.Object
        );
        var actionResult = await articlesController.GetPage(pageNumber, pageSize);

        //Assert
        var okObjectResult = actionResult as OkObjectResult;
        Assert.NotNull(okObjectResult);

        var articleDtoList = okObjectResult.Value as List<ArticleDto>;
        Assert.NotNull(articleDtoList);

        Assert.NotEmpty(articleDtoList);
        Assert.Equal(pageSize, articleDtoList.Count);
        Assert.Equal(expectedArticles.First().Id, articleDtoList.First().Id);
        Assert.Equal(expectedArticles.Last().Id, articleDtoList.Last().Id);
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
                expectedCreatedArticle.Article_type.ToString(),
                expectedCreatedArticle.Tags.Select(t => t.Title).ToList(),
                expectedCreatedArticle.Img_Url
            );

        mockRepository
            .Setup(repo => repo.Create(It.IsAny<Article>(), It.IsAny<List<string>>()))
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

        Assert.Equal(expectedCreatedArticle.Id, createdArticleDto.Id);

        mockRepository.Verify(
            repo => repo.Create(It.IsAny<Article>(), It.IsAny<IEnumerable<string>>()),
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
            repo => repo.Create(It.IsAny<Article>(), It.IsAny<IEnumerable<string>>()),
            Times.Never
        );
    }

    [Fact]
    public async void UpdateArticleTest()
    {
        Mock<IArticlesRepository> mockRepository = new();
        Mock<IUsersRepository> mockUserManager = new();
        Article[] articles = ArticlesProvider.GetArticles().ToArray<Article>();

        var expectedCreatedArticle = articles.First();

        UpdateArticleDto updateArticleDto =
            new(
                expectedCreatedArticle.Title,
                expectedCreatedArticle.Description,
                expectedCreatedArticle.Content,
                expectedCreatedArticle.Article_type.ToString(),
                expectedCreatedArticle.Tags.Select(t => t.Title).ToList(),
                expectedCreatedArticle.Img_Url
            );

        mockRepository
            .Setup(repo => repo.Update(It.IsAny<Article>(), It.IsAny<List<string>>()))
            .ReturnsAsync(expectedCreatedArticle);

        var controller = new ArticlesController(mockRepository.Object, mockUserManager.Object);

        //Act
        var result = await controller.Update(updateArticleDto);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var updatedArticleDto = Assert.IsType<ArticleDto>(okResult.Value);

        Assert.Equal(expectedCreatedArticle.Id, updatedArticleDto.Id);

        mockRepository.Verify(
            repo => repo.Update(It.IsAny<Article>(), It.IsAny<IEnumerable<string>>()),
            Times.Once
        );
    }

    [Fact]
    public async void DeleteArticleTest()
    {
        //Arrange
        Mock<IArticlesRepository> mockRepository = new();
        Mock<IUsersRepository> mockUserManager = new();

        Article[] articles = ArticlesProvider.GetArticles().ToArray<Article>();
        var expectedDeletedArticle = articles.First();
        mockRepository
            .Setup(repo => repo.Delete(It.IsAny<int>()))
            .ReturnsAsync(expectedDeletedArticle);
        ArticlesController controller = new(mockRepository.Object, mockUserManager.Object);
        //Act
        var result = await controller.Delete(expectedDeletedArticle.Id);
        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        int deletedArticleId = int.Parse(Assert.IsType<string>(okResult.Value).Split(' ').Last());
        Assert.Equal(deletedArticleId, expectedDeletedArticle.Id);
        mockRepository.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
    }
}
