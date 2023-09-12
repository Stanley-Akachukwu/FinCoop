using AntDesign;
using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
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

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount.ApplianceLoanApplication
{
    public partial class ApplianceLoanApplication
    {
        private FluentValidationValidator? _fluentValidationValidator_Info;
        private FluentValidationValidator? _fluentValidationValidator_ItemDetail;
        private Query Query_Combo;
        bool showPopup = false;

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        ILogger<ApplianceLoanApplication> Logger { get; set; }

        bool showInfoComponent { get; set; } = false;
        bool showRepaymentComponent { get; set; } = false;
        bool showGuarantorComponent { get; set; } = false;
        bool showDetailComponent { get; set; } = false;
        bool infoComplete { get; set; } = false;
        bool repaymentComplete { get; set; } = false;
        bool guarantorComplete { get; set; } = false;
        bool detailComplete { get; set; } = false;
        bool showAddDrawer { get; set; } = false;
        bool showAddItemDrawer { get; set; } = false;
        bool showSpecialDepositAccount { get; set; } = false;
        bool showPreferredAccount { get; set; } = false;
        Drawer addItemDrawer;
        Drawer addDrawer;
        BrowserDimension BrowserDimension;
        private bool HasRecords { get; set; } = false;
        private bool HasApplianceRecords { get; set; } = false;

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
        string combobox_preferredSpecialAccounts;

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
        public ApplianceDetailDTO ApplianceDetail { get; set; }
        public List<ApplianceDetailDTO> Appliances { get; set; }
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
        string LoanAmountDescription { get; set; } = "";
        string GuarantorDescription { get; set; } = "";
        [Inject] IClientAuditService _auditLogService { get; set; }
        [Inject]
        public ILoanHelperService _loanHelperService { get; set; }
        public GenerateScheduleViewModel generateScheduleViewModel { get; set; }
        public LoanProductViewModel SelectedLoanProduct { get; set; }
        public CreateLoanApplicationCommand Command { get; set; }
        public CustomerMasterView CustomerMasterView { get; set; }
        public string CustomerId { get; set; }
        [Inject] IUtilityService _utilityService { get; set; }
        SfGrid<AmortizationSchedule> grid;
        bool HideGuarantorForm { get; set; } = false;
        bool ShowPsuedoSubmitButton { get; set; } = false;
        bool showMessagePopup { get; set; } = false;
        public CommonResponseDTO CommonResponseDTO { get; set; }
        bool showCustomerEligibilityPopup { get; set; } = false;
        string EligibilityMessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            MemberProfile = new MemberProfileMasterView();
            Model = new LoanApplicationDTO();
            Command = new CreateLoanApplicationCommand();
            ApplianceDetail = new ApplianceDetailDTO();
            Appliances = new List<ApplianceDetailDTO>();
            CustomerMasterView = new CustomerMasterView();
            GuarantorMemberProfileMasterView = new VerifyLoanApplicationGuarantorViewModel();
            GuarantorMemberProfileMasterViewList = new List<VerifyLoanApplicationGuarantorViewModel>();
            generateScheduleViewModel = new GenerateScheduleViewModel();
            generateScheduleViewModel.Schedules = new List<AmortizationSchedule>();
            SelectedLoanProduct = new LoanProductViewModel();
            Model.RepaymentCommencementDate = _utilityService.RepaymentCommencementDate();
            showInfoComponent = true;
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
            if (addItemDrawer != null)
                addItemDrawer.Width = (int)(BrowserDimension.Width * 0.50);
            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private async Task ProceedToDetail()
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

                    LoanAmountDescription = $"Applied loan amount is {Model.Amount}";
                    showRepaymentComponent = false;
                    showDetailComponent = true;
                    showGuarantorComponent = false;
                    showInfoComponent = false;
                    infoComplete = true;
                    detailComplete = false;
                    repaymentComplete = false;
                    guarantorComplete = false;
                    StateHasChanged();
                    await GetRepaymentPlan();
                }
            }
        }

        private async Task ProceedToRepayment()
        {
            if (Appliances.Count > 0)
            {
                showRepaymentComponent = true;
                showDetailComponent = false;
                showGuarantorComponent = false;
                showInfoComponent = false;
                infoComplete = true;
                detailComplete = true;
                repaymentComplete = false;
                guarantorComplete = false;
                if (!generateScheduleViewModel.Schedules.Any())
                {
                    await GetRepaymentPlan();
                }

                StateHasChanged();
            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = "Please, add item to continue",
                    NotificationType = NotificationType.Info
                });
                return;
            }
        }

        private async Task ProceedToGuarantor()
        {
            showRepaymentComponent = false;
            showDetailComponent = false;
            showGuarantorComponent = true;
            showInfoComponent = false;
            infoComplete = true;
            detailComplete = true;
            repaymentComplete = true;
            guarantorComplete = false;
            StateHasChanged();
        }

        private async Task BackToInfo()
        {
            showDetailComponent = false;
            showGuarantorComponent = false;
            showRepaymentComponent = false;
            showInfoComponent = true;
            infoComplete = false;
            StateHasChanged();
        }

        private async Task BackToDetail()
        {
            showRepaymentComponent = false;
            showDetailComponent = true;
            showGuarantorComponent = false;
            showInfoComponent = false;
            detailComplete = false;
            StateHasChanged();
        }

        private async Task BackToRepayment()
        {
            showDetailComponent = false;
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
                SubmitButtonText = "Submitting ...";
                StateHasChanged();
                Logger.LogInformation($"rsp submitpayload->{JsonSerializer.Serialize(Command)}");
                var rsp = await DataService
                    .Create<CreateLoanApplicationCommand, CommandResult<LoanApplicationViewModel>>(
                        nameof(LoanApplication), Command);
                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(Command)}");
                if (rsp.IsSuccessStatusCode)
                {
                    await _auditLogService.LogAudit("Applied for Car Loan Product", "Applied for Car Loan Product",
                        "Accounts", "NA, readonly request", CurrentUser);
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
                $"{Config.API_HOST}/{nameof(LoanProduct)}/{nameof(LoanProductType.HOUSE_APPLIANCE_LOAN)}/userProducts/{ApplicationUserId}";
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
                    _navigationManager.NavigateTo($"/account/homeapplianceloanapplication", true);
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
            _navigationManager.NavigateTo($"/account/homeapplianceloanapplication", true);
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
                    MemberShipId = string.Empty;
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

            Command.Items = new List<ApplianceDetails>();
            foreach (var appliance in Appliances)
            {
                ApplianceDetails applianceDetails = new ApplianceDetails()
                {
                    Amount = appliance.Amount,
                    BrandName = appliance.BrandName,
                    Color = appliance.Color,
                    ItemType = appliance.ItemType,
                    Model = appliance.Model,
                    Name = appliance.Name,
                };
                Command.Items.Add(applianceDetails);
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

        async Task ShowItemDrawer()
        {
            showAddItemDrawer = true;
        }

        private void DeleteItem(ApplianceDetailDTO record)
        {
            Appliances.Remove(record);
            UpdateApplianceUI();
        }

        public void UpdateApplianceUI()
        {
            if (Appliances.Count > 0)
                HasApplianceRecords = true;
            else
                HasApplianceRecords = false;
            StateHasChanged();
        }

        async Task onAddItemDone()
        {
            showAddItemDrawer = false;
        }

        private async Task AddItem()
        {
            if (await _fluentValidationValidator_ItemDetail.ValidateAsync())
            {
                if (Appliances != null && Appliances.Count > 5)
                {
                    showPopup = false;
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Info",
                        Description = "You can not add more than 5 Items",
                        NotificationType = NotificationType.Error
                    });
                }
                else
                {
                    var itemExists = Appliances.Where(f =>
                        f.Name.ToLower() == ApplianceDetail.Name.ToLower() &&
                        f.BrandName.ToLower() == ApplianceDetail.BrandName.ToLower() &&
                        f.Model.ToLower() == ApplianceDetail.Model.ToLower()).FirstOrDefault();
                    if (itemExists == null)
                    {
                        var total = Appliances.Sum(f => f.Amount) + ApplianceDetail.Amount;
                        if (total > Model.Amount)
                        {
                            await notificationService.Open(new NotificationConfig()
                            {
                                Message = "Info",
                                Description = "Cost amount of items is more than the applied loan Amount",
                                NotificationType = NotificationType.Info
                            });
                            return;
                        }

                        Appliances.Add(ApplianceDetail);
                        ApplianceDetail = new ApplianceDetailDTO();
                        showAddItemDrawer = false;
                        UpdateApplianceUI();
                    }
                    else
                    {
                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Info",
                            Description = "Item already added",
                            NotificationType = NotificationType.Error
                        });
                    }
                }
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