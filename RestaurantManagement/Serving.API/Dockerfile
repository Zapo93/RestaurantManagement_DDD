#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Serving.API/Serving.API.csproj", "Serving.API/"]
COPY ["Serving.Application/Serving.Application.csproj", "Serving.Application/"]
COPY ["Common.Application/Common.Application.csproj", "Common.Application/"]
COPY ["Common.Domain/Common.Domain.csproj", "Common.Domain/"]
COPY ["Serving.Domain/Serving.Domain.csproj", "Serving.Domain/"]
COPY ["Identity.Infrastructure/Identity.Infrastructure.csproj", "Identity.Infrastructure/"]
COPY ["Identity.Application/Identity.Application.csproj", "Identity.Application/"]
COPY ["Common.Infrastructure/Common.Infrastructure.csproj", "Common.Infrastructure/"]
COPY ["Serving.Infrastructure/Serving.Infrastructure.csproj", "Serving.Infrastructure/"]
COPY ["Common.Web/Common.Web.csproj", "Common.Web/"]
RUN dotnet restore "Serving.API/Serving.API.csproj"
COPY . .
WORKDIR "/src/Serving.API"
RUN dotnet build "Serving.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Serving.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Serving.API.dll"]