# Event Ticketing System

Event Ticketing System is a REST API for managing venues, seats, events, reservations, bookings, and payments. It models the complete path from defining a venue and its seating plan to reserving an event seat and confirming it through a mock payment flow.

The project is built with .NET 10, ASP.NET Core, Entity Framework Core, and SQL Server. Its code is separated into Domain, Application, Infrastructure, and API layers, with feature-focused command handlers and FluentValidation at the application boundary.

## Features

- Create and browse venues
- Define uniquely identified seats within each venue
- Create events and assign venue seats to them at event-specific prices
- Browse the seat inventory and availability for an event
- Temporarily reserve an available event seat for a user
- Automatically release expired reservations through a background worker
- Convert active reservations into pending bookings
- Create and process mock payments
- Confirm bookings and mark seats as booked after successful payment
- Prevent competing seat updates with SQL Server row-version concurrency
- Prevent duplicate seats, event-seat assignments, bookings, and pending payments with database constraints
- Validate requests with FluentValidation
- Return centralized JSON error responses
- Explore and call the API through Swagger UI in development

## Technology stack

- .NET 10 and C#
- ASP.NET Core Web API
- Entity Framework Core 10
- SQL Server / SQL Server Express LocalDB
- FluentValidation
- Swagger / OpenAPI
- Background services

## Architecture

The solution follows a layered architecture:

- `EventTicketingSystem.Domain` contains the entities and lifecycle enums. It has no dependency on the other projects.
- `EventTicketingSystem.Application` contains feature handlers, validators, response models, repository abstractions, and application exceptions.
- `EventTicketingSystem.Infrastructure` implements the repositories and EF Core persistence model, migrations, constraints, and unit of work.
- `EventTicketingSystem.Api` exposes the HTTP controllers, configures dependency injection, maps exceptions, hosts Swagger, and runs reservation expiration in the background.

The main domain flow is:

```text
Venue -> Seat -> Event -> Event seat -> Reservation -> Booking -> Payment
```

An event seat is a venue seat assigned to a particular event with its own price and availability state. Creating a reservation changes that event seat from `Available` to `Reserved`. A successful payment completes the reservation, confirms the booking, and changes the seat to `Booked`.

![Database schema](docs/architecture/database-schema.png)

## Reservation and payment lifecycle

Reservations are currently held for **40 seconds**. A hosted worker checks for expired reservations every 20 seconds and returns their seats to the available inventory. The short duration is useful for exercising the workflow during development and can be changed in `CreateReservationCommandHandler`.

The normal lifecycle is:

1. Reserve an available event seat.
2. Create a booking before the reservation expires.
3. Create a pending payment for the booking.
4. Process the payment as successful or failed.

A successful payment moves the related records to `Succeeded`, `Confirmed`, `Completed`, and `Booked`. A failed payment leaves the booking pending, allowing another payment attempt. Only one pending payment can exist for a booking at a time.

Event-seat updates use a SQL Server `rowversion` concurrency token. Conflicting reservation, expiration, or payment operations are reported as HTTP `409 Conflict` rather than silently overwriting one another.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server or SQL Server Express LocalDB
- EF Core CLI tools (`dotnet-ef`)

Install the EF Core CLI if it is not already available:

```powershell
dotnet tool install --global dotnet-ef --version 10.0.11
```

## Local setup

Clone the repository and restore its packages:

```powershell
git clone <repository-url>
cd TicketManagement
dotnet restore EventTicketingSystem.slnx
```

Store your development connection string with .NET User Secrets:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your-sql-server-connection-string>" --project src/EventTicketingSystem.Api
```

Apply the included migrations:

```powershell
dotnet ef database update --project src/EventTicketingSystem.Infrastructure --startup-project src/EventTicketingSystem.Api
```

## Run the API

Start the API with its HTTPS launch profile:

```powershell
dotnet run --project src/EventTicketingSystem.Api --launch-profile https
```

The development endpoints are:

- API: `https://localhost:7299`
- Swagger UI: `https://localhost:7299/swagger`
- OpenAPI document: `https://localhost:7299/openapi/v1.json`

If the local HTTPS certificate is not trusted, run:

```powershell
dotnet dev-certs https --trust
```

## API endpoints

| Method | Route | Purpose |
| --- | --- | --- |
| `POST` | `/api/venues` | Create a venue |
| `GET` | `/api/venues` | List venues |
| `GET` | `/api/venues/{id}` | Get a venue |
| `POST` | `/api/venues/{venueId}/seats` | Add a seat to a venue |
| `GET` | `/api/venues/{venueId}/seats` | List a venue's seats |
| `POST` | `/api/events` | Create an event |
| `GET` | `/api/events` | List events |
| `GET` | `/api/events/{id}` | Get an event |
| `POST` | `/api/events/{eventId}/seats` | Assign and price a seat for an event |
| `GET` | `/api/events/{id}/seats` | List an event's seats and availability |
| `POST` | `/api/reservations` | Reserve an event seat |
| `GET` | `/api/reservations` | List reservations |
| `POST` | `/api/bookings` | Create a booking from a reservation |
| `GET` | `/api/bookings` | List bookings |
| `POST` | `/api/payments` | Create a pending payment |
| `POST` | `/api/payments/{paymentId}/process` | Simulate payment success or failure |
| `GET` | `/api/payments` | List payments |

Enums are serialized as readable strings, such as `Available`, `Pending`, and `Mock`.

## Configuration

Configuration follows the standard ASP.NET Core precedence rules. The required key is:

- `ConnectionStrings:DefaultConnection`

For deployed environments, supply it through an environment variable or a managed secret store. Its environment-variable form is `ConnectionStrings__DefaultConnection`.

## Build

```powershell
dotnet build EventTicketingSystem.slnx
```
