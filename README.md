Users Azure Function API

A CRUD API built using Azure Functions (.NET 8 Isolated), Entity Framework Core, and SQLite.

Features
  Get all users
  Get user by ID
  Create user
  Update user
  Delete user

  
Technologies
  .NET 8
  Azure Functions
  Entity Framework Core
  SQLite
  Dependency Injection

  
Run Project
dotnet build
func start


Database Migration
dotnet ef migrations add InitialCreate
dotnet ef database update

