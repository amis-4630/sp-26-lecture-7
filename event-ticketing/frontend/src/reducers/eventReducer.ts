import type { EventAppState, EventAction } from "../types/eventActions";

export function eventReducer(
    state: EventAppState,
    action: EventAction,
): EventAppState {
    switch (action.type) {
        case "ADD_TICKET": {
            const current = state.ticketCounts[action.eventId] ?? 0;
            const event = state.events.find((e) => e.id === action.eventId);
            if (!event || current >= event.availableTickets) return state;
            return {
                ...state,
                ticketCounts: { ...state.ticketCounts, [action.eventId]: current + 1 },
                cartTotal: state.cartTotal + 1,
            };
        }

        case "REMOVE_TICKET": {
            const current = state.ticketCounts[action.eventId] ?? 0;
            if (current <= 0) return state;
            return {
                ...state,
                ticketCounts: { ...state.ticketCounts, [action.eventId]: current - 1 },
                cartTotal: state.cartTotal - 1,
            };
        }

        case "SET_FILTER":
            return { ...state, filter: action.filter };

        case "CLEAR_CART":
            return { ...state, ticketCounts: {}, cartTotal: 0 };
    }
}
