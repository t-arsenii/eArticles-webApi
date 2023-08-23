using Microsoft.EntityFrameworkCore;
using GamingBlog.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GamingBlog.API.Data;

public class GamingBlogDbContext : IdentityUserContext<User>
{
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<ArticleTag> ArticleTags => Set<ArticleTag>();
    public DbSet<Tag> Tags => Set<Tag>();

    public GamingBlogDbContext(DbContextOptions<GamingBlogDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Article>().Property(a => a.Article_type).HasConversion<string>();

        modelBuilder
            .Entity<Article>()
            .HasMany(e => e.Tags)
            .WithMany(e => e.Articles)
            .UsingEntity<ArticleTag>();

        modelBuilder
            .Entity<User>()
            .HasMany(user => user.Articles)
            .WithOne(article => article.User)
            .HasForeignKey(article => article.UserId);

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
