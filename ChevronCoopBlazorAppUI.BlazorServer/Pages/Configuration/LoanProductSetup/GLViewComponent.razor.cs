using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup
{
    public partial class GLViewComponent
    {
        [Parameter]
        public EventCallback<GetLoanProductViewModel> ModelChanged { get; set; }

        [Parameter]
        public GetLoanProductViewModel Model { get; set; }

        [Parameter]
        public EventCallback<int> OnGLAccountChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        public async Task OnProceed()
        {
            await OnGLAccountChanged.InvokeAsync(2);
        }
        //public async Task OnPrevious()
        //{
        //    await OnGuarantorChanged.InvokeAsync(6);
        //}
    }
}