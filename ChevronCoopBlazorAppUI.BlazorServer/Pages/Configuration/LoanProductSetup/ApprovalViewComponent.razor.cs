using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup
{
    public partial class ApprovalViewComponent
    {
        [Parameter]
        public EventCallback<GetLoanProductViewModel> ModelChanged { get; set; }

        [Parameter]
        public GetLoanProductViewModel Model { get; set; }

        [Parameter]
        public EventCallback<int> OnApprovalChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        public async Task OnPrevious()
        {
            await OnApprovalChanged.InvokeAsync(7);
        }
        public async Task OnProceed()
        {
            await OnApprovalChanged.InvokeAsync(9);
        }
    }
}