namespace PB201MovieApp.Business.DTOs.MovieDtos;

public record MovieGetDto(int Id, string Title, string Desc, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate, int GenreId, string GenreName);

