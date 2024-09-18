using FluentValidation;

namespace PB201MovieApp.Business.DTOs.UserDtos;

public record UserLoginDto(string Username, string Password, bool RememberMe);

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x=>x.Username).NotEmpty().NotNull().MaximumLength(50).MinimumLength(3);
        RuleFor(x => x.Password).NotNull().NotEmpty();
        RuleFor(x=>x.RememberMe).NotNull();
    }
}
