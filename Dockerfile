# Use SDK image to build code
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file first to cache dependencies
# Changed from ["TodoApi/TodoApi.csproj", "TodoApi/"]
COPY ["TodoApi.csproj", "./"]
RUN dotnet restore "TodoApi.csproj"

# Copy everything else and publish the build
COPY . .
# Removed: WORKDIR "/src/TodoApi" since we are already in /src
RUN dotnet publish "TodoApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Setup the final runtime container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Explicitly bind to standard container routing paths
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TodoApi.dll"]