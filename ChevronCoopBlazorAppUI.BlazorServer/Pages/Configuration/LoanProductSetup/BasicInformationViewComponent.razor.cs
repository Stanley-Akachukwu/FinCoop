using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup
{
    public partial class BasicInformationViewComponent
    {
        [Parameter]
        public EventCallback<GetLoanProductViewModel> ModelChanged { get; set; }

        [Parameter]
        public GetLoanProductViewModel Model { get; set; }

        [Parameter]
        public EventCallback<int> OnBasicChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        public async Task OnProceed()
        {
            await OnBasicChanged.InvokeAsync(3);
        }

        public async Task OnPrevious()
        {
            await OnBasicChanged.InvokeAsync(1);
        }
    }
}