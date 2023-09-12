using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;

namespace ChevronCoop.Web.AppUI.BlazorServer.Services.Customers.Interface
{
    public interface ICustomersMasterViews
    {
        Task<List<DepositAccountsMasterView>> GetDepositAccountMasterView(string CustomerID);

        Task<CustomerMasterView> GetCustomerMasterView(string CustomerID);

        Task<List<DepositAccountsMasterView>> GetAllDepositAccountMasterView();

        Task<List<DepositAccountsMasterView>> GetCustomerDepositActions(string customerID, string orderbyColumn, int count, string asc_or_desc);

        Task<List<LoanAccountMasterView>> GetAllCustomersLoansAccountMasterView();

        Task<string> GetCurrentUser();

        Task<List<LoanAccountMasterView>> GetCustomerLoanProducts(string CustomerId);

       
    }
}
