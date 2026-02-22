using Microsoft.EntityFrameworkCore;
using EventsApi.Models;

namespace EventsApi.Data;

public class EventsContext : DbContext
{
    public EventsContext(DbContextOptions<EventsContext> options)
        : base(options) { }

    public DbSet<Venue> Venues { get; set; }
    public DbSet<EventCategory> EventCategories { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<TicketOrder> TicketOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed Venues
        modelBuilder.Entity<Venue>().HasData(
            new Venue { Id = 1, Name = "Ohio Stadium", Address = "411 Woody Hayes Dr, Columbus, OH", Capacity = 102780 },
            new Venue { Id = 2, Name = "Value City Arena", Address = "555 Borror Dr, Columbus, OH", Capacity = 18809 },
            new Venue { Id = 3, Name = "Mershon Auditorium", Address = "1871 N High St, Columbus, OH", Capacity = 2477 },
            new Venue { Id = 4, Name = "Lower.com Field", Address = "96 Columbus Crew Way, Columbus, OH", Capacity = 20371 },
            new Venue { Id = 5, Name = "Scioto Mile", Address = "233 Civic Center Dr, Columbus, OH", Capacity = 5000 },
            new Venue { Id = 6, Name = "Columbus Commons", Address = "160 S High St, Columbus, OH", Capacity = 3000 }
        );

        // Seed EventCategories
        modelBuilder.Entity<EventCategory>().HasData(
            new EventCategory { Id = 1, Name = "Sports", Description = "Athletic competitions and sporting events" },
            new EventCategory { Id = 2, Name = "Music", Description = "Concerts, orchestras, and live performances" },
            new EventCategory { Id = 3, Name = "Food & Festival", Description = "Food festivals, tastings, and community gatherings" }
        );

        // Seed Events (migrated from the old static list, now with FK references)
        modelBuilder.Entity<Event>().HasData(
            new Event { Id = 1, Title = "Ohio State Buckeyes Football vs. Penn State", Date = "2026-10-24", VenueId = 1, EventCategoryId = 1, Description = "Big Ten showdown at The Horseshoe featuring Ohio State football under the lights.", AvailableTickets = 120, Price = 145 },
            new Event { Id = 2, Title = "Ohio State Buckeyes Men's Basketball vs. Michigan", Date = "2026-02-14", VenueId = 2, EventCategoryId = 1, Description = "Rivalry game in Columbus with conference implications and high-energy crowd support.", AvailableTickets = 95, Price = 85 },
            new Event { Id = 3, Title = "OSU Symphony Orchestra: Winter Masterworks", Date = "2026-01-31", VenueId = 3, EventCategoryId = 2, Description = "The Ohio State University Symphony Orchestra performs a program of classical masterworks.", AvailableTickets = 160, Price = 30 },
            new Event { Id = 4, Title = "Columbus Crew vs. FC Cincinnati", Date = "2026-05-09", VenueId = 4, EventCategoryId = 1, Description = "High-stakes MLS matchup in downtown Columbus with one of the league's best atmospheres.", AvailableTickets = 210, Price = 55 },
            new Event { Id = 5, Title = "Summer Concert Night: Indie on the Scioto", Date = "2026-07-18", VenueId = 5, EventCategoryId = 2, Description = "Outdoor concert featuring regional indie and alternative artists along the downtown riverfront.", AvailableTickets = 300, Price = 40 },
            new Event { Id = 6, Title = "Columbus Food Truck Festival", Date = "2026-06-21", VenueId = 6, EventCategoryId = 3, Description = "A citywide favorite with dozens of local food trucks, live music, and family-friendly activities.", AvailableTickets = 250, Price = 20 }
        );

        // Seed TicketOrders
        modelBuilder.Entity<TicketOrder>().HasData(
            new TicketOrder { Id = 1, EventId = 1, CustomerName = "Alex Rivera", CustomerEmail = "alex.r@osu.edu", Quantity = 2, TotalPrice = 290.00m, OrderDate = new DateTime(2026, 2, 10, 0, 0, 0, DateTimeKind.Utc) },
            new TicketOrder { Id = 2, EventId = 2, CustomerName = "Jordan Blake", CustomerEmail = "jblake@osu.edu", Quantity = 4, TotalPrice = 340.00m, OrderDate = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc) },
            new TicketOrder { Id = 3, EventId = 5, CustomerName = "Sam Patel", CustomerEmail = "sam.patel@gmail.com", Quantity = 1, TotalPrice = 40.00m, OrderDate = new DateTime(2026, 2, 15, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
