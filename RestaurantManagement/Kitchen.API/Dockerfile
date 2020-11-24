#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Kitchen.API/Kitchen.API.csproj", "Kitchen.API/"]
COPY ["Kitchen.Application/Kitchen.Application.csproj", "Kitchen.Application/"]
COPY ["Common.Application/Common.Application.csproj", "Common.Application/"]
COPY ["Common.Domain/Common.Domain.csproj", "Common.Domain/"]
COPY ["Kitchen.Domain/Kitchen.Domain.csproj", "Kitchen.Domain/"]
COPY ["Kitchen.Infrastructure/Kitchen.Infrastructure.csproj", "Kitchen.Infrastructure/"]
COPY ["Common.Infrastructure/Common.Infrastructure.csproj", "Common.Infrastructure/"]
COPY ["Common.Web/Common.Web.csproj", "Common.Web/"]
RUN dotnet restore "Kitchen.API/Kitchen.API.csproj"
COPY . .
WORKDIR "/src/Kitchen.API"
RUN dotnet build "Kitchen.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kitchen.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kitchen.API.dll"]