using identity_demo.Data;
using identity_demo.Interfaces;
using identity_demo.Models;
using Microsoft.EntityFrameworkCore;

namespace identity_demo.Repository
{
    public class PostRepository : IPostRespository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }

        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateBlogPost(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            return await Save();
        }

        public async Task<bool> DeleteBlogPost(int blogPostId)
        {
            BlogPost? blogPost = await _context.BlogPosts.SingleOrDefaultAsync(bp => bp.Id == blogPostId);

            if (blogPost == null) return false;
            _context.BlogPosts.Remove(blogPost);
            return await Save();
        }

        public async Task<BlogPost?> GetBlogPost(int blogPostId)
        {
            return await _context.BlogPosts.SingleOrDefaultAsync(bp => bp.Id == blogPostId);
        }

        public async Task<List<BlogPost>> GetBlogPosts()
        {
            return await _context.BlogPosts.ToListAsync();
        }

        public async Task<bool> UpdateBlogPost(int blogPostId, BlogPost blogPost)
        {
            BlogPost? existingBlogPost = await _context.BlogPosts.FindAsync(blogPostId);

            if (existingBlogPost == null) return false;

            existingBlogPost.Date = blogPost.Date;
            existingBlogPost.Text = blogPost.Text;

            return await Save();
        }

        public async Task<bool> BlogPostExists(int blogPostId)
        {
            return await _context.BlogPosts.AnyAsync(bp => bp.Id == blogPostId);
        }
    }
}