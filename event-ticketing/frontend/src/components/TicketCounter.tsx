import { useEventContext } from "../contexts/EventContext";

interface TicketCounterProps {
  eventId: number;
  availableTickets: number;
}

// Reads ticketCounts[eventId] from context and dispatches ADD/REMOVE actions.
// No props for state or handlers — context eliminates the need for prop drilling.
export function TicketCounter({
  eventId,
  availableTickets,
}: TicketCounterProps) {
  const { state, dispatch } = useEventContext();
  const count = state.ticketCounts[eventId] ?? 0;

  return (
    <div className="ticket-counter">
      <button
        className="ticket-counter-btn"
        onClick={() => dispatch({ type: "REMOVE_TICKET", eventId })}
        disabled={count === 0}
        aria-label="Remove one ticket"
      >
        −
      </button>
      <span className="ticket-count">{count}</span>
      <button
        className="ticket-counter-btn"
        onClick={() => dispatch({ type: "ADD_TICKET", eventId })}
        disabled={count >= availableTickets}
        aria-label="Add one ticket"
      >
        +
      </button>
    </div>
  );
}
