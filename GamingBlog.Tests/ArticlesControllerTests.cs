using GamingBlog.API.Controllers;
using GamingBlog.API.Data.Dtos;
using GamingBlog.API.Models;
using GamingBlog.API.Services.Repositories;
using GamingBlog.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using GamingBlog.API.Extensions;

namespace GamingBlog.Tests;

public class ArticlesControllerTests
{
    [Fact]
    public void GetArticleTest()
    {
        //Arrange
        Mock<IArticlesRepository> mockRepository = new();
        Article[] articles = ArticlesProvider.GetArticles().ToArray<Article>();
        Article expectedArticle = articles.First(ar => ar.Id == 1);
        mockRepository.Setup(x => x.Get(1)).Returns(expectedArticle);
        ArticlesController articlesController = new ArticlesController(mockRepository.Object);
        //Act
        var result = articlesController.Get(1) as ObjectResult;
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
    public void GetPageTest()
    {
        //Arange
        Mock<IArticlesRepository> mockRepository = new();
        Article[] articles = ArticlesProvider.GetArticles().ToArray<Article>();
        const int pageNumber = 2;
        const int pageSize = 3;
        var expectedArticles = articles
            .OrderBy(ar => ar.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        mockRepository.Setup(x => x.GetPage(pageNumber, pageSize)).Returns(expectedArticles);
        //Act
        ArticlesController articlesController = new ArticlesController(mockRepository.Object);
        var actionResult = articlesController.GetPage(pageNumber, pageSize);

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
    public void CreateArticleTest()
    {
        Mock<IArticlesRepository> mockRepository = new();

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
        CreateArticleDto validArticleDto = createArticleDto;

        mockRepository
            .Setup(repo => repo.Create(It.IsAny<Article>(), It.IsAny<List<string>>()))
            .Returns(expectedCreatedArticle);

        var controller = new ArticlesController(mockRepository.Object);

        //Act
        var result = controller.Create(createArticleDto);

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
    public void Create_InvalidArticleDto_ReturnsBadRequest()
    {
        //Arrange
        Mock<IArticlesRepository> mockRepository = new();
        ArticlesController controller = new(mockRepository.Object);
        controller.ModelState.AddModelError("Title", "Title is required");
        var invalidArticleDto = new CreateArticleDto("", "", "", "");

        //Act
        var result = controller.Create(invalidArticleDto);

        //Assert

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);
        mockRepository.Verify(
            repo => repo.Create(It.IsAny<Article>(), It.IsAny<IEnumerable<string>>()),
            Times.Never
        );
    }
    [Fact]
    public void UpdateArticleTest(){
        Mock<IArticlesRepository> mockRepository = new();
        
    }
}
