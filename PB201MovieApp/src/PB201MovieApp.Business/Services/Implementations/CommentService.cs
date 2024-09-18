using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PB201MovieApp.Business.DTOs.CommentDtos;
using PB201MovieApp.Business.Exceptions.CommonExceptions;
using PB201MovieApp.Business.Services.Interfaces;
using PB201MovieApp.Core.Entities;
using PB201MovieApp.Core.Repositories;
using System.Linq.Expressions;

namespace PB201MovieApp.Business.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository,
                          IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CommentCreateDto dto)
    {
        var data = _mapper.Map<Comment>(dto);

        await _commentRepository.CreateAsync(data);
        await _commentRepository.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id < 1) throw new InvalidIdException();

        var data = await _commentRepository.GetByIdAsync(id);

        if (data is null) throw new EntityNotFoundException();

        _commentRepository.Delete(data);
        await _commentRepository.CommitAsync();
    }

    public async Task<ICollection<CommentGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Comment, bool>>? expression = null, params string[] includes)
        => _mapper.Map<ICollection<CommentGetDto>>(await _commentRepository.GetByExpression(asNoTracking,expression,includes).ToListAsync());
    

    public async Task<CommentGetDto> GetById(int id)
    {
        if (id < 1) throw new InvalidIdException();

        var data = await _commentRepository.GetByIdAsync(id);

        if (data is null) throw new EntityNotFoundException();

        return _mapper.Map<CommentGetDto>(data);
    }

    public async Task<CommentGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Comment, bool>>? expression = null, params string[] includes)
    {
        var data = await _commentRepository.GetByExpression(asNoTracking,expression,includes).FirstOrDefaultAsync();

        if (data is null) throw new EntityNotFoundException();

        return _mapper.Map<CommentGetDto>(data);
    }

    public async Task UpdateAsync(int? id, CommentUpdateDto dto)
    {
        if (id < 1) throw new InvalidIdException();
        var data = await _commentRepository.GetByIdAsync((int)id);

        if (data is null) throw new EntityNotFoundException();

        _mapper.Map(dto, data);

        data.ModifiedDate = DateTime.Now;

        await _commentRepository.CommitAsync();
    }
}
