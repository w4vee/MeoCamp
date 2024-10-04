using MeoCamp.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories.Interface
{
    public interface IOrderRepository
    {
        public Task CreateOrder(Order order);

        public Task<Order> GetOrderDetailByIdAsync(int id);
        
    }
}
