using AntDesign;
using AP.ChevronCoop.AppDomain.Customers.Customers;
using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Loans.LoanApplicationGuarantors;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.GuarantorApproval
{
    public partial class GuarantorApprovalDetailForm
    {
        bool showApprovalBadge { get; set; } = false;
        bool showRejectedBadge { get; set; } = false;
        bool showButton { get; set; } = false;
        bool showApprovalModal { get; set; } = false;
        bool showRejectModal { get; set; } = false;
        bool showPopup { get; set; } = false;
        string popupMessage { get; set; } = string.Empty;
        bool showRemarks { get; set; } = false;
        string Comment { get; set; } = string.Empty;

        [Parameter]
        public string Id { get; set; }

        [Inject]
        ILogger<GuarantorApprovalDetailForm> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        public GetLoanApplicationGuarantorViewModel LoanAppView { get; set; }


        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }
        public LoanApplicationGuarantorMasterView LoanApplicationGuarantorMasterView { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        public string CustomerId { get; set; }

        private string bearToken { get; set; }
        public LoanApplicationGuarantorApprovalCommand Command;

        protected override async Task OnInitializedAsync()
        {
            LoanAppView = new GetLoanApplicationGuarantorViewModel();
            LoanAppView.Applicant = new CustomerViewModel();
            LoanAppView.LoanApplication = new LoanApplicationViewModel();
            LoanAppView.Guarantor = new CustomerViewModel();
            LoanAppView.Product = new LoanProductViewModel();
            await GetCurrentUser();
            Command = new LoanApplicationGuarantorApprovalCommand();
            await GetLoanApplication();
            await GetLoanApplicationGuarantors();
            await _auditLogService.LogAudit("Viewed Loan application detail in Guarantor request.",
                "iewed Loan application detail in Guarantor request.", "Security", "NA, readonly request", CurrentUser);
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
            if (CurrentUser != null)
            {
                CustomerId = CurrentUser.Claims.Where(f => f.Type == "CustomerId").FirstOrDefault().Value;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);
                if (string.IsNullOrEmpty(bearToken))
                {
                    _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
                }

                //await OnRefresh();
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task GetLoanApplication()
        {
            var rsp = await DataService.GetLoanApplication<GetLoanApplicationGuarantorViewModel>(Id);


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
                LoanAppView = JsonSerializer.Deserialize<GetLoanApplicationGuarantorViewModel>(rsp.Content.ToJson());
                if (LoanAppView != null)
                {
                    if (!string.IsNullOrEmpty(LoanAppView.Comment))
                        showRemarks = true;
                    if (LoanAppView.Status == ApprovalStatus.PENDING_APPROVAL)
                    {
                        showApprovalBadge = false;
                        showRejectedBadge = false;
                        showButton = true;
                        StateHasChanged();
                    }
                    else if (LoanAppView.Status == ApprovalStatus.APPROVED)
                    {
                        showApprovalBadge = true;
                        showRejectedBadge = false;
                        showButton = false;
                        StateHasChanged();
                    }
                    else if (LoanAppView.Status == ApprovalStatus.REJECTED)
                    {
                        showApprovalBadge = false;
                        showRejectedBadge = true;
                        showButton = false;
                        StateHasChanged();
                    }
                }
            }
        }
        public async Task GetLoanApplicationGuarantors()
        {
            var rsp = await DataService.GetValue<List<LoanApplicationGuarantorMasterView>>(
                nameof(LoanApplicationGuarantorMasterView), "id", Id);

            if (!rsp.IsSuccessStatusCode)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = $"Oops! Something Went Wrong. Please try again later. Thanks: {rsp.Error} - {rsp.Content} - {rsp.ContentHeaders} ",
                    NotificationType = NotificationType.Error
                });
                LoanApplicationGuarantorMasterView = new LoanApplicationGuarantorMasterView();
            }
            else
            {
                List<LoanApplicationGuarantorMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<LoanApplicationGuarantorMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().Id) &&
                    rspResponse.FirstOrDefault().Id == Id)
                {
                    LoanApplicationGuarantorMasterView = Mapper.Map<LoanApplicationGuarantorMasterView>(rspResponse.FirstOrDefault());
                }
            }
        }
        private async Task SubmitAction(bool status)
        {
            MapToCommand(status);
            if (string.IsNullOrEmpty(Comment) && status == false)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = "Please enter your reason for rejection",
                    NotificationType = NotificationType.Info
                });
                return;
            }
            else
            {
                //SubmitButtonText = "Saving ...";
                StateHasChanged();
                Logger.LogInformation($"rsp submitpayload->{JsonSerializer.Serialize(Command)}");
                var rsp = await DataService
                    .ProcessRequest<LoanApplicationGuarantorApprovalCommand,
                        CommandResult<LoanApplicationGuarantorApprovalViewModel>>(nameof(LoanApplicationGuarantor),
                        "approve", Command);
                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(Command)}");
                if (rsp.IsSuccessStatusCode)
                {
                    await _auditLogService.LogAudit("Guarantor Approve Loan Application", "Approve Loan Application",
                        "Accounts", "NA, readonly request", CurrentUser);
                    showPopup = true;
                    StateHasChanged();
                    //_navigationManager.NavigateTo("/security/guarantorapproval", forceLoad: true);
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
                    //SubmitButtonText = "Submit";
                    StateHasChanged();
                }
            }

            StateHasChanged();
        }

        public void MapToCommand(bool status)
        {
            Command = new LoanApplicationGuarantorApprovalCommand()
            {
                Comment = Comment,
                LoanApplicationId = LoanAppView.LoanApplication.Id,
                GuarantorId = CustomerId,
                IsApproved = status,
                GuarantorApprovalType = (GuarantorApprovalType)System.Enum.Parse(typeof(GuarantorApprovalType), LoanApplicationGuarantorMasterView.GuarantorApprovalType, true)
            };
        }
        public void Done() => _navigationManager.NavigateTo("/security/guarantorapproval", forceLoad: true);

    }
}