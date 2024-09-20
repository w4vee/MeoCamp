
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

            // Add services to the container.
            builder.Services.AddScoped<GenericRepository<Product>>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddControllers();


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
