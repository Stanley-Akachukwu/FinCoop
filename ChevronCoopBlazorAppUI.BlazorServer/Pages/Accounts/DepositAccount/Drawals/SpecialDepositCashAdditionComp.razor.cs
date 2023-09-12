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
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositCashAdditions;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.Drawals
{
    public partial class SpecialDepositCashAdditionComp
    {
        private FluentValidationValidator? _fluentValidationValidator;

        [Parameter]
        public EventCallback<bool> OnUpdateChanged { get; set; }

        public CreateSpecialDepositCashAdditionCommand Command { get; set; }

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
        [Parameter]
        public string BankName { get; set; }

        [Parameter]
        public string AccountNumber { get; set; }
        [Parameter]
        public string AccountName { get; set; }

        [Parameter]
        public bool showAddDrawer { get; set; } = false;

        [Parameter] public decimal AvailableBalance { get; set; } = 0;

        Drawer addDrawer;

        BrowserDimension BrowserDimension;

        public bool ShowAlertPage { get; set; } = false;
        private SuccessMessageModal successModal;

        public string UploadedImage { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            Command = new CreateSpecialDepositCashAdditionCommand();
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
            //Command.Description = $"Cash Addition was added to account no: {SavingsAccountNumber}";

            //Command.CustomerId = CustomerID;
            Command.SpecialDepositAccountId = SpecialDepositAccountID;
            //Command.DateCreated = DateTime.UtcNow.AddHours(1);
            Command.ModeOfPayment = DepositFundingSourceType.BANK_TRANSFER;
            //  Command.ModeOfPaymentAccountId = "";
            Command.CreatedByUserId = CustomerID;
        }

        private async Task OnSave()
        {
            IsButtonDisabled = true;

            MapToCommand();
            //  if (await _fluentValidationValidator.ValidateAsync())
            {
                var rsp = await DataService
                    .Create<CreateSpecialDepositCashAdditionCommand,
                        CommandResult<SpecialDepositCashAdditionViewModel>>(nameof(SpecialDepositCashAddition),
                        Command);

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

                    Command = new CreateSpecialDepositCashAdditionCommand();
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

        public async Task OnChangeDocumentUpload(UploadChangeEventArgs args)
        {
            await InvokeAsync(StateHasChanged);
            var file = args.Files[0];
            if (file != null)
            {
                var response = ImageConverterHelper.ValidateDocuments(file);
                if (string.IsNullOrEmpty(response))
                {
                    Command.Document = ImageConverterHelper.ConvertFileToBase64(file);
                    Command.MimeType = file.FileInfo.Type.ToLower();
                    Command.FileSize = (int)file.FileInfo.Size;
                    Command.FileName = file.FileInfo.Name;

                    UploadedImage = ImageConverterHelper.ConvertFileToBase64(file);
                }
            }
			await InvokeAsync(StateHasChanged);
		}

        private async Task OnRemoveHandler(RemovingEventArgs args)
        {
            UploadedImage = Command.Document = "";
            await InvokeAsync(StateHasChanged);

        }

    }
}