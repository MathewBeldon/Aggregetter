#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Aggregetter.Aggre.API/Aggregetter.Aggre.API.csproj", "Aggregetter.Aggre.API/"]
COPY ["Aggregetter.Aggre.Application/Aggregetter.Aggre.Application.csproj", "Aggregetter.Aggre.Application/"]
COPY ["Aggregetter.Aggre.Domain/Aggregetter.Aggre.Domain.csproj", "Aggregetter.Aggre.Domain/"]
COPY ["Aggregetter.Aggre.Persistance/Aggregetter.Aggre.Persistence.csproj", "Aggregetter.Aggre.Persistance/"]
COPY ["Aggregetter.Aggre.Identity/Aggregetter.Aggre.Identity.csproj", "Aggregetter.Aggre.Identity/"]
RUN dotnet restore "Aggregetter.Aggre.API/Aggregetter.Aggre.API.csproj"
COPY . .
WORKDIR "/src/Aggregetter.Aggre.API"
RUN dotnet build "Aggregetter.Aggre.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aggregetter.Aggre.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aggregetter.Aggre.API.dll"]