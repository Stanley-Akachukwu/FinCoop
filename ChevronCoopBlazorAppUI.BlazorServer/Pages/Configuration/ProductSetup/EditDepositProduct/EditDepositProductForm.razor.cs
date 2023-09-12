using AntDesign;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup.EditDepositProduct
{
    public partial class EditDepositProductForm
    {
        [Parameter]
        public string id { get; set; }

        bool showGLAccounts { get; set; } = false;
        bool showInterestRate { get; set; } = false;
        bool showBasicInformation { get; set; } = false;

        bool showApproval { get; set; } = false;
        bool showBenefitCode { get; set; } = false;
        bool showPopup { get; set; } = false;
        string DepositProductId { get; set; }

        string ApprovalWorkFlowId { get; set; }

        //string Active { get; set; } = "text-blue-600 border-b-2 border-blue-600 active";
        //string Inactive { get; set; } = "border-b-2 border-transparent";
        string Inactive { get; set; } =
            "class=\"inline-block p-4 border-b-2 border-transparent rounded-t-lg hover:text-gray-600 hover:border-gray-300 dark:hover:text-gray-300 text-gray-300\"";

        string Active { get; set; } =
            "class=\"inline-block p-4 text-blue-600 border-b-2 border-blue-600 rounded-t-lg active dark:text-blue-500 dark:border-blue-500\" aria-current=\"page\"";

        string ActiveArialPage { get; set; } = "aria-current=\"page\"";
        string InactiveArialPage { get; set; } = string.Empty;
        string GLAccountTab { get; set; } = string.Empty;
        string BasicInformationTab { get; set; } = string.Empty;
        string InterestRateTab { get; set; } = string.Empty;
        string ApprovalTab { get; set; } = string.Empty;


        [Inject]
        ILogger<EditDepositProductForm> Logger { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        IConfiguration _configuration { get; set; }

        [Inject]
        IClientAuditService _auditLogService { get; set; }

        public CreateDepositProductDTO BasicModel { get; set; }
        public List<CreateDepositProductInterestDTO> InterestRates { get; set; }
        public CreateDepositProductInterestDTO InterestRate { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        public GetDepositProductViewModel Model { get; set; }
        string ApprovalWorkFlow { get; set; }
        public UpdateDepositProductCommand updateDepositProductCommand { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUser();
            DepositProductId = id;
            Model = new GetDepositProductViewModel();
            BasicModel = new CreateDepositProductDTO();
            InterestRate = new CreateDepositProductInterestDTO();
            InterestRates = new List<CreateDepositProductInterestDTO>();
            updateDepositProductCommand = new UpdateDepositProductCommand();

            await GetDepositProduct();
            ActivateTabs(1);
        }

        public void ActivateTabs(int tabPosition)
        {
            switch (tabPosition)
            {
                case 1:
                    showGLAccounts = true;
                    showBasicInformation = false;
                    showInterestRate = false;
                    showApproval = false;
                    GLAccountTab = Active;
                    BasicInformationTab = Inactive;
                    InterestRateTab = Inactive;
                    ApprovalTab = Inactive;
                    StateHasChanged();
                    break;
                case 2:
                    showGLAccounts = false;
                    showBasicInformation = true;
                    showInterestRate = false;
                    showApproval = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Active;
                    InterestRateTab = Inactive;
                    ApprovalTab = Inactive;
                    StateHasChanged();
                    break;
                case 3:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showInterestRate = true;
                    showApproval = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    InterestRateTab = Active;
                    ApprovalTab = Inactive;
                    StateHasChanged();
                    break;
                case 4:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showInterestRate = false;
                    showApproval = true;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    InterestRateTab = Inactive;
                    ApprovalTab = Active;
                    StateHasChanged();
                    break;
            }
        }

        public async Task GetDepositProduct()
        {
            var rsp = await DataService.GetProduct<CommandResult<GetDepositProductViewModel>>(nameof(DepositProduct),
                DepositProductId);


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
                Model = JsonSerializer.Deserialize<GetDepositProductViewModel>(rsp.Content.Response.ToJson());
                MapToBasicModel();

                //MapToWhenDue();
                //MapToApprovalWorkFlow();
            }
        }

        private async Task OnApprovalWorkFlowSubmitChangedHandler(string approvalWorkFlowId)
        {
            ApprovalWorkFlowId = approvalWorkFlowId;
            Model.ApprovalWorkflowId = approvalWorkFlowId;

            await SaveEdit();
        }

        private async Task OnApprovalWorkFlowPreviousChangedHandler(string approvalWorkFlowId)
        {
            ApprovalWorkFlowId = approvalWorkFlowId;
            ActivateTabs(3);
        }

        private async Task Done()
        {
            showPopup = false;
            await InvokeAsync(StateHasChanged);
            _navigationManager.NavigateTo("/ProductSetup/Manage/all", true);
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        public void MapToBasicModel()
        {
            BasicModel.BankDepositAccountId = Model.BankDepositAccountId;
            BasicModel.DefaultCurrencyId = Model.DefaultCurrencyId;
            BasicModel.TenureValue = Model.TenureValue;
            BasicModel.Tenure = Model.Tenure;
            BasicModel.Code = Model.Code;
            BasicModel.Description = Model.Description;
            BasicModel.IsInterestEnabled = Model.IsInterestEnabled;
            //BasicModel.MaximumAge = Model.MaximumAge;
            //BasicModel.MinimumAge = Model.MinimumAge;
            BasicModel.Name = Model.Name;
            BasicModel.ShortName = Model.ShortName;
            BasicModel.ProductCharges = new List<string>();
            foreach (var item in Model.ProductCharges)
            {
                BasicModel.ProductCharges.Add(item.ChargeId);
            }
        }

        private async Task OnProceedChangedHandler(int tabPosition)
        {
            ActivateTabs(tabPosition);
        }

        private async Task OnBasicInfoProceedChangedHandler(CreateDepositProductDTO basic)
        {
            BasicModel = basic;
            updateDepositProductCommand.ProductCharges = new List<CreateDepositProductChargeCommand>();
            foreach (var item in basic.ProductCharges)
            {
                updateDepositProductCommand.ProductCharges.Add(new CreateDepositProductChargeCommand
                    { ProductId = Model.Id, ChargeId = item });
            }

            ActivateTabs(3);
        }

        private async Task OnBasicInfoPreviousChangedHandler(CreateDepositProductDTO basic)
        {
            BasicModel = basic;

            ActivateTabs(1);
            updateDepositProductCommand.ProductCharges = new List<CreateDepositProductChargeCommand>();
            foreach (var item in basic.ProductCharges)
            {
                updateDepositProductCommand.ProductCharges.Add(new CreateDepositProductChargeCommand
                    { ProductId = Model.Id, ChargeId = item });
            }
        }

        private async Task OnInterestProceedChangedHandler(GetDepositProductViewModel interest)
        {
            Model.InterestRanges = interest.InterestRanges;
            Model.IsInterestEnabled = interest.IsInterestEnabled;

            updateDepositProductCommand.InterestRanges = new List<CreateDepositProductInterestRangeCommand>();
            foreach (var item in interest.InterestRanges)
            {
                updateDepositProductCommand.InterestRanges.Add(new CreateDepositProductInterestRangeCommand
                {
                    ProductId = Model.Id, InterestRate = item.InterestRate,
                    LowerLimit = item.LowerLimit, UpperLimit = item.UpperLimit
                });
            }

            ActivateTabs(4);
        }

        private async Task OnInterestPreviousChangedHandler(GetDepositProductViewModel interest)
        {
            Model.InterestRanges = interest.InterestRanges;
            Model.IsInterestEnabled = interest.IsInterestEnabled;
            ActivateTabs(2);
        }

        public async Task SaveEdit()
        {
            try
            {
                MapToUpdateModel();
                updateDepositProductCommand.UpdatedByUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
                var rsp = await DataService.Update<UpdateDepositProductCommand, DepositProductViewModel>(
                    nameof(DepositProduct), updateDepositProductCommand);
                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(updateDepositProductCommand)}");
                if (rsp.IsSuccessStatusCode)
                {
                    showPopup = true;
                    StateHasChanged();
                }
                else
                {
                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    var msg = rspContent?.Response;
                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        msg = rspContent.ValidationErrors[0].Error;
                    }

                    if (msg == null && rspContent?.Message != null)
                        msg = rspContent.Message;
                    Logger.LogInformation($"ErrorMessage->{msg}");
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = msg,
                        NotificationType = NotificationType.Error
                    });
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MapToUpdateModel()
        {
            Model.BankDepositAccountId = BasicModel.BankDepositAccountId;
            Model.DefaultCurrencyId = BasicModel.DefaultCurrencyId;
            Model.TenureValue = BasicModel.TenureValue;
            Model.Tenure = BasicModel.Tenure;
            Model.Code = BasicModel.Code;
            Model.Description = BasicModel.Description;
            Model.IsInterestEnabled = BasicModel.IsInterestEnabled;
            //Model.MaximumAge = BasicModel.MaximumAge;
            //Model.MinimumAge = BasicModel.MinimumAge;
            Model.Name = BasicModel.Name;
            Model.ShortName = BasicModel.ShortName;


            updateDepositProductCommand.BankDepositAccountId = BasicModel.BankDepositAccountId;
            updateDepositProductCommand.DefaultCurrencyId = BasicModel.DefaultCurrencyId;
            updateDepositProductCommand.TenureValue = BasicModel.TenureValue;
            updateDepositProductCommand.Tenure = BasicModel.Tenure;
            updateDepositProductCommand.Code = BasicModel.Code;
            updateDepositProductCommand.Description = BasicModel.Description;
            updateDepositProductCommand.IsInterestEnabled = BasicModel.IsInterestEnabled;
            //updateDepositProductCommand.MaximumAge = BasicModel.MaximumAge;
            //updateDepositProductCommand.MinimumAge = BasicModel.MinimumAge;
            updateDepositProductCommand.Name = BasicModel.Name;
            updateDepositProductCommand.ShortName = BasicModel.ShortName;
            updateDepositProductCommand.Id = Model.Id;
            updateDepositProductCommand.ApprovalWorkflowId = ApprovalWorkFlowId;
        }
    }
}