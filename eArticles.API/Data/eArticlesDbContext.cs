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
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Bookmark> Bookmarks => Set<Bookmark>();

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
            .HasIndex(t => t.Title)
            .IsUnique();

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
            .HasForeignKey(article => article.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<Rating>()
            .HasIndex(e => new { e.UserId, e.ArticleId })
            .IsUnique();

        modelBuilder
            .Entity<Comment>()
            .HasOne(e => e.ParentComment)
            .WithMany(e => e.ChildComments)
            .HasForeignKey(e => e.ParentCommentId)
            .IsRequired(false);

        modelBuilder
            .Entity<Bookmark>()
            .HasOne(e => e.Article)
            .WithMany(e => e.Bookmarks)
            .HasForeignKey(e => e.ArticleId);

        modelBuilder
            .Entity<Bookmark>()
            .HasOne(e => e.User)
            .WithMany(e => e.Bookmarks)
            .HasForeignKey(e => e.UserId);

        modelBuilder
            .Entity<Bookmark>()
            .HasIndex(e => new { e.UserId, e.ArticleId })
            .IsUnique();
    }
}
