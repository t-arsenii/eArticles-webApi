using eArticles.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Data;

public class eArticlesDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<ArticleTag> ArticleTags => Set<ArticleTag>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<ContentType> ContentTypes => Set<ContentType>();
    public DbSet<Category> Categories => Set<Category>();

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
            .Entity<Article>()
            .HasOne(e => e.ContentType)
            .WithMany(e => e.Articles);

        modelBuilder
            .Entity<Article>()
            .HasOne(e => e.Category)
            .WithMany(e => e.Articles);

        modelBuilder
            .Entity<Tag>()
            .HasIndex(t => t.Title).IsUnique();

        modelBuilder
            .Entity<ContentType>()
            .HasIndex(e => e.Title)
            .IsUnique();

        modelBuilder
            .Entity<Category>()
            .HasIndex(e => e.Title)
            .IsUnique();

        modelBuilder
            .Entity<User>()
            .HasMany(user => user.Articles)
            .WithOne(article => article.User)
            .HasForeignKey(article => article.UserId);
    }
}
