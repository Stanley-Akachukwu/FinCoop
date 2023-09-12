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
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Users;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.Drawals
{
    public partial class SavingsAccountRegistration
    {
        private IJSRuntime jsRuntime;

        private FluentValidationValidator? _fluentValidationValidator;
        public CustomerMasterView Model { get; set; }

        public SavingsAccountApplicationViewModel SavingsDepositViewModel { get; set; }


        public CreateSavingsAccountApplicationCommand Command { get; set; }

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

        public List<DepositProductMasterView> DepositProductMasterViews = new List<DepositProductMasterView>();

        public List<DepositProductViewModel> DepositProductViewModel = new List<DepositProductViewModel>();

        string ApplicationUserId { get; set; }

        public CustomerMasterView MemberProfile;


        private string FundingSourceChecked = "None";

        public bool drawalHidden = true;

        bool showDocumentError { get; set; } = false;
        bool showDocumentSuccess { get; set; } = false;

        bool showAlert { get; set; } = false;

        string ErrorMessage { get; set; } = string.Empty;

        [Inject]
        WebConfigHelper Config { get; set; }

        public string interestRate { get; set; }
        public List<InterestRange> InterestRange { get; set; }

        private ElementReference closeButtonRef;

        List<DepositFundingSourceType> FundingSourceType { get; set; }


        [Parameter]
        public bool showAddDrawer { get; set; } = false;

        Drawer addDrawer;

        BrowserDimension BrowserDimension;

        public bool ShowAlertPage { get; set; } = false;
        private SuccessMessageModal successModal;

        public bool IsButtonDisabled { get; set; } = false;

        public string FullName { get; set; }

        [Parameter]
        public EventCallback<bool> OnUpdateSavingsDepositChanged { get; set; }

        [Inject]
        private UserService _UserService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //await base.OnInitializedAsync();
            Model = new CustomerMasterView();
            SavingsDepositViewModel = new SavingsAccountApplicationViewModel();
            Command = new CreateSavingsAccountApplicationCommand();

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

                await Task.Delay(1000);
                FullName = _UserService.Fullname;
            }
        }

        public async Task GetDepositProducts()
        {
            var rsp = await DataService.GetValue<List<DepositProductMasterView>>(
                nameof(DepositProductMasterView), "status", "PUBLISHED", "productType", DepositProductType.SAVINGS.ToString());

            if (rsp.IsSuccessStatusCode)
            {
                DepositProductMasterViews =
                    JsonSerializer.Deserialize<List<DepositProductMasterView>>(rsp.Content.ToJson());
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

        private async Task MapToModel()
        {
            SavingsDepositViewModel.ApplicationNo = Model.ApplicationUserId;
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
                    rspResponse.FirstOrDefault().ApplicationUserId == ApplicationUserId)
                {
                    MemberProfile = new CustomerMasterView();
                    MemberProfile = rspResponse.FirstOrDefault();
                    Model = Mapper.Map<CustomerMasterView>(rspResponse.FirstOrDefault());
                    ;
                }
            }
        }

        public void MapToCommand()
        {
            Command = new CreateSavingsAccountApplicationCommand()
            {
                Description = $"Savings Application by {FullName}",
                Comments = $"Savings Application by {FullName}",
                FullText = $"Savings Application by {FullName}",
                Tags = "Saving Account",
                Caption = Model.Caption,
                DateCreated = DateTime.UtcNow,
                CustomerId = Model.Id,
                DepositProductId = SavingsDepositViewModel.DepositProductId,
                Amount = SavingsDepositViewModel.Amount,
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
                    .Create<CreateSavingsAccountApplicationCommand, CommandResult<SavingsAccountApplicationViewModel>>(
                        nameof(SavingsAccountApplication), Command);

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

                    SavingsDepositViewModel = new SavingsAccountApplicationViewModel();
                    Command = new CreateSavingsAccountApplicationCommand();
                    MapToModel();
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