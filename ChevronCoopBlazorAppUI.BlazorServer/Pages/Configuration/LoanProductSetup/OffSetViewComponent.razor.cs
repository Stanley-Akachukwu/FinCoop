using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup
{
    public partial class OffSetViewComponent
    {
        [Parameter]
        public EventCallback<GetLoanProductViewModel> ModelChanged { get; set; }

        [Parameter]
        public GetLoanProductViewModel Model { get; set; }

        [Parameter]
        public EventCallback<int> OnOffSetChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        public async Task OnProceed()
        {
            await OnOffSetChanged.InvokeAsync(5);
        }

        public async Task OnPrevious()
        {
            await OnOffSetChanged.InvokeAsync(3);
        }
    }
}