using AntDesign;
using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.AppDomain.Loans.LoanApplicationItems;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.AppDomain.Loans.LoanApplicationSchedules;
using AP.ChevronCoop.AppDomain.Loans.LoanTopups;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanApplication;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount.LoanTopUp
{
    public partial class LoanTopUpApplication
    {
        private FluentValidationValidator? _fluentValidationValidator;

        [Inject]
        ILogger<LoanTopUpApplication> Logger { get; set; }

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
        bool HideGuarantorForm { get; set; } = false;
        bool showLoanTopComponent { get; set; } = false;
        bool topUpInformationComplete { get; set; } = false;
        bool showRepaymentScheduleComponent { get; set; } = false;
        bool showGuarantorComponent { get; set; } = false;
        bool loanTopComplete { get; set; } = false;
        bool repaymentScheduleComplete { get; set; } = false;
        bool guarantorComplete { get; set; } = false;
        bool showAddDrawer { get; set; } = false;
        bool showPopup { get; set; } = false;
        bool showLoanTopPopup { get; set; } = false;
        bool AccountIsProvided { get; set; }

        bool IsRegularMember { get; set; } = false;
        string MemberShipId { get; set; }

        string SubmitButtonText = "Submit";
        string guarantorSearchButton = "Search";
        string combobox_preferredSpecialAccounts;
        string combobox_preferredAccounts;
        string AmountDescription { get; set; } = "Top-Up Amount";
        public string CustomerId { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        string ApplicationUserId { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        MemberProfileMasterView MemberProfile { get; set; }

        CreateLoanTopupCommand Command { get; set; }
        CreateLoanApplicationGuarantorCommand CreateLoanGuarantorModel { get; set; }
        DeleteLoanApplicationItemCommand DeleteModel { get; set; }
        private LoanApplicationMasterView ViewModel;
        private LoanAccountMasterView LoanAccountViewModel;
        private List<LoanApplicationGuarantorMasterView> LoanApplicationGuarantorViewModel;
        private List<LoanProductMasterView> LoanProductMasterViewModel;
        private LoanProductMasterView SelectedLoanProduct;
        public LoanTopUpDTO Model { get; set; }

        [Parameter]
        public EventCallback<LoanTopUpDTO> ModelChanged { get; set; }

        public LoanTopUpGuarantorDTO GuarantorMemberProfileMasterView { get; set; }
        public List<LoanTopUpGuarantorDTO> GuarantorMemberProfileMasterViewList { get; set; }
        public List<LoanApplicationScheduleViewModel> LoanApplicationScheduleViewModels { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]

        NavigationManager NavigationManager { get; set; }

        [Inject] IClientAuditService _auditLogService { get; set; }
        public List<LoanApplicationScheduleViewModel> loanApplicationScheduleViewModels { get; set; }

        LoanTopUpGuarantor createForm;
        Drawer createDrawer;
        bool showCreateDrawer { get; set; } = false;

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;

        ClaimsPrincipal CurrentUser { get; set; }

        string GuarantorDescription { get; set; } = "";
        string RepaymentDescription { get; set; } = "Repayment Period";
        private bool HasRecords { get; set; } = false;
        bool showSpecialDepositAccount { get; set; } = false;
        bool showPreferredAccount { get; set; } = false;
        public string LoanProductId { get; set; }
        decimal interest = 0;
        decimal remainingBalance = 0;
        decimal adminCharge = 0;

        public List<LoanApplicationMasterView> LoanApplicationItemMasterViews { get; set; } =
            new List<LoanApplicationMasterView>();

        public List<LoanAccountMasterView> LoanAccountMasterViews { get; set; } = new List<LoanAccountMasterView>();

        public List<LoanApplicationGuarantorMasterView> LoanApplicationGuarantorMasterViews { get; set; } =
            new List<LoanApplicationGuarantorMasterView>();

        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(LoanApplicationMasterView)}";
        string GRID_API_RESOURCE_LOAN_APPLICATION => $"{Config.ODATA_VIEWS_HOST}/{nameof(LoanAccountMasterView)}";

        string GRID_API_RESOURCE_LOAN_APPLICATION_GUARANTOR =>
            $"{Config.ODATA_VIEWS_HOST}/{nameof(LoanApplicationGuarantorMasterView)}";

        string GRID_API_RESOURCE_LOAN_PRODUCT => $"{Config.ODATA_VIEWS_HOST}/{nameof(LoanProductMasterView)}";
        [Inject] IUtilityService _utilityService { get; set; }
        public GenerateScheduleViewModel generateScheduleViewModel { get; set; }
        [Parameter]
        public string loanAccountId { get; set; }
        SfGrid<AmortizationSchedule> grid;
        protected override async Task OnInitializedAsync()
        {
            showLoanTopComponent = true;
            CreateLoanGuarantorModel = new CreateLoanApplicationGuarantorCommand();
            MemberProfile = new MemberProfileMasterView();
            Model = new LoanTopUpDTO();
            Command = new CreateLoanTopupCommand();
            generateScheduleViewModel = new GenerateScheduleViewModel();
            generateScheduleViewModel.Schedules = new List<AmortizationSchedule>();
            GuarantorMemberProfileMasterView = new LoanTopUpGuarantorDTO();
            GuarantorMemberProfileMasterViewList = new List<LoanTopUpGuarantorDTO>();
            loanApplicationScheduleViewModels = new List<LoanApplicationScheduleViewModel>();

            //await GetLoanDetails();
            await GetLoanAccountDetails();
            await MapLoanAccountDetailToModel();
            await GetLoanProduct();
            await GetLoanApplicationGuarantorDetails();

            if (SelectedLoanProduct.IsGuarantorRequired == true)
                GuarantorDescription =
                    $"{SelectedLoanProduct.EmployeeGuarantorCount} Regular Member , {SelectedLoanProduct.NonEmployeeGuarantorCount} Retired Member";
            else
                GuarantorDescription = string.Empty;

            await GetCurrentUser();
            await GetProfile();
            await LoadDropDown();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                createDrawer.Width = (int)(BrowserDimension.Width * 0.40);
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }
        public async Task MapLoanAccountDetailToModel()
        {
            Model = new LoanTopUpDTO();
            if (LoanAccountViewModel != null && !string.IsNullOrEmpty(LoanAccountViewModel.Id))
            {
                Model.LoanProductId = LoanAccountViewModel.LoanApplicationId_LoanProductId;
                Model.MemberProfileId = LoanAccountViewModel.CustomerId_MemberId;
                Model.TenureValue = LoanAccountViewModel.TenureValue;
                Model.Principal = LoanAccountViewModel.Principal;
                Model.UseSpecialDeposit = LoanAccountViewModel.UseSpecialDeposit;
                Model.PrincipalBalance = LoanAccountViewModel.PrincipalBalanceAccountId_LedgerBalance;
                Model.InterestBalance = LoanAccountViewModel.InterestBalanceAccountId_LedgerBalance.GetValueOrDefault();
                Model.AdminFee = 0.00M;
                Model.ApprovalId = LoanAccountViewModel.LoanApplicationId_ApprovalId;
                Model.CommencementDate = _utilityService.RepaymentCommencementDate();
                Model.LoanAccountId = loanAccountId;

            }

        }

        private async Task BackToLoanGridAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/account/loanproductsapplications", true);
            }
        }

        private async Task ProceedToRepaymentScheduleAsync()
        {
            if (string.IsNullOrEmpty(Model.SpecialDepositId) && string.IsNullOrEmpty(Model.DestinationAccountId))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = "Please select Disbursement account",
                    NotificationType = NotificationType.Info
                });
                return;
            }
            if (await _fluentValidationValidator.ValidateAsync())
            {
                showRepaymentScheduleComponent = true;
                topUpInformationComplete = true;
                showGuarantorComponent = false;
                showLoanTopComponent = false;
                StateHasChanged();
                await GetRepaymentPlan();
            }
        }

        private async Task ProceedToGuarantorAsync()
        {
            topUpInformationComplete = true;
            showRepaymentScheduleComponent = false;
            showGuarantorComponent = true;
            showLoanTopComponent = false;
            repaymentScheduleComplete = true;
            StateHasChanged();
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        private async Task BackToRepaymentScheduleAsync()
        {
            showRepaymentScheduleComponent = true;
            showGuarantorComponent = false;
            showLoanTopComponent = false;
            repaymentScheduleComplete = false;
            StateHasChanged();
        }

        private async Task BackToLoanTopUpAsync()
        {
            topUpInformationComplete = false;
            showRepaymentScheduleComponent = false;
            showGuarantorComponent = false;
            showLoanTopComponent = true;
            StateHasChanged();
        }

        private async Task SubmitAsync()
        {
            MapToCommand();
            if (Model.LoanProductId == null)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = "Please select preferred product to continue",
                    NotificationType = NotificationType.Info
                });
                return;
            }

            if(Model.TopUpAmount > remainingBalance)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = $"The Topup amount entered is greater than your current loan balance of {Model.PrincipalBalance.ToString("N2")}",
                    NotificationType = NotificationType.Info
                });
                return;
            }
            else
            {
                var validGuarantorNumber = ValidateGuarantorCount();
                if (validGuarantorNumber != null)
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Info",
                        Description = validGuarantorNumber,
                        NotificationType = NotificationType.Info
                    });
                    return;
                }

                SubmitButtonText = "Submitting ...";
                StateHasChanged();
                Logger.LogInformation($"rsp submitpayload->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                var rsp =
                    await DataService.Create<CreateLoanTopupCommand, CommandResult<LoanTopupViewModel>>(
                        nameof(LoanTopup), Command);
                Logger.LogInformation($"rsp content->{System.Text.Json.JsonSerializer.Serialize(Command)}");
                if (rsp.IsSuccessStatusCode)
                {
                    await _auditLogService.LogAudit("Loan Topup",
                        "Applied for Executive Loan Product", "Accounts", "NA, readonly request", CurrentUser);
                    showPopup = true;
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
                    SubmitButtonText = "Submit";
                    StateHasChanged();
                }
            }

            guarantorComplete = true;
            StateHasChanged();
        }

        public async Task LoadDropDown()
        {
            combobox_preferredAccounts =
                $"{Config.ODATA_VIEWS_HOST}/{nameof(CustomerBankAccountMasterView)}?$filter=CustomerId eq '{CustomerId}' and IsDeleted eq false";
            Logger.LogInformation($"url->{System.Text.Json.JsonSerializer.Serialize(combobox_preferredAccounts)}");
            combobox_preferredSpecialAccounts =
                $"{Config.ODATA_VIEWS_HOST}/{nameof(SpecialDepositAccountMasterView)}?$filter=CustomerId eq '{CustomerId}'";
        }

        async Task onCreate()
        {
            showCreateDrawer = true;
        }

        async Task onCreateDone()
        {
            showCreateDrawer = false;
        }

        public async Task GetProfile()
        {
            ApplicationUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrEmpty(ApplicationUserId))
            {
                _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
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
                }
            }
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

        private async Task Done()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("/account/loanproductsapplications", true);
            }
        }

        private async Task LoanTopUpDone()
        {
            showLoanTopPopup = false;
            StateHasChanged();
        }

        public void UpdateUI()
        {
            if (GuarantorMemberProfileMasterViewList.Count > 0)
                HasRecords = true;
            else
                HasRecords = false;
            StateHasChanged();
        }

        public void ShowSpecialDepositAccounts()
        {
            showSpecialDepositAccount = true;
            showPreferredAccount = false;
            StateHasChanged();
        }

        public void ShowDisbursementAccount(ChangeEventArgs args)
        {
            var valType = int.Parse(args.Value.ToString());
            if (valType == 1)
            {
                showSpecialDepositAccount = true;
                showPreferredAccount = false;
                StateHasChanged();
            }
            else
            {
                showSpecialDepositAccount = false;
                showPreferredAccount = true;
                StateHasChanged();
            }
        }

        public void ShowPreferredAccounts()
        {
            showSpecialDepositAccount = false;
            showPreferredAccount = true;
            StateHasChanged();
        }

        private void SpecialBankValueChangeHandler(ChangeEventArgs<string, SpecialDepositAccountMasterView> args)
        {
            Model.SpecialDepositId = args.Value;
            Model.DestinationAccountId = null;
            Model.UseSpecialDeposit = true;
            Model.DestinationType = TopupFundingSourceType.SPECIAL_DEPOSIT_ACCOUNT;
        }

        private void BankValueChangeHandler(ChangeEventArgs<string, CustomerBankAccountMasterView> args)
        {
            Model.CustomerBankAccountId = args.Value;
            Model.SpecialDepositId = null;
            Model.UseSpecialDeposit = false;
            Model.DestinationType = TopupFundingSourceType.EXISTING_BANK_ACCOUNT;
        }

        private async Task CheckTopUpAmountWithLoanAmount(ChangeEventArgs args)
        {
            var topUpvalue = decimal.Parse(args.Value.ToString());
            remainingBalance = SelectedLoanProduct.PrincipalMaxLimit - Model.PrincipalBalance;
            if (topUpvalue > remainingBalance)
            {
                showLoanTopPopup = true;
                StateHasChanged();
            }
        }

        public async Task GetRepaymentPlan()
        {
            if (SelectedLoanProduct != null && SelectedLoanProduct.Id != null)
            {
                Tenure enumValue = (Tenure)System.Enum.Parse(typeof(Tenure), Model.TenureUnit, true);
                GenerateScheduleCommand generateScheduleCommand = new GenerateScheduleCommand()
                {
                    Principal = Model.TopUpAmount + Model.PrincipalBalance,
                    CommencementDate = Model.CommencementDate,
                    InterestRate = (decimal)Model.InterestRate,
                    TenureValue = Model.TenureValue,
                    TenureUnit = enumValue,
                    CustomerId = CustomerId,
                    LoanProductId = SelectedLoanProduct.Id,
                    DaysInYear = SelectedLoanProduct.DaysInYear,
                    InterestCalculationMethod = SelectedLoanProduct.InterestCalculationMethod != null ? (InterestCalculationMethod)System.Enum.Parse(typeof(InterestCalculationMethod), SelectedLoanProduct.InterestCalculationMethod, true) : InterestCalculationMethod.FLAT_RATE,

                    InterestMethod = SelectedLoanProduct.InterestMethod != null ? (InterestMethod)System.Enum.Parse(typeof(InterestMethod), SelectedLoanProduct.InterestMethod, true) : InterestMethod.SIMPLE,
                    RepaymentPeriod = enumValue,
                    CompoundingPeriod = Tenure.NONE


                };



                Logger.LogInformation(
                    $"rsp payload->{System.Text.Json.JsonSerializer.Serialize(generateScheduleCommand)}");
                //var rsp = await DataService
                //    .Create<CreateLoanApplicationScheduleCommand,
                //        CommandResult<List<LoanApplicationScheduleViewModel>>>(
                //        nameof(LoanApplicationSchedule), createLoanApplicationScheduleCommand);

                var rsp = await DataService
                   .PostCommand<GenerateScheduleCommand, CommandResult<GenerateScheduleViewModel>>(
                       nameof(LoanApplication), "generateSchedule", generateScheduleCommand);

                if (!rsp.IsSuccessStatusCode)
                {
                    if (!string.IsNullOrEmpty(rsp.Error.Content))
                    {
                        var rspContent =
                            System.Text.Json.JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                        var msg = rspContent?.Response;
                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
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
                    //Logger.LogInformation($"rsp content->{System.Text.Json.JsonSerializer.Serialize(rsp.Content)}");
                    //CommandResult<List<LoanApplicationScheduleViewModel>> rspResponse =
                    //    System.Text.Json.JsonSerializer
                    //        .Deserialize<CommandResult<List<LoanApplicationScheduleViewModel>>>(rsp.Content.ToJson());
                    //loanApplicationScheduleViewModels = new List<LoanApplicationScheduleViewModel>();
                    //if (rspResponse != null && rspResponse.Response != null && rspResponse.Response.Count > 0)
                    //    loanApplicationScheduleViewModels = rspResponse.Response;
                    //Logger.LogInformation($"rsp content->{System.Text.Json.JsonSerializer.Serialize(rspResponse)}");




                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(rsp.Content)}");
                    CommandResult<GenerateScheduleViewModel> rspResponse =
                        JsonSerializer.Deserialize<CommandResult<GenerateScheduleViewModel>>(rsp.Content.ToJson());
                    generateScheduleViewModel = new GenerateScheduleViewModel();
                    if (rspResponse != null && rspResponse.Response != null)
                        generateScheduleViewModel = rspResponse.Response;
                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(rspResponse)}");
                    if (!generateScheduleViewModel.IsApproved)
                    {
                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Info",
                            Description = "Customer not eligible for loan (NetPay)",
                            NotificationType = NotificationType.Info
                        });
                        _navigationManager.NavigateTo("/account/loanproductsapplications", true);
                    }
                }
            }
        }

        private void DeleteGuarantor(LoanTopUpGuarantorDTO record)
        {
            GuarantorMemberProfileMasterViewList.Remove(record);
            UpdateUI();
        }

        private async Task AddGuarantor()
        {
            bool canAdd = CanAddGuarantor();
            if (!canAdd)
            {
                string message = "You can not add more Retiree Guarantor";
                if (GuarantorMemberProfileMasterView.GuarantorType != null &&
                    GuarantorMemberProfileMasterView.GuarantorType.ToLower() == "regular")
                    message = "You can not add more Regular Guarantor";
                showPopup = false;
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = message,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                if (GuarantorMemberProfileMasterView != null && GuarantorMemberProfileMasterView.Id != null)
                {
                    MemberShipId = string.Empty;
                    var memberExists = GuarantorMemberProfileMasterViewList
                        .Where(f => f.Id == GuarantorMemberProfileMasterView.Id).FirstOrDefault();
                    if (memberExists == null)
                    {
                        GuarantorMemberProfileMasterViewList.Add(GuarantorMemberProfileMasterView);
                        GuarantorMemberProfileMasterView = new LoanTopUpGuarantorDTO();
                        showAddDrawer = false;
                        UpdateUI();
                    }
                    else
                    {
                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Info",
                            Description = "Guarantor already added",
                            NotificationType = NotificationType.Error
                        });
                    }
                }
            }
        }

        private async Task SearchMember()
        {
            if (!string.IsNullOrEmpty(MemberShipId))
            {
                guarantorSearchButton = "Searching...";
                StateHasChanged();
                VerifyLoanApplicationGuarantorCommand command = new VerifyLoanApplicationGuarantorCommand()
                {
                    MembershipId = MemberShipId.Trim(),
                    CommencementDate = Model.CommencementDate,
                    TenureInMonths = generateScheduleViewModel.Schedules.Count,

                };
                var rsp =
                    await DataService.VerifyGuarantor<CommandResult<VerifyLoanApplicationGuarantorViewModel>, VerifyLoanApplicationGuarantorCommand>(MemberShipId.Trim(), command);

                if (!rsp.IsSuccessStatusCode)
                {
                    guarantorSearchButton = "Search";
                    StateHasChanged();
                    if (!string.IsNullOrEmpty(rsp.Error.Content))
                    {
                        var rspContent =
                            System.Text.Json.JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                        var msg = rspContent?.Response;
                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
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
                    guarantorSearchButton = "Search";
                    StateHasChanged();
                    VerifyLoanApplicationGuarantorViewModel rspResponse =
                        System.Text.Json.JsonSerializer.Deserialize<VerifyLoanApplicationGuarantorViewModel>(
                            rsp.Content.ToJson());

                    if (rspResponse != null && !string.IsNullOrEmpty(rspResponse.GuarantorMembershipId) &&
                        rspResponse.GuarantorMembershipId == MemberShipId)
                    {
                        var eligibility = _utilityService.LoanGuarantorEligibilityTopUp(rspResponse, SelectedLoanProduct, Model.TopUpAmount);
                        if (eligibility == null)
                        {
                            GuarantorMemberProfileMasterView = new LoanTopUpGuarantorDTO();
                            GuarantorMemberProfileMasterView.Id = rspResponse.Id;
                            GuarantorMemberProfileMasterView.GuarantorType = rspResponse.GuarantorType;
                            GuarantorMemberProfileMasterView.GuarantorCustomerId = rspResponse.GuarantorCustomerId;
                            GuarantorMemberProfileMasterView.GuarantorMembershipId = rspResponse.GuarantorMembershipId;
                            GuarantorMemberProfileMasterView.FullName = rspResponse.FullName;
                            UpdateUI();
                        }
                        else
                        {
                            await notificationService.Open(new NotificationConfig()
                            {
                                Message = "Info",
                                Description = eligibility,
                                NotificationType = NotificationType.Info
                            });
                        }
                    }
                    else
                    {
                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Info",
                            Description = "Member not found",
                            NotificationType = NotificationType.Error
                        });
                    }
                }
            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = "Please, enter membership number of the guarantor to search",
                    NotificationType = NotificationType.Error
                });
            }
        }

        public bool CanAddGuarantor()
        {
            var countOfGuarantorType = GuarantorMemberProfileMasterViewList
                .Where(f => f.GuarantorType == GuarantorMemberProfileMasterView.GuarantorType).Count();
            if (countOfGuarantorType >= SelectedLoanProduct.EmployeeGuarantorCount &&
                GuarantorMemberProfileMasterView.GuarantorType != null &&
                GuarantorMemberProfileMasterView.GuarantorType.ToLower() == "regular")
                return false;
            else if (countOfGuarantorType >= SelectedLoanProduct.NonEmployeeGuarantorCount &&
                     GuarantorMemberProfileMasterView.GuarantorType != null &&
                     GuarantorMemberProfileMasterView.GuarantorType.ToLower() != "regular")
                return false;
            return true;
        }

        public void MapToCommand()
        {
            Command = new CreateLoanTopupCommand()
            {
                CommencementDate = Model.CommencementDate,
                LoanAccountId = Model.LoanAccountId,
                SpecialDepositAccountId = Model.SpecialDepositId,
                TopupAmount = Model.TopUpAmount,
                DestinationType = Model.DestinationType,
                InterestBalance = Model.InterestBalance,
                PrincipalBalance = Model.PrincipalBalance,
                CustomerBankAccountId = Model.CustomerBankAccountId,
                TopupDate = DateTime.Now,
                CreatedByUserId = ApplicationUserId
            };
            if (GuarantorMemberProfileMasterViewList.Count > 0)
            {
                Command.Guarantors = new List<GuarantorDetails>();
                foreach (var item in GuarantorMemberProfileMasterViewList)
                {
                    GuarantorDetails guarantorDetails = new GuarantorDetails()
                    {
                        GuarantorCustomerId = item.GuarantorCustomerId,
                        GuarantorType =
                            (GuarantorType)System.Enum.Parse(typeof(GuarantorType), item.GuarantorType, true),
                    };
                    Command.Guarantors.Add(guarantorDetails);
                }
            }
        }

        public string ValidateGuarantorCount()
        {
            var employeeGuarantor = GuarantorMemberProfileMasterViewList
                .Where(f => f.GuarantorType.ToLower() == "regular").Count();
            var nonEmployeeGuarantor = GuarantorMemberProfileMasterViewList
                .Where(f => f.GuarantorType.ToLower() != "regular").Count();
            if (employeeGuarantor != SelectedLoanProduct.EmployeeGuarantorCount)
                return "You have not met the allowed number of Regular guarantor for the loan product";
            if (nonEmployeeGuarantor != SelectedLoanProduct.NonEmployeeGuarantorCount)
                return "You have not met the allowed number of retiree guarantor for the loan product";
            return null;
        }


        private async Task GetLoanAccountDetails()
        {
            try
            {
                LoanAccountViewModel = new LoanAccountMasterView();

                var rsp = await DataService.GetOdataRecord<ODataResponse<LoanAccountMasterView>>(
             nameof(LoanAccountMasterView), "Id", loanAccountId);
                if (rsp.IsSuccessStatusCode)
                {
                    var rspResponse = rsp.Content.Data;
                    if (rspResponse?.Count > 0)
                    {
                        LoanAccountViewModel = rspResponse.FirstOrDefault();
                        StateHasChanged();
                    }
                }
            }
            catch (Exception exp)
            {

            }
        }

        private async Task GetLoanApplicationGuarantorDetails()
        {
            try
            {
                LoanApplicationGuarantorViewModel = new List<LoanApplicationGuarantorMasterView>();

                var rsp = await DataService.GetOdataRecord<ODataResponse<LoanApplicationGuarantorMasterView>>(
  nameof(LoanApplicationGuarantorMasterView), "LoanApplicationId", LoanAccountViewModel.LoanApplicationId);
                if (rsp.IsSuccessStatusCode)
                {
                    if (rsp.Content != null)
                    {
                        var rspResponse = rsp.Content.Data;
                        if (rspResponse?.Count > 0)
                        {
                            LoanApplicationGuarantorViewModel = rspResponse;
                            foreach (var item in LoanApplicationGuarantorViewModel)
                            {
                                LoanTopUpGuarantorDTO guarantor = new LoanTopUpGuarantorDTO()
                                {
                                    FullName = $"{item.GuarantorId_FirstName} {item.GuarantorId_MiddleName} {item.GuarantorId_LastName}",
                                    GuarantorCustomerId = item.GuarantorId,
                                    GuarantorMembershipId = item.GuarantorId_MemberId,
                                    GuarantorType = item.GuarantorType,
                                    Id = item.Id
                                };
                                GuarantorMemberProfileMasterViewList.Add(guarantor);
                            }
                            HasRecords = true;
                            StateHasChanged();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
            }
        }

        private async Task GetLoanProduct()
        {
            try
            {
                LoanProductMasterViewModel = new List<LoanProductMasterView>();
                SelectedLoanProduct = new LoanProductMasterView();


                var rsp = await DataService.GetOdataRecord<ODataResponse<LoanProductMasterView>>(
          nameof(LoanProductMasterView), "Id", LoanAccountViewModel.LoanApplicationId_LoanProductId);
                if (rsp.IsSuccessStatusCode)
                {
                    var rspResponse = rsp.Content.Data;
                    if (rspResponse?.Count > 0)
                    {
                        SelectedLoanProduct = rspResponse.FirstOrDefault();
                        RepaymentDescription =
                    $"Repayment Period (Min:{SelectedLoanProduct.MinTenureValue}, Max:{SelectedLoanProduct.MaxTenureValue})";
                        AmountDescription =
                            $"Top Up Amount (Max:{SelectedLoanProduct.PrincipalMaxLimit.ToString("N2", new CultureInfo("en-US"))})";
                        Model.InterestRate = SelectedLoanProduct.InterestRate;
                        Model.TenureUnit = SelectedLoanProduct.TenureUnit;
                        if (!SelectedLoanProduct.IsGuarantorRequired)
                        {
                            HideGuarantorForm = true;
                        }
                        StateHasChanged();
                    }
                }

            }
            catch (Exception exp)
            {

            }
        }
        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "gridrepayment_excelexport") //Id is combination of Grid's ID and itemname
            {
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Repayment_Schedule.xlsx";
                await this.grid.ExportToExcelAsync(ExportProperties);
            }
        }
    }
}