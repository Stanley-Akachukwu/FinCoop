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
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductInterestRanges;
using ChevronCoop.Web.AppUI.BlazorServer.Services.Customers.Interface;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.Drawals
{
    public partial class SavingsCashAdditionComp : ComponentBase
    {
        private FluentValidationValidator? _fluentValidationValidator;

        [Parameter]
        public EventCallback<bool> OnUpdateChanged { get; set; }

        public CreateSavingsCashAdditionCommand Command { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public bool IsButtonDisabled { get; set; } = false;

        [Parameter] public string CustomerID { get; set; }

        [Parameter] public string SavingsAccountID { get; set; }

        [Parameter] public string SavingsAccountNumber { get; set; }

        [Parameter]
        public string BankName { get; set; }

        [Parameter]
        public string AccountNumber { get; set; }
        [Parameter]
        public string AccountName { get; set; }
        [Parameter] public string MembersName { get; set; }

        [Parameter] public string MembersNumber { get; set; }

        [Parameter]
        public bool showAddDrawer { get; set; } = false;

        Drawer addDrawer;

        BrowserDimension BrowserDimension;

        public bool ShowAlertPage { get; set; } = false;
        private SuccessMessageModal successModal;

        public string UploadedImage { get; set; } = "";

        [Parameter]
        public bool showDepositAccountMasterView { get; set; } = false;

        public List<DepositAccountsMasterView> DepositAccountsMasterViewSrc = new List<DepositAccountsMasterView>();

        [Inject]
        private ICustomersMasterViews CustomersMasterView { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Command = new CreateSavingsCashAdditionCommand();

            DepositAccountsMasterViewSrc = await CustomersMasterView.GetDepositAccountMasterView(CustomerID);
            DepositAccountsMasterViewSrc = DepositAccountsMasterViewSrc.Where(x => x.AccountType == DepositProductType.SAVINGS.ToString()).ToList();
        }

        public async Task GetDepositAccountMasterID(ChangeEventArgs<string, DepositAccountsMasterView> args)
        {
            try
            {
                Command.SavingsAccountId = args.Value;
            }
            catch (Exception exp)
            {
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
            Command.ModeOfPayment = DepositFundingSourceType.BANK_TRANSFER;
            Command.ModeOfPaymentAccountId = "";
            Command.CreatedByUserId = CustomerID;
            Command.Description = $"Cash Addition was added to account no: {SavingsAccountNumber}";
        }

        private async Task OnSave()
        {
            IsButtonDisabled = true;

            MapToCommand();

            if (Command.Amount < 1)
            {
                IsButtonDisabled = false;
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = $"Amount to add must be greater than 0",
                    NotificationType = NotificationType.Error
                });
                return;
            }

            //if (await _fluentValidationValidator.ValidateAsync())
            {
                var rsp = await DataService
                    .Create<CreateSavingsCashAdditionCommand, CommandResult<SavingsCashAdditionViewModel>>(
                        nameof(SavingsCashAddition), Command);

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



                    Command = new CreateSavingsCashAdditionCommand();
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

                    UploadedImage = Command.Document;

                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    await InvokeAsync(StateHasChanged);
                }
            }
            else
            {
                //ErrorMessage = "File not found";
                //showDocumentError = true;
                //showDocumentSuccess = false;
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task OnRemoveHandler(RemovingEventArgs args)
        {
            UploadedImage = Command.Document = "";
            await InvokeAsync(StateHasChanged);

        }
    }
}