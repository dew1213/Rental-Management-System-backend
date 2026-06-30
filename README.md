# Rental Management System

ระบบจัดการอสังหาริมทรัพย์ให้เช่า พัฒนาด้วย ASP.NET Core 8 ตาม Clean Architecture

## Tech Stack
- **Backend:** ASP.NET Core 8 Web API
- **ORM:** Entity Framework Core 8
- **Database:** PostgreSQL
- **Auth:** JWT Bearer Token
- **Architecture:** Clean Architecture, Repository Pattern, Unit of Work
- **Validation:** FluentValidation
- **Mapping:** AutoMapper
- **Docs:** Swagger / OpenAPI
- **Containerization:** Docker + Docker Compose
- **Testing:** xUnit, Moq, FluentAssertions

## Project Structure
```
src/
├── RentalManagement.Domain/         # Entities, Interfaces, Enums
├── RentalManagement.Application/    # DTOs, Services, Validators, Mappings
├── RentalManagement.Infrastructure/ # DbContext, Repositories, JWT
└── RentalManagement.API/            # Controllers, Middleware, Program.cs
tests/
└── RentalManagement.UnitTests/      # xUnit tests
```

## Getting Started

### With Docker
```bash
docker-compose up --build
```
API จะรันที่ http://localhost:8080  
Swagger UI: http://localhost:8080/swagger

### Without Docker
1. อัปเดต connection string ใน `appsettings.Development.json`
2. รัน migration:
```bash
cd src/RentalManagement.API
dotnet ef migrations add InitialCreate --project ../RentalManagement.Infrastructure
dotnet ef database update
```
3. รัน API:
```bash
dotnet run
```

## API Endpoints

| Method | Endpoint | Role |
|--------|----------|------|
| POST | /api/auth/admin/login | Public |
| POST | /api/auth/tenant/login | Public |
| GET/POST | /api/houses | Admin |
| PUT/DELETE | /api/houses/{id} | Admin |
| POST | /api/houses/{id}/image | Admin |
| GET/POST | /api/tenants | Admin |
| GET | /api/payments/overdue | Admin |
| POST | /api/payments/{id}/pay | Admin |
| POST | /api/maintenance | Tenant |
| GET | /api/maintenance | Admin |

## Running Tests
```bash
dotnet test tests/RentalManagement.UnitTests
```
