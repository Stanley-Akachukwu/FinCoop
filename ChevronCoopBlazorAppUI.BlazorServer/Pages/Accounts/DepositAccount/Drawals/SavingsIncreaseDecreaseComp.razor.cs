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
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AutoMapper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.Drawals
{
    public partial class SavingsIncreaseDecreaseComp
    {
        private FluentValidationValidator? _fluentValidationValidator;

        [Parameter]
        public EventCallback<bool> OnUpdateChanged { get; set; }

        public CreateSavingsIncreaseDecreaseCommand Command { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public bool IsButtonDisabled { get; set; } = false;

        [Parameter] public string CustomerID { get; set; }

        [Parameter] public decimal AccountBalance { get; set; }

        [Parameter] public string SavingsAccountID { get; set; }

        [Parameter] public string SavingsAccountNumber { get; set; }

        [Parameter] public decimal PayrollAmount { get; set; }

        [Parameter] public string MembersName { get; set; }

        [Parameter] public string MembersNumber { get; set; }

        [Parameter]
        public bool showAddDrawer { get; set; } = false;

        Drawer addDrawer;

        BrowserDimension BrowserDimension;

        public bool ShowAlertPage { get; set; } = false;
        private SuccessMessageModal successModal;

        public SavingsAccountMasterView _SavingsAccountMasterView = new SavingsAccountMasterView();

        protected override async Task OnInitializedAsync()
        {

            Command = new CreateSavingsIncreaseDecreaseCommand();
            await GetAccountDetails();
        }

        public async Task GetAccountDetails()
        {
            var rsp = await DataService.GetValue<List<SavingsAccountMasterView>>(
                nameof(SavingsAccountMasterView), "id", SavingsAccountID);

            if (!rsp.IsSuccessStatusCode)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = MessageBox.ServerErrorHeader,
                    Description = MessageBox.ServerErrorDescription,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                List<SavingsAccountMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<SavingsAccountMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0)
                {
                    PayrollAmount = rspResponse.FirstOrDefault().PayrollAmount;
                }

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
            Command.CustomerId = CustomerID;
            Command.SavingsAccountId = SavingsAccountID;
            Command.DateCreated = DateTime.UtcNow.AddHours(1);
            Command.CreatedByUserId = CustomerID;
            if (Command.ContributionChangeRequest == ContributionChangeRequest.INCREASE)
            {
                Command.Description =
                    $"Request for increase in monthly contribution for account : {SavingsAccountNumber}";
            }
            else
            {
                Command.Description =
                    $"Request for decrease in monthly contribution for account : {SavingsAccountNumber}";
            }
        }

        private async Task OnSave()
        {
            IsButtonDisabled = true;

            MapToCommand();

            if (String.IsNullOrEmpty(Command.ContributionChangeRequest.ToString()))
            {
                IsButtonDisabled = false;
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = $"Select request type",
                    NotificationType = NotificationType.Error
                });
                return;
            }

            if (Command.ContributionChangeRequest.ToString() == ContributionChangeRequest.INCREASE.ToString() && Command.Amount < PayrollAmount)
            {
                IsButtonDisabled = false;
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = $"New amount cannot be less than current amount of {PayrollAmount.ToString("N0")}",
                    NotificationType = NotificationType.Error
                });
                return;
            }
            if (Command.ContributionChangeRequest.ToString() == ContributionChangeRequest.DECREASE.ToString() && Command.Amount > PayrollAmount)
            {
                IsButtonDisabled = false;
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = $"New amount cannot be greater than current amount of {PayrollAmount.ToString("N0")}",
                    NotificationType = NotificationType.Error
                });
                return;
            }

            if (Command.Amount == PayrollAmount)
            {
                IsButtonDisabled = false;
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = $"Amount must be greater or less than {PayrollAmount.ToString("N0")}",
                    NotificationType = NotificationType.Error
                });
                return;
            }

            //  if (await _fluentValidationValidator.ValidateAsync())
            {
                var rsp = await DataService
                    .Create<CreateSavingsIncreaseDecreaseCommand, CommandResult<SavingsIncreaseDecreaseViewModel>>(
                        nameof(SavingsIncreaseDecrease), Command);

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

                    Command = new CreateSavingsIncreaseDecreaseCommand();
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