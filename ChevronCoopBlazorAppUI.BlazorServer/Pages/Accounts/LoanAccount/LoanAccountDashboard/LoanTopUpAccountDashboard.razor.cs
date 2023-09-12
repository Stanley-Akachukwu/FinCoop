using System;
using Microsoft.AspNetCore.Components;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using System.Text.Json;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using AP.ChevronCoop.Entities.Loans.LoanTopupTransactions;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using Microsoft.AspNetCore.Components.Authorization;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount.LoanAccountDashboard
{
    public partial class LoanTopUpAccountDashboard
    {
        [Parameter]
        public decimal LoanBalance { get; set; } = 0;
        [Parameter]
        public decimal LoanAmount { get; set; } = 0;
        [Parameter]
        public decimal TotalAmountRepaid { get; set; } = 0;
        [Parameter]
        public decimal NextRepaymentAmount { get; set; } = 0;

        [Parameter]
        public string CustomerID { get; set; }

        [Parameter]
        public string LoanAccountID { get; set; }

        [Parameter]
        public string LonaAccountNumber { get; set; }
        [Parameter]
        public bool IsActiveLoanAccount { get; set; }

        public bool showDrawal { get; set; } = true;
        string searchText;
        [Inject]
        IEntityDataService DataService { get; set; }
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Parameter]
        public string MembersName { get; set; }

        [Parameter]
        public string MembersNumber { get; set; }
        public List<LoanTopupMasterView> LoanTopupMasterViews { get; set; }
        public List<LoanTopupMasterView> LoanTopupMasterViews_Src { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        private bool isAdmin { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            await GetTopUpTransactions();
            await HideButton();
        }

        public async Task GetTopUpTransactions()
        {
            var rsp = await DataService.GetValue<List<LoanTopupMasterView>>(
             nameof(LoanTopupMasterView), "loanAccountId", LoanAccountID);
            if (rsp.IsSuccessStatusCode)
            {
                LoanTopupMasterViews = new List<LoanTopupMasterView>();

                LoanTopupMasterViews = JsonSerializer.Deserialize<List<LoanTopupMasterView>>(rsp.Content.ToJson());
                LoanTopupMasterViews_Src = LoanTopupMasterViews.OrderByDescending(c => c.DateCreated).ToList();

            }
        }
        private async Task OnProceedToTopUp()
        {
            var loanId = LoanAccountID;
            var url = $"/account/loanTopUp/{loanId}";
            _navigationManager.NavigateTo(url, true);
        }
        public async void SearchGrid(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                LoanTopupMasterViews_Src = LoanTopupMasterViews_Src
                    .Where(c => c.LoanAccountId_CustomerId == CustomerID).OrderByDescending(c => c.DateCreated).ToList();
            }
            else
            {
                LoanTopupMasterViews_Src = LoanTopupMasterViews_Src
                    .Where(c => (c.Description?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.ApprovalId_Status?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ??
                                    false))
                    .Take(5)
                    .OrderByDescending(c => c.DateCreated)
                    .ToList();
            }
        }
        private async Task HideButton()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var CurrentUser = authState.User;
            if (CurrentUser != null)
            {
                var admin = CurrentUser.Claims.Where(f => f.Type == "IsAdmin").FirstOrDefault();
                if (admin != null && admin.Value.ToLower() == "true")
                    isAdmin = true;
            }
        }
    }
}
