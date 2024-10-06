# Dockerfile for PersonApi

# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PersonApi/PersonApi.csproj", "PersonApi/"]
RUN dotnet restore "PersonApi/PersonApi.csproj"
COPY . .
WORKDIR "/src/PersonApi"

# Remove appsettings.Development.json before build
RUN rm -f appsettings.Development.json

RUN dotnet build "PersonApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonApi.csproj" -c Release -o /app/publish

# Copy the appsettings.json file
COPY ["PersonApi/appsettings.json", "/app/publish/appsettings.json"]

# Use the runtime image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonApi.dll"]
