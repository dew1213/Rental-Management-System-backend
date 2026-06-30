using AutoMapper;
using RentalManagement.Application.DTOs.House;
using RentalManagement.Application.DTOs.Tenant;
using RentalManagement.Application.DTOs.Contract;
using RentalManagement.Application.DTOs.Payment;
using RentalManagement.Application.DTOs.Maintenance;
using RentalManagement.Domain.Entities;

namespace RentalManagement.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<House, HouseDto>();
        CreateMap<CreateHouseRequest, House>();
        CreateMap<UpdateHouseRequest, House>();

        CreateMap<Tenant, TenantDto>();
        CreateMap<CreateTenantRequest, Tenant>().ForMember(d => d.PasswordHash, opt => opt.Ignore());

        CreateMap<Contract, ContractDto>()
            .ForMember(d => d.HouseName, opt => opt.MapFrom(s => s.House.Name))
            .ForMember(d => d.TenantName, opt => opt.MapFrom(s => $"{s.Tenant.FirstName} {s.Tenant.LastName}"));

        CreateMap<Payment, PaymentDto>();
        CreateMap<MaintenanceRequest, MaintenanceDto>();
    }
}
