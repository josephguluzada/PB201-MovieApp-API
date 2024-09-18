using PB201MovieApp.Business.DTOs.CommentDtos;
using PB201MovieApp.Core.Entities;
using System.Linq.Expressions;

namespace PB201MovieApp.Business.Services.Interfaces;

public interface ICommentService
{
    Task CreateAsync(CommentCreateDto dto);
    Task UpdateAsync(int? id, CommentUpdateDto dto);
    Task DeleteAsync(int id);
    Task<CommentGetDto> GetById(int id);
    Task<ICollection<CommentGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Comment, bool>>? expression = null, params string[] includes);
    Task<CommentGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Comment, bool>>? expression = null, params string[] includes);
}
