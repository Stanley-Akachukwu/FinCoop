using AntDesign;
using AP.ChevronCoop.AppDomain.MasterData.Charges;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.MasterData.Charges;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ChargeSetup
{
    public partial class ProductChargesGrid
    {
        public string combobox_Department_res { get; set; }
        public bool enableCreatePermission { get; set; } = false;
        public string percentageValue { get; set; } = string.Empty;
        public string flatValue { get; set; } = string.Empty;
        public string notificationText { get; set; }
        ClaimsPrincipal CurrentUser { get; set; }
        Drawer editDrawer;
        bool reloadGrid = false;
        Drawer addDrawer;
        string editFormActiveTabKey = "1";
        CreateChargeCommand CreateModel { get; set; }
        UpdateChargeCommand UpdateModel { get; set; }

        private Query QueryGrid; // = new Query();

        //public MemberDataUpload ErrorRecord { get; set; } = new MemberDataUpload();
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        string LoggedInUserId = string.Empty;

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        public bool IsPercentageSelected { get; set; } = true;
        public bool IsFlatSelected { get; set; } = false;


        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        ILogger<ProductChargesGrid> Logger { get; set; }

        SfGrid<ChargeMasterView> grid;

        private async void GoBack()
        {
            await jsRuntime.InvokeVoidAsync("history.back");
        }

        public void ActionCompletedHandler(ActionEventArgs<ChargeMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private bool showError = false;
        private string errorMessage = string.Empty;

        bool showAddChargesDrawer { get; set; } = false;
        bool showEditChargesDrawer { get; set; } = false;
        bool showErrorDrawer { get; set; } = false;

        bool showPopup { get; set; } = false;

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject] IClientAuditService _auditLogService { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        BrowserDimension BrowserDimension;

        string searchText;
        private WhereFilter kycFilter;
        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();

        [Inject]
        WebConfigHelper Config { get; set; }

        string ErrorDetails = "";
        private Query Query_Combo;
        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(ChargeMasterView)}?$orderby=DateCreated desc";
        [Inject]

        NavigationManager NavigationManager { get; set; }

        public CreateChargeCommand ModelChanged { get; private set; }

        private async Task GetCurrentUser()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        public void LoadDropDown()
        {
            combobox_Department_res = $"{Config.ODATA_VIEWS_HOST}/{nameof(Currency)}";
        }

        private async Task CheckState()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                if (user.HasClaim(ClaimTypes.Role, PermissionConfig.DepositsChargeCreate))
                    enableCreatePermission = true;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await GetCurrentUser();

                CreateModel = new CreateChargeCommand();

                UpdateModel = new UpdateChargeCommand();


                QueryGrid = new Query();
                LoadDropDown();
                await _auditLogService.LogAudit("Accessed charges' list.", "Accessed charges grid.", "Security",
                    "NA, readonly request", CurrentUser);

                await CheckState();
                await base.OnInitializedAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        void openAddDrawer()
        {
            showAddChargesDrawer = true;
        }

        void closeAddDrawer()
        {
            showAddChargesDrawer = false;
        }

        void closeEditDrawer()
        {
            showEditChargesDrawer = false;
        }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            //this.ErrorDetailMsg = args.Error.ToString();
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();
                editDrawer.Width = (int)(BrowserDimension.Width * 0.40);
                addDrawer.Width = (int)(BrowserDimension.Width * 0.40);

                // await OnRefresh();
                //	ErrorRecord = new MemberDataUpload();
            }
            //StateHasChanged();

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task<bool> ValidateEntry()
        {
            if (CreateModel.MaximumCharge.HasValue && CreateModel.MaximumCharge.Value < 0.0M)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Invalid Maximum Charge",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            if (string.IsNullOrEmpty(CreateModel.Method))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide Method",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(CreateModel.Target))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide Target",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(CreateModel.Name))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide charge name",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(CreateModel.CurrencyId))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, Provide Currency",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(CreateModel.CalculationMethod))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, Provide CalculationMethod",
                    NotificationType = NotificationType.Error
                });
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateUpdateEntry()
        {
            if (UpdateModel.MaximumCharge.HasValue && UpdateModel.MaximumCharge.Value < 0.0M)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Invalid Maximum Charge",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(UpdateModel.Method))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide Method",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(UpdateModel.Target))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide Target",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(UpdateModel.Name))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide charge name",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(UpdateModel.CurrencyId))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, Provide CurrencyId",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(UpdateModel.CalculationMethod))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, Provide CalculationMethod",
                    NotificationType = NotificationType.Error
                });
                return false;
            }

            return true;
        }

        void SwitchRadio(ChangeEventArgs args)
        {
            if (args.Value.ToString() == ChargeMethod.PERCENT.ToString())
            {
                IsPercentageSelected = true;
                IsFlatSelected = false;
                flatValue = "";
            }
            else
            {
                IsPercentageSelected = false;
                IsFlatSelected = true;
                percentageValue = "";
            }

            CreateModel.Method = args.Value.ToString();
            UpdateModel.Method = args.Value.ToString();
        }

        private async Task Done()
        {
            showPopup = false;
            await InvokeAsync(StateHasChanged);

            reloadGrid = true;
            OnCancel();
        }

        public async Task OnSaveClose()
        {
            try
            {
                //CreateModel.ChargeValue = IsPercentageSelected ? decimal.Parse(percentageValue): decimal.Parse(flatValue);
                var checkIsSuccessfull = await ValidateEntry();
                if (checkIsSuccessfull)
                {
                    var rsp = await DataService.Create<CreateChargeCommand, CommandResult<Charge>>(nameof(Charge),
                        CreateModel);

                    Logger.LogInformation($"error->{JsonSerializer.Serialize(rsp?.Error?.Content)}");

                    if (!rsp.IsSuccessStatusCode)
                    {
                        var serverErrorMessages = "Server Error.";
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);
                        var msg = rsp.ReasonPhrase;

                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
                        {
                            serverErrorMessages += " " + rspContent.ValidationErrors[0].Error;
                        }

                        if (!string.IsNullOrEmpty(rspContent.Message))
                        {
                            serverErrorMessages += " " + rspContent.Message;
                        }

                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = msg,
                            NotificationType = NotificationType.Error,
                            Description = serverErrorMessages
                        });
                    }
                    else
                    {
                        var payload = JsonSerializer.Serialize(CreateModel);
                        await _auditLogService.LogAudit("Product charges Creation.",
                            $"Created charges with ID- {rsp.Content.Response.Id}.", "Security", payload, CurrentUser);

                        closeAddDrawer();
                        showPopup = true;
                    }
                }
            }
            catch (Exception ex)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = ex.Message,
                    Description = "Request could not be processed.",
                    NotificationType = NotificationType.Error
                });
            }
        }

        private async Task onBack()
        {
            NavigationManager.NavigateTo("/product-charges/list", true);
        }

        public async Task OnEditClose()
        {
            try
            {
                //UpdateModel.ChargeValue = IsPercentageSelected ? decimal.Parse(percentageValue) : decimal.Parse(flatValue);
                var checkIsSuccessfull = await ValidateUpdateEntry();
                if (checkIsSuccessfull)
                {
                    var rsp = await DataService.Update<UpdateChargeCommand, CommandResult<Charge>>(nameof(Charge),
                        UpdateModel);

                    Logger.LogInformation($"error->{JsonSerializer.Serialize(rsp?.Error?.Content)}");

                    if (!rsp.IsSuccessStatusCode)
                    {
                        var serverErrorMessages = "Server Error.";
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);
                        var msg = rsp.ReasonPhrase;

                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
                        {
                            serverErrorMessages += " " + rspContent.ValidationErrors[0].Error;
                        }

                        if (!string.IsNullOrEmpty(rspContent.Message))
                        {
                            serverErrorMessages += " " + rspContent.Message;
                        }

                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = msg,
                            NotificationType = NotificationType.Error,
                            Description = rspContent.Message,
                        });
                    }
                    else
                    {
                        var payload = JsonSerializer.Serialize(CreateModel);
                        await _auditLogService.LogAudit("Product charges Creation.",
                            $"Created charges with ID- {rsp.Content.Response.Id}.", "Security", payload, CurrentUser);
                        closeEditDrawer();
                        showPopup = true;
                    }
                }
            }
            catch (Exception ex)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = ex.Message,
                    Description = "Request could not be processed.",
                    NotificationType = NotificationType.Error
                });
            }
        }

        public async Task OnCancel()
        {
            if (reloadGrid)
            {
                grid.Refresh();
                //NavigationManager.NavigateTo("/product-charges/list", true);
                reloadGrid = false;
            }
            //ShowModal = false;
            //await ShowModalChanged.InvokeAsync(ShowModal);

            //Model = new CreateApplicationUserCommand();
            //await ModelChanged.InvokeAsync(Model);

            //showPopup = false;
        }

        async Task onErrorDone()
        {
            showErrorDrawer = false;
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            // await ModelChanged.InvokeAsync(CreateModel);
        }


        private async Task OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                WhereFilter firstNameFilter = new WhereFilter
                {
                    Field = nameof(ChargeMasterView.Code),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter caiFilter = new WhereFilter
                {
                    Field = nameof(ChargeMasterView.Target),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter memberShipFilter = new WhereFilter
                {
                    Field = nameof(ChargeMasterView.CalculationMethod),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter emailFilter = new WhereFilter
                {
                    Field = nameof(ChargeMasterView.Method),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter addressFilter = new WhereFilter
                {
                    Field = nameof(ChargeMasterView.CurrencyId_Code),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter lastNameFilter = new WhereFilter
                {
                    Field = nameof(ChargeMasterView.ChargeValue),
                    Operator = "contains",
                    value = searchText
                };

                QueryGrid = new Query().Where(firstNameFilter.Or(lastNameFilter).Or(caiFilter).Or(addressFilter)
                    .Or(emailFilter).Or(memberShipFilter));
            }
        }

        private async Task OnRefresh()
        {
            searchText = string.Empty;
            QueryGrid = new Query().Where(kycFilter);
            grid.Refresh();

            await Task.CompletedTask;
        }

        private async Task OnEditCharges(ChargeMasterView row)
        {
            UpdateModel = Mapper.Map<UpdateChargeCommand>(row);
            //if (UpdateModel.Method == ChargeMethod.Percent.ToString())
            //{
            //    IsFlatSelected = false;
            //    IsPercentageSelected = true;
            //    flatValue = "";
            //    percentageValue = UpdateModel.Method;
            //}
            //else
            //{
            //    IsFlatSelected = true;
            //    percentageValue = "";
            //    flatValue = UpdateModel.Method;
            //    IsPercentageSelected = false;
            //}
            await onEdit();
        }

        async Task onEdit()
        {
            showEditChargesDrawer = true;
        }

        async Task OnEditChargesDone()
        {
            showEditChargesDrawer = false;
            UpdateModel = new UpdateChargeCommand();
        }


        async Task OnAddChargesDone()
        {
            showAddChargesDrawer = false;
        }

        async Task OnAddCharges()
        {
            showAddChargesDrawer = true;
        }

        private async Task OnSearchEnter(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                if (!string.IsNullOrWhiteSpace(searchText))
                    await OnSearch();
                //else
                //	await GetApprovedBulkUpload();
            }
        }
    }
}