using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Users;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AntDesign;
using System.Text.Json;
using AP.ChevronCoop.Commons;
using AutoMapper;
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using ChevronCoop.Web.AppUI.BlazorServer.Data.DepositApplication;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Services.Customers.Interface;
using Microsoft.JSInterop;
using AP.ChevronCoop.Entities;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

using ChevronCoop.Web.AppUI.BlazorServer.Services.AggregateService.Interface;
using DocumentFormat.OpenXml.Bibliography;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositFundTransfer;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Dashboard
{
    public partial class AdminDashboard
    {
        //  protected string UserSID { get; set; }

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

        public List<DepositAccountsMasterView> __DepositAccountsMasterView { get; set; } = new List<DepositAccountsMasterView>();

        public CustomerMasterView _GetCustomerMasterView { get; set; } = new CustomerMasterView();

        public List<LoanAndDepositRequestMasterView> _LoanAndDepositRequestMasterView { get; set; } = new List<LoanAndDepositRequestMasterView>();

        [Inject]
        private IMasterViews _MasterView { get; set; }

        [Inject]
        private ICustomersMasterViews _CustomerMasterView { get; set; }

        [Inject]
        private IAggregationService _AggregationService { get; set; }

        public MemberProfileViewModelResult Model { get; set; }

        protected string UserSID { get; set; }

        public decimal TotalLoans { get; set; } = 0;

        public decimal totalSavings, totalSpecialDeposit, totalFixed = 0;

        public int totalLoanApplicantExecutiveLoan = 0;

        public decimal executiveLoans = 0;

        int totalDepositAccounts = 0;
        decimal totalDepositAmounts = 0;
        decimal totalDepositWithdrawal = 0;
        decimal cashAdditionForSavings = 0;
        decimal cashAdditionForSpecialDeposit = 0;
        decimal totalcashAdditionForSavingsAndSpecialDeposit = 0;

        decimal specialDepositFundTransfer = 0;
        decimal fixedDepositLiquidation = 0;



        public decimal targetLoanAmount = 0;
        public int totalTargetLoanCustomers = 0;

        public decimal longTermLoansAmount = 0;
        decimal totalLongTermLoans = 0;

        decimal shortTermLoanAmount = 0;
        int totalShortTermLoans = 0;

        decimal carLoanAmount = 0;
        int totalCarLoan = 0;

        decimal homeAppliancesAmount = 0;
        int TotalhomeAppliance = 0;

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        DateTime firstDayofMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Model = new MemberProfileViewModelResult();

            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            if (!string.IsNullOrEmpty(Principal))
            {
                var claims = authenticated.User.Claims;
                if (claims.Any())
                {
                  
                    var sid = claims.ElementAt(2).Value;
                    UserSID = sid;

                    ApplicationUserID = _UserService.ApplicationUserId;

                }
                else
                {
                    NavigationManager.NavigateTo("/Identity/Account/LogIn");
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {

                await Task.Run(async () =>
                {
                  
                    await GetAllDepositActions();
                });


                   // await LoanAndDepositRequest();

                InvokeAsync(StateHasChanged);
            }

        }




        public async Task GetAllDepositActions()
        {
            var Customers = new List<MemberProfileMasterView>();

            //Customers = await _MasterView.GetCustomMasterVieWithBoolean<MemberProfileMasterView>(nameof(MemberProfileMasterView), "applicationUserId_IsAdmin", "true", DatabaseFields.MemberProfileFields2);

			totalSavings = await _AggregationService.TotalBalanceByAccountType(nameof(DepositAccountsMasterView), "AccountType", DepositProductType.SAVINGS.ToString(), "LedgerBalance", "Status", "APPROVED");

            totalSpecialDeposit = await _AggregationService.TotalBalanceByAccountType(nameof(DepositAccountsMasterView), "AccountType", DepositProductType.SPECIAL_DEPOSIT.ToString(), "LedgerBalance", "Status", "APPROVED");

            totalFixed = await _AggregationService.TotalBalanceByAccountType(nameof(DepositAccountsMasterView), "AccountType", DepositProductType.FIXED_DEPOSIT.ToString(), "LedgerBalance", "Status", "APPROVED");

            TotalLoans = await _AggregationService.TotalBalance(nameof(LoanAccountMasterView), "principalBalanceAccountId_LedgerBalance", "loanApplicationId_Status", "APPROVED");

           
            totalLoanApplicantExecutiveLoan = await _AggregationService.TotalRowsByAccountType<LoanApplicationMasterView>(nameof(LoanApplicationMasterView), "LoanProductId_LoanProductType", LoanProductType.EXECUTIVE_LOAN.ToString(), "Principal", "Status", "APPROVED");
            executiveLoans = await _AggregationService.TotalBalanceByAccountType(nameof(LoanApplicationMasterView), "LoanProductId_LoanProductType", LoanProductType.EXECUTIVE_LOAN.ToString(), "Principal", "Status", "APPROVED");

            totalTargetLoanCustomers = await _AggregationService.TotalRowsByAccountType<LoanApplicationMasterView>(nameof(LoanApplicationMasterView), "LoanProductId_LoanProductType", LoanProductType.TARGET_LOAN.ToString(), "Principal", "Status", "APPROVED");
            targetLoanAmount = await _AggregationService.TotalBalanceByAccountType(nameof(LoanApplicationMasterView), "LoanProductId_LoanProductType", LoanProductType.TARGET_LOAN.ToString(), "Principal", "Status", "APPROVED");


            totalLongTermLoans = await _AggregationService.TotalRowsByAccountType<LoanApplicationMasterView>(nameof(LoanApplicationMasterView), "LoanProductId_LoanProductType", LoanProductType.LONG_TERM_LOAN.ToString(), "Principal", "Status", "APPROVED");
            longTermLoansAmount = await _AggregationService.TotalBalanceByAccountType(nameof(LoanApplicationMasterView), "LoanProductId_LoanProductType", LoanProductType.LONG_TERM_LOAN.ToString(), "Principal", "Status", "APPROVED");

            shortTermLoanAmount = await _AggregationService.TotalBalanceByAccountType(nameof(LoanApplicationMasterView), "LoanProductId_LoanProductType", LoanProductType.SHORT_TERM_LOAN.ToString(), "Principal", "Status", "APPROVED");
            totalShortTermLoans = await _AggregationService.TotalRowsByAccountType<LoanApplicationMasterView>(nameof(LoanApplicationMasterView), "LoanProductId_LoanProductType", LoanProductType.SHORT_TERM_LOAN.ToString(), "Principal", "Status", "APPROVED");

            totalCarLoan = await _AggregationService.TotalRowsByAccountType<LoanApplicationMasterView>(nameof(LoanApplicationMasterView), "LoanProductId_LoanProductType", LoanProductType.CAR_LOAN.ToString(), "Principal", "Status", "APPROVED");
            carLoanAmount = await _AggregationService.TotalBalanceByAccountType(nameof(LoanApplicationMasterView), "LoanProductId_LoanProductType", LoanProductType.CAR_LOAN.ToString(), "Principal", "Status", "APPROVED");

            TotalhomeAppliance = await _AggregationService.TotalRowsByAccountType<LoanApplicationMasterView>(nameof(LoanApplicationMasterView), "LoanProductId_LoanProductType", LoanProductType.TARGET_LOAN.ToString(), "Principal", "Status", "APPROVED");
            homeAppliancesAmount = await _AggregationService.TotalBalanceByAccountType(nameof(LoanApplicationMasterView), "LoanProductId_LoanProductType", LoanProductType.TARGET_LOAN.ToString(), "Principal", "Status", "APPROVED");

			//Deposit product report

			totalDepositAccounts = await _AggregationService.TotalRowsByApprovalStatus<DepositAccountsMasterView>(nameof(DepositAccountsMasterView), "Amount", "Status", "APPROVED");

            totalDepositAmounts = totalSavings + totalSpecialDeposit + totalFixed;

            totalDepositWithdrawal = await _AggregationService.TotalBalanceByAccountType(nameof(SpecialDepositActionsMasterView), "TransactionType", "Withdrawal", "Amount", "ApprovalId_Status", "APPROVED");

            cashAdditionForSavings = await _AggregationService.TotalBalanceByAccountType(nameof(SavingsCashAdditionMasterView), "TransactionJournalId_TransactionType", "SAVINGS_CASH_ADDITION", "Amount", "ApprovalId_Status", "APPROVED");

            cashAdditionForSpecialDeposit = await _AggregationService.TotalBalanceByAccountType(nameof(SpecialDepositCashAdditionMasterView), "TransactionJournalId_TransactionType", "SPECIAL_DEPOSIT_CASH_ADDITION", "Amount", "ApprovalId_Status", "APPROVED");

            totalcashAdditionForSavingsAndSpecialDeposit = cashAdditionForSavings + cashAdditionForSpecialDeposit;

            specialDepositFundTransfer = await _AggregationService.TotalBalanceByAccountType(nameof(SpecialDepositFundTransferMasterView), "TransactionJournalId_TransactionType", "SPECIAL_DEPOSIT_FUND_TRANSFER", "Amount", "ApprovalId_Status", "APPROVED");

            fixedDepositLiquidation = await _AggregationService.TotalBalanceByAccountType(nameof(FixedDepositLiquidationMasterView), "TransactionJournalId_TransactionType", ApprovalType.FIXED_DEPOSIT_LIQUIDATION.ToString(), "FixedDepositAccountId_Amount", "ApprovalId_Status", "APPROVED");
        }

       

        public async Task LoanAndDepositRequest()
        {
            var loan_deposit = new LoanAndDepositRequestMasterView();

            foreach (var item in _LoanAccountMasterView)
            {
                loan_deposit = new LoanAndDepositRequestMasterView();
                loan_deposit.Id = item.Id;
                loan_deposit.Description = item.Caption;
                loan_deposit.Amount = item.Principal;
                loan_deposit.Status = item.LoanApplicationId_Status;
                loan_deposit.DateCreated = item.DateCreated.GetValueOrDefault(DateTimeOffset.UtcNow).DateTime;
                loan_deposit.IsLoanOrProduct = "Loan Product";
                loan_deposit.ActionType = item.TenureUnit;
                _LoanAndDepositRequestMasterView.Add(loan_deposit);
            }


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

           
        }

      
        private Lazy<decimal> lazyTotalSavings;
        private Lazy<decimal> LazytotalSpecialDeposit;
        private Lazy<decimal> LazytotalFixed;
        private Lazy<decimal> lazyTotalLoans;


    }
}