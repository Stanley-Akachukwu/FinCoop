using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Payroll;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Text.Json;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionScheduleItems;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionScheduleItems;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.DataMigration;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionItems;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Payroll
{


    public partial class ImportFromCNL
    {
        bool showUploadDataModal { get; set; } = false;
        bool showSuccessModal { get; set; } = false;
        bool showPreviewDrawer { get; set; } = false;
        bool showSuccessGrid { get; set; } = false;
        bool showErrorGrid { get; set; } = false;

        [Parameter]
        public bool showAlertComponent { get; set; } = false;
        [Parameter]
        public string schedulename { get; set; }
        

        public CreateMemberBulkUploadCommand createUploadCommand { get; set; } = new CreateMemberBulkUploadCommand();

        [Parameter]
        public ImportPayrollDeductionItemCommand uploadDataModel { get; set; }

        [Parameter]
        public List<PayrollDeductionItemViewModel> UploadResponseModel { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;
        DataMigrationPreviewModal dataMigrationPreviewModal;
        Drawer previewDrawer;
        [Inject]
        ILogger<PayrollDeductionScheduleItemGrid> Logger { get; set; }



        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Parameter]
        public string scheduleId { get; set; }


        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        //[Inject]
        //SessionService SessionService { get; set; }



        SfGrid<PayrollDeductionItemMasterView> grid;

       // string GRID_API_RESOURCE => $"{Config.API_HOST}/{nameof(PayrollDeductionItemMasterView)}/'{scheduleId}'";

        //string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(PayrollDeductionItemMasterView)}";

        public List<PayrollDeductionItemMasterView> PayrollDeductionScheduleItemMasterViewSrc1 = new List<PayrollDeductionItemMasterView>();
        public List<PayrollDeductionItemMasterView> PayrollDeductionScheduleItemMasterViewSrc = new List<PayrollDeductionItemMasterView>();


        private Query QueryGrid;// = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {


                await OnRefresh();

            }
            //StateHasChanged();

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");

        }


        private async Task removeHyphen()
        {
            schedulename = schedulename.Replace("-", " ");
        }

        protected override async Task OnInitializedAsync()
        {
            await removeHyphen();
            QueryGrid = new Query();
            await GetPayrollDeductionScheduleItemMasterView();
         }
        async Task onUpload()
        {
            showUploadDataModal = true;
        }
        public async Task GetPayrollDeductionScheduleItemMasterView()
        {
            //var rsp = await DataService.GetValue<List<PayrollDeductionScheduleItemMasterView>>(
            //    nameof(PayrollDeductionScheduleItemMasterView), "matchPayroll", scheduleId);
            var rsp = await DataService.GetAllValue<List<PayrollDeductionItemMasterView>>($"PayrollDeductionItemMasterView/{scheduleId}");
            if (rsp.IsSuccessStatusCode)
            {
                PayrollDeductionScheduleItemMasterViewSrc = new List<PayrollDeductionItemMasterView>();

                PayrollDeductionScheduleItemMasterViewSrc1 =
                    JsonSerializer.Deserialize<List<PayrollDeductionItemMasterView>>(rsp.Content.ToJson());
                PayrollDeductionScheduleItemMasterViewSrc = PayrollDeductionScheduleItemMasterViewSrc1
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
        private async Task OnUploadDataChangedHandler(ImportPayrollDeductionItemCommand memberBulkUploadViewModel)
        {
            uploadDataModel = memberBulkUploadViewModel;
            if (uploadDataModel != null)
            {
                showUploadDataModal = false;
                showPreviewDrawer = true;
                showAlertComponent = false;
                UploadResponseModel = uploadDataModel.PayrollDeductionItems;
                await InvokeAsync(StateHasChanged); // trigger a UI refresh
            }
        }

        public void ActionCompletedHandler(ActionEventArgs<PayrollDeductionItemMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        public void ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = args.Error.ToString();
            StateHasChanged();

        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "gridPayrollDeductionScheduleItem_excelexport") //Id is combination of Grid's ID and itemname
            {
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Import_fromCNLExport.xlsx";
                await this.grid.ExportToExcelAsync(ExportProperties);
            }

            if (args.Item.Id == "gridPayrollDeductionScheduleItem_pdfexport") //Id is combination of Grid's ID and itemname
            {
                PdfExportProperties ExportProperties = new PdfExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Import_fromCNLExport.pdf";
                await this.grid.ExportToPdfAsync();
            }
        }



        private async Task OnRefresh()
        {

            //Console.WriteLine($"OnRefresh(), searchText->{searchText}");

            searchText = string.Empty;
            QueryGrid = new Query();
            grid.Refresh();

            await Task.CompletedTask;

        }

        private async Task OnSearchEnter(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            //Console.WriteLine($"OnSearchEnter, searchText->{searchText}");

            //Console.WriteLine($"code->{e.Code}, key-> {e.Key}");

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

            //Console.WriteLine($"OnSearch, searchText->{searchText}");

            if (!string.IsNullOrWhiteSpace(searchText))
            {

                WhereFilter roleFilter = new WhereFilter
                {
                    Field = nameof(PayrollDeductionItemMasterView.MemberId),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter nameFilter = new WhereFilter
                {
					Field = nameof(PayrollDeductionItemMasterView.MemberName),
					Operator = "contains",
                    value = searchText
                };

                WhereFilter descriptionFilter = new WhereFilter
                {
					Field = nameof(PayrollDeductionItemMasterView.PayrollCode),
					Operator = "contains",
                    value = searchText
                };


                QueryGrid = new Query().Where(nameFilter.Or(roleFilter).Or(descriptionFilter));

                //this.grid.Refresh();
            }
            else
            {
                await OnRefresh();
            }

        }

        private async Task OnClearSearch()
        {
            //Console.WriteLine($"OnClearSearch, searchText->{searchText}");

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                await OnRefresh();

            }

        }


    }


}



