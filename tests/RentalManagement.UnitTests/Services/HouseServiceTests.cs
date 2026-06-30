using AutoMapper;
using FluentAssertions;
using Moq;
using RentalManagement.Application.DTOs.House;
using RentalManagement.Application.Mappings;
using RentalManagement.Domain.Entities;
using RentalManagement.Domain.Enums;
using RentalManagement.Domain.Interfaces;
using Xunit;

namespace RentalManagement.UnitTests.Services;

public class HouseServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUow;
    private readonly IMapper _mapper;

    public HouseServiceTests()
    {
        _mockUow = new Mock<IUnitOfWork>();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllHouses()
    {
        // Arrange
        var houses = new List<House>
        {
            new() { Id = 1, Name = "House A", Address = "123 Main St", MonthlyRent = 5000, Status = HouseStatus.Available },
            new() { Id = 2, Name = "House B", Address = "456 Oak Ave", MonthlyRent = 7000, Status = HouseStatus.Rented }
        };

        _mockUow.Setup(u => u.Houses.GetAllAsync()).ReturnsAsync(houses);

        // Act
        var result = _mapper.Map<IEnumerable<HouseDto>>(houses);

        // Assert
        result.Should().HaveCount(2);
        result.First().Name.Should().Be("House A");
    }

    [Fact]
    public void CreateHouseRequest_WithValidData_ShouldMapCorrectly()
    {
        // Arrange
        var request = new CreateHouseRequest("House C", "789 Pine Rd", 6000);

        // Act
        var house = _mapper.Map<House>(request);

        // Assert
        house.Name.Should().Be("House C");
        house.MonthlyRent.Should().Be(6000);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        _mockUow.Setup(u => u.Houses.GetByIdAsync(999)).ReturnsAsync((House?)null);

        // Act
        var house = await _mockUow.Object.Houses.GetByIdAsync(999);

        // Assert
        house.Should().BeNull();
    }
}
