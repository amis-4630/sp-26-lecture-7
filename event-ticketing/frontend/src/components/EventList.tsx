import { useEventContext } from "../contexts/useEventContext";
import { EventCard } from "./EventCard";

// No props — reads filtered events directly from context.
export function EventList() {
  const { filteredEvents } = useEventContext();

  if (!filteredEvents || filteredEvents.length === 0) {
    return <p>No events available.</p>;
  }

  return (
    <div className="event-list">
      {filteredEvents.map((event) => (
        <EventCard key={event.id} event={event} />
      ))}
    </div>
  );
}
