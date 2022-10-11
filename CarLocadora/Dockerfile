#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarLocadora/CarLocadora.csproj", "CarLocadora/"]
COPY ["CarLocadora.Modelo/CarLocadora.Modelo.csproj", "CarLocadora.Modelo/"]
COPY ["CarLocadora.Comum/CarLocadora.Comum.csproj", "CarLocadora.Comum/"]
RUN dotnet restore "CarLocadora/CarLocadora.csproj"
COPY . .
WORKDIR "/src/CarLocadora"
RUN dotnet build "CarLocadora.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarLocadora.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarLocadora.dll", "--environment=Development"]