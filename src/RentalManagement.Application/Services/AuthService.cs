
using Microsoft.Extensions.Configuration;
using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.Auth;
using RentalManagement.Application.Services.Interfaces;
using RentalManagement.Domain.Entities;
using RentalManagement.Domain.Interfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RentalManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    // ================= ADMIN LOGIN =================
    public async Task<Result<LoginResponse>> AdminLoginAsync(LoginRequest request)
    {
        var admin = await _unitOfWork.Admins
            .FindAsync(a => a.Email == request.Email);

        var user = admin.FirstOrDefault();

        if (user == null)
            return Result<LoginResponse>.Failure("Admin not found");

        if (!VerifyPassword(request.Password, user.PasswordHash))
            return Result<LoginResponse>.Failure("Invalid password");

        var token = GenerateToken(user.Email, "Admin", user.Username);

        return Result<LoginResponse>.Success(
            new LoginResponse(token, "Admin", user.Username)
        );
    }

    // ================= TENANT LOGIN =================
    public async Task<Result<LoginResponse>> TenantLoginAsync(LoginRequest request)
    {
        var tenants = await _unitOfWork.Tenants
            .FindAsync(t => t.Email == request.Email);

        var user = tenants.FirstOrDefault();

        if (user == null)
            return Result<LoginResponse>.Failure("Tenant not found");

        if (!VerifyPassword(request.Password, user.PasswordHash))
            return Result<LoginResponse>.Failure("Invalid password");

        var token = GenerateToken(user.Email, "Tenant", user.FirstName);

        return Result<LoginResponse>.Success(
            new LoginResponse(token, "Tenant", user.FirstName)
        );
    }

    // ================= JWT =================
    private string GenerateToken(string email, string role, string name)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Name, name)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // ================= PASSWORD CHECK =================
    private bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash); 
    }
}