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
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AutoMapper;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.Drawals
{
    public partial class SpecialDepositIncreaseDecreaseComp
    {
        private FluentValidationValidator? _fluentValidationValidator;

        [Parameter]
        public EventCallback<bool> OnUpdateChanged { get; set; }

        public CreateSpecialDepositIncreaseDecreaseCommand Command { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public bool IsButtonDisabled { get; set; } = false;

        [Parameter] public string CustomerID { get; set; }

        [Parameter] public decimal AccountBalance { get; set; }

        [Parameter] public string SpecialDepositAccountID { get; set; }

        [Parameter] public string SpecialDepositAccountNumber { get; set; }

        [Parameter] public decimal PayrollAmount { get; set; }

        [Parameter] public string MembersName { get; set; }

        [Parameter] public string MembersNumber { get; set; }

        [Parameter]
        public bool showAddDrawer { get; set; } = false;

        Drawer addDrawer;

        BrowserDimension BrowserDimension;

        public bool ShowAlertPage { get; set; } = false;
        private SuccessMessageModal successModal;

        public SpecialDepositAccountMasterView _SpecialDepositAccountMasterView = new SpecialDepositAccountMasterView();

        protected override async Task OnInitializedAsync()
        {

            Command = new CreateSpecialDepositIncreaseDecreaseCommand();
            await GetAccountDetails();
        }

        public async Task GetAccountDetails()
        {
            var rsp = await DataService.GetValue<List<SpecialDepositAccountMasterView>>(
                nameof(SpecialDepositAccountMasterView), "id", SpecialDepositAccountID);

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
                List<SpecialDepositAccountMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<SpecialDepositAccountMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0)
                {
                    PayrollAmount = rspResponse.FirstOrDefault().FundingAmount;
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
            Command.SpecialDepositAccountId = SpecialDepositAccountID;
            Command.DateCreated = DateTime.UtcNow.AddHours(1);
            Command.CreatedByUserId = CustomerID;
            if (Command.ContributionChangeRequest == ContributionChangeRequest.INCREASE)
            {
                Command.Description =
                    $"Request for increase in monthly contribution for account : {SpecialDepositAccountNumber}";
            }
            else
            {
                Command.Description =
                    $"Request for decrease in monthly contribution for account : {SpecialDepositAccountNumber}";
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
            if  (Command.Amount == PayrollAmount)
            {
                IsButtonDisabled = false;
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = $"New Amount and Payroll Amount should not have the same value",
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
                    Description = $"New amount cannot be less than current amount {PayrollAmount.ToString("N0")}",
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

            //  if (await _fluentValidationValidator.ValidateAsync())
            {
                var rsp = await DataService
                    .Create<CreateSpecialDepositIncreaseDecreaseCommand, CommandResult<SpecialDepositIncreaseDecreaseViewModel>>(
                        nameof(SpecialDepositIncreaseDecrease), Command);

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

                    Command = new CreateSpecialDepositIncreaseDecreaseCommand();
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