using AP.ChevronCoop.Entities.MasterData.Departments;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals.ApprovalGroups
{
    public partial class AddGroupMemberDrawerForm
    {
        string notificationText;
        bool showPopup = false;

        [Inject]
        private IMasterViews _MasterView { get; set; }
        [Parameter]
        public SelectedMemberApprovalGroup Model { get; set; }

        [Parameter]
        public EventCallback<SelectedMemberApprovalGroup> ModelChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        private Query Query_Combo;
        private Query Query_Dept_Combo;

        [Inject]
        WebConfigHelper Config { get; set; }

        public string getDepartment_Url { get; set; }
        public string getMemebrs_User_Url { get; set; }


        bool hasSelectedDepartment = true;
        bool hasSelectedMember = false;
        public List<DepartmentMasterView> Departments = new List<DepartmentMasterView>();
        public List<MemberProfileMasterView> MembersByDepartment = new List<MemberProfileMasterView>();

        [Parameter]
        public Func<SelectedMemberApprovalGroup, Task> AddMemberToList { get; set; }

        public string DepartmentId { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            //hasSelectedDepartment = true;
            Model = new SelectedMemberApprovalGroup();
            getDepartment_Url = $"{Config.ODATA_VIEWS_HOST}/{nameof(DepartmentMasterView)}";
            getMemebrs_User_Url = $"{Config.ODATA_VIEWS_HOST}/{nameof(MemberProfileMasterView)}";

          //  await GetDepartments();
            await GetMemberProfiles();
        }

        //private async Task GetDepartments()
        //{
        //    try
        //    {
        //        var rsp = await DataService.GetDepartmentMasterView<List<DepartmentMasterView>>();
        //        Departments = rsp.Content;
        //    }
        //    catch (Exception exp)
        //    {
        //    }
        //}

        private async Task GetMemberProfiles()
        {
            try
            {
                var Customers = new List<MemberProfileMasterView>();



                Customers = await _MasterView.GetCustomMasterVieWithBoolean<MemberProfileMasterView>(nameof(MemberProfileMasterView), "applicationUserId_IsAdmin", "true", DatabaseFields.MemberProfileFields2);

                MembersByDepartment = Customers.ToList();
            }
            catch (Exception exp)
            {
            }
        }

        //private async Task OnDeptValueSelectedhandler(SelectEventArgs<DepartmentMasterView> args)
        //{
        //    DepartmentId = args.ItemData.Id;
        //    WhereFilter deptFilter = new WhereFilter
        //    {
        //        Field = nameof(MemberProfileMasterView.DepartmentId),
        //        Operator = "equal",
        //        value = DepartmentId
        //    };

        //    Query_Dept_Combo = new Query().Where(deptFilter);
        //    hasSelectedDepartment = true;
        //    await OnRefresh();
        //    StateHasChanged();
        //}

        private async Task OnMemberValueSelectedhandler(SelectEventArgs<MemberProfileMasterView> args)
        {
            Model = new SelectedMemberApprovalGroup();
            Model.Email = args.ItemData.PrimaryEmail;
            Model.MemberName = args.ItemData.FullName;
            Model.MembershipId = args.ItemData.MembershipId;
            Model.CustomerId = args.ItemData.Id;
            Model.ApplicationUserId = args.ItemData.ApplicationUserId;
            hasSelectedMember = true;
            StateHasChanged();
        }

        public async Task OnClickAddMember()
        {
            ShowModal = false;
            hasSelectedDepartment = true;
            hasSelectedMember = false;
            await AddMemberToList.Invoke(Model);
            await ShowModalChanged.InvokeAsync(ShowModal);
            await ModelChanged.InvokeAsync(Model);
        }


        public async Task OnCancel()
        {
            hasSelectedDepartment = true;
            hasSelectedMember = false;
            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);
            Model = new SelectedMemberApprovalGroup();
            await ModelChanged.InvokeAsync(Model);
            showPopup = false;
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