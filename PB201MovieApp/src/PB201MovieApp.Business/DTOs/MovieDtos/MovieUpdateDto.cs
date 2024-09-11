using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace PB201MovieApp.Business.DTOs.MovieDtos;

public record MovieUpdateDto(string Title, string Desc, bool isDeleted, int GenreId, IFormFile? ImageFile);


public class MovieUpdateDtoValidator : AbstractValidator<MovieUpdateDto>
{
    public MovieUpdateDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Not empty")
            .NotNull().WithMessage("Not null")
            .MinimumLength(1).WithMessage("Min length must be 1")
            .MaximumLength(200).WithMessage("Length must be less than 200");

        RuleFor(x => x.Desc)
                .NotNull().When(x => !x.isDeleted).WithMessage("If movie is active desc cannot be null")
                .MaximumLength(800).WithMessage("Length must be less than 800");

        RuleFor(x => x.isDeleted).NotNull();


        RuleFor(x => x.GenreId).NotEmpty().NotNull();

        RuleFor(x => x.ImageFile)
            .Must(x => x.ContentType == "image/png" || x.ContentType == "image/jpeg")
            .WithMessage("Image's content type must png/jpeg")
            .Must(x => x.Length < 2 * 1024 * 1024);
        //RuleFor(x => x).Custom((x, context) =>
        //{
        //    if (x.SalePrice < x.CostPrice)
        //    {
        //        context.AddFailure(nameof(x.SalePrice), "SalePrice must be greater than CostPrice");
        //    }
        //});
    }
}