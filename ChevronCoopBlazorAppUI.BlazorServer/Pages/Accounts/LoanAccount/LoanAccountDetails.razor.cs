using AntDesign;
using AntDesign.Core.Extensions;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Services.Customers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Claims;
using System.Text.Json;
using AP.ChevronCoop.AppDomain.Loans.LoanAccounts;
using System.Data;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount
{
    public partial class LoanAccountDetails
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string PageType { get; set; }

        public string PageTitle { get; set; }

        public string MenuToShow { get; set; } = "Transaction";

        public bool showExecutiveLoan { get; set; } = false;

        public bool showTargetLoan { get; set; } = false;

        public bool showShortTermLoan { get; set; } = false;
        public bool showCarLoan { get; set; } = false;
        public bool showHouseApplience { get; set; } = false;
        public bool showLongTermLoan { get; set; } = false;

        string CustomerId { get; set; }

        public CustomerMasterView MemberProfile;

        public GetLoanAccountViewModel AccountsMasterView { get; set; }
        public LoanApplicationMasterView LoanApplicationMasterView { get; set; }
        public LoanAccountMasterView LoanAccountMasterView { get; set; }
        ClaimsPrincipal CurrentUser { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        public CustomerHelper _customerHelper { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }
        [Inject]
        TempObjectService tempService { get; set; }
        [Parameter] public string CustomerFullName { get; set; }
        [Parameter] public string CustomerImage { get; set; }
        [Parameter] public string CustomerAccountNumber { get; set; }
        [Parameter] public string CustomerAccountType { get; set; }

        public decimal LoanBalance { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal TotalAmountRepaid { get; set; }
        public decimal NextRepaymentAmount { get; set; }
        public bool IsActiveLoanAccount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MemberProfile = new CustomerMasterView();

            var authenticated = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            await GetCurrentUser();

            await GetProfile();

            await GetAccountDetails();

            if (LoanApplicationMasterView.LoanProductId_LoanProductType.ToLowerInvariant() == "executive_loan")
            {
                showExecutiveLoan = true;
                PageTitle = "My Executive Loan Account Dashboard";
                MenuToShow = "executive_loan";
            }
            else if (LoanApplicationMasterView.LoanProductId_LoanProductType.ToLowerInvariant() == "target_loan")
            {
                showTargetLoan = true;
                PageTitle = "My Target Loan Account Dashboard";
                MenuToShow = "target_loan";
            }
            else if (LoanApplicationMasterView.LoanProductId_LoanProductType.ToLowerInvariant() == "long_term_loan")
            {
                showLongTermLoan = true;
                PageTitle = "My Long Term Account Dashboard";
                MenuToShow = "long_term_loan";
            }
            else if (LoanApplicationMasterView.LoanProductId_LoanProductType.ToLowerInvariant() == "short_term_loan")
            {
                showShortTermLoan = true;
                PageTitle = "My Short Term Account Dashboard";
                MenuToShow = "short_term_loan";
            }
            else if (LoanApplicationMasterView.LoanProductId_LoanProductType.ToLowerInvariant() == "car_loan")
            {
                showCarLoan = true;
                PageTitle = "My Car Account Dashboard";
                MenuToShow = "car_loan";
            }
            else if (LoanApplicationMasterView.LoanProductId_LoanProductType.ToLowerInvariant() == "house_appliance_loan")
            {
                showHouseApplience = true;
                PageTitle = "My House Applience Account Dashboard";
                MenuToShow = "house_appliance_loan";
            }

        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
            if (CurrentUser != null)
            {
                CustomerId = CurrentUser.Claims.Where(f => f.Type == "CustomerId").FirstOrDefault().Value;
            }
        }

        public async Task GetProfile()
        {
            var rsp = await DataService.GetValue<List<CustomerMasterView>>(
                nameof(CustomerMasterView), "id", CustomerId);

            if (!rsp.IsSuccessStatusCode)
            {
                if (!string.IsNullOrEmpty(rsp.Error.Content))
                {
                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    var msg = rspContent?.Response;
                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        msg = rspContent.ValidationErrors[0].Error;
                    }

                    if (msg == null && rspContent?.Message != null)
                        msg = rspContent.Message;
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = msg,
                        NotificationType = NotificationType.Error
                    });
                }
                else
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = "Oops! Something Went Wrong. Please try again later. Thanks",
                        NotificationType = NotificationType.Error
                    });
                }
            }
            else
            {
                List<CustomerMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<CustomerMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().ApplicationUserId) &&
                    rspResponse.FirstOrDefault().Id == CustomerId)
                {
                    MemberProfile = new CustomerMasterView();
                    MemberProfile = Mapper.Map<CustomerMasterView>(rspResponse.FirstOrDefault());
                    ;

                    CustomerFullName = MemberProfile.FullName;
                    CustomerImage = MemberProfile.ProfileImageUrl;
                }
            }
        }
        public async Task GetAccountDetails()
        {
            var rsp = await DataService.GetRecord<CommandResult<GetLoanAccountViewModel>>(
                nameof(AP.ChevronCoop.Entities.Loans.LoanAccounts.LoanAccount),  Id);

            if (!rsp.IsSuccessStatusCode)
            {
                AccountsMasterView = new GetLoanAccountViewModel();
            }
            else
            {
                var rspResponse =
                    JsonSerializer.Deserialize<GetLoanAccountViewModel>(rsp.Content.Response.ToJson());
                if (!string.IsNullOrEmpty(rspResponse.Id) &&
                    rspResponse.Id == Id)
                {
                    AccountsMasterView = rspResponse;
                    var repLoanApplication = await DataService.GetValue<List<LoanApplicationMasterView>>(
                        nameof(LoanApplicationMasterView), "id", AccountsMasterView.LoanApplicationId);
                    if (!repLoanApplication.IsSuccessStatusCode)
                    {
                        LoanApplicationMasterView = new LoanApplicationMasterView();
                    }
                    else
                    {
                        List<LoanApplicationMasterView> rspResponseLoanApplication =
                            JsonSerializer.Deserialize<List<LoanApplicationMasterView>>(repLoanApplication.Content
                                .ToJson());
                        if (rspResponseLoanApplication != null && rspResponseLoanApplication.Count > 0 &&
                            !string.IsNullOrEmpty(rspResponseLoanApplication.FirstOrDefault().Id) &&
                            rspResponseLoanApplication.FirstOrDefault().Id == AccountsMasterView.LoanApplicationId)
                        {
                            LoanApplicationMasterView =
                                Mapper.Map<LoanApplicationMasterView>(rspResponseLoanApplication.FirstOrDefault());
                        }
                    }

                    CustomerAccountNumber = AccountsMasterView.AccountNo;
                    CustomerAccountType = LoanApplicationMasterView.LoanProductId_LoanProductType;

                    LoanBalance =
                        AccountsMasterView.PrincipalBalanceAccountId_LedgerBalance;

                    LoanAmount = AccountsMasterView.Principal;
                    TotalAmountRepaid = AccountsMasterView.AmountPaid;
                    NextRepaymentAmount = AccountsMasterView.PeriodPayment;
                    if (AccountsMasterView.IsClosed)
                    {
                        IsActiveLoanAccount = false;
                    }
                    else
                    {
                        IsActiveLoanAccount = true;
                    }

                    tempService.SetLoanAccountMasterViewTempObject(AccountsMasterView);
                }
            }
        }

        public async void ChangeMenu(string value)
        {
            MenuToShow = value;
        }
    }
}