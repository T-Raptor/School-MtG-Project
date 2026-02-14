# Magic: The Gathering Deck Builder

A web-based deck building application for Magic: The Gathering enthusiasts, built with ASP.NET Core and Blazor Server.

## 📋 Overview

This project is a comprehensive deck builder for Magic: The Gathering (MtG) that allows users to search for cards, build custom decks, and manage their collections. The application features user authentication, database integration, and an interactive interface for deck construction.

## ✨ Features

- **Card Search**: Search for MtG cards by name, artist, type, set, color, and rarity
- **Deck Builder**: Create and customize your own MtG decks with an intuitive interface
- **Deck Viewer**: View and manage your saved decks
- **User Authentication**: Secure user registration and login system
- **Database Integration**: Persistent storage for cards, decks, and user data

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 8.0
- **Frontend**: Blazor Server (Interactive Server-side rendering)
- **Database**: 
  - SQL Server
  - PostgreSQL (Npgsql)
- **Authentication**: ASP.NET Core Identity
- **ORM**: Entity Framework Core 8.0

## 📦 Key Dependencies

- Microsoft.AspNetCore.Identity.EntityFrameworkCore (8.0.5)
- Microsoft.EntityFrameworkCore.SqlServer (8.0.5)
- Npgsql.EntityFrameworkCore.PostgreSQL (8.0.4)

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- SQL Server or PostgreSQL database
- Visual Studio 2022 or Visual Studio Code (recommended)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/T-Raptor/School-MtG-Project.git
   cd School-MtG-Project
   ```

2. Configure the database connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Your_Connection_String_Here"
   }
   ```

3. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Navigate to `https://localhost:5001` in your browser

## 📁 Project Structure

```
MTG_Project/
├── Components/         # Blazor components and pages
│   ├── Account/       # Authentication components
│   ├── Layout/        # Layout components
│   └── Pages/         # Application pages (DeckBuilder, DeckViewer, etc.)
├── Data/              # Database context and migrations
├── Models/            # Entity models (Card, Deck, Type, etc.)
├── ModelsDTO/         # Data Transfer Objects
├── Services/          # Business logic and utilities
└── wwwroot/           # Static files (CSS, JS, images)
```

## 🎯 Core Models

- **Card**: Represents individual MtG cards with properties like name, type, color, and rarity
- **Deck**: User-created card collections
- **Set**: MtG card sets and expansions
- **Type**: Card types (Creature, Instant, Sorcery, etc.)
- **Color**: MtG color identity
- **Rarity**: Card rarity levels

## 🔐 Authentication

The application uses ASP.NET Core Identity for user authentication, providing:
- User registration
- Secure login/logout
- Password management
- Email confirmation support

## 📝 License

This is a school project developed for educational purposes.

## 👨‍💻 Author

Created as part of a school project to demonstrate full-stack web development skills with modern .NET technologies.
