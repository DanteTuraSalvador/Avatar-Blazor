# Skills Manager - Solution Structure

## 📁 Clean Solution Structure

```
Avatar/
├── 📄 Avatar.sln                    # Main solution file
├── 📄 Directory.Build.props          # Global build properties
├── 📄 DEPLOYMENT.md                  # Deployment documentation
├── 📄 Dockerfile                     # Docker build configuration
├── 📄 docker-compose.yml             # Full Docker setup
├── 📄 docker-compose.db-only.yml     # Database-only Docker setup
├── 📄 docker-compose.override.yml    # Development overrides
│
├── 📁 src/                           # Source code
│   ├── 📁 Avatar.Web/                # Main Blazor web application
│   │   ├── 📁 Components/Pages/      # Razor pages (Skills, TeamMembers, SkillsBuilder)
│   │   ├── 📁 Services/              # Application services (SharedDataService)
│   │   └── 📄 Program.cs             # Application entry point
│   │
│   ├── 📁 Avatar.Core/               # Domain models and interfaces
│   │   ├── 📁 Entities/              # Domain entities
│   │   ├── 📁 DTOs/                  # Data transfer objects
│   │   └── 📁 Interfaces/            # Service interfaces
│   │
│   ├── 📁 Avatar.Infrastructure/     # Data access and external services
│   │   ├── 📁 Data/                  # Entity Framework context
│   │   ├── 📁 Repositories/          # Data repositories
│   │   └── 📁 Services/              # Infrastructure services
│   │
│   ├── 📁 Avatar.AppHost/            # .NET Aspire orchestration
│   │   └── 📄 Program.cs             # Aspire host configuration
│   │
│   └── 📁 Avatar.ServiceDefaults/    # Shared service configurations
│       └── 📄 Extensions.cs          # Common service extensions
│
├── 📁 tests/                         # Test projects
│   ├── 📁 Avatar.UnitTests/          # Unit tests
│   │   ├── 📁 Services/              # Service tests
│   │   ├── 📄 SkillTests.cs          # Skill entity tests
│   │   └── 📄 TeamMemberTests.cs     # Team member tests
│   │
│   ├── 📁 Avatar.IntegrationTests/   # Integration tests
│   │   └── 📄 GlobalUsings.cs        # Test global usings
│   │
│   ├── 📄 simple-test.ps1            # Basic application test
│   ├── 📄 skills-builder-test.ps1    # Skills Builder specific test
│   └── 📄 performance-test.ps1       # Performance testing script
│
└── 📁 scripts/                       # Utility scripts
    ├── 📄 start-db.ps1               # Start database services in Docker
    ├── 📄 stop-db.ps1                # Stop database services
    ├── 📄 aspire-run.ps1             # Run with .NET Aspire
    └── 📄 docker-build.ps1           # Full Docker build and run
```

## 🎯 Key Components

### **🌐 Web Application (Avatar.Web)**
- **Skills Management** - Add, edit, delete skills
- **Team Members Management** - Manage team member information
- **Skills Builder** - Assign skills to team members with proficiency levels
- **Shared Data Service** - Centralized data management

### **🏗️ Architecture Layers**
- **Avatar.Core** - Domain models and business logic
- **Avatar.Infrastructure** - Data access and external services
- **Avatar.Web** - Presentation layer (Blazor Server)

### **🚀 Deployment Options**
- **Local Development** - `dotnet run --project src/Avatar.Web`
- **Database in Docker** - `.\scripts\start-db.ps1` + local web app
- **Full Docker** - `docker-compose up -d`
- **.NET Aspire** - `dotnet run --project src/Avatar.AppHost`

### **🧪 Testing**
- **Unit Tests** - Avatar.UnitTests (12 tests passing)
- **Integration Tests** - Avatar.IntegrationTests
- **Manual Tests** - PowerShell test scripts

## ✅ Clean Solution Benefits

- **No unused files** - All directories and files serve a purpose
- **Clear structure** - Easy to navigate and understand
- **Multiple deployment options** - Flexible for different environments
- **Comprehensive testing** - Unit and integration tests included
- **Professional organization** - Follows .NET best practices
