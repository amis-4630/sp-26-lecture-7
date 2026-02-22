export type LoanApplication = {
    id: number;
    applicantName: string;
    loanAmount: number;
    loanType: string;
    annualIncome: number;
    status: string;
    riskRating: number;
    submittedDate: string;
    notes: string;
};

// Note: In a real application, this data would come from an API or database
export const loanApplications: LoanApplication[] = [
    {
        id: 1,
        applicantName: "Sarah Johnson",
        loanAmount: 250000,
        loanType: "Mortgage",
        annualIncome: 95000,
        status: "Approved",
        riskRating: 2,
        submittedDate: "2026-01-15",
        notes: "Strong credit history, stable employment for 8 years",
    },
    {
        id: 2,
        applicantName: "Michael Chen",
        loanAmount: 32500,
        loanType: "Auto",
        annualIncome: 68000,
        status: "Pending",
        riskRating: 3,
        submittedDate: "2026-02-01",
        notes: "First-time auto loan, good income-to-debt ratio",
    },
    {
        id: 3,
        applicantName: "Emily Rodriguez",
        loanAmount: 320000,
        loanType: "Mortgage",
        annualIncome: 72000,
        status: "Denied",
        riskRating: 5,
        submittedDate: "2026-01-28",
        notes: "Income-to-loan ratio exceeds threshold, recent credit issues",
    },
    {
        id: 4,
        applicantName: "David Kim",
        loanAmount: 15000,
        loanType: "Personal",
        annualIncome: 52000,
        status: "Approved",
        riskRating: 2,
        submittedDate: "2026-02-03",
        notes: "Consolidating credit card debt, excellent payment history",
    },
    {
        id: 5,
        applicantName: "Jessica Martinez",
        loanAmount: 500000,
        loanType: "Business",
        annualIncome: 150000,
        status: "Under Review",
        riskRating: 4,
        submittedDate: "2026-02-05",
        notes: "New restaurant venture, limited business credit history",
    },
    {
        id: 6,
        applicantName: "James Wilson",
        loanAmount: 28000,
        loanType: "Auto",
        annualIncome: 75000,
        status: "Approved",
        riskRating: 1,
        submittedDate: "2026-01-20",
        notes: "Repeat customer, excellent credit score 780+",
    },
    {
        id: 7,
        applicantName: "Amanda Foster",
        loanAmount: 175000,
        loanType: "Mortgage",
        annualIncome: 88000,
        status: "Pending",
        riskRating: 3,
        submittedDate: "2026-02-10",
        notes: "First-time homebuyer, pending employment verification",
    },
    {
        id: 8,
        applicantName: "Robert Taylor",
        loanAmount: 75000,
        loanType: "Business",
        annualIncome: 120000,
        status: "Denied",
        riskRating: 4,
        submittedDate: "2026-02-08",
        notes: "Insufficient collateral for requested amount",
    },
];
