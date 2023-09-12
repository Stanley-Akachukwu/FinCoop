using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup.EditDepositProduct
{
    public partial class EditDepositProductGLAccountComponent
    {
        [Parameter]
        public EventCallback<GetDepositProductViewModel> ModelChanged { get; set; }

        [Parameter]
        public GetDepositProductViewModel Model { get; set; }

        [Parameter]
        public EventCallback<int> OnGLAccountChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

        public async Task OnProceed()
        {
            await OnGLAccountChanged.InvokeAsync(2);
        }
    }
}