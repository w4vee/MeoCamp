
using MeoCamp.Data.Repositories;
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

            // Add services to the container.
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
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
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
