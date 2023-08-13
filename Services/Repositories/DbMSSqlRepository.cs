using GamingBlog.API.Data;
using GamingBlog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GamingBlog.API.Services.Repositories;

public class DbMSSqlRepository : IArticlesRepository
{
    readonly int PageSize = 5;
    readonly GamingBlogDbContext _dbContext;

    public DbMSSqlRepository(GamingBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Article? Create(Article article, List<string> tagNames)
    {
        var newArticle = new Article
        {
            Title = article.Title,
            Description = article.Description,
            Content = article.Content,
            Article_type = article.Article_type,
            Published_Date = article.Published_Date,
            Img_Url = article.Img_Url,
            ArticleTags = new List<ArticleTag>()
        };
        foreach (var tagName in tagNames)
        {
            var tag = _dbContext.Tags.FirstOrDefault(t => t.Title == tagName);
            if (tag == null)
            {
                tag = new Tag { Title = tagName };
                _dbContext.Tags.Add(tag);
            }
            var articleTag = new ArticleTag { Article = newArticle, Tag = tag };
            newArticle.ArticleTags.Add(articleTag);
        }
        newArticle.Published_Date = DateTime.Now;
        _dbContext.Articles.Add(newArticle);
        _dbContext.SaveChanges();
        return newArticle;
    }

    public void Delete(int id)
    {
        Article? articleToDelete = _dbContext.Articles.Find(id);
        if (articleToDelete != null)
        {
            _dbContext.Articles.Remove(articleToDelete);
            _dbContext.SaveChanges();
        }
    }

    public Article? Get(int id)
    {
        return _dbContext.Articles.Find(id);
    }

    public List<string>? GetArticleTags(int id)
    {
        var article = Get(id);
        if (article == null)
        {
            return null;
        }
        var tags = article.ArticleTags?.Select(at => at.Tag?.Title).ToList();
        if (tags?.Count() == 0 || tags == null)
        {
            return null;
        }
        return tags;
    }

    public IEnumerable<Article>? GetPage(int currentPage = 1)
    {
        return _dbContext.Articles
            .OrderBy(ar => ar.Id)
            .Skip((currentPage - 1) * PageSize)
            .Take(PageSize);
    }

    public void Update(Article article)
    {
        _dbContext.Articles.Update(article);
    }
}
