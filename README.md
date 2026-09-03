# Library Management System

A RESTful ASP.NET Core Web API for managing books, authors, members, borrowing records, categories, and user authentication.

## 📖 Overview

The **Library Management System** is a backend API designed to manage common library operations through a structured REST API. It uses a service-based architecture with Entity Framework Core and PostgreSQL for data persistence.

### Key Features

* User authentication using **JWT**
* Role-based authorization for **Admin, Librarian, and Member**
* CRUD operations for:

  * Users
  * Books
  * Authors
  * Categories
  * Members
  * Member profiles
  * Borrow records
  * Author-book relationships
* Pagination for collection endpoints
* Search and sorting functionality
* DTOs for request and response models
* FluentValidation for input validation
* AutoMapper for DTO/entity mapping
* Entity Framework Core database migrations
* PostgreSQL database integration
* Global exception handling middleware
* Swagger/OpenAPI for API testing and documentation

## 🚀 Built With

* **C#**
* **.NET 10 / ASP.NET Core Web API**
* **Entity Framework Core 10**
* **PostgreSQL**
* **Npgsql**
* **JWT Bearer Authentication**
* **BCrypt.Net** for password hashing
* **AutoMapper**
* **FluentValidation**
* **Swagger / OpenAPI**
* **Visual Studio**

## 🛠️ Getting Started

### Prerequisites

Install the following before running the project:

* .NET 10 SDK
* PostgreSQL
* Visual Studio or another C# development environment
* Git

### Installation & Setup

1. Clone the repository:

```bash
git clone <repository-url>
cd LibraryManagementSystem
```

2. Restore the project dependencies:

```bash
dotnet restore
```

3. Configure the database and JWT settings.

Copy the example configuration and provide your local PostgreSQL and JWT values:

```text
appsettings.example.json
```

Example configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=LibraryManagement;Username=Your_Username;Password=Your_Password"
  },
  "Jwt": {
    "Key": "Your_Key",
    "Issuer": "YourApi",
    "Audience": "YourApiAudience"
  }
}
```

Do not commit real passwords, JWT keys, or other sensitive credentials to the repository.

4. Apply the existing Entity Framework Core migrations:

```bash
dotnet ef database update
```

5. Run the API:

```bash
dotnet run
```

The development configuration runs on the URLs defined in `Properties/launchSettings.json`.

## 💻 Usage

### Authenticate

Send the user's email and password to the authentication endpoint:

```http
POST /api/Auth
Content-Type: application/json
```

Example request:

```json
{
  "email": "user@example.com",
  "password": "your-password"
}
```

A successful login returns user information together with a JWT token.

Use the returned token when accessing protected endpoints:

```http
Authorization: Bearer <your-jwt-token>
```

### Library Operations

Authenticated users can access resources according to their assigned role.

For example:

```http
GET /api/Book
```

Books, authors, categories, members, borrow records, users, and author-book relationships support appropriate CRUD and search operations.

Swagger is enabled in the Development environment and can be used to test the API endpoints and JWT authorization.

## 🗺️ Roadmap & Project Status

### Current Status

The main backend functionality has been implemented, including:

* Database models and relationships
* Entity Framework Core migrations
* CRUD services and controllers
* DTOs and mappings
* Validation
* Pagination and search
* JWT authentication
* Role-based authorization
* Global exception handling
* Swagger configuration

### Planned Improvements

* Automated unit and integration testing
* Docker/container deployment improvements
* Production deployment
* Improved API documentation
* Additional business rules and library features

##
