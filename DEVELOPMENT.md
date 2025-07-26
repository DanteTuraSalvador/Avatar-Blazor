# 🛠 Skills Manager - Development Guide

Complete guide for developing and extending the Skills Manager.

## 🎯 **Development Environment Setup**

### **Prerequisites**
- **.NET 8.0 SDK** - Latest version
- **Docker Desktop** - For database services
- **Visual Studio Community 2022** (recommended) or VS Code
- **Git** - For version control

### **Recommended: Hybrid Development Setup**

**🔥 Best Practice: Database in Docker + Web App Local**

**Benefits:**
- ✅ **Fast development** - Local web app with hot reload
- ✅ **Production-like database** - SQL Server in Docker
- ✅ **Easy debugging** - Full IDE support with breakpoints
- ✅ **No Telerik issues** - Avoids Docker build problems
- ✅ **Isolated database** - Clean, consistent data environment

**Setup Steps:**

1. **Clone Repository:**
   ```bash
   git clone <repository-url>
   cd Avatar
   ```

2. **Start Database Services:**
   ```powershell
   .\scripts\start-db.ps1
   ```
   This starts:
   - **SQL Server 2022** at `localhost:1433`
   - **Redis Cache** at `localhost:6379`

3. **Open in Visual Studio:**
   ```bash
   start Avatar.sln
   ```
   - Set **Avatar.Web** as startup project
   - Press **F5** to start with debugging

4. **Verify Setup:**
   - Application: http://localhost:5093
   - Skills Builder: http://localhost:5093/skills-builder ✨

## 🏗 **Architecture Overview**

### **Clean Architecture Layers**

```
📁 Avatar.Web (Presentation)
├── 📁 Components/Pages/     # Blazor pages
├── 📁 Services/            # Application services
└── 📄 Program.cs           # App configuration

📁 Avatar.Core (Domain)
├── 📁 Entities/            # Domain models
├── 📁 DTOs/               # Data transfer objects
└── 📁 Interfaces/         # Service contracts

📁 Avatar.Infrastructure (Data)
├── 📁 Data/               # EF Core context
├── 📁 Repositories/       # Data access
└── 📁 Services/           # External services
```

### **Key Design Patterns**

**🔄 Shared Data Service Pattern:**
- **Centralized data management** across all pages
- **Static storage** for development (in-memory persistence)
- **Real-time synchronization** between components
- **Easy to replace** with database persistence later

**🎯 Component-Based Architecture:**
- **Blazor Server** for real-time updates
- **Bootstrap 5** for responsive design
- **Telerik UI** for professional components
- **Hot Reload** for fast development

## 🎯 **Skills Builder Deep Dive**

### **Core Functionality**

**📊 Skills Assignment System:**
```csharp
// 5-Level Proficiency System
1 - Beginner    (🔴 Red)
2 - Novice      (🟡 Yellow) 
3 - Intermediate (🟠 Orange)
4 - Advanced    (🟢 Green)
5 - Expert      (🔵 Blue)
```

**🔄 Data Flow:**
1. **Load data** from SharedDataService
2. **Select team member** from list
3. **Add/remove skills** with proficiency levels
4. **Update SharedDataService** for persistence
5. **Sync across pages** automatically

### **Key Components**

**📄 SkillsBuilder.razor:**
- Main Skills Builder page
- Team member selection
- Skills assignment interface
- Proficiency level management

**📄 SharedDataService.cs:**
- Centralized data storage
- CRUD operations for skills and team members
- Skill assignment management
- Cross-component synchronization

## 🔧 **Development Workflow**

### **Daily Development Process**

1. **Start Development Session:**
   ```powershell
   # Start database services
   .\scripts\start-db.ps1
   
   # Open Visual Studio
   start Avatar.sln
   
   # Set Avatar.Web as startup project
   # Press F5 to start debugging
   ```

2. **Make Changes:**
   - **Edit Razor pages** with hot reload
   - **Modify C# code** with automatic rebuild
   - **Set breakpoints** for debugging
   - **Use IntelliSense** for code completion

3. **Test Changes:**
   ```powershell
   # Run unit tests
   dotnet test
   
   # Run manual tests
   .\tests\simple-test.ps1
   .\tests\skills-builder-test.ps1
   ```

4. **End Development Session:**
   ```powershell
   # Stop database services
   .\scripts\stop-db.ps1
   ```

### **Hot Reload Development**

**🔥 Blazor Hot Reload Features:**
- **Razor markup changes** - Instant updates
- **CSS changes** - Live style updates  
- **C# code changes** - Automatic rebuild
- **Component state preservation** - No data loss

**🎯 Best Practices:**
- Keep browser open during development
- Use browser dev tools for debugging
- Monitor console for errors
- Test on multiple browsers

## 🧪 **Testing Strategy**

### **Test Structure**

```
📁 tests/
├── 📁 Avatar.UnitTests/        # Unit tests (12 tests)
│   ├── 📄 SkillTests.cs        # Skill entity tests
│   ├── 📄 TeamMemberTests.cs   # Team member tests
│   └── 📁 Services/            # Service tests
├── 📁 Avatar.IntegrationTests/ # Integration tests
└── 📄 *.ps1                   # Manual test scripts
```

