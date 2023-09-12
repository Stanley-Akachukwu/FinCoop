using AntDesign;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.MasterData.Charges;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Users;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.DropDowns;
using System.Security.Claims;
using Syncfusion.Blazor.Data;
using System.Text.Json;
using System;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductCharges;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductInterestRanges;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup.DetailsComp
{
    public partial class EditProduct
    {
        [Parameter] public DepositProductMasterView EditModel { get; set; }

        [Parameter] public DepositProduct DepositProductModel { get; set; } = new DepositProduct();

        private FluentValidationValidator? _fluentValidationValidator;
        private FluentValidationValidator? _fluentValidationValidatorRate;
        private Query Query_Combo;
        bool showPopup = false;

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        ILogger<CreateProductSetupForm> Logger { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        Drawer createDrawer { get; set; } = new Drawer();

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        bool showInterestConfiguration { get; set; } = false;
        bool showApprovalInfo { get; set; } = false;
        bool showBasicInformation { get; set; } = false;
        bool showInterestGrid { get; set; } = false;

        bool disableFalse { get; set; } = false;

        bool showAddRateDrawer { get; set; } = false;
        public UpdateDepositProductCommand UpdateDepositProductCommand { get; set; }
        public List<CreateDepositProductChargeCommand> ChargesCommand { get; set; }
        public List<CreateDepositProductInterestRangeCommand> InterestRateCommand { get; set; }
        public List<CreateDepositProductInterestDTO> InterestRates { get; set; }


        public CreateDepositProductDTO Model { get; set; }

        [Parameter] public DepositProductMasterView UpdateModel { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Parameter]
        public EventCallback<CreateDepositProductDTO> ModelChanged { get; set; }

        bool BasicInfomationComplete { get; set; } = false;
        bool InterestComplete { get; set; } = false;
        public CreateDepositProductInterestDTO CreateInterestRate { get; set; }
        BrowserDimension BrowserDimension;
        string combobox_Currency_res;
        string combobox_Account_res;
        List<string> tenureUnit { get; set; } = new List<string>();

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject] IClientAuditService _auditLogService { get; set; }
        ClaimsPrincipal CurrentUser { get; set; }

        string DROPDOWN_API_RESOURCE_CHARGE_WORKFLOW =>
            $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalWorkflowMasterView)}";

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        public long TenureValueMax = 900000000;

        [Inject]
        private UserService _UserService { get; set; }

        public string ApplicationUserID { get; set; }

        public bool isChecked = false;

        [Inject]
        private IMasterViews _MasterView { get; set; }

        List<string> _DepositProductCharge = new List<string>();

        string[] _DepositProductChargeArray;

        List<ChargeMasterView> _chargeMasterViews = new List<ChargeMasterView>();

        protected override async Task OnInitializedAsync()
        {
            LoadDropDown();

            Model = new CreateDepositProductDTO();
            InterestRates = new List<CreateDepositProductInterestDTO>();
            //await base.OnInitializedAsync();
            showBasicInformation = true;
            CreateInterestRate = new CreateDepositProductInterestDTO();
            InterestRateCommand = new List<CreateDepositProductInterestRangeCommand>();

            var resp = await _MasterView.GetCustomMasterViewEntityWithFilterAndAllFields<DepositProduct>(nameof(DepositProduct), "id", EditModel.Id);

            await GetApprovalWorkFlow();

            await GetInterestRate();

            if (resp != null)
            {
               DepositProductModel = resp.ToList()[0];
            }
            

            await UpdateModelWithMasterView();
        }


        private async Task UpdateModelWithMasterView()
        {
            string productTypeString = EditModel.ProductType;

            var selectedProductType = new DepositProductType();

            var selectedTenure = new Tenure();

            showInterestGrid = EditModel.IsInterestEnabled;

            disableFalse = (showInterestGrid == false) ? true : false;
           

            foreach (DepositProductType pType in System.Enum.GetValues(typeof(DepositProductType)))
            {
                if(pType.ToString() == EditModel.ProductType)
                {
                    selectedProductType = pType;
                }
            }

            foreach (Tenure pType in System.Enum.GetValues(typeof(Tenure)))
            {
                if (pType.ToString() == EditModel.Tenure)
                {
                    selectedTenure = pType;
                }
            }


            foreach (DepositProductCharge charge in DepositProductModel.Charges)
            {
                _DepositProductCharge.Add(charge.ChargeId);
            }

            _DepositProductChargeArray = _DepositProductCharge.ToArray();

            //ChargesCommand

            Model = new CreateDepositProductDTO()
            {
                Code = EditModel.Code,
                Name = EditModel.Name,
                ShortName = EditModel.ShortName,
                // MinimumAge = EditModel.MinimumAge,
                // MaximumAge = EditModel.MaximumAge,
                Tenure = selectedTenure,
                TenureValue = EditModel.TenureValue,
                ProductType = selectedProductType,
                DefaultCurrencyId = EditModel.DefaultCurrencyId,
                BankDepositAccountId = EditModel.BankDepositAccountId,
                IsInterestEnabled = EditModel.IsInterestEnabled,
                Description = EditModel.Description,
                IsDefaultProduct = DepositProductModel.IsDefaultProduct,
                
            };
        }

        private async Task Proceed()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                BasicInfomationComplete = true;
                showInterestConfiguration = true;
                showBasicInformation = false;
                showApprovalInfo = false;
                StateHasChanged();
                MapToCommand();
            }
        }

        private async Task ProceedToApproval()
        {
            BasicInfomationComplete = true;
            InterestComplete = true;
            showInterestConfiguration = false;
            showBasicInformation = false;
            showApprovalInfo = true;
            StateHasChanged();
        }

        private async Task BackToInterestPage()
        {
            showInterestConfiguration = true;
            showBasicInformation = false;
            showApprovalInfo = false;
            StateHasChanged();
            MapToInterestRate();
        }

        private async Task BackToCreatePage()
        {
            showInterestConfiguration = false;
            showBasicInformation = true;
            showApprovalInfo = false;
            StateHasChanged();
        }

        private async Task onBack()
        {
            _navigationManager.NavigateTo("/ProductSetup/Manage/all", true);
        }

        private async Task EnableInterest()
        {
            showInterestGrid = true;
            disableFalse = false;
            StateHasChanged();
        }

        private async Task DisableInterest()
        {
            showInterestGrid = false;
            disableFalse = true;
            StateHasChanged();
        }

        string ApprovalWorkFlow = string.Empty;

        [Inject]
        IEntityDataService DataService { get; set; }

        string DROPDOWN_API_RESOURCE;
        string SubmitButtonText = "Submit";

        async Task onAddRateDone()
        {
            showAddRateDrawer = false;
        }

        async Task onShowAddInterestRateDone()
        {
            showAddRateDrawer = true;
        }

        private async Task Done()
        {
            showPopup = false;
            await InvokeAsync(StateHasChanged);
            _navigationManager.NavigateTo("/ProductSetup/Manage/all", true);
        }

        private void ValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            ChargesCommand = new List<CreateDepositProductChargeCommand>();
            var selectedValue = args.Value;
            if (selectedValue.Count() > 0)
            {
                foreach (var item in selectedValue)
                {
                    CreateDepositProductChargeCommand charges = new CreateDepositProductChargeCommand()
                    {
                        ChargeId = item,
                        ProductId = "1",
                        // IsActive = true,
                    };
                    if (!ChargesCommand.Any(f => f.ChargeId == charges.ChargeId))
                        ChargesCommand.Add(charges);
                }
            }
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public void MapToCommand()
        {
            UpdateDepositProductCommand = new UpdateDepositProductCommand()
            {

                BankDepositAccountId = Model.BankDepositAccountId,
                Code = Model.Code,
                DefaultCurrencyId = Model.DefaultCurrencyId,
                //Description = Model.Description,
                // IsActive = true,
                Name = Model.Name,
                Tenure = Model.Tenure,
                TenureValue = Model.TenureValue,
                IsInterestEnabled = true,
                //MinimumAge = Model.MinimumAge,
                //MaximumAge = Model.MaximumAge,
                ProductCharges = ChargesCommand,
                InterestRanges = InterestRateCommand,
                ShortName = Model.ShortName,
                ProductType = Model.ProductType,
                CreatedByUserId = ApplicationUserID,
                IsDefaultProduct = Model.IsDefaultProduct,
                Id = EditModel.Id,

            };
        }

        public void MapToInterestRate()
        {
            foreach (var item in InterestRates)
            {
                CreateDepositProductInterestRangeCommand createDepositProductInterestRangeCommand =
                    new CreateDepositProductInterestRangeCommand()
                    {
                        ProductId = EditModel.Id,
                        UpperLimit = item.UpperLimit,
                        LowerLimit = item.LowerLimit,
                        InterestRate = item.InterestRate,
                    };
                if (!InterestRateCommand.Any(f =>
                        f.UpperLimit == createDepositProductInterestRangeCommand.UpperLimit &&
                        f.LowerLimit == createDepositProductInterestRangeCommand.LowerLimit &&
                        f.InterestRate == createDepositProductInterestRangeCommand.InterestRate))
                    InterestRateCommand.Add(createDepositProductInterestRangeCommand);
            }
        }

        public void CreateRate()
        {
            int Id = InterestRates.Count + 1;
            CreateInterestRate.Id = Id.ToString();
            InterestRates.Add(CreateInterestRate);
            CreateInterestRate = new CreateDepositProductInterestDTO();
            showInterestConfiguration = true;
            showBasicInformation = false;
            showAddRateDrawer = false;
            StateHasChanged();
        }

        public void DeleteRate(string Id)
        {
            InterestRates.Remove(InterestRates.Where(f => f.Id == Id).FirstOrDefault());
            //if (InterestRates.Count == 0)
            //    showInterestGrid = false;
            //showInterestConfiguration = true;
            showBasicInformation = false;
            showAddRateDrawer = false;
            StateHasChanged();
        }

        public async Task SaveProduct()
        {
            if (string.IsNullOrEmpty(ApprovalWorkFlow))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please Select Approval workflow to continue",
                    NotificationType = NotificationType.Error
                });
                StateHasChanged();
            }
            else
            {
                SubmitButtonText = "Submitting ...";
                // StateHasChanged();

                //await GetApprovalWorkFlow();

                MapToInterestRate();
                MapToCommand();
                UpdateDepositProductCommand.ApprovalWorkflowId = ApprovalWorkFlow;

                Logger.LogInformation($"error->{JsonSerializer.Serialize(UpdateDepositProductCommand)}");

                var rsp = await DataService.Update<UpdateDepositProductCommand, DepositProductViewModel>(
                    nameof(DepositProduct), UpdateDepositProductCommand);
                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(UpdateDepositProductCommand)}");
                //Logger.LogInformation($"rsp response->{JsonSerializer.Serialize(rsp)}");
                if (rsp.IsSuccessStatusCode)
                {
                    await _auditLogService.LogAudit("Deposit Product Updated", "Deposit product was successful", "Security",
                        "NA, readonly request", CurrentUser);
                    SubmitButtonText = "Submit";
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
        }

        private async Task GetApprovalWorkFlow()
        {
            var rsp = await DataService.GetMasterView<List<ApprovalWorkflowViewModel>>("ApprovalWorkflows");

            if (rsp.IsSuccessStatusCode)
            {
                List<ApprovalWorkflowViewModel> rspResponse =
                    JsonSerializer.Deserialize<List<ApprovalWorkflowViewModel>>(rsp.Content.ToJson());
                if (rspResponse != null)
                {
                    if (rspResponse.Any())
                    {
                        ApprovalWorkFlow = EditModel.ApprovalWorkflowId;
                    }
                }
            }
            else
            {
                var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                var msg = rspContent?.Response;
                if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                {
                    msg = rspContent.ValidationErrors[0].Error;
                }
            }
        }

        private async Task GetInterestRate()
        {
            var rsp = await _MasterView.GetCustomMasterView<DepositProductInterestRangeMasterView>(nameof(DepositProductInterestRangeMasterView), "productId", EditModel.Id, "LowerLimit,UpperLimit,InterestRate");

            var getInterest = new CreateDepositProductInterestDTO();

            foreach (var item in rsp)
            {
                getInterest = new CreateDepositProductInterestDTO();
                getInterest.ProductId = item.ProductId;
                getInterest.LowerLimit = item.LowerLimit;
                getInterest.UpperLimit = item.UpperLimit;
                getInterest.InterestRate = item.InterestRate;
                getInterest.Id = item.Id;
                InterestRates.Add(getInterest);
            }
           
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                //addRateDrawer = new Drawer();
                //BrowserDimension = await BrowserService.GetDimensions();
                //addRateDrawer.Width = (int)(BrowserDimension.Width * 0.50);

                BrowserDimension = await BrowserService.GetDimensions();
                createDrawer.Width = (int)(BrowserDimension.Width * 0.40);

                await Task.Delay(1000);
                ApplicationUserID = _UserService.ApplicationUserId;

            }

            await InvokeAsync(StateHasChanged);
            //await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async void LoadDropDown()
        {
            combobox_Account_res = $"{Config.ODATA_VIEWS_HOST}/{nameof(CompanyBankAccountMasterView)}";
            combobox_Currency_res = $"{Config.ODATA_VIEWS_HOST}/{nameof(CurrencyMasterView)}";
            //var tenureUnit = Enum.(typeof(Tenure)).ToList();
            DROPDOWN_API_RESOURCE = $"{Config.ODATA_VIEWS_HOST}/{nameof(ChargeMasterView)}";
            tenureUnit = System.Enum.GetNames(typeof(Tenure)).ToList();

            _chargeMasterViews = await _MasterView.GetCustomMasterViewEntityAllFields<ChargeMasterView>(nameof(ChargeMasterView));

        }

        public async Task OnCancel()
        {
            showAddRateDrawer = false;
        }

        public async Task UpdateTenureValue()
        {

        }

        private void ValueChangeHandler(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (inputValue.ToLowerInvariant() == "none")
            {
                TenureValueMax = 0;
            }
            else
            {
                TenureValueMax = 90000000;
            }
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        private void Change(Syncfusion.Blazor.Buttons.ChangeEventArgs<bool?> args)
        {
            Model.IsDefaultProduct = args.Checked;
        }

    }
}
