# Skill Progress Tracker (.NET 9 Razor Pages)

This is a .NET 9 Razor Pages application for tracking the progress of your skills. You can:
- Add categories to organize your skills
- Add skills to each category
- Rate each skill from 0 to 10 in increments of 0.1
- Update skill ratings at any time

## Getting Started

1. Run the application using `dotnet run` in the project directory.
2. Navigate to the Skills page to manage your skills and categories.

## Project Structure
- `Pages/Skills/Index.cshtml`: Main UI for skill and category management
- `Data/SkillModels.cs`: Models for Skill and Category
- `Data/SkillService.cs`: Service for managing skills and categories
- `Data/SkillDbContext.cs`: Entity Framework Core database context
- `Controllers/SkillsController.cs`: Controller for skill-related actions
- `Views/Skills/`: Razor views for displaying and editing skills

## Notes
- This project currently uses in-memory storage or a local database (depending on configuration). Data may reset on application restart if using in-memory.
- Built with Razor Pages and follows .NET 9 best practices.