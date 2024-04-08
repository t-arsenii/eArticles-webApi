using eArticles.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Data;

public class eArticlesDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<ArticleTag> ArticleTags => Set<ArticleTag>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<ArticleType> ArticleTypes => Set<ArticleType>();

    public eArticlesDbContext(DbContextOptions<eArticlesDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Article>()
            .HasMany(e => e.Tags)
            .WithMany(e => e.Articles)
            .UsingEntity<ArticleTag>();

        modelBuilder
            .Entity<Tag>()
            .HasIndex(t => t.Title).IsUnique();

        // modelBuilder
        //     .Entity<User>()
        //     .HasMany(user => user.Articles)
        //     .WithOne(article => article.User)
        //     .HasForeignKey(article => article.UserId);

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
