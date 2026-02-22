import type { LoanApplication } from "../data/loanApplications";
import LoanApplicationCard from "./LoanApplicationCard";

type LoanApplicationListProps = {
  loans: LoanApplication[];
};

function LoanApplicationList({ loans }: LoanApplicationListProps) {
  return (
    <div className="loan-grid">
      {loans.map((loan) => (
        <LoanApplicationCard key={loan.id} loan={loan} />
      ))}
    </div>
  );
}

export default LoanApplicationList;
