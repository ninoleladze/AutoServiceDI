using AutoService_DI.DTOs;
using FluentValidation;

namespace AutoService_DI.Validators;

public class TechInspectionDtoValidator : AbstractValidator<TechInspectionDto>
{
    public TechInspectionDtoValidator()
    {
        RuleFor(x => x.IssueDate)
            .NotEmpty().WithMessage("გაცემის თარიღი სავალდებულოა")
            .Must(d => d.Date <= DateTime.Today).WithMessage("გაცემის თარიღი არ უნდა იყოს მომავალში");

        RuleFor(x => x.ExpiryDate)
            .GreaterThan(x => x.IssueDate).WithMessage("ვადის გასვლის თარიღი უნდა იყოს გაცემის თარიღზე გვიან");
    }
}
