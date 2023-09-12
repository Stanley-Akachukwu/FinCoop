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
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositFundTransfers;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.SpecialDeposit;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositFundTransfer;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.Drawals
{
    public partial class SpecialDepositFundTransferComp
    {
        private FluentValidationValidator? _fluentValidationValidator;

        [Parameter]
        public EventCallback<bool> OnUpdateChanged { get; set; }

        public CreateSpecialDepositFundTransferCommand Command { get; set; }
        public List<DepositAccountsMasterView> GetCustomerChevronAccounts = new List<DepositAccountsMasterView>();

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public bool IsButtonDisabled { get; set; } = false;

        [Parameter] public string CustomerID { get; set; }

        [Parameter] public string SpecialDepositAccountID { get; set; }

        [Parameter] public string SpecialDepositAccountNumber { get; set; }

        [Parameter] public string MembersName { get; set; }

        [Parameter] public string MembersNumber { get; set; }

        [Parameter] public decimal AvailableBalance { get; set; } = 0;

        [Parameter]
        public bool showAddDrawer { get; set; } = false;

        Drawer addDrawer;

        BrowserDimension BrowserDimension;

        public bool ShowAlertPage { get; set; } = false;
        private SuccessMessageModal successModal;

        protected override async Task OnInitializedAsync()
        {
            Command = new CreateSpecialDepositFundTransferCommand();
            await GetCustomerBankAccount();
        }

        public async Task GetCustomerBankAccount()
        {
            var rsp = await DataService.GetValue<List<DepositAccountsMasterView>>(
                nameof(DepositAccountsMasterView), "customerId", CustomerID);

            if (rsp.IsSuccessStatusCode)
            {
                GetCustomerChevronAccounts =
                    JsonSerializer.Deserialize<List<DepositAccountsMasterView>>(rsp.Content.ToJson());

                GetCustomerChevronAccounts = GetCustomerChevronAccounts.Where(x => x.Id != SpecialDepositAccountID && x.AccountType != DepositProductType.SPECIAL_DEPOSIT.ToString()).ToList();
                foreach(var account in GetCustomerChevronAccounts)
                {
                    account.Tags = $"{account.AccountType} - Acc no - {account.AccountNo} (\u20A6{account.LedgerBalance.ToString("N0")})";
                }
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

        public async Task GetCoopAccount(ChangeEventArgs<string, DepositAccountsMasterView> args)
        {
            
            string accountType = GetCustomerChevronAccounts.Where(x => x.Id == args.Value).FirstOrDefault().AccountType;

            if (accountType == DepositProductType.SAVINGS.ToString())
            {
                Command.SavingAccountDestinationId = args.Value;
                Command.DestinationAccountType = DestinationAccountType.SAVINGS_ACCOUNT;
            }
            else
            {
                Command.FixedDepositDestinationAccountId = args.Value;
                Command.DestinationAccountType = DestinationAccountType.FIXED_DEPOSIT_ACCOUNT;
            }

          
        }

        public void MapToCommand()
        {
            Command.SpecialDepositAccountId = SpecialDepositAccountID;
            Command.CreatedByUserId = CustomerID;
        }

        private async Task OnSave()
        {
            IsButtonDisabled = true;

            if(AvailableBalance < Command.Amount)
            {
                IsButtonDisabled = false;
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = $"You cannot transfer more than {AvailableBalance.ToString("N0")}",
                    NotificationType = NotificationType.Error
                });
                return;
            }

            MapToCommand();
            //  if (await _fluentValidationValidator.ValidateAsync())
            {
                var payload = JsonSerializer.Serialize(Command);

                var rsp = await DataService
                    .Create<CreateSpecialDepositFundTransferCommand, CommandResult<SpecialDepositFundTransfer>>(
                        nameof(SpecialDepositFundTransfer), Command);

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

                    Command = new CreateSpecialDepositFundTransferCommand();
                }
            }
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