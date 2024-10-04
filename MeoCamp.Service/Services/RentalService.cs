using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeoCamp.Data.Models;
using MeoCamp.Data.Repositories;
using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository;
using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services;
using MeoCamp.Service.Services.Interface;

namespace MeoCamp.Service.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        GenericRepository<Rental> _genericRepo;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
            _genericRepo ??= new GenericRepository<Rental>();
        }
        private double CalculateTotalPrice(Product product, DateTime startDate, DateTime? endDate)
        {
            // Nếu không có ngày kết thúc thì mặc định thời gian thuê là 1 ngày
            if (!endDate.HasValue)
            {
                return product.Price; // Tính 1 ngày thuê
            }

            // Tính tổng số giờ thuê
            var rentalDuration = endDate.Value - startDate;

            // Tính số ngày, làm tròn lên để đảm bảo dù thuê chưa tròn 1 ngày vẫn tính trọn cả ngày
            int totalDays = (int)Math.Ceiling(rentalDuration.TotalDays);

            // Tính tổng giá dựa trên số ngày và giá mỗi ngày
            return product.Price * totalDays;
        }
        public async Task<Rental> CreateRental(int userId, RentalModel model)
        {
            try
            {
                var user = await _userRepository.GetUserByUserIdAsync(userId);
                var product = await _productRepository.GetProductById(model.ProductId);
                if (user == null || product == null)
                {
                    // throw new KeyNotFoundException("Người dùng này ko tồn tại");
                    return null;
                }

                var totalPrice = CalculateTotalPrice(product, model.RentalStartDate, model.RentalEndDate);

                var rental = new Rental
                {
                    CustomerId = userId,
                    ProductId = model.ProductId,
                    RentalStartDate = model.RentalStartDate,
                    RentalEndDate = model.RentalEndDate,
                    Description = model.Description,
                    TotalPrice = totalPrice,
                    RentalStatus = model.RentalStatus,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                await _rentalRepository.CreateRental(rental);

                return rental;
            }
            catch (Exception ex)
            {
                throw new Exception("User not found.");
                return null;
            }
        }

        public async Task<bool> DeleteRental(int userId)
        {
            var rental = await _rentalRepository.GetRentalbyUserIdAsync(userId);
            if (rental != null)
            {
                await _rentalRepository.DeleteRental(rental);
                return true;
            }
            return false;
        }

        public async Task<List<Rental>> GetAllRentalAsync()
        {
            try
            {
                var List = await _rentalRepository.GetAllRentalAsync();
                return List;
            }
            catch (Exception ex)
            {
                throw new Exception("List rong");
            }
        }

        public async Task<Rental> GetRentalbyUserIdAsync(int userId)
        {
            return await _rentalRepository.GetRentalbyUserIdAsync(userId);
        }

        public async Task<Rental> UpdateRental(int userId, RentalModel model)
        {
            try
            {
                var user = await _userRepository.GetUserByUserIdAsync(userId);
                var rental = await _rentalRepository.GetRentalbyUserIdAsync(userId);
                if (user == null && rental == null)
                {
                    // throw new KeyNotFoundException("Người dùng này ko tồn tại");
                    return null;
                }

                if (model.RentalStartDate != null && model.RentalStartDate > rental.RentalStartDate)
                {
                    rental.RentalStartDate = model.RentalStartDate;
                }

                if (model.RentalEndDate != null && model.RentalEndDate > rental.RentalStartDate)
                {
                    if (model.RentalEndDate.HasValue && model.RentalEndDate < model.RentalStartDate)
                    {
                        throw new Exception("Rental end date must be greater than or equal to rental start date.");
                    }
                    else
                    {
                        rental.RentalEndDate = model.RentalEndDate;
                    }
                }

                var product = await _productRepository.GetProductById(rental.ProductId);
                var totalPrice = CalculateTotalPrice(product, model.RentalStartDate, model.RentalEndDate);
                rental.TotalPrice = totalPrice;
                
                if (model.Description != null)
                {
                    rental.Description = model.Description;
                }

                if (model.RentalStatus != null)
                {
                    rental.RentalStatus = model.RentalStatus;
                }
                
                rental.UpdatedAt = DateTime.UtcNow;

                await _rentalRepository.UpdateRental(rental);

                return rental;
            }
            catch (Exception ex)
            {
                throw new Exception("User not found.");
                return null;
            }
        }
    }
}

