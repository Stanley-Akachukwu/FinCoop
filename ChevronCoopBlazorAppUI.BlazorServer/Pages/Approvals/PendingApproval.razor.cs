using AntDesign;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositApplications;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositChangeInMaturities;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountApplications;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositFundTransfer;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanOffSetTransactions;
using AP.ChevronCoop.Entities.Loans.LoanTopupTransactions;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
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
    public partial class PendingApproval
    {
        [Inject]
        ILogger<PendingApproval> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

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

       
        public CreateSpecialDepositAccountApplicationCommand CreateSpecialDepositAccountApplicationCommand { get; set; }
        public UpdateMemberProfileCommand UpdateMemberProfileCommand { get; set; }
        public RegisterMemberCommand RegisterMemberCommand { get; set; }
        
        public CreateDepositProductCommand CreateDepositProductCommand { get; set; }

        public List<SavingsCashAdditionMasterView> _SavingsCashAdditionMasterView { get; set; } = new List<SavingsCashAdditionMasterView>();

        public List<SavingsIncreaseDecreaseMasterView> _SavingsIncreaseDecreaseMasterView { get; set; } = new List<SavingsIncreaseDecreaseMasterView>();

        public List<SavingsAccountApplicationMasterView> _SavingsAccountApplicationMasterView { get; set; } = new List<SavingsAccountApplicationMasterView>();

        public List<SpecialDepositIncreaseDecreaseMasterView> _SpecialDepositIncreaseDecreaseMasterView { get; set; } = new List<SpecialDepositIncreaseDecreaseMasterView>();

    

        public List<DepositProduct> _DepositProducts { get; set; } = new List<DepositProduct>();

        public List<LoanOffsetMasterView> _LoanOffsetMasterView { get; set; } = new List<LoanOffsetMasterView>();

        public List<LoanTopupMasterView> _LoanTopupMasterView { get; set; } = new List<LoanTopupMasterView>();

        public List<SpecialDepositCashAdditionMasterView> _SpecialDepositCashAdditionMasterView { get; set; } = new List<SpecialDepositCashAdditionMasterView>();

        public List<SpecialDepositAccountApplicationMasterView> _SpecialDepositAccountApplicationMasterView { get; set; } = new List<SpecialDepositAccountApplicationMasterView>();
        public List<SpecialDepositFundTransferMasterView> _SpecialDepositFundTransferMasterView { get; set; } = new List<SpecialDepositFundTransferMasterView>();

        public List<SpecialDepositWithdrawalMasterView> _SpecialDepositWithdrawalMasterView { get; set; } = new List<SpecialDepositWithdrawalMasterView>();

        public List<FixedDepositLiquidationMasterView> _FixedDepositLiquidationMasterView { get; set; } = new List<FixedDepositLiquidationMasterView>();

        public List<FixedDepositChangeInMaturityMasterView> _FixedDepositChangeInMaturityMasterView { get; set; } = new List<FixedDepositChangeInMaturityMasterView>();

        public List<FixedDepositAccountApplicationMasterView> _FixedDepositAccountApplicationMasterView { get; set; } = new List<FixedDepositAccountApplicationMasterView>();

        public string ApprovalId { get; set; }
        public string ApprovalWorkFlowId { get; set; }
        public ApprovalDTO Model { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        public List<ApprovalMasterView> ApprovalMasterViews { get; set; } = new List<ApprovalMasterView>();

        private FluentValidationValidator? _fluentValidationValidator;
        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalMasterView)}";

        private bool isLoading = true;

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

            isLoading = false;
        }

        private void GoToApprovals()
        {
            navigationManager.NavigateTo("/approvals/all", true);
            showApprovalSuccessPopup = false;
            showRejectSuccessPopup = false;
        }

        private void Cancel()
        {
            showApprovalSuccessPopup = false;
            showRejectSuccessPopup = false;
            showApprovalPopup = false;
            showRejectPopup = false;
            StateHasChanged();
        }

        private void ViewApprovalTrail()
        {
            navigationManager.NavigateTo("/approvals/trail", true);
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        public async Task ShowSeverErrorAlert()
        {
            
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
                        _FixedDepositAccountApplicationMasterView = new List<FixedDepositAccountApplicationMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _FixedDepositAccountApplicationMasterView =
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

                else if (ViewModel.ApprovalType.Equals(ApprovalType.SPECIAL_DEPOSIT_APPLICATION.ToString()))
                {
                    var rsped = await DataService.GetValue<List<SpecialDepositAccountApplicationMasterView>>(
                      nameof(SpecialDepositAccountApplicationMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        _SpecialDepositAccountApplicationMasterView = new List<SpecialDepositAccountApplicationMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _SpecialDepositAccountApplicationMasterView =
                            JsonConvert.DeserializeObject<List<SpecialDepositAccountApplicationMasterView>>(
                                rsped.Content.ToJson());
                    }
                }

                else if (ViewModel.ApprovalType.Equals(ApprovalType.KYC_COMPLETION.ToString()))
                {
                    UpdateMemberProfileCommand = new UpdateMemberProfileCommand();
                    UpdateMemberProfileCommand =
                        System.Text.Json.JsonSerializer.Deserialize<UpdateMemberProfileCommand>(ViewModel.Payload);
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
                    var rsped = await DataService.GetValue<List<DepositProduct>>(
                        nameof(DepositProduct), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        _DepositProducts = new List<DepositProduct>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _DepositProducts = JsonConvert.DeserializeObject<List<DepositProduct>>(rsped.Content.ToJson());
                    }
                }

                else if (ViewModel.ApprovalType.Equals(ApprovalType.SAVINGS_CASH_ADDITION.ToString()))
                {
                    var rsped = await DataService.GetValue<List<SavingsCashAdditionMasterView>>(
                        nameof(SavingsCashAdditionMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        _SavingsCashAdditionMasterView = new List<SavingsCashAdditionMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _SavingsCashAdditionMasterView =
                            JsonConvert.DeserializeObject<List<SavingsCashAdditionMasterView>>(rsped.Content.ToJson());
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

                else if (ViewModel.ApprovalType.Equals(ApprovalType.SAVINGS_INCREASE_DECREASE.ToString()))
                {
                    var rsped = await DataService.GetValue<List<SavingsIncreaseDecreaseMasterView>>(
                        nameof(SavingsIncreaseDecreaseMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        _SavingsIncreaseDecreaseMasterView = new List<SavingsIncreaseDecreaseMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _SavingsIncreaseDecreaseMasterView =
                            JsonConvert.DeserializeObject<List<SavingsIncreaseDecreaseMasterView>>(
                                rsped.Content.ToJson());
                    }
                }
                else if (ViewModel.ApprovalType.Equals(ApprovalType.SPECIAL_DEPOSIT_INCREASE_DECREASE.ToString()))
                {
                    var rsped = await DataService.GetValue<List<SpecialDepositIncreaseDecreaseMasterView>>(
                        nameof(SpecialDepositIncreaseDecreaseMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        _SpecialDepositIncreaseDecreaseMasterView = new List<SpecialDepositIncreaseDecreaseMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _SpecialDepositIncreaseDecreaseMasterView =
                            JsonConvert.DeserializeObject<List<SpecialDepositIncreaseDecreaseMasterView>>(
                                rsped.Content.ToJson());
                    }
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

                if (ViewModel.ApprovalType.Equals(ApprovalType.SPECIAL_DEPOSIT_CASH_ADDITION.ToString()))
                {
                    var rsped = await DataService.GetValue<List<SpecialDepositCashAdditionMasterView>>(
                        nameof(SpecialDepositCashAdditionMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        _SpecialDepositCashAdditionMasterView = new List<SpecialDepositCashAdditionMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _SpecialDepositCashAdditionMasterView =
                            JsonConvert.DeserializeObject<List<SpecialDepositCashAdditionMasterView>>(
                                rsped.Content.ToJson());
                    }
                }

                if (ViewModel.ApprovalType.Equals(ApprovalType.SPECIAL_DEPOSIT_FUND_TRANSFER.ToString()))
                {
                    var rsped = await DataService.GetValue<List<SpecialDepositFundTransferMasterView>>(
                        nameof(SpecialDepositFundTransferMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        _SpecialDepositFundTransferMasterView = new List<SpecialDepositFundTransferMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _SpecialDepositFundTransferMasterView =
                            JsonConvert.DeserializeObject<List<SpecialDepositFundTransferMasterView>>(
                                rsped.Content.ToJson());
                    }
                }
                else if (ViewModel.ApprovalType.Equals(ApprovalType.SPECIAL_DEPOSIT_WITHDRAWAL.ToString()))
                {
                    var rsped = await DataService.GetValue<List<SpecialDepositWithdrawalMasterView>>(
                        nameof(SpecialDepositWithdrawalMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        _SpecialDepositWithdrawalMasterView = new List<SpecialDepositWithdrawalMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _SpecialDepositWithdrawalMasterView =
                            JsonConvert.DeserializeObject<List<SpecialDepositWithdrawalMasterView>>(
                                rsped.Content.ToJson());
                    }
                }

                else if (ViewModel.ApprovalType.Equals(ApprovalType.FIXED_DEPOSIT_LIQUIDATION.ToString()))
                {
                    var rsped = await DataService.GetValue<List<FixedDepositLiquidationMasterView>>(
                        nameof(FixedDepositLiquidationMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        _FixedDepositLiquidationMasterView = new List<FixedDepositLiquidationMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _FixedDepositLiquidationMasterView =
                            JsonConvert.DeserializeObject<List<FixedDepositLiquidationMasterView>>(
                                rsped.Content.ToJson());
                    }
                }

                else if (ViewModel.ApprovalType.Equals(ApprovalType.FIXED_DEPOSIT_CHANGE_IN_MATURITY.ToString()))
                {
                    var rsped = await DataService.GetValue<List<FixedDepositChangeInMaturityMasterView>>(
                        nameof(FixedDepositChangeInMaturityMasterView), "approvalId", ApprovalId);
                    if (rsped.IsSuccessStatusCode)
                    {
                        _FixedDepositChangeInMaturityMasterView = new List<FixedDepositChangeInMaturityMasterView>();
                        var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                        _FixedDepositChangeInMaturityMasterView =
                            JsonConvert.DeserializeObject<List<FixedDepositChangeInMaturityMasterView>>(
                                rsped.Content.ToJson());
                    }
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

        public void MapToCommand()
        {
            Command = new HandleApprovalCommand()
            {
                ApprovalId = Model.ApprovalId,
                ApplicationUserId = Model.ApplicationUserId
            };
        }

        private async Task ApproveAsync()
        {
            MapToCommand();
            Command.Status = AP.ChevronCoop.Entities.ApprovalStatus.APPROVED;
            Command.Comment = Model.Comment;
            if (Model.ApprovalId == null)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = "Approval Id cannot be null",
                    NotificationType = NotificationType.Info
                });
                return;
            }
            else
            {
                StateHasChanged();
                Logger.LogInformation($"rsp submitpayload->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                var rsp =
                    await DataService.Approval<HandleApprovalCommand, CommandResult<HandleApprovalCommand>>(
                        nameof(Approval), Command);
                Logger.LogInformation($"rsp content->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                if (rsp.IsSuccessStatusCode)
                {
                    await _auditLogService.LogAudit("Request Approval", "Request Approval", "Approval",
                        "NA, readonly request", CurrentUser);
                    showApprovalSuccessPopup = true;
                    showApprovalPopup = false;
                    StateHasChanged();
                }
                else
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
                    Logger.LogInformation($"ErrorMessage->{msg}");
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = msg,
                        NotificationType = NotificationType.Error
                    });
                    StateHasChanged();
                    showApprovalPopup = false;
                }
            }

            StateHasChanged();
        }

        private async Task RejectAsync()
        {
            MapToCommand();
            Command.Status = AP.ChevronCoop.Entities.ApprovalStatus.REJECTED;
            Command.Comment = Model.Comment;
            if (Model.ApprovalId == null)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = "Approval Id cannot be null",
                    NotificationType = NotificationType.Info
                });
                return;
            }
            else
            {
                StateHasChanged();
                Logger.LogInformation($"rsp submitpayload->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                var rsp =
                    await DataService.Approval<HandleApprovalCommand, CommandResult<HandleApprovalCommand>>(
                        nameof(Approval), Command);
                Logger.LogInformation($"rsp content->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                if (rsp.IsSuccessStatusCode)
                {
                    await _auditLogService.LogAudit("Request Approval", "Request Approval", "Approval",
                        "NA, readonly request", CurrentUser);
                    showRejectSuccessPopup = true;
                    showRejectPopup = false;
                    StateHasChanged();
                }
                else
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
                    Logger.LogInformation($"ErrorMessage->{msg}");
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = msg,
                        NotificationType = NotificationType.Error
                    });
                    StateHasChanged();
                    showApprovalPopup = false;
                }
            }

            StateHasChanged();
        }

        private async Task ShowApproval()
        {
            showApprovalPopup = true;
        }

        private async Task ShowApprovalPopUpMessage()
        {
            showApprovalSuccessPopup = true;
        }

        private async Task ShowReject()
        {
            showRejectPopup = true;
        }

        private async Task ShowRejectPopUpMessage()
        {
            showRejectSuccessPopup = true;
        }
    }

    public class ApprovalMaster
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@odata.count")]
        public int odatacount { get; set; }

        public List<ApprovalMasterView> value { get; set; }
    }
}