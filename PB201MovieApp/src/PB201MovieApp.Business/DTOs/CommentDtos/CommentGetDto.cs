namespace PB201MovieApp.Business.DTOs.CommentDtos;

public record CommentGetDto(int Id, string Content, string AppUserUserName,string AppUserId, int MovieId);

