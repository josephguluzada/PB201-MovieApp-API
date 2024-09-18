using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PB201MovieApp.Core.Repositories;
using PB201MovieApp.DAL.Contexts;
using PB201MovieApp.DAL.Repositories;

namespace PB201MovieApp.DAL;

public static class ServiceRegistration
{
    public static void AddRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();

        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
    }
}
