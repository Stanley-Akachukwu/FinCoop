using AntDesign;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup
{
    public partial class ViewLoanProductSetup
    {
        [Parameter]
        public string id { get; set; }

        bool showGLAccounts { get; set; } = false;
        bool showTopUpInfo { get; set; } = false;
        bool showBasicInformation { get; set; } = false;
        bool showTargetSetup { get; set; } = false;
        bool showOffset { get; set; } = false;
        bool showDueForLoan { get; set; } = false;
        bool showGuarantorInfo { get; set; } = false;
        bool showApproval { get; set; } = false;
        bool showBenefitCode { get; set; } = false;
        bool showPopup { get; set; } = false;
        bool showCustomerAccounts { get; set; } = false;

        string LoanProductId { get; set; }

        //string Active { get; set; } = "text-blue-600 border-b-2 border-blue-600 active";
        //string Inactive { get; set; } = "border-b-2 border-transparent";
        string Inactive { get; set; } =
            "class=\"inline-block p-4 border-b-2 border-transparent rounded-t-lg hover:text-gray-600 hover:border-gray-300 dark:hover:text-gray-300\"";

        string Active { get; set; } =
            "class=\"inline-block p-4 text-blue-600 border-b-2 border-blue-600 rounded-t-lg active dark:text-blue-500 dark:border-blue-500\" aria-current=\"page\"";

        string ActiveArialPage { get; set; } = "aria-current=\"page\"";
        string InactiveArialPage { get; set; } = string.Empty;
        string GLAccountTab { get; set; } = string.Empty;
        string BasicInformationTab { get; set; } = string.Empty;
        string TargetSetupTab { get; set; } = string.Empty;
        string TopupTab { get; set; } = string.Empty;
        string OffsetTab { get; set; } = string.Empty;
        string DueLoanTab { get; set; } = string.Empty;
        string GuarantorTab { get; set; } = string.Empty;
        string ApprovalTab { get; set; } = string.Empty;
        string CustomerAccountTab { get; set; } = string.Empty;
        public GetLoanProductViewModel Model { get; set; }

        [Inject]
        ILogger<ViewLoanProductSetup> Logger { get; set; }

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
        IEntityDataService DataService { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }
        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            LoanProductId = id;
            Model = new GetLoanProductViewModel();
            await GetLoanProduct();
            ActivateTabs(1);
        }

        public void ActivateTabs(int tabPosition)
        {
            switch (tabPosition)
            {
                case 1:
                    showGLAccounts = true;
                    showBasicInformation = false;
                    showTargetSetup = false;
                    showTopUpInfo = false;
                    showOffset = false;
                    showDueForLoan = false;
                    showGuarantorInfo = false;
                    showApproval = false;
                    showCustomerAccounts = false;
                    GLAccountTab = Active;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Inactive;
                    TopupTab = Inactive;
                    OffsetTab = Inactive;
                    DueLoanTab = Inactive;
                    ApprovalTab = Inactive;
                    GuarantorTab = Inactive;
                    CustomerAccountTab = Inactive;

                    StateHasChanged();
                    break;
                case 2:
                    showGLAccounts = false;
                    showBasicInformation = true;
                    showTargetSetup = false;
                    showTopUpInfo = false;
                    showOffset = false;
                    showDueForLoan = false;
                    showGuarantorInfo = false;
                    showApproval = false;
                    showCustomerAccounts = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Active;
                    TargetSetupTab = Inactive;
                    TopupTab = Inactive;
                    OffsetTab = Inactive;
                    DueLoanTab = Inactive;
                    ApprovalTab = Inactive;
                    GuarantorTab = Inactive;
                    CustomerAccountTab = Inactive;
                    StateHasChanged();
                    break;
                case 3:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showTargetSetup = true;
                    showTopUpInfo = false;
                    showOffset = false;
                    showDueForLoan = false;
                    showGuarantorInfo = false;
                    showApproval = false;
                    showCustomerAccounts = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Active;
                    TopupTab = Inactive;
                    OffsetTab = Inactive;
                    DueLoanTab = Inactive;
                    ApprovalTab = Inactive;
                    GuarantorTab = Inactive;
                    CustomerAccountTab = Inactive;
                    StateHasChanged();
                    break;
                case 4:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showTargetSetup = false;
                    showTopUpInfo = false;
                    showOffset = true;
                    showDueForLoan = false;
                    showGuarantorInfo = false;
                    showApproval = false;
                    showCustomerAccounts = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Inactive;
                    TopupTab = Inactive;
                    OffsetTab = Active;
                    DueLoanTab = Inactive;
                    ApprovalTab = Inactive;
                    GuarantorTab = Inactive;
                    CustomerAccountTab = Inactive;
                    StateHasChanged();
                    break;
                case 5:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showTargetSetup = false;
                    showTopUpInfo = true;
                    showOffset = false;
                    showDueForLoan = false;
                    showGuarantorInfo = false;
                    showApproval = false;
                    showCustomerAccounts = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Inactive;
                    TopupTab = Active;
                    OffsetTab = Inactive;
                    DueLoanTab = Inactive;
                    ApprovalTab = Inactive;
                    GuarantorTab = Inactive;
                    CustomerAccountTab = Inactive;
                    StateHasChanged();
                    break;
                case 6:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showTargetSetup = false;
                    showTopUpInfo = false;
                    showOffset = false;
                    showDueForLoan = true;
                    showGuarantorInfo = false;
                    showApproval = false;
                    showCustomerAccounts = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Inactive;
                    TopupTab = Inactive;
                    OffsetTab = Inactive;
                    DueLoanTab = Active;
                    ApprovalTab = Inactive;
                    GuarantorTab = Inactive;
                    CustomerAccountTab = Inactive;
                    StateHasChanged();
                    break;
                case 7:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showTargetSetup = false;
                    showTopUpInfo = false;
                    showOffset = false;
                    showDueForLoan = false;
                    showGuarantorInfo = true;
                    showApproval = false;
                    showCustomerAccounts = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Inactive;
                    TopupTab = Inactive;
                    OffsetTab = Inactive;
                    DueLoanTab = Inactive;
                    ApprovalTab = Inactive;
                    GuarantorTab = Active;
                    CustomerAccountTab = Inactive;
                    StateHasChanged();
                    break;
                case 8:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showTargetSetup = false;
                    showTopUpInfo = false;
                    showOffset = false;
                    showDueForLoan = false;
                    showGuarantorInfo = false;
                    showApproval = true;
                    showCustomerAccounts = false;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Inactive;
                    TopupTab = Inactive;
                    OffsetTab = Inactive;
                    DueLoanTab = Inactive;
                    ApprovalTab = Active;
                    GuarantorTab = Inactive;
                    CustomerAccountTab = Inactive;
                    StateHasChanged();
                    break;
                case 9:
                    showGLAccounts = false;
                    showBasicInformation = false;
                    showTargetSetup = false;
                    showTopUpInfo = false;
                    showOffset = false;
                    showDueForLoan = false;
                    showGuarantorInfo = false;
                    showApproval = false;
                    showCustomerAccounts = true;
                    GLAccountTab = Inactive;
                    BasicInformationTab = Inactive;
                    TargetSetupTab = Inactive;
                    TopupTab = Inactive;
                    OffsetTab = Inactive;
                    DueLoanTab = Inactive;
                    ApprovalTab = Inactive;
                    GuarantorTab = Inactive;
                    CustomerAccountTab = Active;
                    StateHasChanged();
                    break;
            }
        }

        public async Task GetLoanProduct()
        {
            var rsp = await DataService.GetProduct<CommandResult<GetLoanProductViewModel>>(nameof(LoanProduct),
                LoanProductId);


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
                Model = JsonSerializer.Deserialize<GetLoanProductViewModel>(rsp.Content.Response.ToJson());
            }
        }

        private async Task OnProceedChangedHandler(int tabPosition)
        {
            ActivateTabs(tabPosition);
        }
    }
}