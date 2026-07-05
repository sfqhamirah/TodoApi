# Use the official Microsoft .NET SDK image to build the app
FROM ://microsoft.com AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Use the official runtime image to run the app
FROM ://microsoft.com
WORKDIR /app
COPY --from=build /app .

# Expose the port and dynamically bind to Railway's $PORT
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TodoApi.dll"]
