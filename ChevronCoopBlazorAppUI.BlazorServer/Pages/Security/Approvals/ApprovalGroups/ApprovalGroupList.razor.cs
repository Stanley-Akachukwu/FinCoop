using AP.ChevronCoop.AppDomain.Security.Approvals;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using System.Text;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Data;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using System.Text.Json;
using Blazored.SessionStorage;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals.ApprovalGroups
{
    public partial class ApprovalGroupList
    {
        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        string FETCH_ALL_APPROVAL_GRPS => $"{Config.API_HOST}/ApprovalGroup/fetchAll";
        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        public List<ApprovalGroupViewModel> approvalGroups { get; set; } = new List<ApprovalGroupViewModel>();

        private string ErrorDetails = "";
        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        private Query QueryGrid;
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        string GRID_API_RESOURCE =>
            $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalGroupMasterView)}?$orderby=DateCreated desc";

        private WhereFilter activeFilter;
        SfGrid<ApprovalGroupMasterView> grid;

        public List<CasscadedApprovalGroupField> CasscadedApprovalGroupFields { get; set; } =
            new List<CasscadedApprovalGroupField>();

        [Inject]
        ISessionStorageService _sessionStorage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetCurrentUser();
            await ApplyDefaultQueryFilter();
            await ApplyDefaultQueryFilter();
            //await GetGroupMembers();
        }

        private async Task ApplyDefaultQueryFilter()
        {
            //activeFilter = new WhereFilter
            //{
            //    Field = nameof(ApprovalWorkflowMasterView.IsActive),
            //    Operator = "equal",
            //    value = true
            //};
            //QueryGrid = new Query().Where(activeFilter);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                //bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);
                //if (string.IsNullOrEmpty(bearToken))
                //{
                //    _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
                //}
                if (await _sessionStorage.GetItemAsStringAsync("CURRENTAPPROVALWORKFLOWID") != null)
                {
                    await _sessionStorage.RemoveItemAsync("CURRENTAPPROVALWORKFLOWID");
                }
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private async Task OnRefresh()
        {
            grid.Refresh();
            //  await GetGroupMembers();
            await GetCurrentUser();
            await ApplyDefaultQueryFilter();
            await Task.CompletedTask;
        }

        public void ActionCompletedHandler(ActionEventArgs<ApprovalGroupMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task CellInfoHandler(QueryCellInfoEventArgs<ApprovalGroupMasterView> Args)
        {
            //var fields = CasscadedApprovalGroupFields.FirstOrDefault(c => c.GroupId == Args.Data.Id);
            //if(fields != null)
            //{
            //    Args.Data.NumberOfApprovers = fields.NumberOfApprovers == null ? 0 : fields.NumberOfApprovers;
            //    Args.Data.Department = fields.Department;
            //}
            //else
            //{
            //    Args.Data.NumberOfApprovers = 0;
            //    Args.Data.Department = string.Empty;
            //}
        }


        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        public async Task onAddApprovalGroup()
        {
            var navigateBackUrl = "navigate-to-approval".ToLower();
            _navigationManager.NavigateTo($"/approval/group/add/{navigateBackUrl}", forceLoad: true);
        }
    }


    public class CasscadedApprovalGroupField
    {
        public string GroupId { get; set; }
        public int NumberOfApprovers { get; set; }
        public string Department { get; set; }
    }

    public class ApprovalGroupResponse
    {
        public List<ApprovalGroupViewModel> response { get; set; }
        public string message { get; set; }
        public int statusCode { get; set; }
    }
}