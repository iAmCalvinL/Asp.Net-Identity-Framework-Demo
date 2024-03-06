using identity_demo.Models;

namespace identity_demo.Interfaces
{
    public interface IPostRespository
    {
        Task<List<BlogPost>> GetBlogPosts();
        Task<BlogPost?> GetBlogPost(int blogPostId);
        Task<bool> CreateBlogPost(BlogPost blogPost);
        Task<bool> UpdateBlogPost(int blogPostId, BlogPost blogPost);
        Task<bool> DeleteBlogPost(int blogPostId);
        Task<bool> BlogPostExists(int blogPostId);
    }
}