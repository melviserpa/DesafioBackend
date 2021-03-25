#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Investimentos.API/Investimentos.API.csproj", "Investimentos.API/"]
COPY ["Investimentos.Custodia/Investimentos.Custodia.csproj", "Investimentos.Custodia/"]
RUN dotnet restore "Investimentos.API/Investimentos.API.csproj"
COPY . .
WORKDIR "/src/Investimentos.API"
RUN dotnet build "Investimentos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Investimentos.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Investimentos.API.dll"]
