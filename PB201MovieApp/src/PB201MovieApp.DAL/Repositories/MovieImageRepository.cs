using PB201MovieApp.Core.Entities;
using PB201MovieApp.Core.Repositories;
using PB201MovieApp.DAL.Contexts;

namespace PB201MovieApp.DAL.Repositories;

public class MovieImageRepository : GenericRepository<MovieImage>, IMovieImageRepository
{
    public MovieImageRepository(AppDbContext context) : base(context)
    {
    }
}