### **Running Tests**

**🧪 Unit Tests:**
```powershell
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/Avatar.UnitTests

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

**🔧 Manual Tests:**
```powershell
# Basic functionality test
.\tests\simple-test.ps1

# Skills Builder specific test  
.\tests\skills-builder-test.ps1

# Performance test
.\tests\performance-test.ps1
```

### **Writing New Tests**

**📝 Unit Test Example:**
```csharp
[Fact]
public void Should_Create_Skill_With_Valid_Data()
{
    // Arrange
    var skill = new SkillItem
    {
        Name = "Test Skill",
        Category = "Testing",
        Description = "Test description"
    };

    // Act & Assert
    Assert.NotNull(skill);
    Assert.Equal("Test Skill", skill.Name);
}
```

## 🔄 **Adding New Features**

### **Adding a New Page**

1. **Create Razor Page:**
   ```razor
   @page "/new-feature"
   @rendermode InteractiveServer
   @using Avatar.Web.Services

   <h3>New Feature</h3>

   @code {
       // Component logic
   }
   ```

2. **Add Navigation:**
   ```razor
   <!-- In Layout/NavMenu.razor -->
   <div class="nav-item px-3">
       <NavLink class="nav-link" href="new-feature">
           <span class="bi bi-plus-square-fill" aria-hidden="true"></span> New Feature
       </NavLink>
   </div>
   ```

3. **Update SharedDataService** (if needed):
   ```csharp
   public static class SharedDataService
   {
       // Add new data collections
       private static List<NewEntity> _newEntities = new();
       
       public static List<NewEntity> NewEntities => _newEntities;
       
       // Add CRUD methods
       public static void AddNewEntity(NewEntity entity) { ... }
   }
   ```

### **Extending Skills Builder**

**🎯 Common Extensions:**

1. **Add Skill Categories Filter:**
   ```razor
   <select @bind="selectedCategory" @onchange="FilterSkills">
       <option value="">All Categories</option>
       @foreach (var category in availableCategories)
       {
           <option value="@category">@category</option>
       }
   </select>
   ```

2. **Add Skill Search:**
   ```razor
   <input type="text" @bind="searchTerm" @oninput="SearchSkills" 
          placeholder="Search skills..." />
   ```

3. **Add Bulk Operations:**
   ```csharp
   private void AssignMultipleSkills(List<string> skills, int level)
   {
       foreach (var skill in skills)
       {
           selectedMember.Skills[skill] = level;
           SharedDataService.AssignSkill(selectedMember.Name, skill, level);
       }
   }
   ```

## 🐛 **Debugging Tips**

### **Common Issues and Solutions**

**🔧 Database Connection Issues:**
```powershell
# Check if Docker is running
docker info

# Restart database services
.\scripts\stop-db.ps1
.\scripts\start-db.ps1

# Check connection string in appsettings.Development.json
```

**🔧 Hot Reload Not Working:**
- Restart the application (Ctrl+F5)
- Check for compilation errors
- Clear browser cache
- Verify file changes are saved

**🔧 Skills Builder Data Not Syncing:**
- Check SharedDataService implementation
- Verify OnInitializedAsync is called
- Debug LoadDataFromSharedSources method
- Check browser console for JavaScript errors

### **Debugging Tools**

**🔍 Visual Studio Debugging:**
- **Breakpoints** - Set in C# code and Razor pages
- **Watch Window** - Monitor variable values
- **Call Stack** - Trace execution path
- **Immediate Window** - Execute code during debugging

**🌐 Browser Debugging:**
- **F12 Developer Tools** - Network, Console, Elements
- **Blazor DevTools** - Component inspection
- **Network Tab** - Monitor SignalR connections
- **Console** - JavaScript errors and logs

## 🚀 **Performance Optimization**

### **Blazor Server Optimization**

**🔄 Component Optimization:**
```razor
@* Use @key for list items *@
@foreach (var item in items)
{
    <div @key="item.Id">@item.Name</div>
}

@* Implement ShouldRender for expensive components *@
@code {
    protected override bool ShouldRender()
    {
        return hasChanges;
    }
}
```

**📊 Data Optimization:**
- **Lazy loading** for large datasets
- **Pagination** for long lists
- **Caching** frequently accessed data
- **Debouncing** for search inputs

### **Database Optimization**

**🗄️ SQL Server in Docker:**
- **Persistent volumes** for data retention
- **Memory allocation** for performance
- **Index optimization** for queries
- **Connection pooling** for efficiency

## 📚 **Additional Resources**

### **Documentation Links**
- **[Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)**
- **[Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)**
- **[Docker Documentation](https://docs.docker.com/)**
- **[.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/)**

### **Useful Commands**
```powershell
# Build solution
dotnet build

# Run tests
dotnet test

# Clean solution
dotnet clean

# Restore packages
dotnet restore

# Check for updates
dotnet list package --outdated
```

---

**🎯 Happy coding! Build amazing features for Skills Manager!** 🚀
