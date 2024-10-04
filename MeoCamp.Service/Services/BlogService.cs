using MeoCamp.Data.Models;
using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository;
using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MeoCamp.Service.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly GenericRepository<Blog> _genericRepo;

        public BlogService(IBlogRepository blogRepository, IUserRepository userRepository,GenericRepository<Blog> genericRepo )
        {
            _blogRepository = blogRepository;
            _genericRepo = genericRepo;
            _userRepository = userRepository;
        }
        // tao Blog
        public async Task<Blog> CreateBlog(int userId, BlogModel model)
        {
            try
            {
                var user = await _userRepository.GetUserByUserIdAsync(userId);
                if (user == null)
                {
                    // throw new KeyNotFoundException("Người dùng này ko tồn tại");
                    return null;
                }
                var blog = new Blog
                {
                    CustomerId = userId,
                    Title = model.title,
                    Content = model.content,
                    Post_date = DateTime.Now,
                    Image = model.image
                };

                await _blogRepository.CreateBlog(blog);

                return blog;
            }
            catch (Exception ex)
            {
                throw new Exception("User not found.");
                return null;
            }
        }

        // xoa Blog
        public async Task<bool> DeleteBlog(int userId)
        {
            var blog = await _blogRepository.GetBlogbyUserIdAsync(userId);
            if (blog != null)
            {
                await _blogRepository.DeleteBlog(blog);
                return true;
            }
            return false;
        }

        // tra list Blog
        public async Task<List<Blog>> GetAllBlogAsync()
        {
            var blogs = await _genericRepo.GetAllAsync();
            if (blogs == null || !blogs.Any())
            {
                throw new Exception("No blogs found");
            }
            return blogs;
        }

        // tim chu nhan cua Blog
        public async Task<Blog> GetBlogbyUserIdAsync(int userId)
        {
            return await _blogRepository.GetBlogbyUserIdAsync(userId);
        }

        // cap nhat Blog
        public async Task<Blog> UpdateBlog(int userId, BlogModel model)
        {
            var blog = await _blogRepository.GetBlogbyUserIdAsync(userId);
            if (blog != null)
            {
                if (model.title != null || model.title != " ")
                {
                    blog.Title = model.title;
                }
                else if (model.content != null || model.content != " ")
                {
                    blog.Content = model.content;
                }
                else if (model.image != null)
                {
                    blog.Image = model.image;
                }
            }

            await _blogRepository.UpdateBlog(blog);

            return blog;
        }
    }
}

