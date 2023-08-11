using Microsoft.EntityFrameworkCore;
using GamingBlog.API.Models;
namespace GamingBlog.API.Data;

public class GamingBlogDbContext : DbContext
{
    DbSet<Article> Articles => Set<Article>();
    DbSet<ArticleTag> ArticleTags => Set<ArticleTag>();
    DbSet<Tag> Tags => Set<Tag>();
    public GamingBlogDbContext(DbContextOptions<DbContext> options): base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArticleTag>()
        .HasKey(at => new { at.ArticleId, at.TagId});
        
        modelBuilder.Entity<ArticleTag>()
        .HasOne(at => at._Article)
        .WithMany(at => at.ArticleTags)
        .HasForeignKey(at => at.ArticleId);

        modelBuilder.Entity<ArticleTag>()
        .HasOne(at => at._Tag)
        .WithMany(at => at.ArticleTags)
        .HasForeignKey(at => at.TagId);
    }
    
}
