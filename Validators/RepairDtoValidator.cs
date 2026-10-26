using AutoService_DI.DTOs;
using FluentValidation;

namespace AutoService_DI.Validators;

public class RepairDtoValidator : AbstractValidator<RepairDto>
{
    public RepairDtoValidator()
    {
        RuleFor(x => x.CarId)
            .GreaterThan(0).WithMessage("CarId უნდა იყოს 0-ზე მეტი");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("აღწერა სავალდებულოა")
            .MaximumLength(500).WithMessage("აღწერა მაქსიმუმ 500 სიმბოლო");

        RuleFor(x => x.TotalCost)
            .GreaterThanOrEqualTo(0).WithMessage("ღირებულება არ შეიძლება იყოს უარყოფითი");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("სტატუსი უნდა იყოს: Pending, InProgress ან Done");

        RuleFor(x => x.MechanicIds)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("მინიმუმ ერთი მექანიკოსი უნდა მიუთითოთ")
            .Must(ids => ids.Distinct().Count() == ids.Count).WithMessage("MechanicIds არ უნდა შეიცავდეს დუბლიკატებს");

        RuleForEach(x => x.MechanicIds)
            .GreaterThan(0).WithMessage("მექანიკოსის Id უნდა იყოს 0-ზე მეტი");
    }
}
