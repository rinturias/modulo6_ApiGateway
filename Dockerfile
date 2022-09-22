FROM centos:7 AS base

RUN rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm \
    && yum install -y aspnetcore-runtime-6.0

ENV ASPNETCORE_URLS=http://*:5000
ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./Solution.Aerolinea.Gateway/Solution.Aerolinea.Gateway.csproj", "src/Solution.Aerolinea.Gateway/"]
RUN dotnet restore "src/Solution.Aerolinea.Gateway/Solution.Aerolinea.Gateway.csproj"
COPY . .
WORKDIR "/src/Solution.Aerolinea.Gateway/"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Solution.Aerolinea.Gateway.dll"]