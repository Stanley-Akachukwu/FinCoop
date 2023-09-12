using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount
{
    public partial class LoanAccountGrid
    {
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        private async Task GoToLoanTopUp()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/account/loan-top-up", true);
            }
        }
    }
}