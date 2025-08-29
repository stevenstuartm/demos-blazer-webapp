# Blazor Web App Learning Repository

A collection of Blazor Web App demos and examples for learning purposes.

## Overview

This repository contains various Blazor components and features to help understand Blazor Web App development. It's structured as a learning playground with different features organized in separate folders.

## Features

### 🍕 Pizza Ordering System
- **Location**: `Features/Pizza/`
- **Pages**: Pizzas1.razor, Pizzas2.razor, Pizzas3.razor
- **Features**: 
  - Pizza ordering interface
  - Database integration with SQLite
  - API controllers for pizza operations
  - DTOs for data transfer

### 📊 Counter Component
- **Location**: `Features/Counter/`
- **Page**: Counter.razor
- **Features**: Basic counter functionality demonstrating Blazor component lifecycle

### ✅ Todo List
- **Location**: `Features/Todo/`
- **Page**: Todo.razor
- **Features**: Simple todo list management

### 🌤️ Weather Forecast
- **Location**: `Features/Weather/`
- **Page**: Weather.razor
- **Features**: Weather data display

## Project Structure

```
demos.blazer.webapp/
├── Features/           # Feature-specific components
│   ├── Counter/       # Counter demo
│   ├── Pizza/         # Pizza ordering system
│   ├── Todo/          # Todo list
│   └── Weather/       # Weather forecast
├── Layout/            # Layout components
├── Pages/             # Main pages
├── wwwroot/           # Static assets
└── Program.cs         # Application entry point
```

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2022 or VS Code

### Running the Application

1. Clone the repository
2. Navigate to the project directory
3. Run the application:
   ```bash
   dotnet run
   ```
4. Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

## Technology Stack

- **Framework**: Blazor Web App (.NET 9)
- **Database**: SQLite with Entity Framework Core
- **Styling**: Bootstrap CSS
- **Fonts**: Quicksand (Google Fonts)

## Learning Objectives

This repository serves as a hands-on learning environment for:
- Blazor component development
- Data binding and state management
- API integration
- Database operations with Entity Framework
- CSS styling and responsive design
- Navigation and routing in Blazor

## Notes

This is a learning repository and not intended for production use. Feel free to experiment with the code and modify components to better understand Blazor concepts.