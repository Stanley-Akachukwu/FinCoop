using AP.ChevronCoop.Entities.Loans.LoanAccounts;

namespace AP.ChevronCoop.AppDomain.Loans.LoanAccounts;

public class GetLoanAccountViewModel : LoanAccountMasterView
{
    public decimal PeriodPayment { get; set; }
    public decimal AmountPaid { get; set; }
}