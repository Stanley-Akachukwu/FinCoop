using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities;
using ChevronCoop.Web.AppUI.BlazorServer.Data.DepositApplication;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.SharedComponents;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using System.Net.Http.Headers;
using System.Security.Claims;
using JsonSerializer = System.Text.Json.JsonSerializer;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Blazored.FluentValidation;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.AppDomain.Customers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.Drawals
{
    public partial class SpecialDepositWithdrawalComp
    {
        private FluentValidationValidator? _fluentValidationValidator;

        [Parameter]
        public EventCallback<bool> OnUpdateChanged { get; set; }


        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public bool IsButtonDisabled { get; set; } = false;
        private string FundingSourceChecked = "None";
        [Parameter] public string CustomerID { get; set; }

        [Parameter] public string SavingsAccountID { get; set; }

        [Parameter] public string SavingsAccountNumber { get; set; }

        [Parameter] public string MembersName { get; set; }

        [Parameter] public string MembersNumber { get; set; }

        [Parameter]
        public bool showAddDrawer { get; set; } = false;

        Drawer addDrawer;

        BrowserDimension BrowserDimension;
        public string LiquidationHeader = "";
        public string accountType = "";
        public bool ShowAlertPage { get; set; } = false;

        [Parameter] public decimal AvailableBalance { get; set; } = 0;

        private SuccessMessageModal successModal;
        public List<DepositAccountsMasterView> GetDepositAccountsMasterView { get; set; }

        public List<CustomerBankAccountMasterView> GetCustomerBankAccounts { get; set; }
        public CreateSpecialDepositWithdrawalCommand Command { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Command = new CreateSpecialDepositWithdrawalCommand();
            await GetCustomerBankAccount();
        }

        public async Task GetCoopAccount(ChangeEventArgs<string, CustomerBankAccountMasterView> args)
        {
            Command.CustomerDestinationBankAccountId = args.Value;
        }

        public async Task GetProjectAccount(ChangeEventArgs<string, DepositAccountsMasterView> args)
        {
            Command.CustomerDestinationBankAccountId = args.Value;
        }

        public async Task ShowLiquidation(string param)
        {
            accountType = param;
        }

        public async Task GetCustomerBankAccount()
        {
            var rsp = await DataService.GetValue<List<CustomerBankAccountMasterView>>(
                nameof(CustomerBankAccount), "customerId", CustomerID);

            if (rsp.IsSuccessStatusCode)
            {
                GetCustomerBankAccounts =
                    JsonSerializer.Deserialize<List<CustomerBankAccountMasterView>>(rsp.Content.ToJson());
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

        public async Task GetProjectBankAccount()
        {
            var rsp = await DataService.GetValue<List<DepositAccountsMasterView>>(
                nameof(DepositAccountsMasterView), "customerId", CustomerID);

            if (rsp.IsSuccessStatusCode)
            {
                GetDepositAccountsMasterView =
                    JsonSerializer.Deserialize<List<DepositAccountsMasterView>>(rsp.Content.ToJson());
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                addDrawer.Width = (int)(BrowserDimension.Width * 0.50);
            }
        }


        public void MapToCommand()
        {
            Command.SpecialDepositSourceAccountId = SavingsAccountID;
            Command.WithdrawalDestinationType = WithdrawalAccountType.EXISTING_BANK_ACCOUNT;
            Command.CreatedByUserId = CustomerID;
        }

        private async Task OnSave()
        {
            IsButtonDisabled = true;

            if(AvailableBalance < Command.Amount )
            {
                IsButtonDisabled = false;
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = $"Your available balance is {AvailableBalance.ToString("N0")} please fund your account",
                    NotificationType = NotificationType.Error
                });
                return;
            }

            MapToCommand();
            //  if (await _fluentValidationValidator.ValidateAsync())
          //  {
                var rsp = await DataService
                    .Create<CreateSpecialDepositWithdrawalCommand, CommandResult<SpecialDepositWithdrawalViewModel>>(
                        nameof(SpecialDepositWithdrawal), Command);

                if (!rsp.IsSuccessStatusCode)
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
                await OnUpdateChanged.InvokeAsync(true);
                showAddDrawer = false;
                    ShowAlertPage = true;

                    Command = new CreateSpecialDepositWithdrawalCommand();
                }
           // }
            IsButtonDisabled = false;
        }

        public async Task ShowSuccessMessage()
        {
            ShowAlertPage = true;
        }

        async Task onAddDone()
        {
            showAddDrawer = false;
        }
    }
}