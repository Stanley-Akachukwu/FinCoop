using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.SessionStorage;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.Enrolment
{
    public partial class CompletedKyc
    {
        [Inject]
        ILogger<EnrolmentGrid> Logger { get; set; }

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
        ISessionStorageService _sessionStorage { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        SfGrid<MemberProfileMasterView> grid;

        [Parameter] public string EnrolmentFIlter { get; set; }
        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(MemberProfileMasterView)}";
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        private Query QueryGrid;
        private WhereFilter kycFilter { get; set; }
        private WhereFilter statusFilter { get; set; }


        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;


        ApproveKYCCommand ApproveModel { get; set; }
        UpdateMemberProfileCommand UpdateModel { get; set; }
        string MemberEmail { get; set; }

        DeleteApplicationUserCommand DeleteModel { get; set; }
        ApproveKYCCompletionForm approveForm;
        Drawer approveDrawer;
        bool showApproveDrawer { get; set; } = false;

        EditEnrolmentForm editEnrolmentForm;
        Drawer editDrawer;
        bool showEditDrawer { get; set; } = false;
        string editFormActiveTabKey = "1";

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;
        string ApprovalWorkFlow = string.Empty;
        string LoggedInUserId = string.Empty;

        ClaimsPrincipal CurrentUser { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUser();
            ApproveModel = new ApproveKYCCommand();
            UpdateModel = new UpdateMemberProfileCommand();
            DeleteModel = new DeleteApplicationUserCommand();
            EnrolmentFIlter = nameof(MemberProfileStatus.AWAITING_KYC_APPROVAL);
            statusFilter = new WhereFilter
            {
                Field = nameof(MemberProfileMasterView.Status),
                Operator = "equal",
                value = EnrolmentFIlter
            };
            QueryGrid = new Query().Where(statusFilter);
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();
                approveDrawer.Width = (int)(BrowserDimension.Width * 0.25);

                var bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);

                if (string.IsNullOrEmpty(bearToken))
                {
                    _navigationManager.NavigateTo("/Account/Logout", forceLoad: true);
                }

                HeaderData.Add("Bearer", bearToken);
                StateHasChanged();
                await OnRefresh();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        protected async Task ApproveActionRefreshGrid()
        {
            StateHasChanged();
            await OnRefresh();
        }

        public void ActionCompletedHandler(ActionEventArgs<MemberProfileMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<MemberProfileMasterView> Args)
        {
            if (Args.Data.Status == "PENDING_APPROVAL")
            {
                Args.Data.Status = "PENDING APPROVAL";
            }
        }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }

        async Task onApprove()
        {
            showApproveDrawer = true;
        }

        async Task onApproveDone()
        {
            showApproveDrawer = false;
        }

        async Task onEdit()
        {
            showEditDrawer = true;
        }

        private async Task OnApproveKyc(MemberProfileMasterView row)
        {
            var user = CurrentUser;
            ApproveModel = new ApproveKYCCommand();
            //ApproveModel.ApprovalId = row.ApprovalId;
            ApproveModel.ApprovedBy = user.FindFirstValue(ClaimTypes.Sid);
            ApproveModel.MemberProfileId = row.Id;

            await onApprove();
        }

        public async Task OnViewRow(MemberProfileMasterView row)
        {
            _navigationManager.NavigateTo($"/viewCompletedKyc/{row.ApplicationUserId}");
        }

        private async Task OnRefresh()
        {
            EnrolmentFIlter = nameof(MemberProfileStatus.AWAITING_KYC_APPROVAL);
            statusFilter = new WhereFilter
            {
                Field = nameof(MemberProfileMasterView.Status),
                Operator = "equal",
                value = EnrolmentFIlter
            };
            QueryGrid = new Query().Where(statusFilter);
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

        private async Task OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                WhereFilter roleFilter = new WhereFilter
                {
                    Field = nameof(MemberProfileMasterView.FirstName),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter nameFilter = new WhereFilter
                {
                    Field = "RetireeNumber",
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter descriptionFilter = new WhereFilter
                {
                    Field = "FirstName",
                    Operator = "contains",
                    value = searchText
                };


                QueryGrid = new Query().Where(nameFilter.Or(roleFilter).Or(descriptionFilter));
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
    }
}