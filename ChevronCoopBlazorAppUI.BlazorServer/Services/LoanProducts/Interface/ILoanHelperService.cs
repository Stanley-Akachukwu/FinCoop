using ChevronCoop.Web.AppUI.BlazorServer.Data;

namespace ChevronCoop.Web.AppUI.BlazorServer.Services.LoanProducts.Interface
{
    public interface ILoanHelperService
    {
        Task<CommonResponseDTO> CheckCustomerLoanEligibility(string ProductId, string CustomerId);
    }
}
