
using MeoCamp.Data.Models;
using MeoCamp.Data.Repositories;
using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository;
using MeoCamp.Repository.Models;
using MeoCamp.Service.Services;
using MeoCamp.Service.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace MeoCamp
{
    public class Program
    {
        public static void Main(string[] args)
        {   
            

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MeoCampDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()   // Allow any origin
                                      .AllowAnyMethod()   // Allow any HTTP method
                                      .AllowAnyHeader()); // Allow any headers
            });

            //builder.Services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //});
            // Add services to the container.
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IFeedbackRepsitory, FeedbackRepository>();
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IRentalRepository, RentalRepository>();


            // Registering Services with their interfaces
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();
            builder.Services.AddScoped<IBlogService, BlogService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IRentalService, RentalService>();

            // Add generic repository if needed
            builder.Services.AddScoped<GenericRepository<Product>>();
            builder.Services.AddScoped<GenericRepository<Order>>();
            builder.Services.AddScoped<GenericRepository<ShoppingCart>>();
            builder.Services.AddScoped<GenericRepository<Payment>>();
            builder.Services.AddScoped<GenericRepository<Feedback>>();
            builder.Services.AddScoped<GenericRepository<Blog>> ();
            builder.Services.AddScoped<GenericRepository<Category>>();
            builder.Services.AddScoped<GenericRepository<Rental>>();

            builder.Services.AddControllers();
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");  // Apply the CORS policy defined above

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
