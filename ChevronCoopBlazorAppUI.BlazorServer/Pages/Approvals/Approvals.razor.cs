using AntDesign;
using AntDesign.Core.Extensions;
using AP.ChevronCoop.AppCore.Services.ApprovalServices.ApprovalFactory;
using AP.ChevronCoop.AppDomain.Security.Approvals;
using AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Approvals;
using AP.ChevronCoop.Entities.Security.Approvals.Approvals;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using Query = Syncfusion.Blazor.Data.Query;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Approvals
{
    public partial class Approvals
    {
        public string combobox_Department_res { get; set; }

        string notificationText;
        bool showPopup = false;

        [Inject]
        ILogger<Approvals> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        ApprovalStateContainerService _stateService { get; set; }

        SfGrid<ApprovalView> grid;

        /// <summary>
        /// string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalMasterView)}?$orderby=dateCreated desc";
        /// </summary>

        //$"https://chevroncoop-dev-api.azurewebsites.net/ApprovalMasterView/userApprovals/{ApplicationUserId}"); 
        string GRID_API_RESOURCE => $"{Config.API_HOST}/{nameof(ApprovalMasterView)}/userApprovals/";
        string Single_API_RESOURCE => $"{Config.API_HOST}/{nameof(ApprovalMasterView)}/getById/";
        private Query QueryGrid;
        private Query Query_Combo;

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        private string bearToken { get; set; }
        string ErrorDetails = "";
        ErrorDetails errors;

        [Parameter]
        public string filter { get; set; }

        private WhereFilter statusFilter { get; set; }
        private WhereFilter searchApprovedFilter { get; set; }
        private WhereFilter searchRejectedFilter { get; set; }
        private WhereFilter searchPendingApprovalFilter { get; set; }
        private WhereFilter searchPublishedFilter { get; set; }
        string Active { get; set; } = "text-blue-600 border-b-2 border-blue-600 active";
        string Inactive { get; set; } = "border-b-2 border-transparent";
        string All { get; set; } = string.Empty;
        string Pending { get; set; } = string.Empty;
        string Approved { get; set; } = string.Empty;
        string Initiated { get; set; } = string.Empty;
        string Created { get; set; } = string.Empty;
        string Rejected { get; set; } = string.Empty;
        string ApprovalId { get; set; } = string.Empty;
        string ApprovalWorkFlowId { get; set; } = string.Empty;
        string ApplicationUserId { get; set; }
        public List<ApprovalMasterView> ApprovalMasterViewList { get; set; }
        public List<ApprovalView> ApprovalViews { get; set; }
        public ApprovalMasterView approvalMasterView { get; set; }
        public string JsonVM { get; set; }
        [Inject] IApprovalDetailFactory _approvalDetail { get; set; }
        bool isLoading = false;


        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            await GetCurrentUser();
            try
            {
                if(!string.IsNullOrEmpty(ApplicationUserId))
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Get,$"{GRID_API_RESOURCE}{ApplicationUserId}");
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        ApprovalViews = await response.Content.ReadAsAsync<List<ApprovalView>>();
                        ApprovalViews = ApprovalViews.OrderByDescending(e => e.DateCreated).ToList();  
                    }
                }

                QueryGrid = new Query();
                await ExecuteFilteringAsync();
              
                await _auditLogService.LogAudit("Accessed Approval list", "Accessed Approval list", "Security",
                    "NA, readonly request", CurrentUser);
                isLoading = false;
            }
            catch (Exception ex)
            {
                await notificationService.Error(new NotificationConfig()
                {
                    Message = "Server Error",
                    Description = "Request could not be completed due to server error.",
                    NotificationType = NotificationType.Error
                });
            }
        }
        private async Task PendingRecord(ApprovalView row)
        {
            //JsonVM = await GetJsonViewModel(row);
            approvalMasterView = new ApprovalMasterView();
            if (!string.IsNullOrEmpty(ApplicationUserId))
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{Single_API_RESOURCE}{row.Id}");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    ApprovalMasterViewList = await response.Content.ReadAsAsync<List<ApprovalMasterView>>();
                    approvalMasterView = ApprovalMasterViewList[0];
                }
            }
            if (string.IsNullOrEmpty(approvalMasterView?.ApprovalViewModelPayload))
            {
                JsonVM = await GetJsonViewModel(row);
                if (!string.IsNullOrEmpty(JsonVM))
                {
                    approvalMasterView.ApprovalViewModelPayload = JsonVM;
                }
                else
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Approval Details Generation Error.",
                        NotificationType = NotificationType.Info,
                        Description = "Could not generate all the required details for your request. Please contact thee administrator"
                    });
                    return;
                }
            }

            _stateService.SetValue(approvalMasterView);
            _navigationManager.NavigateTo("/approval/process");

        }
        private async Task<string> GetJsonViewModel(ApprovalView row)
        {
            try
            {
                ApprovalType type = (ApprovalType)System.Enum.Parse(typeof(ApprovalType), row.ApprovalType, true);

                var jsonVM = await _approvalDetail.ProcessDetail(type, row.Id);
                return jsonVM;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

       
        private async Task ExecuteFilteringAsync()
        {
            QueryGrid = new Query();
            switch (filter)
            {
                case "initiated":
                    statusFilter = new WhereFilter
                    {
                        Field = nameof(ApprovalMasterView.Status),
                        Operator = "equal",
                        value = nameof(ApprovalStatus.INITIATED)
                    };
                    QueryGrid = new Query().Where(statusFilter);
                    ActivateTab();
                    Initiated = Active;
                    break;
                case "created":
                    statusFilter = new WhereFilter
                    {
                        Field = nameof(ApprovalMasterView.Status),
                        Operator = "equal",
                        value = nameof(ApprovalStatus.CREATED)
                    };
                    QueryGrid = new Query().Where(statusFilter);
                    ActivateTab();
                    Created = Active;
                    break;
                case "approved":
                    statusFilter = new WhereFilter
                    {
                        Field = nameof(ApprovalMasterView.Status),
                        Operator = "equal",
                        value = nameof(ApprovalStatus.APPROVED)
                    };
                    QueryGrid = new Query().Where(statusFilter);
                    ActivateTab();
                    Approved = Active;
                    break;
                case "rejected":
                    statusFilter = new WhereFilter
                    {
                        Field = nameof(ApprovalMasterView.Status),
                        Operator = "equal",
                        value = nameof(ApprovalStatus.REJECTED)
                    };
                    QueryGrid = new Query().Where(statusFilter);
                    ActivateTab();
                    Rejected = Active;
                    break;
                case "pending_approval":
                    statusFilter = new WhereFilter
                    {
                        Field = nameof(ApprovalMasterView.Status),
                        Operator = "equal",
                        value = nameof(ApprovalStatus.PENDING_APPROVAL)
                    };
                    QueryGrid = new Query().Where(statusFilter);
                    ActivateTab();
                    Pending = Active;
                    break;
                default:
                case "all":
                    var approvedFilter = new WhereFilter
                    {
                        Field = nameof(ApprovalMasterView.Status),
                        Operator = "equal",
                        value = nameof(ApprovalStatus.APPROVED)
                    };
                    var initiatedFilter = new WhereFilter
                    {
                        Field = nameof(ApprovalMasterView.Status),
                        Operator = "equal",
                        value = nameof(ApprovalStatus.INITIATED)
                    };
                    var createdFilter = new WhereFilter
                    {
                        Field = nameof(ApprovalMasterView.Status),
                        Operator = "equal",
                        value = nameof(ApprovalStatus.CREATED)
                    };
                    var pendingApprovalFilter = new WhereFilter
                    {
                        Field = nameof(ApprovalMasterView.Status),
                        Operator = "equal",
                        value = nameof(ApprovalStatus.PENDING_APPROVAL)
                    };
                    var rejectedFilter = new WhereFilter
                    {
                        Field = nameof(ApprovalMasterView.Status),
                        Operator = "equal",
                        value = nameof(ApprovalStatus.REJECTED)
                    };

                    QueryGrid = new Query().Where(
                        approvedFilter.Or(
                            pendingApprovalFilter.Or(rejectedFilter.Or(initiatedFilter.Or(createdFilter)))));
                    ActivateTab();
                    All = Active;
                    break;
            }
        }

        public void ActivateTab()
        {
            All = Inactive;
            Approved = Inactive;
            Pending = Inactive;
            Rejected = Inactive;
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
            if (CurrentUser != null)
                ApplicationUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
        }

        async Task onFiltering(string filter)
        {
            _navigationManager.NavigateTo($"/approvals/{filter}", forceLoad: true);
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

                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private async Task onViewApprovalDetails(ApprovalMasterView row)
        {
            //ApproveCommand = new ApproveDepositProductCommand();
            //ApproveCommand.Status = row.Status;
            //ApproveCommand.DepositProductId = row.Id;
            //ApproveCommand.ApprovalId = row.ApprovalId;
            //ApproveCommand.ApprovedBy = CurrentUser.FindFirstValue(ClaimTypes.Sid);
            //var payload = JsonSerializer.Serialize(ApproveCommand);
            //await _auditLogService.LogAudit("Deposit Product Setup Approval.", $"Approved Deposit Product Setup with ID- {row.Id}.", "Security", payload, CurrentUser);
            //await onApprove();
        }

        public void ActionCompletedHandler(ActionEventArgs<ApprovalView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            //this.ErrorDetailMsg = args.Error.ToString();
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
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
            await SearchFiltering();
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
            //ExecuteFilteringAsync();
            grid.Refresh();

            await Task.CompletedTask;
        }

        private async Task SearchFiltering()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                WhereFilter NameFilter = new WhereFilter
                {
                    Field = nameof(ApprovalMasterView.ApprovalType),
                    Operator = "contains",
                    value = searchText
                };


                WhereFilter tenureFilter = new WhereFilter
                {
                    Field = nameof(ApprovalMasterView.Status),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter statusFilter = new WhereFilter
                {
                    Field = nameof(ApprovalMasterView.Comment),
                    Operator = "contains",
                    value = searchText
                };
                switch (filter)
                {
                    case "approved":
                        statusFilter = new WhereFilter
                        {
                            Field = nameof(ApprovalMasterView.Status),
                            Operator = "equal",
                            value = nameof(ApprovalStatus.APPROVED)
                        };
                        QueryGrid = new Query().Where(statusFilter.And(NameFilter.Or(tenureFilter).Or(statusFilter)));
                        ActivateTab();
                        Approved = Active;
                        break;
                    case "rejected":
                        statusFilter = new WhereFilter
                        {
                            Field = nameof(ApprovalMasterView.Status),
                            Operator = "equal",
                            value = nameof(ApprovalStatus.REJECTED)
                        };
                        QueryGrid = new Query().Where(statusFilter.And(NameFilter.Or(tenureFilter).Or(statusFilter)));
                        ActivateTab();
                        Rejected = Active;
                        break;
                    case "pending_approval":
                        statusFilter = new WhereFilter
                        {
                            Field = nameof(ApprovalMasterView.Status),
                            Operator = "equal",
                            value = nameof(ApprovalStatus.PENDING_APPROVAL)
                        };
                        QueryGrid = new Query().Where(statusFilter.And(NameFilter.Or(tenureFilter).Or(statusFilter)));
                        ActivateTab();
                        Pending = Active;
                        break;
                    default:
                    case "all":
                        searchApprovedFilter = new WhereFilter
                        {
                            Field = nameof(ApprovalMasterView.Status),
                            Operator = "equal",
                            value = nameof(ApprovalStatus.APPROVED)
                        };
                        searchRejectedFilter = new WhereFilter
                        {
                            Field = nameof(ApprovalMasterView.Status),
                            Operator = "equal",
                            value = nameof(ApprovalStatus.REJECTED)
                        };
                        searchPendingApprovalFilter = new WhereFilter
                        {
                            Field = nameof(ApprovalMasterView.Status),
                            Operator = "equal",
                            value = nameof(ApprovalStatus.PENDING_APPROVAL)
                        };

                        QueryGrid = new Query();
                        ActivateTab();
                        All = Active;
                        break;
                }
            }
            else
            {
                await OnRefresh();
            }
        }

        private async Task RejectedRecord(ApprovalMasterView row)
        {
            ApprovalId = row.Id;
            ApprovalWorkFlowId = row.ApprovalWorkflowId;
            var url = $"/approval-rejection?ApprovalId={ApprovalId}&ApprovalWorkFlowId={ApprovalWorkFlowId}";
            _navigationManager.NavigateTo(url, true);
        }
      

        public void Dispose()
        {
            _stateService.OnStateChange -= StateHasChanged;
        }
        private async Task ApprovedRecord(ApprovalMasterView row)
        {
            ApprovalId = row.Id;
            ApprovalWorkFlowId = row.ApprovalWorkflowId;
            var url = $"/approval-success?ApprovalId={ApprovalId}&ApprovalWorkFlowId={ApprovalWorkFlowId}";
            _navigationManager.NavigateTo(url, true);
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<ApprovalView> Args)
        {
            Args.Data.Status = Args.Data.Status != null ? Args.Data.Status.Replace("_", " ") : "";
        }
        private async Task GetLoginUserApprovalRecords()
        {
            try
            {


                var rsp = await DataService.GetApprovalRecord<List<ApprovalMasterView>>(
  nameof(ApprovalMasterView), ApplicationUserId);
                if (rsp.IsSuccessStatusCode)
                {
                    ApprovalMasterViewList = new List<ApprovalMasterView>();
                    ApprovalMasterViewList = rsp.Content;

                }
            }
            catch (Exception exp)
            {
            }
        }
    }
}