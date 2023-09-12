using AntDesign;
using AP.ChevronCoop.AppDomain.MasterData.GlobalCodes;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.MasterData.GlobalCodes;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.GlobalConfiguration.ProductCodeSetup
{
    public partial class ProductCodeGrid
    {
        [Parameter]
        public EventCallback<CreateGlobalCodeCommand> ModelChanged { get; set; }

        bool showAddChargesDrawer { get; set; } = false;
        bool showEditChargesDrawer { get; set; } = false;
        bool showErrorDrawer { get; set; } = false;

        bool showPopup { get; set; } = false;
        bool showDeletePopup { get; set; } = false;
        public bool enableCreatePermission { get; set; } = false;

        public string notificationText { get; set; }
        ClaimsPrincipal CurrentUser { get; set; }
        Drawer editDrawer;
        bool reloadGrid = false;
        Drawer addDrawer;
        string editFormActiveTabKey = "1";
        string ErrorDetails = "";
        CreateGlobalCodeCommand CreateModel { get; set; }
        UpdateGlobalCodeCommand UpdateModel { get; set; }
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();
        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        [Inject] IClientAuditService _auditLogService { get; set; }

        private string searchText;

        private Query QueryGrid; // = new Query();

        //public MemberDataUpload ErrorRecord { get; set; } = new MemberDataUpload();
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(GlobalCodeMasterView)}";

        [Inject]
        ILogger<ProductCodeGrid> Logger { get; set; }

        SfGrid<GlobalCodeMasterView> grid;

        [Inject]
        BrowserService BrowserService { get; set; }

        public DeleteGlobalCodeCommand DeleteModel { get; set; }

        BrowserDimension BrowserDimension;

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

        public void ActionCompletedHandler(ActionEventArgs<GlobalCodeMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private async void GoBack()
        {
            await jsRuntime.InvokeVoidAsync("history.back");
        }

        private async Task GetCurrentUser()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await GetCurrentUser();

                CreateModel = new CreateGlobalCodeCommand();
                UpdateModel = new UpdateGlobalCodeCommand();
                DeleteModel = new DeleteGlobalCodeCommand();

                QueryGrid = new Query();
                // LoadDropDown();
                //   await _auditLogService.LogAudit("Accessed charges' list.", "Accessed charges grid.", "Security", "NA, readonly request", CurrentUser);

                await CheckState();
                await base.OnInitializedAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //private async Task OnRefresh()
        //{

        //    searchText = string.Empty;
        //    QueryGrid = new Query().Where(kycFilter);
        //    grid.Refresh();

        //    await Task.CompletedTask;

        //}

        private async Task OnEditProductCode(GlobalCodeMasterView row)
        {
            UpdateModel = Mapper.Map<UpdateGlobalCodeCommand>(row);
            openEditDrawer();
        }

        private async Task OnDeleteProductCode(GlobalCodeMasterView row)
        {
            DeleteModel = Mapper.Map<DeleteGlobalCodeCommand>(row);
            openDeletePopup();
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(CreateModel);
        }

        void openAddDrawer()
        {
            showAddChargesDrawer = true;
        }

        void openEditDrawer()
        {
            showAddChargesDrawer = true;
        }

        void openDeletePopup()
        {
            showDeletePopup = true;
        }

        void closeDeletePopup()
        {
            showDeletePopup = false;
            DeleteModel = new DeleteGlobalCodeCommand();
        }

        void closeAddDrawer()
        {
            showAddChargesDrawer = false;
        }

        void closeEditDrawer()
        {
            UpdateModel = new UpdateGlobalCodeCommand();
            showEditChargesDrawer = false;
        }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            //this.ErrorDetailMsg = args.Error.ToString();
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }

        public async Task OnSaveClose()
        {
            try
            {
                // CreateModel.Method = IsPercentageSelected ? percentage: flat;
                var checkIsSuccessfull = await ValidateEntry();
                if (checkIsSuccessfull)
                {
                    var rsp = await DataService.Create<CreateGlobalCodeCommand, CommandResult<GlobalCode>>(
                        nameof(GlobalCode), CreateModel);
                    CreateModel.CreatedByUserId = GetCurrentUser().Id.ToString();
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
                        await _auditLogService.LogAudit("Product code Creation.",
                            $"Created  with ID- {rsp.Content.Response.Id}.", "Security", payload, CurrentUser);

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

        public async Task OnDeleteClose()
        {
            try
            {
                // CreateModel.Method = IsPercentageSelected ? percentage: flat;
                var checkIsSuccessfull = await ValidateDeleteEntry();
                if (checkIsSuccessfull)
                {
                    var rsp = await DataService.Create<DeleteGlobalCodeCommand, CommandResult<GlobalCode>>(
                        nameof(GlobalCode), DeleteModel);
                    DeleteModel.DeletedByUserId = GetCurrentUser().Id.ToString();
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
                        await _auditLogService.LogAudit("Product code Creation.",
                            $"Created  with ID- {rsp.Content.Response.Id}.", "Security", payload, CurrentUser);

                        closeDeletePopup();
                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Delete",
                            NotificationType = NotificationType.Success,
                            Description = "record has been deleted successfully"
                        });
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

        public async Task OnEditClose()
        {
            try
            {
                // CreateModel.Method = IsPercentageSelected ? percentage: flat;
                var checkIsSuccessfull = await ValidateUpdateEntry();
                if (checkIsSuccessfull)
                {
                    var rsp = await DataService.Update<UpdateGlobalCodeCommand, CommandResult<GlobalCode>>(
                        nameof(GlobalCode), UpdateModel);
                    CreateModel.CreatedByUserId = GetCurrentUser().Id.ToString();
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
                        await _auditLogService.LogAudit("Product code updated.",
                            $"Updated  with ID- {rsp.Content.Response.Id}.", "Security", payload, CurrentUser);

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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                // var enums = GlobalCodeTypeKeys.GENOTYPE.ToString();

                BrowserDimension = await BrowserService.GetDimensions();
                editDrawer.Width = (int)(BrowserDimension.Width * 0.40);
                addDrawer.Width = (int)(BrowserDimension.Width * 0.40);

                // await OnRefresh();
                //	ErrorRecord = new MemberDataUpload();
            }
            //StateHasChanged();

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task<bool> ValidateDeleteEntry()
        {
            if (string.IsNullOrEmpty(DeleteModel.Id))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Delete Id not found, refresh and try again",
                    NotificationType = NotificationType.Error
                });
                return false;
            }

            return true;
        }

        public async Task<bool> ValidateEntry()
        {
            if (string.IsNullOrEmpty(CreateModel.Code))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide Code",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(CreateModel.Name))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide Name",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(CreateModel.CodeType))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide CodeType",
                    NotificationType = NotificationType.Error
                });
                return false;
            }


            return true;
        }

        public async Task<bool> ValidateUpdateEntry()
        {
            if (string.IsNullOrEmpty(UpdateModel.Code))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide Code",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(UpdateModel.Name))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide Name",
                    NotificationType = NotificationType.Error
                });
                return false;
            }
            else if (string.IsNullOrEmpty(UpdateModel.CodeType))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please, provide CodeType",
                    NotificationType = NotificationType.Error
                });
                return false;
            }

            return true;
        }
    }
}