using FluentValidation;
using RentalManagement.Application.DTOs.Tenant;

namespace RentalManagement.Application.Validators;

public class CreateTenantValidator : AbstractValidator<CreateTenantRequest>
{
    public CreateTenantValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Phone).NotEmpty().Matches(@"^\d{10}$").WithMessage("Phone must be 10 digits");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}
