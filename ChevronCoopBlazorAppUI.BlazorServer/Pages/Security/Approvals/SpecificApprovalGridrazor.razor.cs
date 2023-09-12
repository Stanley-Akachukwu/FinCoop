using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals
{
    public partial class SpecificApprovalGridrazor
    {
        [Parameter]
        public string type { get; set; }

        public string Title { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }

        [Inject]
        ILogger<SpecificApprovalGridrazor> Logger { get; set; }

        private string bearToken { get; set; }
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        //[Parameter]
        //public string navigateBackUrl { get; set; }
        [Parameter]
        public string entityId { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        SfGrid<ApprovalMasterView> grid;

        //string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalMasterView)}?$filter=approvalType eq '{type}'&$orderby=DateCreated desc";
        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalMasterView)}?$orderby=DateCreated desc";

        private Query QueryGrid;
        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;
        string searchText;
        string ApproveButtonText = "Save";
        bool showPopup { get; set; } = false;

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        Drawer approveDrawer;
        bool showApproveDrawer { get; set; } = false;

        [Parameter]
        public HandleApprovalCommand Model { get; set; } = new HandleApprovalCommand();

        [Inject]
        BrowserService BrowserService { get; set; }

        public ApprovalStatus status { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        private WhereFilter statusfilter;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Model = new HandleApprovalCommand();
            await GetCurrentUser();
            //Title = type != null ? type.Replace("_", " ") : "";
            Title = "List of pending approvals";
            //if (!string.IsNullOrEmpty(type))
            //await GetApprovalRecords();
            //statusfilter = new WhereFilter
            //{
            //	Field = nameof(ApprovalMasterView.Status),
            //	Operator = "equal",
            //	value = ApprovalStatus.APPROVED
            //};
            //QueryGrid = new Query().Where(statusfilter);
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

                BrowserDimension = await BrowserService.GetDimensions();
                approveDrawer.Width = (int)(BrowserDimension.Width * 0.40);
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
            ApplicationUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
        }

        public async Task GetApprovalRecords()
        {
            //GRID_API_RESOURCE = $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalMasterView)}?$approvalType={type}";
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

        private async Task OnSearch()
        {
            //if (!string.IsNullOrWhiteSpace(searchText))
            //{

            //	WhereFilter firstNameFilter = new WhereFilter
            //	{
            //		Field = nameof(ApprovalConsolidationMasterView.RequestName),
            //		Operator = "contains",
            //		value = searchText
            //	};

            //	WhereFilter caiFilter = new WhereFilter
            //	{
            //		Field = nameof(ApprovalConsolidationMasterView.Description),
            //		Operator = "contains",
            //		value = searchText
            //	};
            //	WhereFilter emailFilter = new WhereFilter
            //	{
            //		Field = nameof(ApprovalConsolidationMasterView.Size),
            //		Operator = "contains",
            //		value = searchText
            //	};
            //	WhereFilter addressFilter = new WhereFilter
            //	{
            //		Field = nameof(ApprovalConsolidationMasterView.DateUpdated),
            //		Operator = "contains",
            //		value = searchText
            //	};
            //	WhereFilter lastNameFilter = new WhereFilter
            //	{
            //		Field = nameof(ApprovalConsolidationMasterView.AprrovalProcessingPage),
            //		Operator = "contains",
            //		value = searchText
            //	};
            //	QueryGrid = new Query().Where(kycFilter.And(firstNameFilter.Or(lastNameFilter).Or(caiFilter).Or(addressFilter).Or(emailFilter)));

            //}
            //else
            //{
            //	await OnRefresh();
            //}
        }

        private async Task OnClearSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                await OnRefresh();
            }
        }

        private async Task OnRefresh()
        {
            searchText = string.Empty;
            //QueryGrid = new Query().Where(statusfilter);
            grid.Refresh();
            await Task.CompletedTask;
        }

        public void ActionCompletedHandler(ActionEventArgs<ApprovalMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<ApprovalMasterView> Args)
        {
            Args.Data.Status = Args.Data.Status != null ? Args.Data.Status.Replace("_", " ") : "";
        }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }

        async Task onApproveDone()
        {
            showApproveDrawer = false;
        }

        public async Task OnCancel()
        {
            showApproveDrawer = false;
        }

        public async Task OnSave()
        {
            await Save();
            showApproveDrawer = false;
        }

        private async Task ApproveRow(ApprovalMasterView row)
        {
            Model = new HandleApprovalCommand();
            Model.ApprovalId = row.Id;
            showApproveDrawer = true;
        }

        public async Task Save()
        {
            Model.Status = status;
            if (Model != null && Model.ApprovalId != null && Model.Status != null && ApplicationUserId != null)
            {
                ApproveButtonText = "Processing...";
                StateHasChanged();
                Model.ApplicationUserId = ApplicationUserId;
                var rsp = await DataService.ProcessRequest<HandleApprovalCommand, CommandResult<string>>(
                    nameof(Approval), "handle", Model);
                if (!rsp.IsSuccessStatusCode)
                {
                    var serverErrorMessages = "Server Error.";
                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);
                    var msg = rsp.ReasonPhrase;

                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        serverErrorMessages += " " + rspContent.ValidationErrors[0].Error;
                    }

                    if (!string.IsNullOrEmpty(rspContent.Message))
                    {
                        serverErrorMessages += " " + rspContent.Message;
                    }

                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = serverErrorMessages,
                        NotificationType = NotificationType.Error,
                        Description = rspContent.Message,
                    });
                }
                else
                {
                    showPopup = true;
                    var payload = JsonSerializer.Serialize(Model);
                    await _auditLogService.LogAudit($"Product Approval", $"{Model.Status} on product of type {type} ",
                        "Product", payload, CurrentUser);
                    StateHasChanged();
                    //OnCancel();
                }
            }
        }

        private async Task Done()
        {
            showPopup = false;
            await InvokeAsync(StateHasChanged);
            _navigationManager.NavigateTo($"/chevroncoop/approval/{type}", true);
        }
    }
}