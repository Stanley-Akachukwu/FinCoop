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
using AP.ChevronCoop.Entities.Loans.LoanOffSetTransactions;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AutoMapper;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup;
using AntDesign;
using Microsoft.JSInterop;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount.LoanAccountDashboard
{
    public partial class LoanOffsetAccountDashboard
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
        public bool IsActiveLoanAccount { get; set; }
        [Inject]
        AutoMapper.IMapper Mapper { get; set; }
        [Parameter]
        public string CustomerID { get; set; }
        public LoanOffSetDTO Model { get; set; }
        [Parameter]
        public string LoanAccountID { get; set; }

        [Parameter]
        public string LonaAccountNumber { get; set; }
        public bool showDrawal { get; set; } = true;

        [Inject]
        IEntityDataService DataService { get; set; }
        public LoanProductMasterView LoanProductMasterView { get; set; }
        public LoanAccountMasterView LoanAccountMasterView { get; set; }
        [Parameter]
        public string MembersName { get; set; }

        [Parameter]
        public string MembersNumber { get; set; }
        public List<LoanOffsetMasterView> LoanOffSetMasterViews { get; set; }
        public List<LoanOffsetMasterView> LoanOffSetMasterViews_Src { get; set; }
        [Inject]
        BrowserService BrowserService { get; set; }

        Drawer createDrawer;
        bool showCreateDrawer { get; set; } = false;

        BrowserDimension BrowserDimension;
        ClaimsPrincipal CurrentUser { get; set; }
        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }
        private string bearToken { get; set; }
        public string CustomerId { get; set; }
        string searchText;
        private bool isAdmin { get; set; } = false;
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected IJSRuntime jsRuntime { get; set; }
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();
        [Inject] NavigationManager _navigationManager { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                createDrawer.Width = (int)(BrowserDimension.Width * 0.40);

                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }
        protected override async Task OnInitializedAsync()
        {
            await HideButton();
            await GetOffSetTransactions();
            await GetCurrentUser();
        }
        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
            if (CurrentUser != null && !isAdmin)
            {
                CustomerId = CurrentUser.Claims.Where(f => f.Type == "CustomerId").FirstOrDefault().Value;
            }
        }
        public async Task GetOffSetTransactions()
        {
            var rsp = await DataService.GetValue<List<LoanOffsetMasterView>>(
             nameof(LoanOffsetMasterView), "loanAccountId", LoanAccountID);
            if (rsp.IsSuccessStatusCode)
            {
                LoanOffSetMasterViews = new List<LoanOffsetMasterView>();

                LoanOffSetMasterViews = JsonSerializer.Deserialize<List<LoanOffsetMasterView>>(rsp.Content.ToJson());
                LoanOffSetMasterViews_Src = LoanOffSetMasterViews.OrderByDescending(c => c.DateCreated).ToList();

            }
        }

        async Task onShowOffSetDrawer()
        {
            Model = new LoanOffSetDTO();
            var loanAccountResponse = await DataService.GetValue<List<LoanAccountMasterView>>(
                nameof(LoanAccountMasterView), "Id", LoanAccountID);
            if(!loanAccountResponse.IsSuccessStatusCode)
            {
                LoanAccountMasterView = new LoanAccountMasterView();
            }
            else
            {
                List<LoanAccountMasterView> loanResponseList = JsonSerializer
                    .Deserialize<List<LoanAccountMasterView>>(loanAccountResponse.Content.ToJson());
                if (loanResponseList != null && loanResponseList.Count > 0 &&
                   !string.IsNullOrEmpty(loanResponseList.FirstOrDefault().Id) &&
                   loanResponseList.FirstOrDefault().Id == LoanAccountID)
                {
                    LoanAccountMasterView = Mapper.Map<LoanAccountMasterView>(loanResponseList.FirstOrDefault());
                }
            }
            var rsp = await DataService.GetValue<List<LoanProductMasterView>>(
               nameof(LoanProductMasterView), "id", LoanAccountMasterView.LoanApplicationId_LoanProductId);

            if (!rsp.IsSuccessStatusCode)
            {
                LoanProductMasterView = new LoanProductMasterView();
            }
            else
            {
                List<LoanProductMasterView> rspResponse =
                   JsonSerializer.Deserialize<List<LoanProductMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().Id) &&
                    rspResponse.FirstOrDefault().Id == LoanAccountMasterView.LoanApplicationId_LoanProductId)
                {
                    LoanProductMasterView = Mapper.Map<LoanProductMasterView>(rspResponse.FirstOrDefault());
                }
            }

            Model.PrincipalBalance = LoanAccountMasterView.Principal;
            Model.OffSetRepaymentDate = LoanAccountMasterView.RepaymentCommencementDate.Date;
            Model.OffsetToBeCalculatedAfter = LoanAccountMasterView.RepaymentCommencementDate.Date;
            Model.LoanAccountId = LoanAccountMasterView.Id;
            Model.DeductionStartAfter = DateTime.Now.Date;
            Model.LedgerBalance = LoanAccountMasterView.PrincipalBalanceAccountId_LedgerBalance;
            showCreateDrawer = true;
        }
        async Task onCreateDone()
        {
            showCreateDrawer = false;
        }
        public async void SearchGrid(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                LoanOffSetMasterViews_Src = LoanOffSetMasterViews_Src
                    .Where(c => c.LoanAccountId_CustomerId == CustomerID).OrderByDescending(c => c.DateCreated).ToList();
            }
            else
            {
                LoanOffSetMasterViews_Src = LoanOffSetMasterViews_Src
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
