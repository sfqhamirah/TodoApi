# Use SDK image to build code
FROM ://microsoft.com AS build
WORKDIR /src

# Copy solution and project files first to cache dependencies
COPY ["TodoApi/TodoApi.csproj", "TodoApi/"]
RUN dotnet restore "TodoApi/TodoApi.csproj"

# Copy everything else and publish the build
COPY . .
WORKDIR "/src/TodoApi"
RUN dotnet publish "TodoApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Setup the final runtime container
FROM ://microsoft.com AS final
WORKDIR /app
COPY --from=build /app/publish .

# Explicitly bind to standard container routing paths
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TodoApi.dll"]
