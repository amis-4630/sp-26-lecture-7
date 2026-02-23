import { useEffect, useState } from "react";
import {
  fetchLoanApplications,
  type LoanApplication,
} from "./data/loanApplications";
import LoanApplicationList from "./components/LoanApplicationList";
import "./App.css";

function App() {
  const [selectedType, setSelectedType] = useState("All");

  // ─── Three-State Pattern ─────────────────────────────────
  // Every data-fetching component needs three pieces of state:
  //   1. loading  — is the request in flight?
  //   2. error    — did something go wrong?
  //   3. data     — the successful response
  const [loans, setLoans] = useState<LoanApplication[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  // ─── useEffect ───────────────────────────────────────────
  // Runs the fetch as a *side-effect* after the component mounts.
  // The empty dependency array [] means "run once on mount".
  useEffect(() => {
    fetchLoanApplications()
      .then((data) => {
        setLoans(data);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.message);
        setLoading(false);
      });
  }, []);

  // ── Derive filter options & filtered list from state ─────
  const loanTypes = [
    "All",
    ...new Set(loans.map((loan) => loan.loanType.name)),
  ];

  const filteredLoans =
    selectedType === "All"
      ? loans
      : loans.filter((loan) => loan.loanType.name === selectedType);

  // ─── Three-State Rendering ───────────────────────────────
  // Render different UI for each state: loading → error → data
  return (
    <div className="app">
      <header>
        <h1>Buckeye Lending</h1>
        <p>Loan Application Dashboard</p>
      </header>
      <main>
        {loading && <p className="loading">Loading loan applications…</p>}

        {error && <p className="error">Failed to load applications: {error}</p>}

        {!loading && !error && (
          <>
            <div className="type-filter">
              {loanTypes.map((type) => (
                <button
                  key={type}
                  className={type === selectedType ? "active" : ""}
                  onClick={() => setSelectedType(type)}
                  type="button"
                >
                  {type}
                </button>
              ))}
            </div>
            <LoanApplicationList loans={filteredLoans} />
            <p className="loan-count">{filteredLoans.length} applications</p>
          </>
        )}
      </main>
    </div>
  );
}

export default App;
