using MeoCamp.Data.Models;
using MeoCamp.Data.Repositories;
using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository;
using MeoCamp.Repository.Models;
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

        public BlogService(IBlogRepository blogRepository, IUserRepository userRepository)
        {
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _genericRepo ??= new GenericRepository<Blog>();
        }
        // duyet Blog
        public async Task<bool> ApproveBlog(int blogId)
        {
            var blog = await _blogRepository.GetBlogbyIdAsync(blogId);
            if (blog != null)
            {
                if (blog.Status == false)
                {
                    blog.Status = true;

                    await _blogRepository.UpdateBlog(blog);
                    return true;

                }
            }

            return false;
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
                    Image = model.image,
                    Status = false
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
        public async Task<bool> DeleteBlog(int Id)
        {
            var blog = await _blogRepository.GetBlogbyIdAsync(Id);
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
            try
            {
                    var List = await _blogRepository.GetAllBlogAsync();
                    return List;
            }
            catch (Exception e)
            {
                throw new Exception("Loi lay list");
            }
        }

        // tim chu nhan cua Blog
        public async Task<List<Blog>> GetBlogbyUserIdAsync(int userId)
        {
            try
            {
                var List = await _blogRepository.GetBlogbyUserIdAsync(userId);
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception("Loi lay list");
            }
        }

        // cap nhat Blog
        public async Task<Blog> UpdateBlog(int Id, BlogModel model)
        {
            var blog = await _blogRepository.GetBlogbyIdAsync(Id);
            if (blog != null)
            {
                if (model.title != null || model.title != blog.Title)
                {
                    blog.Title = model.title;
                }
                
                if (model.content != null || model.content != blog.Content)
                {
                    blog.Content = model.content;
                }
                
                if (model.image != null || model.image != blog.Image)
                {
                    blog.Image = model.image;
                }
            }

            await _blogRepository.UpdateBlog(blog);

            return blog;
        }
    }
}

