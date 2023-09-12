using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup
{
    public partial class TopUpViewComponent
    {
        [Parameter]
        public EventCallback<GetLoanProductViewModel> ModelChanged { get; set; }

        [Parameter]
        public GetLoanProductViewModel Model { get; set; }

        [Parameter]
        public EventCallback<int> OnTopUpSetChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        public async Task OnProceed()
        {
            await OnTopUpSetChanged.InvokeAsync(6);
        }

        public async Task OnPrevious()
        {
            await OnTopUpSetChanged.InvokeAsync(4);
        }
    }
}