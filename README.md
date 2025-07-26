# 🎯 Skills Manager

A modern, professional skills management system built with **Blazor Server**, **Entity Framework Core**, and **Docker**. Manage team members, skills, and skill assignments with proficiency levels.

![Skills Manager](https://img.shields.io/badge/Blazor-Server-blue) ![.NET](https://img.shields.io/badge/.NET-8.0-purple) ![Docker](https://img.shields.io/badge/Docker-Ready-blue) ![Tests](https://img.shields.io/badge/Tests-12%20Passing-green)

## ✨ **Key Features**

### 🎯 **Skills Builder** (Main Feature)
- **Assign skills** to team members with 5-level proficiency system
- **Visual skill matrix** with color-coded proficiency levels
- **Real-time updates** across all pages
- **Persistent data** survives page refreshes

### 👥 **Team Members Management**
- **Add, edit, delete** team members
- **Professional profiles** with position and department
- **Skill count tracking** for each member

### 🛠 **Skills Management**
- **Comprehensive skill catalog** with categories
- **Add, edit, delete** skills with descriptions
- **Usage tracking** across team members

### 🔄 **Data Synchronization**
- **Shared Data Service** - Centralized data management
- **Real-time sync** between Skills, Team Members, and Skills Builder
- **Persistent storage** across component instances

## 🚀 **Quick Start**

### **🔥 Recommended: Hybrid Development Setup**

**Database in Docker + Web App Local = Best Development Experience**

1. **Start Database Services:**
   ```powershell
   .\scripts\start-db.ps1
   ```

2. **Run Web Application:**
   ```powershell
   dotnet run --project src/Avatar.Web
   ```

3. **Access Application:**
   - **Home:** http://localhost:5093
   - **Skills:** http://localhost:5093/skills
   - **Team Members:** http://localhost:5093/team-members
   - **Skills Builder:** http://localhost:5093/skills-builder ✨

4. **Stop Database (when done):**
   ```powershell
   .\scripts\stop-db.ps1
   ```

### **🐳 Alternative: Full Docker**
```powershell
docker-compose up -d
# Access at: http://localhost:8080
```

### **🚀 Alternative: .NET Aspire**
```powershell
dotnet run --project src/Avatar.AppHost
# Dashboard: https://localhost:15888
```

## 🎯 **Skills Builder Walkthrough**

### **Step 1: Add Team Members**
1. Go to **Team Members** page
2. Click **"Add New Team Member"**
3. Fill in details (Name, Position, Department)
4. Save

### **Step 2: Add Skills**
1. Go to **Skills** page
2. Click **"Add New Skill"**
3. Enter skill details (Name, Category, Description)
4. Save

### **Step 3: Assign Skills (Skills Builder)**
1. Go to **Skills Builder** page
2. **Select a team member** from the list
3. **Add skills** from available skills dropdown
4. **Set proficiency levels** (1-5 scale):
   - 🔴 **1 - Beginner** - Basic understanding
   - 🟡 **2 - Novice** - Limited experience
   - 🟠 **3 - Intermediate** - Some experience
   - 🟢 **4 - Advanced** - Extensive experience
   - 🔵 **5 - Expert** - Recognized authority

### **Step 4: View Results**
- **Skills matrix** shows all assignments
- **Color-coded levels** for quick assessment
- **Real-time updates** across all pages
- **Data persists** across page refreshes

## 🏗 **Architecture**

### **Clean Architecture Layers:**
- **Avatar.Web** - Blazor Server presentation layer
- **Avatar.Core** - Domain models and business logic
- **Avatar.Infrastructure** - Data access and external services
- **Avatar.ServiceDefaults** - Shared configurations
- **Avatar.AppHost** - .NET Aspire orchestration

### **Key Technologies:**
- **Frontend:** Blazor Server, Bootstrap 5, Telerik UI
- **Backend:** .NET 8, Entity Framework Core
- **Database:** SQL Server 2022 (Docker)
- **Caching:** Redis (Docker)
- **Testing:** xUnit, 12 passing tests
- **Deployment:** Docker, .NET Aspire

## 🛠 **Development Setup**

### **Prerequisites:**
- **.NET 8.0 SDK**
- **Docker Desktop**
- **Visual Studio Community** (recommended) or VS Code

### **Clone and Setup:**
```bash
git clone <repository-url>
cd Avatar
dotnet restore
```

### **Visual Studio Setup:**
1. **Open:** `Avatar.sln` in Visual Studio Community
2. **Set Startup Project:** Right-click `Avatar.Web` → Set as Startup Project
3. **Start Database:** Run `.\scripts\start-db.ps1`
4. **Press F5** to start debugging

### **Development Workflow:**
1. **Start database services** in Docker
2. **Run web app locally** for fast development
3. **Use Hot Reload** for instant code changes
4. **Debug with breakpoints** in Visual Studio
5. **Run tests** to verify changes

## 📊 **Testing**

### **Run All Tests:**
```powershell
dotnet test
```

### **Test Coverage:**
- **Unit Tests:** 12 tests covering core functionality
- **Integration Tests:** End-to-end testing
- **Manual Tests:** PowerShell scripts for quick verification

### **Test Scripts:**
```powershell
# Basic application test
.\tests\simple-test.ps1

# Skills Builder specific test
.\tests\skills-builder-test.ps1

# Performance testing
.\tests\performance-test.ps1
```

## 📚 **Documentation**

- **[DEPLOYMENT.md](DEPLOYMENT.md)** - Comprehensive deployment guide
- **[SOLUTION_STRUCTURE.md](SOLUTION_STRUCTURE.md)** - Project structure overview
- **[DEVELOPMENT.md](DEVELOPMENT.md)** - Detailed development guide
- **[API.md](API.md)** - API documentation and endpoints

## 🎯 **Project Status**

### **✅ Completed Features:**
- ✅ **Skills Management** - Full CRUD operations
- ✅ **Team Members Management** - Full CRUD operations  
- ✅ **Skills Builder** - Assign skills with proficiency levels
- ✅ **Data Synchronization** - Real-time updates across pages
- ✅ **Shared Data Service** - Centralized data management
- ✅ **Professional UI** - Bootstrap 5 + Telerik components
- ✅ **Multiple Deployment Options** - Docker, Aspire, Local
- ✅ **Comprehensive Testing** - Unit and integration tests
- ✅ **Clean Architecture** - Proper separation of concerns

### **🚀 Future Enhancements:**
- 🔄 **Database persistence** (currently in-memory with static storage)
- 🔐 **Authentication and authorization**
- 📊 **Reporting and analytics**
- 🌐 **API endpoints** for external integrations
- 📱 **Mobile-responsive improvements**

## 🤝 **Contributing**

1. **Fork** the repository
2. **Create** a feature branch
3. **Make** your changes
4. **Run** tests to ensure everything works
5. **Submit** a pull request

## 📄 **License**

This project is licensed under the MIT License - see the LICENSE file for details.

---

**🎊 Built with ❤️ for professional skills management**
