using AP.ChevronCoop.AppDomain.Security.Approvals;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using Blazored.SessionStorage;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using Newtonsoft.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals.Approvalworkflows
{
    public partial class ApprovalWorkflowList
    {
        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        string FETCH_ALL_APPROVAL_WKFL => $"{Config.API_HOST}/ApprovalWorkflows/fetchWorkflowsGroups";
        string GRID_API_RESOURCE2 => $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalWorkflowMasterView)}?$filter=IsActive eq true & $orderby=DateCreated desc";
        //string GRID_API_RESOURCE2 => $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalWorkflowMasterView)}";

        private Query QueryGrid;
        SfGrid<ApprovalWorkflowMasterView> grid;
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();
        private WhereFilter activeFilter;


        private string ErrorDetails = "";
        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        ISessionStorageService _sessionStorage { get; set; }

        public bool selectWorkflow { get; set; } = false;
        public List<ApprovalWorkflowViewModel> workflows { get; set; } = new List<ApprovalWorkflowViewModel>();
        public ApprovalWorkflowViewModel workflow { get; set; } = new ApprovalWorkflowViewModel();
        public List<CasscadedWorkflowField> CasscadedWorkflowFields { get; set; } = new List<CasscadedWorkflowField>();
        public string bearToken { get; set; }


        public List<ApprovalWorkflowMasterView> ApprovalWorkflowMasterViews { get; set; } =
            new List<ApprovalWorkflowMasterView>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetCurrentUser();
            await GetWorkflows();
            await ApplyDefaultQueryFilter();
        }

        private async Task ApplyDefaultQueryFilter()
        {
            //activeFilter = new WhereFilter
            //{
            //    Field = nameof(ApprovalWorkflowMasterView.IsActive),
            //    Operator = "eq",
            //    value = true
            //};
            //QueryGrid = new Query().Where(activeFilter);
        }


        private async Task GetWorkflows()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(GRID_API_RESOURCE2);
                Stream stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsAsync<WorkFloMasterView>();
                if (content != null)
                    ApprovalWorkflowMasterViews = content.value;
            }
            catch (Exception exp)
            {
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                var dd = CasscadedWorkflowFields;

                bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);
                if (string.IsNullOrEmpty(bearToken))
                {
                    _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
                }

                HeaderData.Add("Bearer", bearToken);

                // await _sessionStorage.RemoveItemAsync("CURRENTAPPROVALWORKFLOWID");
                await _sessionStorage.SetItemAsync("CURRENTAPPROVALWORKFLOWID", new ApprovalWorkflow().Id);
                await OnRefresh();
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private async Task OnRefresh()
        {
            grid.Refresh();
            await GetCurrentUser();
            await ApplyDefaultQueryFilter();
            await Task.CompletedTask;
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

        public void ActionCompletedHandler(ActionEventArgs<ApprovalWorkflowMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task CellInfoHandler(QueryCellInfoEventArgs<ApprovalWorkflowMasterView> Args)
        {
        }


        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }
    }


    public class CasscadedWorkflowField
    {
        public string WorkflowId { get; set; }
        public int NumberOfApprovers { get; set; }
        public int NumberOfApprovalGroups { get; set; }
        public string DepartmentsSelected { get; set; }
    }

    public class ApprovalGroupResponse
    {
        public List<ApprovalWorkflowViewModel> response { get; set; }
        public string message { get; set; }
        public int statusCode { get; set; }
    }

    public class WorkFloMasterView
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("@odata.count")]
        public int odatacount { get; set; }

        public List<ApprovalWorkflowMasterView> value { get; set; }
    }
}