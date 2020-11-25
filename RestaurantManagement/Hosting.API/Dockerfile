#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Hosting.API/Hosting.API.csproj", "Hosting.API/"]
COPY ["Hosting.Domain/Hosting.Domain.csproj", "Hosting.Domain/"]
COPY ["Common.Domain/Common.Domain.csproj", "Common.Domain/"]
COPY ["Common.Application/Common.Application.csproj", "Common.Application/"]
COPY ["Hosting.Application/Hosting.Application.csproj", "Hosting.Application/"]
COPY ["Common.Infrastructure/Common.Infrastructure.csproj", "Common.Infrastructure/"]
COPY ["Hosting.Infrastructure/Hosting.Infrastructure.csproj", "Hosting.Infrastructure/"]
COPY ["Common.Web/Common.Web.csproj", "Common.Web/"]
RUN dotnet restore "Hosting.API/Hosting.API.csproj"
COPY . .
WORKDIR "/src/Hosting.API"
RUN dotnet build "Hosting.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hosting.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hosting.API.dll"]