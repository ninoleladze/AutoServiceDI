using AutoService_DI.DTOs;
using FluentValidation;

namespace AutoService_DI.Validators;

public class MechanicDtoValidator : AbstractValidator<MechanicDto>
{
    public MechanicDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("სახელი სავალდებულოა")
            .Length(3, 100).WithMessage("სახელი უნდა იყოს 3-100 სიმბოლო");

        RuleFor(x => x.ExperienceYears)
            .InclusiveBetween(0, 60).WithMessage("გამოცდილება უნდა იყოს 0-60 წელი");
    }
}
