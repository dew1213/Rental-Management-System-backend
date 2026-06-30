namespace RentalManagement.Application.DTOs.Tenant;

public record TenantDto(int Id, string FirstName, string LastName, string Email, string Phone, DateTime CreatedAt);
public record CreateTenantRequest(string FirstName, string LastName, string Email, string Phone, string Password);
public record UpdateTenantRequest(string FirstName, string LastName, string Phone);
