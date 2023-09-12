using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalGroups;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.SessionStorage;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using System.Net.Http.Headers;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals.Approvalworkflows
{
    public partial class EditGroupDrawerForm
    {
        [Parameter]
        public GroupToApproveViewModel Model { get; set; } = new GroupToApproveViewModel();


        [Inject]
        WebConfigHelper Config { get; set; }

        [Parameter]
        public EventCallback<GroupToApproveViewModel> ModelChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Parameter]
        public Func<GroupToApproveViewModel, Task> AddGroupToList { get; set; }

        string APPROVE_ALL_GROUPS => $"{Config.API_HOST}/ApprovalGroup/fetchAll";
        public List<ApprovalGroupViewModel> allGroups { get; set; } = new List<ApprovalGroupViewModel>();

        [Inject]
        ISessionStorageService _sessionStorage { get; set; }

        public string ApprovalWorkflowType { get; set; }
        public ApprovalGroupViewModel approvalGroupViewModel { get; set; } = new ApprovalGroupViewModel();

        string APPROVE_API_RESOURCE =>
            $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalGroupMasterView)}?$orderby=DateCreated desc";

        public List<ApprovalGroupMasterView> ApprovalGroups = new List<ApprovalGroupMasterView>();
        public List<GroupToApproveViewModel> selectedGroups { get; set; } = new List<GroupToApproveViewModel>();

        [Inject]
        NotificationService notificationService { get; set; }

        public List<ApprovalWorkflowViewModel> Workflows { get; set; } = new List<ApprovalWorkflowViewModel>();

        [Inject]
        IEntityDataService DataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await LoadApprovalGroups();
        }

        private async Task LoadApprovalGroups()
        {
            try
            {
                var rsp = await DataService.GetApprovalGroupMasterView<List<ApprovalGroupMasterView>>();
                ApprovalGroups = rsp.Content;
            }
            catch (Exception exp)
            {
            }
        }

        private async Task OnGroupValueSelectedhandler(SelectEventArgs<ApprovalGroupMasterView> args)
        {
            var group = new GroupToApproveViewModel
            {
                GroupName = args.ItemData.Name,
                Id = args.ItemData.Id,
                NumberOfApprovers = 0
            };

            Model.Id = group.Id;
            Model.GroupName = args.ItemData.Name;
            //Model.RequiredApprovers = args.ItemData.RequiredApprovers;
            selectedGroups.Add(Model);
            StateHasChanged();
        }

        public async Task OnCancel()
        {
            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);
            Model = new GroupToApproveViewModel();
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnClickAddGroup()
        {
            ShowModal = false;
            await AddGroupToList.Invoke(Model);
            await ShowModalChanged.InvokeAsync(ShowModal);
            await ModelChanged.InvokeAsync(Model);
        }

        private async Task OnRefresh()
        {
            await Task.CompletedTask;
        }

        public async Task OnInput(ChangeEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnChange(ChangeEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        private async Task onFocus(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }
    }
}