# Use .NET SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and restore dependencies
COPY ./src/Invoicer.Api/Invoicer.Api.csproj ./src/Invoicer.Api/
RUN dotnet restore ./src/Invoicer.Api/Invoicer.Api.csproj

# Copy the rest of the source code and build the application
COPY ./src ./src
WORKDIR /app/src/Invoicer.Api
RUN dotnet publish -c Release -o /out

# Use ASP.NET runtime for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

# Expose the application on port 8080
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Invoicer.Api.dll"]
