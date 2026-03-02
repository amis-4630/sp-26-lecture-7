import type { Event } from "../data/events";
import { TicketCounter } from "./TicketCounter";

interface EventCardProps {
  event: Event;
}

export function EventCard({ event }: EventCardProps) {
  const isSoldOut = event.availableTickets === 0;
  const isLowStock = event.availableTickets > 0 && event.availableTickets < 50;

  return (
    <div className={`event-card ${isSoldOut ? "sold-out" : ""}`}>
      <div className="event-card-header">
        <h2 className="event-title">{event.title}</h2>
        {isSoldOut && <span className="badge badge-sold-out">Sold Out</span>}
        {isLowStock && <span className="badge badge-low-stock">Low Stock</span>}
      </div>
      <p className="event-date">
        📅{" "}
        {new Date(event.date).toLocaleDateString("en-US", {
          weekday: "long",
          year: "numeric",
          month: "long",
          day: "numeric",
        })}
      </p>
      <p className="event-location">📍 {event.location}</p>
      <p className="event-description">{event.description}</p>
      <div className="event-card-footer">
        <span className="event-price">${event.price} / ticket</span>
        <span className="event-availability">
          {event.availableTickets} left
        </span>
        {/* TicketCounter dispatches ADD_TICKET / REMOVE_TICKET directly from context */}
        <TicketCounter
          eventId={event.id}
          availableTickets={event.availableTickets}
        />
      </div>
    </div>
  );
}
