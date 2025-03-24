# 🎓 Education Center

## 📌 Overview
Education Center is a management system for educational institutions that helps streamline student registration, course management, and scheduling. Built using **ASP.NET Core** and **Entity Framework Core**, this project follows an **N-Tier Architecture** with a **Repository Pattern** to ensure scalability and maintainability.

## ✨ Features
- 👤 **User Management**: Register, update, and manage student and instructor profiles.
- 📚 **Course Management**: Create, edit, and delete courses.
- 📝 **Enrollment System**: Students can enroll in available courses.
- 📅 **Scheduling**: Manage class schedules.
- 🔒 **Authentication & Authorization**: Secure login and role-based access control.
- 🗄 **Database Integration**: Uses **MSSQL** with **Entity Framework Core**.

## 🛠 Technologies Used
- **Backend**: ⚡ ASP.NET Core
- **Database**: 🗃 MS SQL Server (using Entity Framework Core)
- **Architecture**: 🏗 N-Tier (Domain, Data, Service, API)
- **Repository Pattern**
- **LINQ for querying data**

## 📂 Project Structure
```
EducationCenter/
│-- Domain/          # Core business models
│-- Data/            # Database context and repositories
│-- Service/         # Business logic and service layer
│-- API/             # Controllers and API endpoints
│-- README.md        # Project documentation
```

## 🚀 Getting Started
### 🔧 Prerequisites
- .NET SDK 7.0+
- MSSQL Server
- Visual Studio or VS Code

### 📥 Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/education-center.git
   cd education-center
   ```
2. Configure **appsettings.json** with your MSSQL connection string.
3. Apply database migrations:
   ```sh
   dotnet ef database update
   ```
4. Run the application:
   ```sh
   dotnet run --project API
   ```

## 🔗 API Endpoints
| 🔍 Endpoint         | 🏷 Method | 📄 Description             |
|----------------------|--------|------------------------------|
| `/api/users`        | GET    | Get all users                |
| `/api/courses`      | GET    | Get all courses              |
| `/api/enrollments`  | POST   | Enroll a student in a course |


## 📜 License
This project is licensed under the Apache License 2.0.
