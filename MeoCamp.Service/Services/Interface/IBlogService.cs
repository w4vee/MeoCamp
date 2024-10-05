using MeoCamp.Data.Models;
using MeoCamp.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.Services.Interface
{
    public interface IBlogService
    {
        public Task<List<Blog>> GetAllBlogAsync();
        public Task<List<Blog>> GetBlogbyUserIdAsync(int userId);
        public Task<bool> ApproveBlog(int blogId);
        public Task<Blog> CreateBlog(int userId, BlogModel model);
        public Task<Blog> UpdateBlog(int userId, BlogModel model);
        public Task<bool> DeleteBlog(int userId);
    }
}
