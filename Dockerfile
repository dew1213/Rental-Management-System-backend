FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/RentalManagement.API/RentalManagement.API.csproj", "src/RentalManagement.API/"]
COPY ["src/RentalManagement.Application/RentalManagement.Application.csproj", "src/RentalManagement.Application/"]
COPY ["src/RentalManagement.Domain/RentalManagement.Domain.csproj", "src/RentalManagement.Domain/"]
COPY ["src/RentalManagement.Infrastructure/RentalManagement.Infrastructure.csproj", "src/RentalManagement.Infrastructure/"]
RUN dotnet restore "src/RentalManagement.API/RentalManagement.API.csproj"

COPY . .
WORKDIR "/src/src/RentalManagement.API"
RUN dotnet build "RentalManagement.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RentalManagement.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentalManagement.API.dll"]
