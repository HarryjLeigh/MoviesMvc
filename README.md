# Movies & Recipes MVC Solution

This repository contains two ASP.NET Core MVC projects that demonstrate how to build data-driven web applications for tracking both movie collections and recipe databases using Entity Framework Core, Razor views.
## Table of Contents
1. [Projects Overview](#projects-overview)
2. [Features](#features)
3. [Challenges Faced](#challenges-faced)
4. [Getting Started](#getting-started)
   - [Prerequisites](#prerequisites)
   - [Clone & Setup](#clone--setup)
   - [Configure Database](#configure-database)
   - [Apply Migrations & Seed Data](#apply-migrations--seed-data)
   - [Run the Applications](#run-the-applications)
5. [Project Structure](#project-structure)
6. [Lessons Learned](#lessons-learned)

## Projects Overview

- **Movies**: A code-first web application for managing a movie catalog, with search, filtering, and CRUD operations.
- **Recipes**: A feature-rich MVC application demonstrating multi-criteria filtering for recipes, dynamic dropdowns, and a standalone rating module powered by a separate controller and dedicated views.

## Features

### Movies Project
- **CRUD Operations**: Manage movie entries (Create, Read, Update, Delete) `MoviesController`.
- **Search & Filter**: Search by title and filter by genre using LINQ and a `MovieGenreViewModel`.
- **Data Validation**: Server- and client-side checks via DataAnnotations (e.g., `[Required]`, `[StringLength]`, `[Range]`, regex patterns).
- **EF Core Code-First**: Automatic migrations, `MvcMoviesContext`, and seed data initialization via `SeedData.Initialize`.
- **Responsive UI**: Bootstrap-based layouts, shared `_Layout.cshtml`, and partial views for validation scripts.

### Recipes Project
- **CRUD Operations**: Full create, read, update, delete support for recipes.
- **Multi-Criteria Filtering**: Filter recipes by cuisine, vegetarian option, difficulty, and dish type concurrently.
- **Standalone Rating Module**: A separate `RatingController` and dedicated Razor pages under `Views/Rating` allow users to submit ratings, view average scores, and rating counts. Ratings persist in the database with validation to ensure data integrity.
- **Dynamic Dropdowns**: Generate filter dropdowns on-the-fly with LINQ and `SelectList` in views.
- **EF Core Migrations & Seeding**: Code-first migrations in `Data/Migrations` and seed logic for initial recipe and rating data.

## Challenges Faced
- **Configuring Multiple Migrations**: Consolidating EF Core migrations across two projects while maintaining correct namespaces.
- **Composable LINQ Queries**: Designing flexible query composition to handle multiple optional filters.
- **Rating Module Separation**: Isolating rating logic into its own controller and views required careful routing and view organization.
- **Data Seeding Idempotency**: Writing seed methods that detect existing entries to prevent duplicates on reruns.

## Getting Started

### Prerequisites
- [.NET 9.0 SDK or later](https://dotnet.microsoft.com/download)
- SQL Server or another database provider (e.g., SQLite)
- Optional: Visual Studio 2022 / VS Code with C# extensions

### Clone & Setup
```bash
git clone <repository-url>
cd <repository-root>
```

### Configure Database
1. Open `Movies/appsettings.json` and `Recipes/appsettings.json`.
2. Update the `ConnectionStrings:DefaultConnection` to point to your database server.

### Apply Migrations & Seed Data
```bash
# Movies project
dotnet ef database update --project Movies

# Recipes project
dotnet ef database update --project Recipes
```

### Run the Applications
```bash
# From the solution root
dotnet run --project Movies
# In another terminal
dotnet run --project Recipes
```

## Project Structure
```
/MoviesMvc.sln
│
├── Movies/                 # Movies MVC project
│   ├── Controllers/        # MVC controllers (MoviesController)
│   ├── Data/               # EF Core DbContext (MvcMoviesContext) and Migrations
│   ├── Models/             # Movie domain and view models
│   ├── Views/              # Razor views (Movies and Shared)
│   ├── wwwroot/            # Static assets
│   └── Program.cs          # App startup
│
└── Recipes/                # Recipes MVC project
    ├── Controllers/        # MVC controllers
    │   ├── RecipeController      # Main CRUD controller
    │   └── RatingController      # Separate rating module controller
    ├── Data/               # EF Core DbContext (RecipesContext) and Migrations
    ├── Models/             # Recipe and Rating domain models
    ├── Views/              # Razor views
    │   ├── Recipe/             # CRUD views for recipes
    │   └── Rating/             # Dedicated rating pages (submit & view)
    ├── wwwroot/            # Static assets
    └── Program.cs          # App startup
```

## Lessons Learned
- **EF Core Code-First** simplifies schema evolution but needs disciplined migration management.
- **LINQ Composition** enables powerful, flexible filtering capabilities.
- **DataAnnotations** bridge server and client validation seamlessly.
- **Modular Design**: Splitting ratings into a separate controller/page clarifies responsibilities and enhances maintainability.
- Multi-project solutions reinforce best practices in separation of concerns and project organization.
