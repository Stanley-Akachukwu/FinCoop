using AntDesign;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Audit
{
    public partial class AuditTrail2
    {
        string ErrorDetails = "";

        protected DateTime startDate { get; set; }

        protected DateTime endDate { get; set; }

        string searchText;

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        protected List<AuditTrailMasterViewResult> AuditTrailList { get; set; }
        protected AuditTrailMasterViewResult[] AuditTrails { get; set; }

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();

        string viewFormActiveTabKey = "1";

        AuditTrailMasterViewResult ViewModel { get; set; }

        AuditViewForm2 viewForm;
        Drawer viewDrawer;

        bool showViewDrawer { get; set; } = false;

        [Inject]
        IEntityDataService DataService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            endDate = DateTime.Now;
            startDate = DateTime.Now.AddDays(-5); //five days ago set as start date
            ViewModel = new AuditTrailMasterViewResult();
            AuditTrailList = new List<AuditTrailMasterViewResult>();

            await ReloadGridAsync();
        }

        private async Task ReloadGridAsync()
        {
            var rsp = await DataService.GetAuditTrailMasterView<IEnumerable<AuditTrailMasterViewResult>>();

            if (rsp.IsSuccessStatusCode)
            {
                IEnumerable<AuditTrailMasterViewResult> returnedAuditTrails = rsp.Content;

                if (returnedAuditTrails.Any())
                {
                    AuditTrailList = returnedAuditTrails.ToList();

                    AuditTrails = AuditTrailList.ToArray();
                }
            }
        }

        async Task onViewDone()
        {
            showViewDrawer = false;
            // editForm.Errors = null;
        }

        private async Task OnViewRow(AuditTrailMasterViewResult row)
        {
            //UpdateModel = Mapper.Map<UpdateApplicationRoleCommand>(row);

            ViewModel = row;


            await onView();
        }

        async Task onView()
        {
            showViewDrawer = true;
        }

        private async Task SearchByDate()
        {
            if (endDate < startDate)
            {
                this.ErrorDetails = "End Date Can Not Be Before Start Date! Please Correct This!";
                StateHasChanged();
                await Task.CompletedTask;
                return;
            }
            else
            {
                await ReloadGridAsync(startDate, endDate);
            }
        }

        private async Task ReloadGridAsync(DateTime startDate, DateTime endDate)
        {
            var rsp = await DataService.GetAuditTrailMasterView<IEnumerable<AuditTrailMasterViewResult>>();

            if (rsp.IsSuccessStatusCode)
            {
                IEnumerable<AuditTrailMasterViewResult> returnedAuditTrails = rsp.Content;


                if (returnedAuditTrails.Any())
                {
                    var filteredList = returnedAuditTrails
                        .Where(p => p.dateCreated >= startDate && p.dateCreated <= endDate)
                        .OrderByDescending(p => p.dateCreated).ToList();
                    AuditTrailList = filteredList;

                    AuditTrails = AuditTrailList.ToArray();
                }
                else
                {
                    AuditTrailList = new List<AuditTrailMasterViewResult>();
                    AuditTrails = AuditTrailList.ToArray();
                }
            }
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
            if (string.IsNullOrEmpty(searchText))
            {
                await ReloadGridAsync();
            }
            else
            {
                await ReloadGridAsync(searchText);
            }
        }

        private async Task ReloadGridAsync(string searchParam)
        {
            var rsp = await DataService.GetAuditTrailMasterView<IEnumerable<AuditTrailMasterViewResult>>();

            if (rsp.IsSuccessStatusCode)
            {
                IEnumerable<AuditTrailMasterViewResult> returnedAuditTrails = rsp.Content;

                returnedAuditTrails = await EliminateNullsFromSearchColumns(returnedAuditTrails);

                if (returnedAuditTrails.Any())
                {
                    searchParam = searchParam.ToLower().Trim();
                    object searchParamObject = searchParam;
                    var filteredList = returnedAuditTrails.Where(p =>
                        p.action.ToLower() == searchParam || p.applicationUserId_Email == searchParam ||
                        p.ipAddress == searchParamObject).ToList();
                    if (filteredList.Any())
                    {
                        AuditTrailList = filteredList.ToList();

                        AuditTrails = AuditTrailList.ToArray();
                    }
                    else
                    {
                        AuditTrailList = new List<AuditTrailMasterViewResult>();
                        AuditTrails = AuditTrailList.ToArray();
                    }
                }
            }
        }

        private async Task<IEnumerable<AuditTrailMasterViewResult>> EliminateNullsFromSearchColumns(
            IEnumerable<AuditTrailMasterViewResult> returnedAuditTrails)
        {
            var list = new List<AuditTrailMasterViewResult>();
            if (returnedAuditTrails.Any())
            {
                foreach (var auditTrail in returnedAuditTrails)
                {
                    if (string.IsNullOrWhiteSpace(auditTrail.action))
                    {
                        auditTrail.action = "";
                    }

                    if (string.IsNullOrWhiteSpace(auditTrail.module))
                    {
                        auditTrail.module = "";
                    }

                    if (auditTrail.ipAddress == null)
                    {
                        auditTrail.ipAddress = "";
                    }

                    if (string.IsNullOrWhiteSpace(auditTrail.applicationUserId_Email))
                    {
                        auditTrail.applicationUserId_Email = "";
                    }

                    list.Add(auditTrail);
                }
            }

            return await Task.FromResult(list);
        }

        private async Task OnRefresh()
        {
        }

        public void ActionCompletedHandler(ActionEventArgs<AuditTrailMasterViewResult> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            //this.ErrorDetailMsg = args.Error.ToString();
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }
    }
}