import { useState } from "react";
import { loanApplications } from "./data/loanApplications";
import LoanApplicationList from "./components/LoanApplicationList";
import "./App.css";

function App() {
  const [selectedType, setSelectedType] = useState("All");

  const loanTypes = [
    "All",
    ...new Set(loanApplications.map((loan) => loan.loanType)),
  ];

  const filteredLoans =
    selectedType === "All"
      ? loanApplications
      : loanApplications.filter((loan) => loan.loanType === selectedType);

  return (
    <div className="app">
      <header>
        <h1>Buckeye Lending</h1>
        <p>Loan Application Dashboard</p>
      </header>
      <main>
        <div className="type-filter">
          {loanTypes.map((type) => (
            <button
              key={type}
              className={type === selectedType ? "active" : ""} // note the className change here
              onClick={() => setSelectedType(type)}
              type="button"
            >
              {type}
            </button>
          ))}
        </div>
        <LoanApplicationList loans={filteredLoans} />
        <p className="loan-count">{filteredLoans.length} applications</p>
      </main>
    </div>
  );
}

export default App;
