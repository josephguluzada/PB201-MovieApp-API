namespace PB201MovieApp.Business.DTOs.CommentDtos;

public record CommentCreateDto(string Content, string AppUserId, int MovieId);
