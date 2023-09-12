using AntDesign;
using AP.ChevronCoop.AppDomain.Payroll.PayrollCronJobConfigs;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.DepositCronJobConfigurations;
using AP.ChevronCoop.Entities.Payroll.PayrollCronJobConfigurations;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;
using Query = Syncfusion.Blazor.Data.Query;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Schedule.Job_Processing
{
    public partial class JobProcessingGrid
    {
        private FluentValidationValidator? _fluentValidationValidator;
        private Query Query_Combo;
        bool showPopup = false;

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        ILogger<JobProcessingGrid> Logger { get; set; }

        bool DeletePopUp { get; set; } = false;
        bool showAddDrawer { get; set; } = false;
        bool showEditDrawer { get; set; } = false;

        Drawer addDrawer;
        BrowserDimension BrowserDimension;
        Drawer editDrawer;
        private bool HasRecords { get; set; } = false;

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }


        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        SfGrid<PayrollCronJobConfigMasterView> grid;


        private Query QueryGrid;

        string SubmitButtonText = "Submit";
        string DeleteButtonText = "Yes, I'm sure";

        [Inject] IClientAuditService _auditLogService { get; set; }
        public string ApplicationUserId { get; set; }
        public List<PayrollCronJobConfigMasterView> PayrollCronJobConfigMasterViewSrc1 = new List<PayrollCronJobConfigMasterView>();
        public List<PayrollCronJobConfigMasterView> PayrollCronJobConfigMasterViewSrc = new List<PayrollCronJobConfigMasterView>();

        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(PayrollCronJobConfigMasterView)}?scheduleId={scheduleId}";
        public List<string> CronJobTypes { get; set; } = new List<string>();
        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();
        string searchText;
        [Parameter]
        public string filter { get; set; }

        [Parameter]
        public string scheduleId { get; set; }

        [Parameter]
        public string schedulename { get; set; }
        
        string ErrorDetails = "";

        ErrorDetails errors;
        private WhereFilter statusFilter { get; set; }
        string Active { get; set; } = "text-blue-600 border-b-2 border-blue-600 active";
        string Inactive { get; set; } = "border-b-2 border-transparent";
        string All { get; set; } = string.Empty;
        string Complete { get; set; } = string.Empty;
        string Failed { get; set; } = string.Empty;
        string Started { get; set; } = string.Empty;
        string Pending { get; set; } = string.Empty;
        public CreateJobDTO Model { get; set; }
        [Parameter]
        public EventCallback<CreateJobDTO> ModelChanged { get; set; }
        public CreatePayrollCronJobConfigCommand Command { get; set; }
        public UpdatePayrollCronJobConfigCommand UpdateCommand { get; set; }
        public CreateJobDTO UpdateModel { get; set; }
        private async Task removeHyphen()
        {
            schedulename = schedulename.Replace("-", " ")+ " - Job Processing";
        }

        protected override async Task OnInitializedAsync()
        {
            await removeHyphen();
            ActivateTab();
            All = Active;
            Command = new CreatePayrollCronJobConfigCommand();
            UpdateCommand = new UpdatePayrollCronJobConfigCommand();
            Model = new CreateJobDTO();
            UpdateModel = new CreateJobDTO();
            QueryGrid = new Query();
           // await ExecuteFilteringAsync();
            await GetCurrentUser();
            LoadDropDown();
            await GetPayrollCronJobConfigMasterView();

        }

        private void NavigateToComponent(PayrollCronJobConfigMasterView param)
        {
            //        var query = new Dictionary<string, string> {
            //    { "schedulename", param.DeductionScheduleId_ScheduleName },
            //            { "desc",param.Description}
            //};
           var schedulenameParam = param.DeductionScheduleId_ScheduleName + ";" + param.Description;
          var replace =  schedulenameParam.Replace(" ", "-");

           var replace2 = replace.Replace("/", "_");

            _navigationManager.NavigateTo($"/payroll/PayrollDeductionScheduleItem/{param.Id}/{replace2}/");
        }
        private string removeSpaces(string name)
        {
            return name.Replace(" ", "-");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();
            }

            //if (addDrawer != null)
            //    addDrawer.Width = (int)(BrowserDimension.Width * 0.50);
            //if (editDrawer != null)
            //    editDrawer.Width = (int)(BrowserDimension.Width * 0.50);
            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public void Save()
        {
            ApplicationUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }
        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }
       
        public void LoadDropDown()
        {

            CronJobTypes = System.Enum.GetNames(typeof(CronJobType)).ToList();
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
        public void ActionCompletedHandler(ActionEventArgs<PayrollCronJobConfigMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            //this.ErrorDetailMsg = args.Error.ToString();
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }
        async Task onFiltering(string filter)
        {
            ActivateTab();
            if (filter == CronJobStatus.PENDING.ToString())
            {
                Pending = Active;
            }
            else if (filter == CronJobStatus.STARTED.ToString())
            {
                Started = Active;
            }
            else if (filter == CronJobStatus.COMPLETED.ToString())
            {
                Complete = Active;
            }
            else if (filter == CronJobStatus.FAILED.ToString())
            {
                Failed = Active;
            }
            else
            {
                All = Active;
            }
        

            if (filter == "all")
            {   
                PayrollCronJobConfigMasterViewSrc = PayrollCronJobConfigMasterViewSrc1
                    .OrderByDescending(c => c.DateCreated).ToList();
            }
            else
            {
                PayrollCronJobConfigMasterViewSrc = PayrollCronJobConfigMasterViewSrc1
                    .Where(c => c.JobStatus == filter).ToList();
            }
            await InvokeAsync(StateHasChanged);

            // _navigationManager.NavigateTo($"/schedule/jobsProcessing/{filter}/{scheduleId}", forceLoad: true);
        }
        public void ActivateTab()
        {
            All = Inactive;
            Complete = Inactive;
            Pending = Inactive;
            Failed = Inactive;
            Started = Inactive; 
        }
        public async Task GetPayrollCronJobConfigMasterView()
        { 
            var rsp = await DataService.GetValue<List<PayrollCronJobConfigMasterView>>(
                nameof(PayrollCronJobConfigMasterView), "deductionScheduleId", scheduleId);
            if (rsp.IsSuccessStatusCode)
            {
                PayrollCronJobConfigMasterViewSrc = new List<PayrollCronJobConfigMasterView>();

                PayrollCronJobConfigMasterViewSrc1 =
                    JsonSerializer.Deserialize<List<PayrollCronJobConfigMasterView>>(rsp.Content.ToJson());
                PayrollCronJobConfigMasterViewSrc = PayrollCronJobConfigMasterViewSrc1
                    .OrderByDescending(c => c.DateCreated).ToList();
                // ActivateTab();
            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = $"Error connecting to server {rsp.Error} - {rsp.Content} - {rsp.RequestMessage}",
                    NotificationType = NotificationType.Error
                });
            }
        }
        private async Task SearchFiltering()
        {
           
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    PayrollCronJobConfigMasterViewSrc = PayrollCronJobConfigMasterViewSrc1
                        .OrderByDescending(c => c.DateCreated).ToList();
                }
                else
                {
                    PayrollCronJobConfigMasterViewSrc = PayrollCronJobConfigMasterViewSrc1
                        .Where(c => (c.CronJobType?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false)
                                    || (c.JobStatus?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false)
                                   || (c.JobName?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false)
                                   || (c.DeductionScheduleId_ScheduleName?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false))
                        .OrderByDescending(c => c.DateCreated)
                        .ToList();
                }
          
        }
        //private async Task ExecuteFilteringAsync()
        //{
        //    switch (filter)
        //    {
        //        case nameof(CronJobStatus.COMPLETED):
        //            statusFilter = new WhereFilter
        //            {
        //                Field = nameof(PayrollCronJobConfigMasterView.JobStatus),
        //                Operator = "equal",
        //                value = filter
        //            };
        //            QueryGrid = new Query().Where(
        //                statusFilter);
        //            ActivateTab();
        //            Complete = Active;
        //            break;
        //        case nameof(CronJobStatus.STARTED):
        //            statusFilter = new WhereFilter
        //            {
        //                Field = nameof(PayrollCronJobConfigMasterView.JobStatus),
        //                Operator = "equal",
        //                value = filter
        //            };
        //            QueryGrid = new Query().Where(
        //                statusFilter);
        //            ActivateTab();
        //            Started = Active;
        //            break;
        //        case nameof(CronJobStatus.PENDING):
        //            statusFilter = new WhereFilter
        //            {
        //                Field = nameof(PayrollCronJobConfigMasterView.JobStatus),
        //                Operator = "equal",
        //                value = filter
        //            };
        //            QueryGrid = new Query().Where(
        //                statusFilter);
        //            ActivateTab();
        //            Pending = Active;
        //            break;
        //        case nameof(CronJobStatus.FAILED):
        //            statusFilter = new WhereFilter
        //            {
        //                Field = nameof(PayrollCronJobConfigMasterView.JobStatus),
        //                Operator = "equal",
        //                value = filter
        //            };
        //            QueryGrid = new Query().Where(
        //                  statusFilter);
        //            ActivateTab();
        //            Failed = Active;
        //            break;
        //        default:
        //        case "all":
        //            QueryGrid = new Query();
        //            ActivateTab();
        //            All = Active;
        //            break;
        //    }

        //}

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
          //  await ExecuteFilteringAsync();
            grid.Refresh();

            await Task.CompletedTask;

        }    
    
     
    }
}