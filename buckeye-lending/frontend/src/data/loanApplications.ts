// Types that match the shape returned by our ASP.NET Core API

export type LoanType = {
    id: number;
    name: string;
    description: string;
    maxTermMonths: number;
};

export type LoanApplication = {
    id: number;
    applicantName: string;
    loanAmount: number;
    annualIncome: number;
    status: string;
    riskRating: number;
    submittedDate: string;
    notes: string;
    applicantId: number;
    loanTypeId: number;
    loanType: LoanType;
};

const API_BASE = "http://localhost:5000";

/**
 * Fetch all loan applications from the API.
 *
 * Concepts demonstrated:
 *   - Fetch API:   browser-native way to make HTTP requests
 *   - Async/Await: makes asynchronous code read like synchronous code
 */
export async function fetchLoanApplications(): Promise<LoanApplication[]> {
    const response = await fetch(`${API_BASE}/api/loanapplications`);

    // fetch does NOT throw on 4xx/5xx — we must check manually
    if (!response.ok) {
        throw new Error(`API error: ${response.status} ${response.statusText}`);
    }

    const data: LoanApplication[] = await response.json();
    return data;
}
