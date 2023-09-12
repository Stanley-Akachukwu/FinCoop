using AntDesign;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using ChevronCoop.Web.AppUI.BlazorServer.Data.DepositApplication;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Users;
using ChevronCoop.Web.AppUI.BlazorServer.Services.Customers.Interface;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Dashboard
{
    public partial class DefaultDashboard : ComponentBase
    {
        protected string UserSID { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }
        private bool ShowGetStarted { get; set; } = false;

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        private UserService _UserService { get; set; }

        public string ApplicationUserID { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        string CustomerId { get; set; }

        private bool isLoading = true;

        public List<DepositAccountsMasterView> _DepositAccountsMasterViewSrc { get; set; } = new List<DepositAccountsMasterView>();

        public List<LoanAccountMasterView> _LoanAccountMasterView { get; set; } = new List<LoanAccountMasterView>();

        public List<DepositAccountsMasterView> _DepositAccountsMasterView { get; set; } = new List<DepositAccountsMasterView>();

        public List<LoanApplicationMasterView> _LoanApplicationMasterView { get; set; } = new List<LoanApplicationMasterView>();

        public CustomerMasterView _GetCustomerMasterView { get; set; } = new CustomerMasterView();

        public List<LoanAndDepositRequestMasterView> _LoanAndDepositRequestMasterView { get; set; } = new List<LoanAndDepositRequestMasterView>();

        public List<DepositAccountsMasterView> _CheckDepositAccountsMasterView { get; set; } = new List<DepositAccountsMasterView>();

        [Inject]
        private ICustomersMasterViews _CustomersMasterView { get; set; }

        [Inject]
        private ICustomersMasterViews _CustomerMasterView { get; set; }


        public bool showAddDrawal = true;

        [Inject]
        private IMasterViews _MasterView { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity.IsAuthenticated)
            {
                var hasCompletedKyc = Boolean.Parse(user.FindFirstValue(ClaimTypes.StateOrProvince));
                if (!hasCompletedKyc)
                    ShowGetStarted = true;

                ApplicationUserID = _UserService.ApplicationUserId;

                await GetCurrentUser();
                _GetCustomerMasterView = await _CustomersMasterView.GetCustomerMasterView(CustomerId);

                await GetCustomerDepositProducts();

                await LoanAndDepositRequest();

                isLoading = false;

            }
            else
            {
                NavigationManager.NavigateTo("/identity/account/logIn");
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {


            }

        }


        private async Task GetCurrentUser()
        {

            try
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                CurrentUser = authState.User;
                if (CurrentUser != null)
                {
                    var checker = CurrentUser.Claims.Where(f => f.Type == "CustomerId").FirstOrDefault();
                    CustomerId = (checker != null) ? checker.Value : "";
                }
            }
            catch { }
        }


        decimal savingsAccount = 0;
        decimal SpecialDepositAccount = 0;
        decimal FixedDepositAccount = 0;
        public async Task GetCustomerDepositProducts()
        {
            _DepositAccountsMasterViewSrc = await _CustomerMasterView.GetDepositAccountMasterView(CustomerId);

            savingsAccount = @_DepositAccountsMasterViewSrc.Where(x => x.AccountType == DepositProductType.SAVINGS.ToString()).Sum(x => x.LedgerBalance);

            SpecialDepositAccount = _DepositAccountsMasterViewSrc.Where(x => x.AccountType == DepositProductType.SPECIAL_DEPOSIT.ToString()).Sum(x => x.LedgerBalance);

            FixedDepositAccount = _DepositAccountsMasterViewSrc.Where(x => x.AccountType == DepositProductType.FIXED_DEPOSIT.ToString()).Sum(x => x.LedgerBalance);

            _LoanAccountMasterView = await _CustomerMasterView.GetCustomerLoanProducts(CustomerId);
            _DepositAccountsMasterView = await _CustomerMasterView.GetCustomerDepositActions(CustomerId, "dateCreated", 10, "desc");
        }



        public async Task LoanAndDepositRequest()
        {

            _LoanApplicationMasterView = await _MasterView.GetCustomMasterView<LoanApplicationMasterView>(nameof(LoanApplicationMasterView), "customerId", CustomerId, DatabaseFields.LoanUserDashboard);

            var loan_deposit = new LoanAndDepositRequestMasterView();

            foreach (var item in _DepositAccountsMasterView)
            {
                loan_deposit = new LoanAndDepositRequestMasterView();
                loan_deposit.Id = item.Id;
                loan_deposit.Description = item.Caption;
                loan_deposit.Amount = item.Amount;
                loan_deposit.Status = item.Status;
                loan_deposit.DateCreated = item.DateCreated.GetValueOrDefault(DateTimeOffset.UtcNow).DateTime;
                loan_deposit.IsLoanOrProduct = "Deposit Product";
                loan_deposit.ActionType = item.AccountType;

                _LoanAndDepositRequestMasterView.Add(loan_deposit);
            }

            foreach (var item in _LoanApplicationMasterView)
            {
                loan_deposit = new LoanAndDepositRequestMasterView();
                loan_deposit.Id = item.Id;
                loan_deposit.Description = $"{item.LoanProductId_LoanProductType} ({item.LoanProductId_Name})";
                loan_deposit.Amount = item.Principal;
                loan_deposit.Status = item.Status;
                loan_deposit.DateCreated = item.DateCreated.GetValueOrDefault(DateTimeOffset.UtcNow).DateTime;
                loan_deposit.IsLoanOrProduct = "Loan Product";
                loan_deposit.ActionType = item.LoanProductId_Name;

                _LoanAndDepositRequestMasterView.Add(loan_deposit);
            }

        }

        private string GetGridItemClass(int index)
        {
            return index % 2 == 0 ? "" : "bg-gray-50 dark:bg-gray-700";
        }

        private async Task OnUpdateChangedHandler(bool isComplete)
        {

            await InvokeAsync(StateHasChanged);
        }

        private async Task OnShowDrawal()
        {
            showCashAdditionDrawal = true;
            showAddDrawal = true;
        }


        private bool IsDropdownOpen = false;

        private void ToggleDropdown()
        {
            IsDropdownOpen = !IsDropdownOpen;
        }

        public bool showCashAdditionDrawal { get; set; } = false;

    }
}