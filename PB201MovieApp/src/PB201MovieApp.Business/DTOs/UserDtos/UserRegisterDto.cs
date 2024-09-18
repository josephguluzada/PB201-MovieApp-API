using FluentValidation;

namespace PB201MovieApp.Business.DTOs.UserDtos;

public record UserRegisterDto(string Fullname, string Username, string Email, string Password, string ConfirmPassword);

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(x=>x.Fullname).NotNull().NotEmpty().MaximumLength(50).MinimumLength(2);
        RuleFor(x=>x.Username).NotNull().NotEmpty().MaximumLength(50).MinimumLength(3);
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).MinimumLength(8).MaximumLength(50);

        RuleFor(x => x).Custom((x, context) =>
        {
            if(x.Password != x.ConfirmPassword)
            {
                context.AddFailure("ConfirmPassword", "Password and ConfirmPassword don`t match");
            }
        });
    }
}

