using AutoService_DI.DTOs;
using FluentValidation;

namespace AutoService_DI.Validators;

public class CarDtoValidator : AbstractValidator<CarDto>
{
    public CarDtoValidator()
    {
        RuleFor(x => x.Plate)
            .NotEmpty().WithMessage("სანომრე ნიშანი სავალდებულოა")
            .Matches(@"^[A-Z]{2}-\d{3}-[A-Z]{2}$").WithMessage("სანომრე ნიშნის ფორმატი: AB-123-CD");

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("მარკა სავალდებულოა")
            .MaximumLength(50).WithMessage("მარკა მაქსიმუმ 50 სიმბოლო");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("მოდელი სავალდებულოა")
            .MaximumLength(50).WithMessage("მოდელი მაქსიმუმ 50 სიმბოლო");

        RuleFor(x => x.Year)
            .Must(year => year >= 1990 && year <= DateTime.Now.Year)
            .WithMessage(_ => $"წელი უნდა იყოს 1990-დან {DateTime.Now.Year}-მდე");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("CustomerId უნდა იყოს 0-ზე მეტი");
    }
}
