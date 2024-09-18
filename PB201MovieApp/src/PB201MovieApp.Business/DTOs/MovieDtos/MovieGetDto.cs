using PB201MovieApp.Business.DTOs.CommentDtos;
using PB201MovieApp.Business.DTOs.MovieImageDtos;
using PB201MovieApp.Core.Entities;

namespace PB201MovieApp.Business.DTOs.MovieDtos;

public record MovieGetDto(int Id, string Title, string Desc, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate, int GenreId, string GenreName, ICollection<MovieImageGetDto> MovieImages, ICollection<CommentGetDto> Comments);

