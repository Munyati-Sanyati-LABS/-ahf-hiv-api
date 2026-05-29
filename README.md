# AHF HIV API

REST API backend for the Africa HIV & Health Dashboard — built for the AIDS Healthcare Foundation portfolio submission.

## Tech Stack
- ASP.NET Core 8 Web API
- Entity Framework Core
- SQLite database
- CsvHelper (data seeding)
- C# / .NET 8

## Endpoints
- GET /api/countries — returns all 19 monitored African countries
- GET /api/hiv-stats — returns HIV health indicators by country and year
- GET /api/hiv-stats/{countryCode} — returns stats for a specific country
- GET /api/climate-risk — returns climate vulnerability data by country
- GET /api/programme-performance — returns AHF programme metrics

## Database
SQLite database seeded from open-source health and climate datasets
aligned with UNAIDS and World Bank indicators.

## Architecture
Clean layered architecture applying SOLID principles:
- Controllers — HTTP request handling
- Services — business logic layer
- Repository pattern — data access abstraction
- Entity Framework Core — ORM and migrations
- DbContext — database session management

## Setup
dotnet restore
dotnet run

API runs on http://localhost:5000
