using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository;
using MeoCamp.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly MeoCampDBContext _context;

        public ShoppingCartRepository(MeoCampDBContext context)
        {
            _context = context;
        }

        public async Task RemoveAllCartItems(ShoppingCart cart)
        {
            _context.CartItems.RemoveRange(cart.CartItems); // Xóa tất cả CartItem
            cart.CartItems.Clear(); // Làm sạch danh sách CartItems trong giỏ hàng (tùy chọn)
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
        }


        public async Task<ShoppingCart> ClearCart(ShoppingCart cart)
        {
            cart.CartItems.Clear();
            _context.ShoppingCarts.Update(cart);
            await _context.SaveChangesAsync();
            return cart; 
        }


        public async Task<ShoppingCart> GetCartByUserId(int id)
        {
            return await _context.ShoppingCarts
                                 .Include(c => c.CartItems)
                                 .ThenInclude(ci => ci.Product)
                                 .FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<ShoppingCart> UpdateCart(ShoppingCart cart)
        {
            var existingCart = await _context.ShoppingCarts
                .Include(c => c.CartItems) // Bao gồm CartItems để giữ nguyên trạng thái
                .FirstOrDefaultAsync(c => c.Id == cart.Id);

            if (existingCart == null)
            {
                throw new Exception("Shopping cart not found");
            }

            // Cập nhật các thuộc tính của giỏ hàng
            existingCart.UpdatedAt = DateTime.Now;

            // Cập nhật hoặc thêm các CartItem
            foreach (var cartItem in cart.CartItems)
            {
                var existingCartItem = existingCart.CartItems
                    .FirstOrDefault(ci => ci.ProductId == cartItem.ProductId);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity = cartItem.Quantity; // Cập nhật số lượng
                    existingCartItem.AddedAt = cartItem.AddedAt; // Cập nhật thời gian
                }
                else
                {
                    existingCartItem.CartId = existingCart.Id; // Gán lại CartId nếu cần
                    _context.CartItems.Add(cartItem); // Thêm mới
                }
            }

            await _context.SaveChangesAsync();
            return cart;
        }


        public async Task<CartItem> GetCartItemById(int id)
        {
            return await _context.CartItems
                .Include(ci => ci.Product)  // Optional: Load related product if needed
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }

    }
}
