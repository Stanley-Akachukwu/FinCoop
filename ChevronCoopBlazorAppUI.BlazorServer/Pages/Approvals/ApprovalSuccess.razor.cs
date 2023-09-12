using AntDesign;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositApplications;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanOffSetTransactions;
using AP.ChevronCoop.Entities.Loans.LoanTopupTransactions;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data.Approval;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Approvals
{
    public partial class ApprovalSuccess
    {
        [Inject]
        NavigationManager navigationManager { get; set; }

        [Inject]
        ILogger<ApprovalSuccess> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }
        public List<SavingsAccountApplicationMasterView> _SavingsAccountApplicationMasterView { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }
        MemberProfileMasterView MemberProfile { get; set; }
        HandleApprovalCommand Command { get; set; }
        bool showApprovalPopup { get; set; } = false;
        bool showApprovalSuccessPopup { get; set; } = false;
        bool showRejectPopup { get; set; } = false;
        bool showRejectSuccessPopup { get; set; } = false;
        string ApplicationUserId { get; set; }
        public ApprovalMasterView ViewModel { get; set; }
        public CreateLoanProductCommand CreateLoanProductCommand { get; set; }
        public List<LoanApplicationMasterView> CreateLoanApplicationCommand { get; set; }
        public CreateSavingsAccountApplicationCommand CreateSavingsAccountApplicationCommand { get; set; }
        public CreateSpecialDepositAccountApplicationCommand CreateSpecialDepositAccountApplicationCommand { get; set; }
        public UpdateMemberProfileCommand UpdateMemberProfileCommand { get; set; }
        public RegisterMemberCommand RegisterMemberCommand { get; set; }
        public List<FixedDepositAccountApplicationMasterView> CreateFixedDepositAccountApplicationCommand { get; set; }
        public DepositProductViewModel _DepositProductViewModel { get; set; }
        public List<LoanOffsetMasterView> _LoanOffsetMasterView { get; set; }
        public List<LoanTopupMasterView> _LoanTopupMasterView { get; set; }
        public string ApprovalId { get; set; }
        public string ApprovalWorkFlowId { get; set; }
        public ApprovalDTO Model { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        public List<ApprovalMasterView> ApprovalMasterViews { get; set; } = new List<ApprovalMasterView>();

        private FluentValidationValidator? _fluentValidationValidator;
        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalMasterView)}";

        protected override async Task OnInitializedAsync()
        {
            Model = new ApprovalDTO();
            ViewModel = new ApprovalMasterView();
            //get object from uri
            var uri = new Uri(navigationManager.Uri);
            var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);

            if (query.TryGetValue("ApprovalId", out var approvalIdValue))
            {
                ApprovalId = approvalIdValue;
            }

            Model.ApprovalId = ApprovalId;
            if (query.TryGetValue("ApprovalWorkFlowId", out var approvalWorkFlowIdValue))
            {
                ApprovalWorkFlowId = approvalWorkFlowIdValue;
            }

            await GetApprovalDetails();

            await GetCurrentUser();
            await GetProfile();
            Model.ApplicationUserId = ApplicationUserId;
        }

        private void GoToApprovals()
        {
            navigationManager.NavigateTo("/approvals/all", true);
        }

        private async Task GetApprovalDetails()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(GRID_API_RESOURCE);
                Stream stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsAsync<ApprovalMaster>();
                if (content != null)
                    ApprovalMasterViews = content.value;
                var model = ApprovalMasterViews.Where(x => x.Id.Equals(ApprovalId)).FirstOrDefault();

                ViewModel = model;
                if (ViewModel.ApprovalType.Equals(ApprovalType.LOAN_PRODUCT.ToString()))
                {
                    CreateLoanProductCommand = new CreateLoanProductCommand();
                    CreateLoanProductCommand =
                        System.Text.Json.JsonSerializer.Deserialize<CreateLoanProductCommand>(ViewModel.Payload);
                }

               else if (ViewModel.ApprovalType.Equals(ApprovalType.FIXED_DEPOSIT_APPLICATION.ToString()))
                {
                    var rsped = await DataService.GetValue<List<FixedDepositAccountApplicationMasterView>>(
                        nameof(FixedDepositAccountApplicationMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        CreateFixedDepositAccountApplicationCommand = new List<FixedDepositAccountApplicationMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        CreateFixedDepositAccountApplicationCommand =
                            JsonConvert.DeserializeObject<List<FixedDepositAccountApplicationMasterView>>(
                                rsped.Content.ToJson());
                    }

                }
                else if (ViewModel.ApprovalType.Equals(ApprovalType.SAVING_DEPOSIT_APPLICATION.ToString()))
                {
                    var rsped = await DataService.GetValue<List<SavingsAccountApplicationMasterView>>(
                       nameof(SavingsAccountApplicationMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        _SavingsAccountApplicationMasterView = new List<SavingsAccountApplicationMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _SavingsAccountApplicationMasterView =
                            JsonConvert.DeserializeObject<List<SavingsAccountApplicationMasterView>>(
                                rsped.Content.ToJson());
                    }

                }

                else if(ViewModel.ApprovalType.Equals(ApprovalType.SPECIAL_DEPOSIT_APPLICATION.ToString()))
                {
                    CreateSpecialDepositAccountApplicationCommand = new CreateSpecialDepositAccountApplicationCommand();
                    CreateSpecialDepositAccountApplicationCommand =
                        System.Text.Json.JsonSerializer.Deserialize<CreateSpecialDepositAccountApplicationCommand>(
                            ViewModel.Payload);
                }

                else if (ViewModel.ApprovalType.Equals(ApprovalType.KYC_COMPLETION.ToString()))
                {
                    UpdateMemberProfileCommand = new UpdateMemberProfileCommand();
                    UpdateMemberProfileCommand =
                        System.Text.Json.JsonSerializer.Deserialize<UpdateMemberProfileCommand>(ViewModel.Payload);
                }
                else if (ViewModel.ApprovalType.Equals(ApprovalType.LOAN_OFFSET_APPLICATION.ToString()))
                {
                    var rsped = await DataService.GetValue<List<LoanOffsetMasterView>>(
                        nameof(LoanOffsetMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        _LoanOffsetMasterView = new List<LoanOffsetMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _LoanOffsetMasterView =
                            JsonConvert.DeserializeObject<List<LoanOffsetMasterView>>(
                                rsped.Content.ToJson());
                    }
                }

                else if (ViewModel.ApprovalType.Equals(ApprovalType.LOAN_TOPUP_APPLICATION.ToString()))
                {
                    var rsped = await DataService.GetValue<List<LoanTopupMasterView>>(
                        nameof(LoanTopupMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        _LoanTopupMasterView = new List<LoanTopupMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _LoanTopupMasterView =
                            JsonConvert.DeserializeObject<List<LoanTopupMasterView>>(
                                rsped.Content.ToJson());
                    }
                }
                else if (ViewModel.ApprovalType.Equals(ApprovalType.MEMBER_ENROLLMENT.ToString()))
                {
                    RegisterMemberCommand = new RegisterMemberCommand();
                    RegisterMemberCommand =
                        System.Text.Json.JsonSerializer.Deserialize<RegisterMemberCommand>(ViewModel.Payload);
                }

                else if (ViewModel.ApprovalType.Equals(ApprovalType.LOAN_PRODUCT_APPLICATION.ToString()))
                {
                    var rsped = await DataService.GetValue<List<LoanApplicationMasterView>>(
                      nameof(LoanApplicationMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        CreateLoanApplicationCommand = new List<LoanApplicationMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        CreateLoanApplicationCommand =
                            JsonConvert.DeserializeObject<List<LoanApplicationMasterView>>(
                                rsped.Content.ToJson());
                    }
                }

                else if (ViewModel.ApprovalType.Equals(ApprovalType.DEPOSIT_PRODUCT.ToString()))
                {
                    _DepositProductViewModel = new DepositProductViewModel();
                    _DepositProductViewModel =
                        System.Text.Json.JsonSerializer.Deserialize<DepositProductViewModel>(ViewModel.Payload);
                }
            }
            catch (Exception exp)
            {
            }
        }

        public async Task GetProfile()
        {
            ApplicationUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrEmpty(ApplicationUserId))
            {
                navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
            }

            var rsp = await DataService.GetValue<List<MemberProfileMasterView>>(
                nameof(MemberProfileMasterView), ApplicationUserId);


            if (!rsp.IsSuccessStatusCode)
            {
                if (!string.IsNullOrEmpty(rsp.Error.Content))
                {
                    var rspContent =
                        System.Text.Json.JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

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
                List<MemberProfileMasterView> rspResponse =
                    System.Text.Json.JsonSerializer.Deserialize<List<MemberProfileMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().ApplicationUserId) &&
                    rspResponse.FirstOrDefault().ApplicationUserId == ApplicationUserId)
                {
                    MemberProfile = new MemberProfileMasterView();
                    MemberProfile = rspResponse.FirstOrDefault();
                    MemberProfile = Mapper.Map<MemberProfileMasterView>(rspResponse.FirstOrDefault());
                    ;
                }
            }
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }
    }
}