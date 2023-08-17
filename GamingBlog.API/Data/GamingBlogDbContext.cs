using Microsoft.EntityFrameworkCore;
using GamingBlog.API.Models;
namespace GamingBlog.API.Data;

public class GamingBlogDbContext : DbContext
{
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<ArticleTag> ArticleTags => Set<ArticleTag>();
    public DbSet<Tag> Tags => Set<Tag>();
    public GamingBlogDbContext(DbContextOptions<GamingBlogDbContext> options): base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
        .Property(a => a.Article_type)
        .HasConversion<string>();

         modelBuilder.Entity<Article>()
        .HasMany(e => e.Tags)
        .WithMany(e => e.Articles)
        .UsingEntity<ArticleTag>();

        // modelBuilder.Entity<ArticleTag>()
        // .HasKey(at => new { at.ArticleId, at.TagId});
        
        // modelBuilder.Entity<ArticleTag>()
        // .HasOne(at => at.Article)
        // .WithMany(at => at.ArticleTags)
        // .HasForeignKey(at => at.ArticleId);

        // modelBuilder.Entity<ArticleTag>()
        // .HasOne(at => at.Tag)
        // .WithMany(at => at.ArticleTags)
        // .HasForeignKey(at => at.TagId);
    }
    
}