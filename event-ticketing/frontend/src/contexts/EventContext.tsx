import { createContext, useContext, useReducer, type ReactNode } from "react";
import { eventReducer } from "../reducers/eventReducer";
import type { EventAppState, EventAction } from "../types/eventActions";
import type { Event } from "../data/events";
import { events as initialEvents } from "../data/events";

type EventContextType = {
  state: EventAppState;
  dispatch: React.Dispatch<EventAction>;
  filteredEvents: Event[];
};

const EventContext = createContext<EventContextType | null>(null);

const initialState: EventAppState = {
  events: initialEvents,
  filter: "All",
  ticketCounts: {},
  cartTotal: 0,
};

export function EventProvider({ children }: { children: ReactNode }) {
  const [state, dispatch] = useReducer(eventReducer, initialState);

  const filteredEvents =
    state.filter === "All"
      ? state.events
      : state.events.filter((e) =>
          e.title.toLowerCase().includes(state.filter.toLowerCase()),
        );

  return (
    <EventContext.Provider value={{ state, dispatch, filteredEvents }}>
      {children}
    </EventContext.Provider>
  );
}

export function useEventContext() {
  const context = useContext(EventContext);
  if (!context) {
    throw new Error("useEventContext must be used within an EventProvider");
  }
  return context;
}
