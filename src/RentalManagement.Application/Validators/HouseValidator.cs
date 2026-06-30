using FluentValidation;
using RentalManagement.Application.DTOs.House;

namespace RentalManagement.Application.Validators;

public class CreateHouseValidator : AbstractValidator<CreateHouseRequest>
{
    public CreateHouseValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(500);
        RuleFor(x => x.MonthlyRent).GreaterThan(0);
    }
}
