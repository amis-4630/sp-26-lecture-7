# Week 7 — Entity Model Expansion (Event Ticketing)

**Topic:** Adding EF Core with related entities, foreign keys, and navigation properties to the Event Ticketing API.

---

## Lecture Connection

This code directly applies the four core concepts from the Week 7 lecture:

| Lecture Topic | Where you'll see it in the code |
|---|---|
| **DbContext & DbSet** | `EventsContext` manages 4 `DbSet<>` properties — one per table |
| **LINQ** | `EventsController.GetAll()` chains `.Where()` and `.ToListAsync()` to filter by category and search text |
| **Relationships** | `VenueId`/`EventCategoryId`/`EventId` foreign keys + navigation properties wire up one-to-many across entities |
| **Eager Loading** | `GetAll()` and `GetById()` use `.Include(e => e.Venue).Include(e => e.EventCategory)` to pull related data in a single query |

---

## Overview

Previously the Event Ticketing API used a **static in-memory list** in the controller — no database, no relationships. This update introduces **EF Core with the InMemory provider** and **3 new entities** that normalize the data model.

### Before

```
EventsController (static List<Event>)
└── Event
    ├── Id
    ├── Title
    ├── Date
    ├── Location (string)       ← flat string, no referential integrity
    ├── Description
    ├── AvailableTickets
    └── Price
```

### After

```
Venue  ──1:N──►  Event  ◄──1:N──  EventCategory (lookup)
                   │
                   └──1:N──►  TicketOrder
```

---

## New Entities

### 1. `Venue` — `Models/Venue.cs`

Replaces the flat `Location` string with a proper entity. One venue hosts many events.

| Property   | Type     | Notes                   |
|------------|----------|-------------------------|
| `Id`       | `int`    | Primary key             |
| `Name`     | `string` | Required, max 150 chars |
| `Address`  | `string` | Required, max 200 chars |
| `Capacity` | `int`    | Max attendees           |

**Navigation:** `List<Event> Events`

**Key concept:** Lookup table — replaces free-text location strings with a normalized entity.

**Seed data:**

| Id | Name              | Capacity |
|----|-------------------|----------|
| 1  | Ohio Stadium      | 102,780  |
| 2  | Value City Arena  | 18,809   |
| 3  | Mershon Auditorium| 2,477    |
| 4  | Lower.com Field   | 20,371   |
| 5  | Scioto Mile       | 5,000    |
| 6  | Columbus Commons  | 3,000    |

---

### 2. `EventCategory` — `Models/EventCategory.cs`

Categorizes events into types (Sports, Music, Food & Festival).

| Property      | Type     | Notes                   |
|---------------|----------|-------------------------|
| `Id`          | `int`    | Primary key             |
| `Name`        | `string` | Required, max 50 chars  |
| `Description` | `string` | Max 200 chars           |

**Navigation:** `List<Event> Events`

**Key concept:** Lookup table — ensures consistent categorization instead of free-text.

**Seed data:**

| Id | Name           |
|----|----------------|
| 1  | Sports         |
| 2  | Music          |
| 3  | Food & Festival|

---

### 3. `TicketOrder` — `Models/TicketOrder.cs`

Tracks ticket purchases against an event. Many orders per event.

| Property        | Type       | Notes                       |
|-----------------|------------|-----------------------------|
| `Id`            | `int`      | Primary key                 |
| `CustomerName`  | `string`   | Required, max 100 chars     |
| `CustomerEmail` | `string`   | Required, max 150 chars     |
| `Quantity`      | `int`      | Number of tickets purchased |
| `TotalPrice`    | `decimal`  | Auto-calculated on creation |
| `OrderDate`     | `DateTime` | Server-set timestamp        |
| `EventId`       | `int`      | Foreign key                 |

**Navigation:** `Event? Event`

**Key concept:** Foreign key + business logic — each order belongs to one event; placing an order reduces `AvailableTickets`.

---

## Changes to Existing Files

### `Models/Event.cs`

