using PB201MovieApp.Core.Entities;
using PB201MovieApp.Core.Repositories;
using PB201MovieApp.DAL.Contexts;

namespace PB201MovieApp.DAL.Repositories;

public class MovieRepository : GenericRepository<Movie>, IMovieRepository
{
    public MovieRepository(AppDbContext context) : base(context){ }
}
