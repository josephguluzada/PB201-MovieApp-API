using FluentValidation;

namespace PB201MovieApp.Business.DTOs.GenreDtos;

public record GenreCreateDto(string Name, bool IsDeleted);

public class GenreCreateDtoValidator : AbstractValidator<GenreCreateDto>
{
    public GenreCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50).MinimumLength(2);
        RuleFor(x => x.IsDeleted).NotNull();
    }
}
