# 132nd Virtual Wing - Web Application Backend

This repository contains the source code for the backend of the 132nd Virtual Wing's web application. The backend is responsible for providing a RESTful API for managing the wing's data, such as squadrons, pilots, and missions.

## Table of Contents

* [About The Project](#about-the-project)
* [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [Installation](#installation)
* [Project Structure](#project-structure)
* [API Endpoints](#api-endpoints)
* [Database](#database)
* [Contributing](#contributing)
* [License](#license)
* [Contact](#contact)

## About The Project

The 132nd Virtual Wing is a virtual combat aviation wing that simulates military aviation operations. This web application is designed to support the wing's activities by providing a centralized platform for managing its members, squadrons, and operations.

The backend is built using a clean architecture approach, with a focus on separation of concerns, testability, and maintainability. It exposes a RESTful API that can be consumed by a variety of clients, including a web-based front-end, a mobile app, or a desktop application.

## Built With

*   [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
*   [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-8.0)
*   [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
*   [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

*   [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
*   [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or your favorite code editor
*   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or another compatible database

### Installation

1.  Clone the repo
    ```sh
    git clone https://github.com/your_username_/132ndWebApp_Backend.git
    ```
2.  Open the solution file `132ndWebsite.sln` in Visual Studio.
3.  Configure the database connection string in `src/API/appsettings.json`.
4.  Run the database migrations to create the database schema.
    ```sh
    dotnet ef database update --project src/Infrastructure
    ```
5.  Run the API project.
    ```sh
    dotnet run --project src/API
    ```

## Project Structure

The project is organized into four main projects, following the principles of Clean Architecture:

*   **`132ndWebsite.Core`**: Contains the core domain models of the application, such as the `Squadron` class. This project has no dependencies on any other project in the solution.
*   **`132ndWebsite.Application`**: Contains the application logic, including services and Data Transfer Objects (DTOs). This project depends on the `Core` project.
*   **`132ndWebsite.Infrastructure`**: Contains the implementation of the data access layer, using Entity Framework Core. This project depends on the `Application` project.
*   **`132ndWebsite.API`**: The main entry point of the application, which exposes the RESTful API. This project depends on the `Application` and `Infrastructure` projects.

## API Endpoints

The following endpoints are available for the `Squadron` resource:

*   **`GET /api/squadrons`**: Get all squadrons.
*   **`GET /api/squadrons/{id}`**: Get a squadron by ID.
*   **`POST /api/squadrons`**: Create a new squadron.
*   **`PUT /api/squadrons/{id}`**: Update an existing squadron.
*   **`DELETE /api/squadrons/{id}`**: Delete a squadron by ID.

### Squadron Model

| Field      | Type   | Description                |
| ---------- | ------ | -------------------------- |
| `Id`       | `int`  | The unique ID of the squadron. |
| `Name`     | `string` | The name of the squadron.      |
| `Callsign` | `string` | The callsign of the squadron.  |

### Create Squadron DTO

| Field      | Type   | Description                |
| ---------- | ------ | -------------------------- |
| `Name`     | `string` | The name of the squadron.      |
| `Callsign` | `string` | The callsign of the squadron.  |

### Update Squadron DTO

| Field      | Type   | Description                |
| ---------- | ------ | -------------------------- |
| `Name`     | `string` | The name of the squadron.      |
| `Callsign` | `string` | The callsign of the squadron.  |

## Database

The application uses Entity Framework Core to manage the database. The database schema is defined in the `ApplicationDbContext` class in the `Infrastructure` project.

To create the database and apply the migrations, run the following command from the root of the project:

```sh
dotnet ef database update --project src/Infrastructure
```

## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4.  Push to the Branch (`git push origin feature/AmazingFeature`)
5.  Open a Pull Request

## Contact

Project Link: [https://github.com/Heidana/132ndWebApp_Backend](https://github.com/Heidana/132ndWebApp_Backend)
