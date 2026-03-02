import { EventProvider, useEventContext } from "./contexts/EventContext";
import { EventList } from "./components/EventList";
import "./App.css";

// ─── CartSummary ──────────────────────────────────────────────────────────────
// Reads cartTotal from context and dispatches CLEAR_CART.
// Only renders when tickets have been selected.
function CartSummary() {
  const { state, dispatch } = useEventContext();
  if (state.cartTotal === 0) return null;

  return (
    <div className="cart-summary">
      <span>
        🎟 {state.cartTotal} ticket{state.cartTotal !== 1 ? "s" : ""} selected
      </span>
      <button onClick={() => dispatch({ type: "CLEAR_CART" })}>
        Clear Cart
      </button>
    </div>
  );
}

// ─── App ─────────────────────────────────────────────────────────────────────
// EventProvider owns the reducer. All descendants access state via useEventContext().
function App() {
  return (
    <EventProvider>
      <div className="app">
        <h1>OSU Event Finder</h1>
        <CartSummary />
        <EventList />
      </div>
    </EventProvider>
  );
}

export default App;
