using AntDesign;
using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.AppDomain.Loans.LoanApplicationSchedules;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanApplication;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using ChevronCoop.Web.AppUI.BlazorServer.Services.LoanProducts.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount.ExecutiveLoanApplication
{
    public partial class ExecutiveLoanApplication
    {
        private FluentValidationValidator? _fluentValidationValidator_Info;
        private Query Query_Combo;
        bool showPopup = false;

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        ILogger<ExecutiveLoanApplication> Logger { get; set; }
        [Inject]
        public ILoanHelperService _loanHelperService { get; set; }

        bool showInfoComponent { get; set; } = false;
        bool showRepaymentComponent { get; set; } = false;
        bool showGuarantorComponent { get; set; } = false;
        bool infoComplete { get; set; } = false;
        bool repaymentComplete { get; set; } = false;
        bool guarantorComplete { get; set; } = false;
        bool showAddDrawer { get; set; } = false;
        bool showSpecialDepositAccount { get; set; } = false;
        bool showPreferredAccount { get; set; } = false;

        Drawer addDrawer;
        BrowserDimension BrowserDimension;
        private bool HasRecords { get; set; } = false;

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }


        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        public LoanApplicationDTO Model { get; set; }
        string combobox_loanproducts;
        string combobox_preferredAccounts;

        [Parameter]
        public EventCallback<LoanApplicationDTO> ModelChanged { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        string ApplicationUserId { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        MemberProfileMasterView MemberProfile { get; set; }
        string AccountIsProvided { get; set; }
        string MemberShipId { get; set; }
        public VerifyLoanApplicationGuarantorViewModel GuarantorMemberProfileMasterView { get; set; }
        public List<VerifyLoanApplicationGuarantorViewModel> GuarantorMemberProfileMasterViewList { get; set; }
        bool IsRegularMember { get; set; } = false;
        string guarantorSearchButton = "Search";
        string SubmitButtonText = "Submit";
        decimal? Interest { get; set; }
        string TenorUnit { get; set; }
        string RepaymentDescription { get; set; } = "Repayment Period";
        string AmountDescription { get; set; } = "Loan Amount";
        string GuarantorDescription { get; set; } = "";
        [Inject] IClientAuditService _auditLogService { get; set; }
        public GenerateScheduleViewModel generateScheduleViewModel { get; set; }
        public LoanProductViewModel SelectedLoanProduct { get; set; }
        public CreateLoanApplicationCommand Command { get; set; }

        [Parameter]
        public string loantype { get; set; }

        public string loanProductType { get; set; }
        public string loanLabel { get; set; }
        public List<LoanProductViewModel> LoanProductViewModel { get; set; }
        public CustomerMasterView CustomerMasterView { get; set; }
        [Inject] IUtilityService _utilityService { get; set; }
        public string CustomerId { get; set; }
        string combobox_preferredSpecialAccounts;
        public LoanProductType LoanProductTypeEnum { get; set; }
        public LoanApplicationScheduleViewModel[] loanApplicationScheduleViewModelArray { get; set; }
        SfGrid<AmortizationSchedule> grid;
        bool HideGuarantorForm { get; set; } = false;
        bool ShowPsuedoSubmitButton { get; set; } = false;
        bool showMessagePopup { get; set; } = false;
        bool showCustomerEligibilityPopup { get; set; } = false;
        public CommonResponseDTO CommonResponseDTO { get; set; }
        string EligibilityMessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            MemberProfile = new MemberProfileMasterView();
            Model = new LoanApplicationDTO();
            Command = new CreateLoanApplicationCommand();
            CustomerMasterView = new CustomerMasterView();
            GuarantorMemberProfileMasterView = new VerifyLoanApplicationGuarantorViewModel();
            GuarantorMemberProfileMasterViewList = new List<VerifyLoanApplicationGuarantorViewModel>();
            generateScheduleViewModel = new GenerateScheduleViewModel();
            generateScheduleViewModel.Schedules = new List<AmortizationSchedule>();

            SelectedLoanProduct = new LoanProductViewModel();
            showInfoComponent = true;
            LoanProductViewModel = new List<LoanProductViewModel>();
            Model.RepaymentCommencementDate = _utilityService.RepaymentCommencementDate();
            if (loantype == "executive")
            {
                loanProductType = nameof(LoanProductType.EXECUTIVE_LOAN);
                loanLabel = "Executive";
            }
            else if (loantype == "short")
            {
                loanProductType = nameof(LoanProductType.SHORT_TERM_LOAN);
                loanLabel = "Short-Term";
            }
            else if (loantype == "long")
            {
                loanProductType = nameof(LoanProductType.LONG_TERM_LOAN);
                loanLabel = "Long-Term";
            }
            else if (loantype == "target")
            {
                loanProductType = nameof(LoanProductType.TARGET_LOAN);
                loanLabel = "Target";
            }

            await GetCurrentUser();
            await GetProfile();
            LoadDropDown();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();
            }

            if (addDrawer != null)
                addDrawer.Width = (int)(BrowserDimension.Width * 0.50);
            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private async Task ProceedToRepayment()
        {
            if (await _fluentValidationValidator_Info.ValidateAsync())
            {
                if (SelectedLoanProduct != null && SelectedLoanProduct.Id != null)
                {
                    if (SelectedLoanProduct.MinTenureValue > Model.RepaymentPeriod ||
                        SelectedLoanProduct.MaxTenureValue < Model.RepaymentPeriod)
                    {
                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Info",
                            Description = "Please, the entered Repayment period is not allowed",
                            NotificationType = NotificationType.Info
                        });
                        return;
                    }

                    if (SelectedLoanProduct.PrincipalMinLimit > Model.Amount ||
                        SelectedLoanProduct.PrincipalMaxLimit < Model.Amount)
                    {
                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Info",
                            Description = "Please, the entered Amount allowed",
                            NotificationType = NotificationType.Info
                        });
                        return;
                    }


                    showRepaymentComponent = true;
                    showGuarantorComponent = false;
                    showInfoComponent = false;
                    infoComplete = true;
                    repaymentComplete = false;
                    guarantorComplete = false;
                    StateHasChanged();
                    await GetRepaymentPlan();
                }
            }
        }

        private async Task ProceedToGuarantor()
        {
            showRepaymentComponent = false;
            showGuarantorComponent = true;
            showInfoComponent = false;
            infoComplete = true;
            repaymentComplete = true;
            guarantorComplete = false;
            StateHasChanged();
        }

        private async Task BackToInfo()
        {
            showGuarantorComponent = false;
            showRepaymentComponent = false;
            showInfoComponent = true;
            infoComplete = false;
            StateHasChanged();
        }

        private async Task BackToRepayment()
        {
            showRepaymentComponent = true;
            showGuarantorComponent = false;
            showInfoComponent = false;
            repaymentComplete = false;
            StateHasChanged();
        }

        private async Task Submit()
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
                Logger.LogInformation($"rsp submitpayload->{JsonSerializer.Serialize(Command)}");
                var rsp = await DataService
                    .Create<CreateLoanApplicationCommand, CommandResult<LoanApplicationViewModel>>(
                        nameof(LoanApplication), Command);
                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(Command)}");
                if (rsp.IsSuccessStatusCode)
                {
                    await _auditLogService.LogAudit("Applied for Executive Loan Product",
                        "Applied for Executive Loan Product", "Accounts", "NA, readonly request", CurrentUser);
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
                    SubmitButtonText = "Submit";
                    StateHasChanged();
                }
            }

            StateHasChanged();
        }

        async Task ShowGuarantorDrawer()
        {
            showAddDrawer = true;
        }

        async Task onAddDone()
        {
            showAddDrawer = false;
        }

        async Task OnclodeModal()
        {
            showPopup = false;
        }

        public async Task LoadDropDown()
        {
            combobox_loanproducts =
                $"{Config.API_HOST}/{nameof(LoanProduct)}/{loanProductType}/userProducts/{ApplicationUserId}";
            combobox_preferredAccounts =
                $"{Config.ODATA_VIEWS_HOST}/{nameof(CustomerBankAccountMasterView)}?$filter=CustomerId eq '{CustomerId}' and IsDeleted eq false";
            Logger.LogInformation($"url->{JsonSerializer.Serialize(combobox_preferredAccounts)}");
            combobox_preferredSpecialAccounts =
                $"{Config.ODATA_VIEWS_HOST}/{nameof(SpecialDepositAccountMasterView)}?$filter=CustomerId eq '{CustomerId}'";
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task GetProfile()
        {
            ApplicationUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrEmpty(ApplicationUserId))
            {
                _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
            }

            var rsp = await DataService.GetValue<List<CustomerMasterView>>(
                nameof(CustomerMasterView), ApplicationUserId);


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
                    CustomerMasterView = new CustomerMasterView();
                    CustomerMasterView = rspResponse.FirstOrDefault();
                    CustomerMasterView = Mapper.Map<CustomerMasterView>(rspResponse.FirstOrDefault());
                    ;
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

        public void ShowSpecialDepositAccounts()
        {
            showSpecialDepositAccount = true;
            showPreferredAccount = false;
            AccountIsProvided = "Product";
            StateHasChanged();
        }

        public void ShowPreferredAccounts()
        {
            showSpecialDepositAccount = false;
            showPreferredAccount = true;
            AccountIsProvided = "Bank";
            StateHasChanged();
        }

        private async Task ValueChangeHandler(ChangeEventArgs<string, LoanProductViewModel> args)
        {
            Model.LoanProductId = args.Value;
            SelectedLoanProduct = new LoanProductViewModel();
            SelectedLoanProduct = args.ItemData;
            if (SelectedLoanProduct != null)
            {
                CommonResponseDTO = await _loanHelperService.CheckCustomerLoanEligibility(SelectedLoanProduct.Id, CustomerId);
                if (!CommonResponseDTO.IsEligible && !CommonResponseDTO.IsError)
                {
                    showMessagePopup = true;
                    StateHasChanged();
                    return;
                }
                else if (CommonResponseDTO.IsError)
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = CommonResponseDTO.Message,
                        NotificationType = NotificationType.Error
                    });
                    _navigationManager.NavigateTo($"/account/LoanApplication/{loantype}", true);
                    //return;
                }
                else if (CommonResponseDTO.IsEligible && !CommonResponseDTO.IsError)
                    PopulateSelectedProductDetail();
            }
        }
        public void PopulateSelectedProductDetail()
        {
            showMessagePopup = false;
            StateHasChanged();
            if (SelectedLoanProduct != null)
            {

                RepaymentDescription =
                    $"Repayment Period (Min:{SelectedLoanProduct.MinTenureValue}, Max:{SelectedLoanProduct.MaxTenureValue})";
                AmountDescription =
                    $"Loan Amount (Min:{SelectedLoanProduct.PrincipalMinLimit.ToString("N2", new CultureInfo("en-US"))}, Max:{SelectedLoanProduct.PrincipalMaxLimit.ToString("N2", new CultureInfo("en-US"))})";
                Interest = SelectedLoanProduct.InterestRate;
                Model.RepaymentTenureUnit =
                    (Tenure)System.Enum.Parse(typeof(Tenure), SelectedLoanProduct.TenureUnit, true);

                if (SelectedLoanProduct.IsGuarantorRequired == true)
                {
                    GuarantorDescription =
                        $"{SelectedLoanProduct.EmployeeGuarantorCount} Regular Member , {SelectedLoanProduct.NonEmployeeGuarantorCount} Retired Member";
                    HideGuarantorForm = false;
                    ShowPsuedoSubmitButton = false;
                }
                else
                {
                    GuarantorDescription = string.Empty;
                    HideGuarantorForm = true;
                    ShowPsuedoSubmitButton = true;
                    StateHasChanged();
                }

            }
            else
            {
                RepaymentDescription = $"Repayment Period";
                Interest = null;
                AmountDescription = $"Loan Amount";
                Model.RepaymentTenureUnit = new Tenure();
                StateHasChanged();
            }
        }
        public void DeclineWaiverCharge()
        {
            _navigationManager.NavigateTo($"/account/LoanApplication/{loantype}", true);
        }

        private void BankValueChangeHandler(ChangeEventArgs<string, CustomerBankAccountMasterView> args)
        {
            Model.DestinationAccountId = args.Value;
            Model.SpecialDepositId = null;
            Model.UseSpecialDeposit = false;
        }

        private void SpecialBankValueChangeHandler(ChangeEventArgs<string, SpecialDepositAccountMasterView> args)
        {
            Model.SpecialDepositId = args.Value;
            Model.DestinationAccountId = null;
            Model.UseSpecialDeposit = true;
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
                    MemberShipId = null;
                    var memberExists = GuarantorMemberProfileMasterViewList
                        .Where(f => f.Id == GuarantorMemberProfileMasterView.Id).FirstOrDefault();
                    if (memberExists == null)
                    {
                        GuarantorMemberProfileMasterViewList.Add(GuarantorMemberProfileMasterView);
                        GuarantorMemberProfileMasterView = new VerifyLoanApplicationGuarantorViewModel();
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
                if (CustomerMasterView.MemberId != null && CustomerMasterView.MemberId.Trim() == MemberShipId.Trim())
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Info",
                        Description = "Please, you cannot use yourself as guarantor!",
                        NotificationType = NotificationType.Error
                    });

                }
                else
                {
                    guarantorSearchButton = "Searching...";
                    StateHasChanged();
                    VerifyLoanApplicationGuarantorCommand command = new VerifyLoanApplicationGuarantorCommand()
                    {
                        MembershipId = MemberShipId.Trim(),
                        CommencementDate = Model.RepaymentCommencementDate,
                        TenureInMonths = generateScheduleViewModel.Schedules.Count,

                    };
                    var rsp =
                        await DataService.VerifyGuarantor<CommandResult<VerifyLoanApplicationGuarantorViewModel>, VerifyLoanApplicationGuarantorCommand>(MemberShipId.Trim(), command);
                    Logger.LogInformation($"rsp verifyguarantor->{JsonSerializer.Serialize(command)}");

                    if (!rsp.IsSuccessStatusCode)
                    {
                        guarantorSearchButton = "Search";
                        StateHasChanged();
                        if (!string.IsNullOrEmpty(rsp.Error.Content))
                        {
                            var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

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
                            JsonSerializer.Deserialize<VerifyLoanApplicationGuarantorViewModel>(rsp.Content.Response.ToJson());
                        if (rspResponse != null && !string.IsNullOrEmpty(rspResponse.GuarantorMembershipId) &&
                            rspResponse.GuarantorMembershipId == MemberShipId)
                        {
                            var eligibility = _utilityService.LoanGuarantorEligibility(rspResponse, SelectedLoanProduct, Model.Amount);
                            if (eligibility == null)
                            {
                                GuarantorMemberProfileMasterView = new VerifyLoanApplicationGuarantorViewModel();
                                GuarantorMemberProfileMasterView = rspResponse;
                                GuarantorMemberProfileMasterView =
                                    Mapper.Map<VerifyLoanApplicationGuarantorViewModel>(rspResponse);
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

        private void DeleteGuarantor(VerifyLoanApplicationGuarantorViewModel record)
        {
            GuarantorMemberProfileMasterViewList.Remove(record);
            UpdateUI();
        }

        public void UpdateUI()
        {
            if (GuarantorMemberProfileMasterViewList.Count > 0)
                HasRecords = true;
            else
                HasRecords = false;
            StateHasChanged();
        }

        public void MapToCommand()
        {
            Command = new CreateLoanApplicationCommand()
            {
                Amount = Model.Amount,
                RepaymentCommencementDate = Model.RepaymentCommencementDate,
                LoanProductId = Model.LoanProductId,
                TenureValue = Model.RepaymentPeriod,
                TenureUnit = Model.RepaymentTenureUnit,
                CustomerId = CustomerId,
                DestinationAccountId = Model.DestinationAccountId,
                UseSpecialDeposit = Model.UseSpecialDeposit,
                SpecialDepositId = Model.SpecialDepositId,
                ApplicationUserId = ApplicationUserId
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

        public async Task GetRepaymentPlan()
        {
            if (SelectedLoanProduct != null && SelectedLoanProduct.Id != null)
            {
                GenerateScheduleCommand generateScheduleCommand = new GenerateScheduleCommand()
                {
                    Principal = Model.Amount,
                    CommencementDate = Model.RepaymentCommencementDate,
                    InterestRate = (decimal)Interest,
                    TenureValue = Model.RepaymentPeriod,
                    TenureUnit = Model.RepaymentTenureUnit,
                    CustomerId = CustomerId,
                    LoanProductId = SelectedLoanProduct.Id,
                    DaysInYear = SelectedLoanProduct.DaysInYear,
                    InterestCalculationMethod = SelectedLoanProduct.InterestCalculationMethod != null ? (InterestCalculationMethod)System.Enum.Parse(typeof(InterestCalculationMethod), SelectedLoanProduct.InterestCalculationMethod, true) : InterestCalculationMethod.FLAT_RATE,

                    InterestMethod = SelectedLoanProduct.InterestMethod != null ? (InterestMethod)System.Enum.Parse(typeof(InterestMethod), SelectedLoanProduct.InterestMethod, true) : InterestMethod.SIMPLE,
                    RepaymentPeriod = Model.RepaymentTenureUnit,
                    CompoundingPeriod = Tenure.NONE
                };
                Logger.LogInformation($"rsp payload->{JsonSerializer.Serialize(generateScheduleCommand)}");
                var rsp = await DataService
                    .PostCommand<GenerateScheduleCommand, CommandResult<GenerateScheduleViewModel>>(
                        nameof(LoanApplication), "generateSchedule", generateScheduleCommand);

                //Logger.LogInformation($"rsp->{JsonSerializer.Serialize(rsp)}");
                if (!rsp.IsSuccessStatusCode)
                {
                    if (!string.IsNullOrEmpty(rsp.Error.Content))
                    {
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

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

        private async Task Done()
        {
            showPopup = false;
            await InvokeAsync(StateHasChanged);
            _navigationManager.NavigateTo("/account/loanproductsapplications", true);
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

        public string ValidateGuarantorCount()
        {
            var employeeGuarantor = GuarantorMemberProfileMasterViewList.Where(f =>
                f.GuarantorType.ToLower() == "regular" || f.GuarantorType.ToLower() == "employee").Count();
            var nonEmployeeGuarantor = GuarantorMemberProfileMasterViewList.Where(f =>
                f.GuarantorType.ToLower() != "regular" && f.GuarantorType.ToLower() != "employee").Count();
            if (employeeGuarantor != SelectedLoanProduct.EmployeeGuarantorCount)
                return "You have not met the allowed number of Regular guarantor for the loan product";
            if (nonEmployeeGuarantor != SelectedLoanProduct.NonEmployeeGuarantorCount)
                return "You have not met the allowed number of retiree guarantor for the loan product";
            return null;
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
        protected async Task HandleOnChange(ChangeEventArgs args)
        {
            if (SelectedLoanProduct != null && !string.IsNullOrEmpty(SelectedLoanProduct.Id) && Model.Amount > 0)
            {
                await LoanEligibility();
            }
        }
        public async Task LoanEligibility()
        {
            if (SelectedLoanProduct != null && SelectedLoanProduct.Id != null)
            {
                LoanApplicationEligibilityCommand loanApplicationEligibilityCommand = new LoanApplicationEligibilityCommand()
                {
                    Amount = Model.Amount,
                    CustomerId = CustomerId,
                    LoanProductId = SelectedLoanProduct.Id
                };
                Logger.LogInformation($"rsp payload->{JsonSerializer.Serialize(loanApplicationEligibilityCommand)}");
                var rsp = await DataService
                    .PostCommand<LoanApplicationEligibilityCommand, CommandResult<LoanApplicationEligibilityViewModel>>(
                        nameof(LoanApplication), "checkEligibility", loanApplicationEligibilityCommand);
                if (!rsp.IsSuccessStatusCode)
                {
                    if (!string.IsNullOrEmpty(rsp.Error.Content))
                    {
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

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
                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(rsp.Content)}");
                    CommandResult<LoanApplicationEligibilityViewModel> rspResponse =
                        JsonSerializer.Deserialize<CommandResult<LoanApplicationEligibilityViewModel>>(rsp.Content.ToJson());

                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(rspResponse)}");
                    if (rspResponse.Response == null)
                    {
                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Info",
                            Description = "Your eligibility cannot be ascertained",
                            NotificationType = NotificationType.Info
                        });
                        _navigationManager.NavigateTo("/account/loanproductApplication", true);
                    }
                    if (rspResponse.Response != null && !rspResponse.Response.IsEligible)
                    {
                        EligibilityMessage = rspResponse.Response.Reason;
                        showCustomerEligibilityPopup = true;
                        StateHasChanged();
                    }

                }
            }
        }

        public void OnModalClose()
        {
            _navigationManager.NavigateTo("/account/loanproductApplication", true);
        }
    }
}