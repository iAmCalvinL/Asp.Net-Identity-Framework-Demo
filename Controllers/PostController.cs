using identity_demo.Dto;
using identity_demo.Interfaces;
using identity_demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace identity_demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRespository _postRepository;

        public PostController(IPostRespository postRespository)
        {
            _postRepository = postRespository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BlogPost>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetBlogPosts()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            List<BlogPost> posts = await _postRepository.GetBlogPosts();

            return Ok(posts);
        }

        [HttpGet("{blogPostId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBlogPost(int blogPostId)
        {
            if (!ModelState.IsValid) return BadRequest();

            BlogPost? post = await _postRepository.GetBlogPost(blogPostId);

            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateBlogPost([FromBody] BlogPostDto blogPostDto)
        {
            if (blogPostDto == null) return BadRequest(ModelState);

            BlogPost newBlogPost = new()
            {
                Date = new DateOnly(
                    blogPostDto.Date.Year,
                    blogPostDto.Date.Month,
                    blogPostDto.Date.Day
                ),
                Text = blogPostDto.Text
            };

            if (!await _postRepository.CreateBlogPost(newBlogPost))
            {
                ModelState.AddModelError("", "Error Creating Blog Post");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateBlogPost([FromBody] UpdateBlogPostDto updateBlogPostDto)
        {
            if (updateBlogPostDto == null) return BadRequest(ModelState);
            if (!await _postRepository.BlogPostExists(updateBlogPostDto.Id)) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            BlogPost updatedBlogPost = new()
            {
                Date = new DateOnly(
                    updateBlogPostDto.Date.Year,
                    updateBlogPostDto.Date.Month,
                    updateBlogPostDto.Date.Day
                ),
                Text = updateBlogPostDto.Text
            };

            if (!await _postRepository.UpdateBlogPost(updateBlogPostDto.Id, updatedBlogPost))
            {
                ModelState.AddModelError("", "Could not update blog post");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteBlogPost(int blogPostId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _postRepository.BlogPostExists(blogPostId)) return NotFound();

            if (!await _postRepository.DeleteBlogPost(blogPostId))
            {
                ModelState.AddModelError("", "Failed to delete blog post");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
