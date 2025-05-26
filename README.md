# Blazor Skill Progress Tracker

This is a Blazor Server application for tracking the progress of your skills. You can:
- Add categories to organize your skills
- Add skills to each category
- Rate each skill from 0 to 10 in increments of 0.1
- Update skill ratings at any time

## Getting Started

1. Run the application using `dotnet run` in the project directory.
2. Navigate to the Skill Tracker page from the sidebar to manage your skills and categories.

## Project Structure
- `Pages/SkillTracker.razor`: Main UI for skill and category management
- `Data/SkillModels.cs`: Models for Skill and Category
- `Data/SkillService.cs`: In-memory service for managing skills and categories

## Notes
- This project uses in-memory storage. Data will reset on application restart.
- Designed with component-based architecture and Blazor best practices.
