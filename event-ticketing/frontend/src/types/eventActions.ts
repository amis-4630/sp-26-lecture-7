import type { Event } from "../data/events";

export type EventAppState = {
    events: Event[];
    filter: string;
    ticketCounts: Record<number, number>;
    cartTotal: number;
};

export type EventAction =
    | { type: "ADD_TICKET"; eventId: number }
    | { type: "REMOVE_TICKET"; eventId: number }
    | { type: "SET_FILTER"; filter: string }
    | { type: "CLEAR_CART" };
