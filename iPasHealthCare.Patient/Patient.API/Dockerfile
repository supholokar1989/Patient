#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 30000
EXPOSE 30001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Patient.API/Patient.API.csproj", "Patient.API/"]
COPY ["Patient.Domain/Patient.Domain.csproj", "Patient.Domain/"]
COPY ["iPas.Infrastructure.EventBus/iPas.Infrastructure.EventBus.csproj", "iPas.Infrastructure.EventBus/"]
COPY ["iPas.Infrastructure.EventBusRabbitMQ/iPas.Infrastructure.EventBusRabbitMQ.csproj", "iPas.Infrastructure.EventBusRabbitMQ/"]
COPY ["iPas.Infrastructure.IntegrationEventLogEF/iPas.Infrastructure.IntegrationEventLogEF.csproj", "iPas.Infrastructure.IntegrationEventLogEF/"]
COPY ["iPas.Infrastructure.EventBusServiceBus/iPas.Infrastructure.EventBusServiceBus.csproj", "iPas.Infrastructure.EventBusServiceBus/"]
COPY ["Patient.Infrastructure/Patient.Infrastructure.csproj", "Patient.Infrastructure/"]
RUN dotnet restore "Patient.API/Patient.API.csproj"
COPY . .
WORKDIR "/src/Patient.API"
RUN dotnet build "Patient.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Patient.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Patient.API.dll"]