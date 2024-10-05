using MeoCamp.Data.Models;
using MeoCamp.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories.Interface
{
    public interface IRevenueRepository
    {
        public Task<Revenue> GetTotalOrdersAsync();
        public Task<Revenue> GetTotalRevenuesAsync();
        public Task<Revenue> UpdateRevenue(Revenue revenue);
        public Task<bool> DeleteBlog(Blog blog);
    }
}
