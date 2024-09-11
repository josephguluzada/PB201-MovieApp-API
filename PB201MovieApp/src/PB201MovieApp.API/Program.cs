using FluentValidation.AspNetCore;
using PB201MovieApp.Business;
using PB201MovieApp.Business.DTOs.MovieDtos;
using PB201MovieApp.Business.MappingProfiles;
using PB201MovieApp.DAL;
namespace PB201MovieApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssembly(typeof(MovieCreateDtoValidator).Assembly);
            });

            builder.Services.AddAutoMapper(opt =>
            {
                opt.AddProfile<MapProfile>();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddRepositories(builder.Configuration.GetConnectionString("default"));
            builder.Services.AddServices();
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
