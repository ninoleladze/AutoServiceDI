using AutoService_DI.DTOs;
using FluentValidation;

namespace AutoService_DI.Validators;

public class CustomerDtoValidator : AbstractValidator<CustomerDto>
{
    public CustomerDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("სახელი სავალდებულოა")
            .Length(3, 100).WithMessage("სახელი უნდა იყოს 3-100 სიმბოლო");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("ტელეფონი სავალდებულოა")
            .Matches(@"^5\d{8}$").WithMessage("ტელეფონი უნდა იყოს ფორმატით 5XXXXXXXX (9 ციფრი)");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email სავალდებულოა")
            .EmailAddress().WithMessage("Email-ის ფორმატი არასწორია");
    }
}
