# Skills Manager - Multi-stage Docker build
# Stage 1: Build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["src/Avatar.Web/Avatar.Web.csproj", "src/Avatar.Web/"]
COPY ["src/Avatar.Infrastructure/Avatar.Infrastructure.csproj", "src/Avatar.Infrastructure/"]
COPY ["src/Avatar.Core/Avatar.Core.csproj", "src/Avatar.Core/"]
COPY ["Directory.Build.props", "./"]

# Restore dependencies
RUN dotnet restore "src/Avatar.Web/Avatar.Web.csproj"

# Copy all source code
COPY . .

# Build the application
WORKDIR "/src/src/Avatar.Web"
RUN dotnet build "Avatar.Web.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "Avatar.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Create non-root user for security
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

# Copy published application
COPY --from=publish /app/publish .

# Expose port
EXPOSE 8080

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Entry point
ENTRYPOINT ["dotnet", "Avatar.Web.dll"]
