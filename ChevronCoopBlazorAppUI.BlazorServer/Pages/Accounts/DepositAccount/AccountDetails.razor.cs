using AntDesign;
using AntDesign.Core.Extensions;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;
using AutoMapper;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Services.Customers;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Claims;
using System.Text.Json;


namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount
{
    public partial class AccountDetails
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter] public bool IsAdmin { get; set; } = false;

        [Parameter]
        public string PageType { get; set; }

        public string PageTitle { get; set; }
        public CompanyBankAccountMasterView companyBankAccountMasterView { get; set; }
        public string MenuToShow { get; set; } = "Transaction";

        public bool showSavings { get; set; } = false;

        public bool showSpecialDeposit { get; set; } = false;

        public bool showFixedDeposit { get; set; } = false;

        [Parameter]
        public string CustomerId { get; set; }

        public CustomerMasterView MemberProfile;

        

        public DepositAccountsMasterView AccountsMasterView = new DepositAccountsMasterView();

        public DepositProductMasterView _DepositProductMasterView = new DepositProductMasterView();

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

        [Parameter] public string CustomerFullName { get; set; }
        [Parameter] public string CustomerImage { get; set; }
        [Parameter] public string CustomerAccountNumber { get; set; }
        [Parameter] public string CustomerAccountType { get; set; }

        [Parameter] public string MembershipNumber { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        public decimal AvailableBalance { get; set; } = 0;
        decimal LedgerBalance { get; set; } = 0;

		[Inject]
		IMasterViews _MasterViews { get; set; }

		[Parameter] public decimal MonthlyContribution { get; set; } = 0;

        decimal SpecialDepositLedgerBalance { get; set; } = 0;
        decimal SpecialDepositAvailableBalance { get; set; } = 0;

        decimal FicedDepositLedgerBalance { get; set; } = 0;
        decimal FixedDepositAvailableBalance { get; set; } = 0;

        public List<DepositAccountsMasterView> _DepositAccountsMasterView = new List<DepositAccountsMasterView>();


        public string SelectedMenu =
            "items-start bg-blue-200 hover:bg-blue-200 cursor-pointer justify-center p-4 flex flex-col xl:block 2xl:flex sm:space-x-4 xl:space-x-0 2xl:space-x-4";
        public string DefaultSelection = "items-start hover:bg-blue-200 cursor-pointer justify-center p-4 flex flex-col xl:block 2xl:flex sm:space-x-4 xl:space-x-0 2xl:space-x-4";

        public string DefaultMenu1 { get; set; }
        public string DefaultMenu2 { get; set; }
        public string DefaultMenu3 { get; set; }

        public string DefaultMenu4 { get; set; }
        public string DefaultMenu5 { get; set; }
        public string DefaultMenu6 { get; set; }
        public string DefaultMenu7 { get; set; }
        public string DefaultMenu8 { get; set; }
        public string DefaultMenu9 { get; set; }
        public string DefaultMenu10 { get; set; }
        public string DefaultMenu11 { get; set; }
        public string DefaultMenu12 { get; set; }
        public string DefaultMenu13 { get; set; }
        public string DefaultMenu14 { get; set; }
		public string DefaultMenu15 { get; set; }
		public string DefaultMenu16 { get; set; }
		public string DefaultMenu17 { get; set; }
		public string DefaultMenu30 { get; set; }
		

		public string ContributionType { get; set; } = "Monthly Contribution";

        protected override async Task OnInitializedAsync()
        {
            MemberProfile = new CustomerMasterView();

            var authenticated = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            await GetCurrentUser();

            await GetProfile();

            await GetAccountDetails();

            await ResetSideButton();

          //  await GetProductDetails();


			if (PageType == DepositProductType.SAVINGS.ToString().ToLowerInvariant())
            {
                showSavings = true;
                PageTitle = "My Savings Account Dashboard";
                MenuToShow = "savings_dashboard";
                DefaultMenu1 = SelectedMenu;
            }
            else if (PageType == DepositProductType.SPECIAL_DEPOSIT.ToString().ToLowerInvariant())
            {
                showSpecialDeposit = true;
                PageTitle = "My Special Deposit Account Dashboard";
                MenuToShow = "special_dashboard";
                DefaultMenu5 = SelectedMenu;
            }
            else if (PageType == DepositProductType.FIXED_DEPOSIT.ToString().ToLowerInvariant())
            {
                showFixedDeposit = true;
                PageTitle = "My Fixed Deposit Account Dashboard";
                MenuToShow = "fixed_dashboard";
                DefaultMenu10 = SelectedMenu;
                ContributionType = "Interest Rate";
                
            }

        }
        private async Task GoBack()
        {
            await jsRuntime.InvokeVoidAsync("history.back");
        }
        public async Task ResetSideButton()
        {
            DefaultMenu1 = DefaultMenu2 =
                DefaultMenu3 = DefaultMenu4 = DefaultMenu5 = DefaultMenu6 =
                    DefaultMenu7 = DefaultMenu8 = DefaultMenu9 =
                        DefaultMenu10 = DefaultMenu11 =
                            DefaultMenu12 = DefaultMenu13 = DefaultMenu14 = DefaultMenu15= DefaultMenu16= DefaultMenu17= DefaultMenu30 =

								"items-start hover:bg-blue-200 cursor-pointer justify-center p-4 flex flex-col xl:block 2xl:flex sm:space-x-4 xl:space-x-0 2xl:space-x-4";
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
            if (CurrentUser != null)
            {
               
                var userType = CurrentUser.Claims.Where(f => f.Type == "IsAdmin").FirstOrDefault().Value;
                if(userType.ToLower() == "true") IsAdmin = true;
              
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
                        Message = MessageBox.ServerErrorHeader,
                        Description = MessageBox.ServerErrorDescription,
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


                    CustomerFullName = MemberProfile.FullName;
                    CustomerImage = MemberProfile.ProfileImageUrl;

                    MembershipNumber = MemberProfile.MemberId;
                }
            }
        }


		//public async Task GetProductDetails()
		//{
		//	_DepositProductMasterView = new DepositProductMasterView();
		//	var rsp = await DataService.GetValue<List<DepositProductMasterView>>(
		//	   nameof(DepositProductMasterView), "id", AccountsMasterView.DepositProductId);
		//	if (rsp.IsSuccessStatusCode)
		//	{
		//		_DepositProductMasterView = JsonSerializer.Deserialize<List<DepositProductMasterView>>(rsp.Content.ToJson()).FirstOrDefault();
		//	}

		//}

		public async Task GetAccountDetails()
        {
            var rsp = await DataService.GetValue<List<DepositAccountsMasterView>>(
                nameof(DepositAccountsMasterView), "id", Id);

            if (!rsp.IsSuccessStatusCode)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = MessageBox.ServerErrorHeader,
                    Description = MessageBox.ServerErrorDescription,
                    NotificationType = NotificationType.Error
                });
                AccountsMasterView = new DepositAccountsMasterView();
            }
            else
            {
                List<DepositAccountsMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<DepositAccountsMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().CustomerId) &&
                    rspResponse.FirstOrDefault().CustomerId == CustomerId)
                {
                    AccountsMasterView = Mapper.Map<DepositAccountsMasterView>(rspResponse.FirstOrDefault());

                    CustomerAccountNumber = AccountsMasterView.AccountNo;
                    CustomerAccountType = AccountsMasterView.AccountType;

                    AvailableBalance = AccountsMasterView.AvailableBalance;

                    LedgerBalance = AccountsMasterView.LedgerBalance;
                    if(PageType == DepositProductType.FIXED_DEPOSIT.ToString().ToLowerInvariant())
                    {
                        await GetFixedDepositDetailMasterView();
                    }
                    else
                    {
                        MonthlyContribution = AccountsMasterView.MonthlyContributionAmount ?? 0;
                    }

                    //change this to an API
                    var depositProductID = AccountsMasterView.DepositProductId;

                    var depositRsp = await _MasterViews.GetCustomMasterView<DepositProductMasterView>(nameof(DepositProductMasterView), "id", depositProductID, "BankDepositAccountId");
					var bankDepositAccountId = depositRsp.FirstOrDefault().BankDepositAccountId;

					var bankrsp = await _MasterViews.GetCustomMasterView<CompanyBankAccountMasterView>(nameof(CompanyBankAccountMasterView), "id", bankDepositAccountId, "AccountNumber,AccountName,BankId_Name");
					companyBankAccountMasterView = bankrsp.FirstOrDefault();

				 }

            }
        }

		//public async Task GetIncreaseDecreaseMasterView()
		//{
		//	_SavingsIncreaseDecreaseMasterViewSrc1 = new List<SavingsIncreaseDecreaseMasterView>();

		//	_SavingsIncreaseDecreaseMasterViewSrc1 = await _MasterViews.Get2FiltersCustomMasterView<SavingsIncreaseDecreaseMasterView>(nameof(SavingsIncreaseDecreaseMasterView), "savingsAccountId", SavingsAccountID, "savingsAccountId_CustomerId", CustomerID, "DateCreated,Description,Amount,ApprovalId_Status");

		//	_SavingsIncreaseDecreaseMasterViewSrc = _SavingsIncreaseDecreaseMasterViewSrc1
		//		   .OrderByDescending(c => c.DateCreated).ToList();


		//}


		public async Task GetFixedDepositDetailMasterView()
        {
            var rsp = await DataService.GetValue<List<FixedDepositAccountMasterView>>(
                nameof(FixedDepositAccountMasterView), "id", Id);
            if (rsp.IsSuccessStatusCode)
            {

                List<FixedDepositAccountMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<FixedDepositAccountMasterView>>(rsp.Content.ToJson());
                MonthlyContribution =  (rspResponse.Count > 0) ?  rspResponse.FirstOrDefault().InterestRate : 0;
                
            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = MessageBox.ServerErrorHeader,
                    Description = MessageBox.ServerErrorDescription,
                    NotificationType = NotificationType.Error
                });
            }
        }

        public async void ChangeMenu(string value, string buttonName)
        {
            ResetSideButton();
            MenuToShow = value;

            switch (buttonName)
            {
                case "DefaultMenu1":
                    DefaultMenu1 = SelectedMenu;
                    break;
                case "DefaultMenu2":
                    DefaultMenu2 = SelectedMenu;
                    break;
                case "DefaultMenu3":
                    DefaultMenu3 = SelectedMenu;
                    break;
                case "DefaultMenu4":
                    DefaultMenu4 = SelectedMenu;
                    break;
                case "DefaultMenu5":
                    DefaultMenu5 = SelectedMenu;
                    break;
                case "DefaultMenu6":
                    DefaultMenu6 = SelectedMenu;
                    break;
                case "DefaultMenu7":
                    DefaultMenu7 = SelectedMenu;
                    break;
                case "DefaultMenu8":
                    DefaultMenu8 = SelectedMenu;
                    break;
                case "DefaultMenu9":
                    DefaultMenu9 = SelectedMenu;
                    break;
                case "DefaultMenu10":
                    DefaultMenu10 = SelectedMenu;
                    break;
                case "DefaultMenu11":
                    DefaultMenu11 = SelectedMenu;
                    break;
                case "DefaultMenu12":
                    DefaultMenu12 = SelectedMenu;
                    break;
                case "DefaultMenu13":
                    DefaultMenu13 = SelectedMenu;
                    break;
                case "DefaultMenu14":
                    DefaultMenu14 = SelectedMenu;
                    break;
				case "DefaultMenu15":
					DefaultMenu15 = SelectedMenu;
					break;
				case "DefaultMenu16":
					DefaultMenu16 = SelectedMenu;
					break;
				case "DefaultMenu17":
					DefaultMenu17 = SelectedMenu;
					break;
				case "DefaultMenu30":
					DefaultMenu30 = SelectedMenu;//for ledger
					break;
			}
            await InvokeAsync(StateHasChanged);

        }
    }
}