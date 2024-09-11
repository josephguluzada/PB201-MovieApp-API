using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PB201MovieApp.Business.DTOs.MovieDtos;
using PB201MovieApp.Business.Exceptions.CommonExceptions;
using PB201MovieApp.Business.Services.Interfaces;
using PB201MovieApp.Business.Utilities;
using PB201MovieApp.Core.Entities;
using PB201MovieApp.Core.Repositories;
using System.Linq.Expressions;

namespace PB201MovieApp.Business.Services.Implementations;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;
    private readonly IGenreService _genreService;
    private readonly IWebHostEnvironment _env;

    public MovieService(IMovieRepository movieRepository, IMapper mapper, IGenreService genreService, IWebHostEnvironment env)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
        _genreService = genreService;
        _env = env;
    }

    public async Task<MovieGetDto> CreateAsync(MovieCreateDto dto)
    {
        if (!await _genreService.IsExistAsync(x => x.Id == dto.GenreId && x.IsDeleted == false)) throw new EntityNotFoundException();
        Movie movie = _mapper.Map<Movie>(dto);
        
        string imageUrl = dto.ImageFile.SaveFile(_env.WebRootPath, "Uploads");
        movie.MovieImages = new List<MovieImage>(); 
        MovieImage movieImage = new MovieImage()
        {
            ImageUrl = imageUrl,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            IsDeleted = false
        };

        movie.MovieImages.Add(movieImage);
        movie.CreatedDate = DateTime.Now;
        movie.ModifiedDate = DateTime.Now;
        await _movieRepository.CreateAsync(movie);
        await _movieRepository.CommitAsync();
        MovieGetDto getDto = _mapper.Map<MovieGetDto>(movie);

        return getDto;
    }
    public async Task DeleteAsync(int id)
    {
        if (id < 1) throw new InvalidIdException();
        var data = await _movieRepository.GetByIdAsync(id);
        if(data is null) throw new EntityNotFoundException(404,"EntityNotFound");
        _movieRepository.Delete(data);
        await _movieRepository.CommitAsync();
    }

    public async Task<ICollection<MovieGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes)
    {
        var datas = await _movieRepository.GetByExpression(asNoTracking, expression, includes).ToListAsync();

        ICollection<MovieGetDto> dtos = _mapper.Map<ICollection<MovieGetDto>>(datas);

        return dtos;
    }

    public async Task<MovieGetDto> GetById(int id)
    {
        if (id < 1) throw new InvalidIdException();
        var data = await _movieRepository.GetByIdAsync(id);

        if (data is null) throw new EntityNotFoundException(404, "EntityNotFound");

        //MovieGetDto dto = new MovieGetDto(data.Id, data.Title, data.Desc, data.IsDeleted, data.CreatedDate, data.ModifiedDate);

        MovieGetDto dto = _mapper.Map<MovieGetDto>(data);

        return dto;
    }

    public async Task<MovieGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes)
    {
        var data = await _movieRepository.GetByExpression(asNoTracking, expression, includes).FirstOrDefaultAsync();
        if (data is null) throw new EntityNotFoundException(404, "EntityNotFound");

        MovieGetDto dto = _mapper.Map<MovieGetDto>(data);

        return dto;
    }

    public async Task UpdateAsync(int? id,MovieUpdateDto dto)
    {
        if(!await _genreService.IsExistAsync(x => x.Id == dto.GenreId && x.IsDeleted == false)) throw new EntityNotFoundException();
        if (id<1 || id is null) throw new InvalidIdException();

        var data = await _movieRepository.GetByIdAsync((int)id);

        if (data is null) throw new EntityNotFoundException();

        string imageUrl = dto.ImageFile.SaveFile(_env.WebRootPath, "Uploads");
        data.MovieImages = new List<MovieImage>();
        MovieImage movieImage = new MovieImage()
        {
            ImageUrl = imageUrl,
            ModifiedDate = DateTime.Now,
            IsDeleted = false
        };

        data.MovieImages.Add(movieImage);

        _mapper.Map(dto,data);

        data.ModifiedDate = DateTime.Now;

        await _movieRepository.CommitAsync();
    }
}
