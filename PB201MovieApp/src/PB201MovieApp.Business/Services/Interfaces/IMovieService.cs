using PB201MovieApp.Business.DTOs.MovieDtos;
using PB201MovieApp.Core.Entities;
using System.Linq.Expressions;

namespace PB201MovieApp.Business.Services.Interfaces;

public interface IMovieService
{
    Task<MovieGetDto> CreateAsync(MovieCreateDto dto);
    Task UpdateAsync(int? id,MovieUpdateDto dto);
    Task DeleteAsync(int id);
    Task<MovieGetDto> GetById(int id);
    Task<ICollection<MovieGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Movie, bool>>? expression = null,  params string[] includes);
    Task<MovieGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes);
}