- **Removed:** `string Location` property
- **Added:** `VenueId` (int FK) + `Venue?` navigation
- **Added:** `EventCategoryId` (int FK) + `EventCategory?` navigation
- **Added:** `List<TicketOrder> TicketOrders` collection
- **Added:** Data annotation attributes (`[Required]`, `[MaxLength]`, `[Column]`)

### `Controllers/EventsController.cs`

- **Replaced** static `List<Event>` with injected `EventsContext`
- `GetAll()` now uses LINQ `.Where()` with `categoryId` and `search` query params
- `GetAll()` and `GetById()` use `.Include()` for eager loading of `Venue` and `EventCategory`
- `GetById()` also includes `TicketOrders`

### `Program.cs`

- **Added:** `using` for `EventsApi.Data` and EF Core registration
- **Added:** `AddDbContext<EventsContext>` with InMemory provider
- **Added:** `ReferenceHandler.IgnoreCycles` to prevent circular reference serialization errors
- **Added:** Seed data initialization block (`EnsureCreated()`)

### `api.csproj`

- **Added package:** `Microsoft.EntityFrameworkCore.InMemory`

---

## New Files

| File | Purpose |
|------|---------|
| `Data/EventsContext.cs` | `DbContext` with 4 `DbSet<>` properties and all seed data |
| `Models/Venue.cs` | Venue entity |
| `Models/EventCategory.cs` | Event category lookup entity |
| `Models/TicketOrder.cs` | Ticket order entity |
| `Controllers/VenuesController.cs` | `GET /api/venues`, `GET /api/venues/{id}` |
| `Controllers/EventCategoriesController.cs` | `GET /api/eventcategories`, `GET /api/eventcategories/{id}` |
| `Controllers/OrdersController.cs` | `GET/POST /api/events/{id}/orders` (nested route) |

---

## New API Endpoints

| Controller                | Route                           | Methods   |
|---------------------------|---------------------------------|-----------|
| `VenuesController`        | `api/venues`                    | GET       |
| `EventCategoriesController`| `api/eventcategories`          | GET       |
| `OrdersController`        | `api/events/{id}/orders`        | GET, POST |

**Key concept:** Nested routes — orders are scoped under their parent event.

---

## EF Core Concepts Demonstrated

| Concept                   | Where it appears                                         |
|---------------------------|----------------------------------------------------------|
| DbContext & DbSet         | `EventsContext` with 4 `DbSet<>` properties              |
| LINQ queries              | `.Where()` chains in `EventsController.GetAll()`         |
| Foreign keys              | `VenueId`, `EventCategoryId`, `EventId`                  |
| Navigation properties     | `Venue.Events`, `Event.TicketOrders`, etc.               |
| One-to-many relationships | Venue → Events, Event → TicketOrders                     |
| Lookup / reference tables | `Venue` and `EventCategory` entities                     |
| Eager loading (`Include`) | `EventsController.GetAll()` and `GetById()`              |
| Seed data (`HasData`)     | `EventsContext.OnModelCreating()`                        |
| Cycle handling            | `ReferenceHandler.IgnoreCycles` in `Program.cs`          |

---

## Try It

```bash
cd event-ticketing/api
dotnet run
```

Then explore using **Swagger UI** at [http://localhost:5000/swagger](http://localhost:5000/swagger), or with curl:

```bash
# All venues
curl http://localhost:5000/api/venues

# All event categories
curl http://localhost:5000/api/eventcategories

# All events (with venue + category included)
curl http://localhost:5000/api/events

# Filter events by category (1=Sports)
curl http://localhost:5000/api/events?categoryId=1

# Single event with orders
curl http://localhost:5000/api/events/1

# Orders for event 1
curl http://localhost:5000/api/events/1/orders

# Place a ticket order
curl -X POST http://localhost:5000/api/events/1/orders \
  -H "Content-Type: application/json" \
  -d '{"customerName":"Jane Doe","customerEmail":"jane@osu.edu","quantity":2}'
```
