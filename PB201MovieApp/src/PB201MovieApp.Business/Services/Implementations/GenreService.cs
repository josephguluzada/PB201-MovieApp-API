using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PB201MovieApp.Business.DTOs.GenreDtos;
using PB201MovieApp.Business.Exceptions.CommonExceptions;
using PB201MovieApp.Business.Exceptions.GenreExceptions;
using PB201MovieApp.Business.Services.Interfaces;
using PB201MovieApp.Core.Entities;
using PB201MovieApp.Core.Repositories;
using System.Linq.Expressions;

namespace PB201MovieApp.Business.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public GenreService(IGenreRepository genreRepository,IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(GenreCreateDto dto)
    {
        if(await _genreRepository.Table.AnyAsync(x=>x.Name.Trim().ToLower() == dto.Name.Trim().ToLower())) throw new GenreAlreadyExistException(StatusCodes.Status400BadRequest,"Name","Genre already exists");
        Genre data = _mapper.Map<Genre>(dto);

        data.CreatedDate = DateTime.Now;
        data.ModifiedDate = DateTime.Now;

        await _genreRepository.CreateAsync(data);
        await _genreRepository.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 1) throw new InvalidIdException();

        var data = await _genreRepository.GetByIdAsync(id);

        if(data is null) throw new EntityNotFoundException(404, "Genre not found");

        _genreRepository.Delete(data);
        await _genreRepository.CommitAsync();
    }

    public async Task<ICollection<GenreGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes)
    {
        var datas = await _genreRepository.GetByExpression(asNoTracking, expression, includes).ToListAsync();

        return _mapper.Map<ICollection<GenreGetDto>>(datas);
    }

    public async Task<GenreGetDto> GetById(int id)
    {
        if(id<1) throw new InvalidIdException("Id yanlishhhhdir");

        var data = await _genreRepository.GetByIdAsync(id);

        if (data is null) throw new EntityNotFoundException(404,"Genre not found!");

        return _mapper.Map<GenreGetDto>(data);
    }

    public async Task<GenreGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes)
    {
        var data = await _genreRepository.GetByExpression(asNoTracking,expression, includes).FirstOrDefaultAsync();

        return _mapper.Map<GenreGetDto>(data);
    }

    public async Task<bool> IsExistAsync(Expression<Func<Genre, bool>>? expression = null)
    {
        return await _genreRepository.Table.AnyAsync(expression);
    }

    public async Task UpdateAsync(int id, GenreUpdateDto dto)
    {
        if (id < 1) throw new InvalidIdException();
        if (await _genreRepository.Table.AnyAsync(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower())) throw new GenreAlreadyExistException(StatusCodes.Status400BadRequest, "Name", "Genre already exists");
        var data = await _genreRepository.GetByIdAsync(id);

        if (data is null) throw new EntityNotFoundException();

        _mapper.Map(dto, data);

        data.ModifiedDate = DateTime.Now;

        await _genreRepository.CommitAsync();
    }
}
