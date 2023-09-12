using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.DataMigration
{
    public partial class DataMigrationApprovalForm
    {
        string notificationText;
        bool showPopup = false;
        public int RowCounter = 0;

        [Inject]
        ILogger<DataMigrationApprovalForm> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }


        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }


        string searchText;


        SfGrid<MemberBulkUploadSessionMasterView> grid;

        string GRID_API_RESOURCE =>
            $"{Config.ODATA_VIEWS_HOST}/{nameof(MemberBulkUploadSessionMasterView)}?$orderby=DateCreated desc";


        private Query QueryGrid;

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;
        string QueryParameter;

        [Inject]

        NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        string LoggedInUserId = string.Empty;

        [Inject]
        NotificationService notificationService { get; set; }

        DataMigrationForApprovalGrid viewForm;
        Drawer viewDrawer;
        bool showViewDrawer { get; set; } = false;

        MemberBulkUploadSessionMasterView ViewModel { get; set; }
        public List<MemberBulkUploadTemp> MemberBulkUploadTempModel { get; set; }
        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        private string bearToken { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUser();
            ViewModel = new MemberBulkUploadSessionMasterView();
            MemberBulkUploadTempModel = new List<MemberBulkUploadTemp>();
            QueryGrid = new Query();
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

                HeaderData.Add("Bearer", bearToken);
                BrowserDimension = await BrowserService.GetDimensions();
                viewDrawer.Width = (int)(BrowserDimension.Width * 0.60);
                await OnRefresh();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }


        public void ActionCompletedHandler(ActionEventArgs<MemberBulkUploadSessionMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<MemberBulkUploadSessionMasterView> Args)
        {
            if (Args.Data.Status == "PENDING_APPROVAL ")
            {
                Args.Data.Status = "PENDING APPROVAL";
            }
        }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            //this.ErrorDetailMsg = args.Error.ToString();
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }

        private async Task OnRefresh()
        {
            searchText = string.Empty;
            grid.Refresh();
            await Task.CompletedTask;
        }

        private async Task OnSearchEnter(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                if (!string.IsNullOrWhiteSpace(searchText))
                    await OnSearch();
                else
                    await OnRefresh();
            }
        }

        private async Task onApproval()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity.IsAuthenticated)
            {
                LoggedInUserId = user.FindFirstValue(ClaimTypes.Sid);
                if (!string.IsNullOrEmpty(LoggedInUserId))
                {
                    ApproveMemberBulkUploadCommand command = new ApproveMemberBulkUploadCommand()
                    {
                        ApprovedById = LoggedInUserId,
                        MemberBulkUploadSessionId = ViewModel.MemberBulkUploadSessionId,
                        ApprovalId = ViewModel.ApprovalId,
                    };
                    var j = JsonSerializer.Serialize(command);
                    Logger.LogInformation($"payload->{j}");
                    var rsp = await DataService
                        .ProcessRequest<ApproveMemberBulkUploadCommand, CommandResult<MemberBulkUploadViewModel>>(
                            "MemberBulkUploads", "approve", command);

                    if (!rsp.IsSuccessStatusCode)
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
                        var payload = JsonSerializer.Serialize(command);
                        await _auditLogService.LogAudit("Data Migration Approval.",
                            $"Approved datamigration with session ID- {command.MemberBulkUploadSessionId}.", "Security",
                            payload, CurrentUser);
                        notificationText = $"Operation waas successfull";
                        showPopup = true;
                        NavigationManager.NavigateTo("/Security/MigrationApproval", true);
                    }
                }
            }
        }

        private async Task OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                WhereFilter dateFilter = new WhereFilter
                {
                    Field = nameof(MemberBulkUploadSessionMasterView.DateUpdated),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter statusFilter = new WhereFilter
                {
                    Field = nameof(MemberBulkUploadSessionMasterView.Status),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter uploadedByFilter = new WhereFilter
                {
                    Field = nameof(MemberBulkUploadSessionMasterView.UserId_UserName),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter descriptionFilter = new WhereFilter
                {
                    Field = nameof(MemberBulkUploadSessionMasterView.Description),
                    Operator = "contains",
                    value = searchText
                };
                QueryGrid = new Query().Where(dateFilter.Or(descriptionFilter).Or(statusFilter).Or(uploadedByFilter));
            }
            else
            {
                await OnRefresh();
            }
        }

        private async Task OnClearSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                await OnRefresh();
            }
        }

        async Task onView()
        {
            showViewDrawer = true;
        }

        async Task onViewDone()
        {
            showViewDrawer = false;
        }

        private async Task OnViewRow(MemberBulkUploadSessionMasterView row)
        {
            ViewModel = row;
            await GetMemberDataUploadMasterView();
            await onView();
        }

        private async Task onApproveRow(MemberBulkUploadSessionMasterView row)
        {
            ViewModel = row;
            await onApproval();
        }

        private async Task GetMemberDataUploadMasterView()
        {
            GetMemberBulkUploadTempCommand command = new GetMemberBulkUploadTempCommand()
            {
                SessionId = ViewModel.MemberBulkUploadSessionId
            };
            var rsp = await DataService
                .ProcessRequest<GetMemberBulkUploadTempCommand, CommandResult<List<MemberBulkUploadTemp>>>(
                    "MemberBulkUploads", "getValidTempBulkUpload", command);

            if (!rsp.IsSuccessStatusCode)
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
                MemberBulkUploadTempModel =
                    JsonSerializer.Deserialize<List<MemberBulkUploadTemp>>(rsp.Content.Response.ToJson());
            }
        }
    }
}