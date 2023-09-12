using AntDesign;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data.DepositApplication;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.SharedComponents;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using System.Security.Claims;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.Drawals
{
    public partial class SpecialDepositAccount
    {
        private IJSRuntime jsRuntime;

        private FluentValidationValidator? _fluentValidationValidator;
        public CustomerMasterView Model { get; set; }

        public SpecialDepositAccountApplication SpecialDepositModel { get; set; }

        public CreateSpecialDepositAccountApplicationCommand Command { get; set; }


        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        public List<DepositProduct> DepositProductMasterViews = new List<DepositProduct>();

        public List<DepositProduct> DepositProductsList = new List<DepositProduct>();

        public List<ProductSetupGrid> ProductSetupGrids = new List<ProductSetupGrid>();

        string ApplicationUserId { get; set; }

        public CustomerMasterView MemberProfile;


        private string FundingSourceChecked = "None";

        public string ShowBank = "hidden";

        public bool drawalHidden = true;

        bool showDocumentError { get; set; } = false;
        bool showDocumentSuccess { get; set; } = false;

        bool showAlert { get; set; } = false;

        string ErrorMessage { get; set; } = string.Empty;


        [Inject]
        WebConfigHelper Config { get; set; }

        public string interestRate { get; set; }
        public List<InterestRange> InterestRange = new List<InterestRange>();

        private ElementReference closeButtonRef;

        List<DepositFundingSourceType> FundingSourceType { get; set; }


        string Uploaded_Document { get; set; } = string.Empty;

        [Parameter]
        public EventCallback<bool> OnUpdateSpecialDepositChanged { get; set; }

        [Parameter]
        public bool showAddDrawer { get; set; } = false;

        Drawer addDrawer;

        BrowserDimension BrowserDimension;

        public bool ShowAlertPage { get; set; } = false;
        private SuccessMessageModal successModal;

        public bool IsButtonDisabled { get; set; } = false;

        public string UploadedImage { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            //await base.OnInitializedAsync();
            Model = new CustomerMasterView();
            SpecialDepositModel = new SpecialDepositAccountApplication();
            SpecialDepositModel.PaymentDocument = new CustomerPaymentDocument();
            Command = new CreateSpecialDepositAccountApplicationCommand();

            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            if (!string.IsNullOrEmpty(Principal))
            {
                var user = authenticated.User;

                if (user.Identity.IsAuthenticated)
                {
                    ApplicationUserId = user.FindFirstValue(ClaimTypes.Sid);

                    await GetProfile();
                    await GetDepositProducts();
                }
            }
            else
            {
            }

            await MapToModel();
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

        public async Task GetDepositProducts()
        {
            var rsp = await DataService.GetValue<List<DepositProduct>>(
                nameof(DepositProduct), "status", "PUBLISHED", "productType", DepositProductType.SPECIAL_DEPOSIT.ToString());

            if (rsp.IsSuccessStatusCode)
            {
                DepositProductMasterViews = JsonSerializer.Deserialize<List<DepositProduct>>(rsp.Content.ToJson());
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

        public async Task GetInterestRate(ChangeEventArgs<string, DepositProduct> args)
        {
            try
            {
                SpecialDepositModel.DepositProductId = args.Value;

                SpecialDepositModel.InterestRate = DepositProductMasterViews.FirstOrDefault(ex => ex.Id == args.Value)
                    ?.InterestRanges.FirstOrDefault()?.InterestRate ?? 0;
            }
            catch (Exception exp)
            {
            }
        }

        private async Task MapToModel()
        {
            SpecialDepositModel.ApplicationNo = Model.ApplicationUserId;
            SpecialDepositModel.CustomerId = Model.CustomerNo;
            SpecialDepositModel.CreatedByUserId = Model.CreatedByUserId;
        }


        public async Task GetProfile()
        {
            var rsp = await DataService.GetValue<List<CustomerMasterView>>(
                nameof(Customer), ApplicationUserId);

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
                    rspResponse.FirstOrDefault().ApplicationUserId == ApplicationUserId)
                {
                    MemberProfile = new CustomerMasterView();
                    MemberProfile = rspResponse.FirstOrDefault();
                    Model = Mapper.Map<CustomerMasterView>(rspResponse.FirstOrDefault());
                    ;
                    SpecialDepositModel.CustomerId = Model.CustomerNo;
                }
            }
        }

        public void MapToCommand()
        {
            Command = new CreateSpecialDepositAccountApplicationCommand()
            {
                CustomerId = Model.Id,
                DepositProductId = SpecialDepositModel.DepositProductId,
                Amount = SpecialDepositModel.Amount,
                InterestRate = SpecialDepositModel.InterestRate,
                PaymentAccountNumber = SpecialDepositModel.PaymentAccountNumber,
                PaymentBankName = SpecialDepositModel.PaymentBankName,
                ModeOfPayment = SpecialDepositModel.ModeOfPayment,
                Document = SpecialDepositModel.PaymentDocument.Document,
                MimeType = SpecialDepositModel.PaymentDocument.MimeType,
                FileName = SpecialDepositModel.PaymentDocument.FileName,
                FileSize = SpecialDepositModel.PaymentDocument.FileSize,
                CreatedByUserId = Model.ApplicationUserId
            };
        }

        private async Task OnSave()
        {
            IsButtonDisabled = true;
            MapToCommand();
            if (await _fluentValidationValidator.ValidateAsync())
            {
                var rsp = await DataService
                    .Create<CreateSpecialDepositAccountApplicationCommand,
                        CommandResult<SpecialDepositAccountApplicationViewModel>>(
                        nameof(SpecialDepositAccountApplication), Command);

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
                    showAddDrawer = false;
                    ShowAlertPage = true;
                    await onAddDone();

                    MapToModel();
                }
            }

            IsButtonDisabled = false;
        }

        public async Task ShowBankMenu(string value, DepositFundingSourceType mode)
        {
            ShowBank = value;

            SpecialDepositModel.ModeOfPayment = mode;
        }

        public async Task OnChangeDocumentUpload(UploadChangeEventArgs args)
        {
            ErrorMessage = string.Empty;
            showDocumentError = false;
            await InvokeAsync(StateHasChanged);
            var file = args.Files[0];
            if (file != null)
            {
                var response = ImageConverterHelper.ValidateDocuments(file);
                if (string.IsNullOrEmpty(response))
                {
                    SpecialDepositModel.PaymentDocument.Document = ImageConverterHelper.ConvertFileToBase64(file);
                    SpecialDepositModel.PaymentDocument.MimeType = file.FileInfo.Type.ToLower();
                    SpecialDepositModel.PaymentDocument.FileSize = (int)file.FileInfo.Size;
                    SpecialDepositModel.PaymentDocument.FileName = file.FileInfo.Name;

                    UploadedImage = ImageConverterHelper.ConvertFileToBase64(file);

                    ErrorMessage = "Upload was successfull";
                    showDocumentError = false;
                    showDocumentSuccess = true;
                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    ErrorMessage = response;
                    showDocumentError = true;
                    showDocumentSuccess = false;
                    await InvokeAsync(StateHasChanged);
                }
            }
            else
            {
                ErrorMessage = "File not found";
                showDocumentError = true;
                showDocumentSuccess = false;
                await InvokeAsync(StateHasChanged);
            }
        }


        public async Task ShowSuccessMessage()
        {
            ShowAlertPage = true;
        }


        public async Task onAddDone()
        {
            showAddDrawer = false;
        }

        private async Task OnRemoveHandler(RemovingEventArgs args)
        {
            UploadedImage = Command.Document = "";
            await InvokeAsync(StateHasChanged);

        }
    }
}