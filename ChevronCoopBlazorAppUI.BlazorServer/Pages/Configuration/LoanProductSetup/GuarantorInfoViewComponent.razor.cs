using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup
{
    public partial class GuarantorInfoViewComponent
    {
        [Parameter]
        public EventCallback<GetLoanProductViewModel> ModelChanged { get; set; }

        [Parameter]
        public GetLoanProductViewModel Model { get; set; }

        [Parameter]
        public EventCallback<int> OnGuarantorChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        public async Task OnProceed()
        {
            await OnGuarantorChanged.InvokeAsync(8);
        }

        public async Task OnPrevious()
        {
            await OnGuarantorChanged.InvokeAsync(6);
        }
    }
}