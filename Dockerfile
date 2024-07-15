# Use .NET SDK 6.0 image as base
FROM mcr.microsoft.com/dotnet/sdk:6.0

# Set working directory
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ubiq.csproj .
RUN dotnet restore

# Copy the entire project and build it
COPY . .
RUN dotnet build -c Release -o out

# Expose port 80 for the web API
EXPOSE 80

# Entry point
CMD ["dotnet", "run", "--project", "ubiq.csproj"]



