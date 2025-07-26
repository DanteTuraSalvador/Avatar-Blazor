# 🌐 Skills Manager - API Documentation

API endpoints and data models for the Skills Manager application.

## 🎯 **Overview**

The Skills Manager currently uses **Blazor Server** with **SharedDataService** for data management. This document outlines the current data models and potential REST API endpoints for future development.

## 📊 **Data Models**

### **SkillItem**
```csharp
public class SkillItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public string Description { get; set; } = "";
    public int TeamMemberCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

**Example:**
```json
{
    "id": 1,
    "name": "C# Programming",
    "category": "Programming",
    "description": "Object-oriented programming with C# language",
    "teamMemberCount": 2,
    "createdAt": "2024-01-15T00:00:00Z"
}
```

### **TeamMemberItem**
```csharp
public class TeamMemberItem
{
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Position { get; set; } = "";
    public string Department { get; set; } = "";
    public int SkillCount { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
```

**Example:**
```json
{
    "id": 1,
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@company.com",
    "position": "Senior Developer",
    "department": "Technology",
    "skillCount": 3,
    "fullName": "John Doe"
}
```

### **SkillAssignment**
```csharp
public class SkillAssignment
{
    public string MemberName { get; set; }
    public string SkillName { get; set; }
    public int ProficiencyLevel { get; set; } // 1-5
}
```

**Proficiency Levels:**
- **1 - Beginner** - Basic understanding
- **2 - Novice** - Limited experience  
- **3 - Intermediate** - Some experience
- **4 - Advanced** - Extensive experience
- **5 - Expert** - Recognized authority

**Example:**
```json
{
    "memberName": "John Doe",
    "skillName": "C# Programming",
    "proficiencyLevel": 4
}
```

## 🔄 **Current Data Service**

### **SharedDataService Methods**

**Skills Management:**
```csharp
// Get all skills
List<SkillItem> Skills { get; }

// Add new skill
void AddSkill(SkillItem skill)

// Update existing skill
void UpdateSkill(SkillItem skill)

// Remove skill
void RemoveSkill(SkillItem skill)
```

**Team Members Management:**
```csharp
// Get all team members
List<TeamMemberItem> TeamMembers { get; }

// Add new team member
void AddTeamMember(TeamMemberItem teamMember)

// Update existing team member
void UpdateTeamMember(TeamMemberItem teamMember)

// Remove team member
void RemoveTeamMember(TeamMemberItem teamMember)
```

**Skill Assignments Management:**
```csharp
// Get all skill assignments
Dictionary<string, Dictionary<string, int>> SkillAssignments { get; }

// Assign skill to member
void AssignSkill(string memberName, string skillName, int level)

// Remove skill assignment
void RemoveSkillAssignment(string memberName, string skillName)

// Get member's skills
Dictionary<string, int> GetMemberSkills(string memberName)
```

## 🚀 **Future REST API Endpoints**

### **Skills Endpoints**

**GET /api/skills**
- **Description:** Get all skills
- **Response:** `List<SkillItem>`
- **Status Codes:** 200 OK

**GET /api/skills/{id}**
- **Description:** Get skill by ID
- **Response:** `SkillItem`
- **Status Codes:** 200 OK, 404 Not Found

**POST /api/skills**
- **Description:** Create new skill
- **Request Body:** `SkillItem` (without ID)
- **Response:** `SkillItem` (with generated ID)
- **Status Codes:** 201 Created, 400 Bad Request

**PUT /api/skills/{id}**
- **Description:** Update existing skill
- **Request Body:** `SkillItem`
- **Response:** `SkillItem`
- **Status Codes:** 200 OK, 404 Not Found, 400 Bad Request

**DELETE /api/skills/{id}**
- **Description:** Delete skill
- **Status Codes:** 204 No Content, 404 Not Found

### **Team Members Endpoints**

**GET /api/team-members**
- **Description:** Get all team members
- **Response:** `List<TeamMemberItem>`
- **Status Codes:** 200 OK

**GET /api/team-members/{id}**
- **Description:** Get team member by ID
- **Response:** `TeamMemberItem`
- **Status Codes:** 200 OK, 404 Not Found

**POST /api/team-members**
- **Description:** Create new team member
- **Request Body:** `TeamMemberItem` (without ID)
- **Response:** `TeamMemberItem` (with generated ID)
- **Status Codes:** 201 Created, 400 Bad Request

**PUT /api/team-members/{id}**
- **Description:** Update existing team member
- **Request Body:** `TeamMemberItem`
- **Response:** `TeamMemberItem`
- **Status Codes:** 200 OK, 404 Not Found, 400 Bad Request

**DELETE /api/team-members/{id}**
- **Description:** Delete team member
- **Status Codes:** 204 No Content, 404 Not Found

### **Skill Assignments Endpoints**

**GET /api/skill-assignments**
- **Description:** Get all skill assignments
- **Response:** `List<SkillAssignment>`
- **Status Codes:** 200 OK

**GET /api/skill-assignments/member/{memberName}**
- **Description:** Get skills for specific member
- **Response:** `List<SkillAssignment>`
- **Status Codes:** 200 OK

**POST /api/skill-assignments**
- **Description:** Assign skill to member
- **Request Body:** `SkillAssignment`
- **Response:** `SkillAssignment`
- **Status Codes:** 201 Created, 400 Bad Request

**PUT /api/skill-assignments**
- **Description:** Update skill assignment level
- **Request Body:** `SkillAssignment`
- **Response:** `SkillAssignment`
- **Status Codes:** 200 OK, 404 Not Found

**DELETE /api/skill-assignments**
- **Description:** Remove skill assignment
- **Request Body:** `{ memberName, skillName }`
- **Status Codes:** 204 No Content, 404 Not Found

## 📊 **Response Formats**

### **Success Response**
```json
{
    "success": true,
    "data": { ... },
    "message": "Operation completed successfully"
}
```

### **Error Response**
```json
{
    "success": false,
    "error": {
        "code": "VALIDATION_ERROR",
        "message": "Invalid input data",
        "details": [
            "Name is required",
            "Email format is invalid"
        ]
    }
}
```

### **Pagination Response**
```json
{
    "success": true,
    "data": [...],
    "pagination": {
        "page": 1,
        "pageSize": 10,
        "totalItems": 25,
        "totalPages": 3
    }
}
```

## 🔍 **Query Parameters**

### **Skills Filtering**
```
GET /api/skills?category=Programming&search=C#&page=1&pageSize=10
```

**Parameters:**
- `category` - Filter by skill category
- `search` - Search in name and description
- `page` - Page number (default: 1)
- `pageSize` - Items per page (default: 10)

### **Team Members Filtering**
```
GET /api/team-members?department=Technology&position=Developer&page=1&pageSize=10
```

**Parameters:**
- `department` - Filter by department
- `position` - Filter by position
- `page` - Page number (default: 1)
- `pageSize` - Items per page (default: 10)

## 🔐 **Authentication (Future)**

### **JWT Token Authentication**
```http
Authorization: Bearer <jwt-token>
```

### **API Key Authentication**
```http
X-API-Key: <api-key>
```

## 📈 **Rate Limiting (Future)**

### **Rate Limit Headers**
```http
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 999
X-RateLimit-Reset: 1640995200
```

### **Rate Limit Response**
```json
{
    "success": false,
    "error": {
        "code": "RATE_LIMIT_EXCEEDED",
        "message": "Too many requests. Please try again later.",
        "retryAfter": 60
    }
}
```

## 🧪 **Testing API Endpoints**

### **Using PowerShell**
```powershell
# Get all skills
Invoke-RestMethod -Uri "http://localhost:5093/api/skills" -Method GET

# Create new skill
$skill = @{
    name = "Python Programming"
    category = "Programming"
    description = "Python development skills"
}
Invoke-RestMethod -Uri "http://localhost:5093/api/skills" -Method POST -Body ($skill | ConvertTo-Json) -ContentType "application/json"
```

### **Using curl**
```bash
# Get all skills
curl -X GET http://localhost:5093/api/skills

# Create new skill
curl -X POST http://localhost:5093/api/skills \
  -H "Content-Type: application/json" \
  -d '{"name":"Python Programming","category":"Programming","description":"Python development skills"}'
```

## 🎯 **Implementation Notes**

### **Current State**
- **Data Storage:** In-memory with SharedDataService
- **Persistence:** Static storage across component instances
- **API:** Not implemented (Blazor Server only)

### **Future Implementation**
- **Database:** Entity Framework Core with SQL Server
- **API Controllers:** ASP.NET Core Web API
- **Authentication:** JWT or API Key based
- **Validation:** FluentValidation or Data Annotations
- **Documentation:** Swagger/OpenAPI

### **Migration Path**
1. **Add Entity Framework** for database persistence
2. **Create API Controllers** for REST endpoints
3. **Implement authentication** and authorization
4. **Add validation** and error handling
5. **Generate OpenAPI** documentation
6. **Update frontend** to use API instead of SharedDataService

---

**🌐 Ready for API development when needed!** 🚀
