using GamingBlog.API.Models;

namespace GamingBlog.API.Data.Repositories;

public interface IArticlesRepository
{
    public void Create();
    public Article Read();
    public void Update();
    public void Delete();
}
