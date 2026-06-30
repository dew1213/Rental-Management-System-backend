using RentalManagement.Domain.Common;

namespace RentalManagement.Domain.Entities;

public class Tenant : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
