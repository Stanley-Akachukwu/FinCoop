using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Commons;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.DataMigration
{
    public partial class DataMigrationGridForm
    {
        bool showUploadDataModal { get; set; } = false;
        bool showSuccessModal { get; set; } = false;
        bool showPreviewDrawer { get; set; } = false;
        bool showSuccessGrid { get; set; } = false;
        bool showErrorGrid { get; set; } = false;

        [Parameter]
        public bool showAlertComponent { get; set; } = false;

        public CreateMemberBulkUploadCommand createUploadCommand { get; set; } = new CreateMemberBulkUploadCommand();

        [Parameter]
        public MemberBulkUploadViewModel uploadDataModel { get; set; }

        [Parameter]
        public List<MemberDataUpload> UploadResponseModel { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;
        DataMigrationPreviewModal dataMigrationPreviewModal;
        Drawer previewDrawer;
        SfGrid<MemberDataUpload> grid;
        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;

        [Parameter]
        public EventCallback<MemberBulkUploadViewModel> OnUploadDataChanged { get; set; }

        [Inject]

        IWebHostEnvironment Environment { get; set; }

        async Task onUpload()
        {
            showUploadDataModal = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                previewDrawer.Width = (int)(BrowserDimension.Width * 0.50);
                //await OnRefresh();
            }
            //StateHasChanged();

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        protected override async Task OnInitializedAsync()
        {
            UploadResponseModel = new List<MemberDataUpload>();
        }

        private async Task OnUploadDataChangedHandler(MemberBulkUploadViewModel memberBulkUploadViewModel)
        {
            uploadDataModel = memberBulkUploadViewModel;
            if (uploadDataModel != null)
            {
                showUploadDataModal = false;
                showPreviewDrawer = true;
                showAlertComponent = false;
                UploadResponseModel = uploadDataModel.AcceptedMemberDataUpload;
                await InvokeAsync(StateHasChanged); // trigger a UI refresh
            }
        }

        private async Task OnSuccessfulCommitChangedHandler(bool success)
        {
            if (success)
            {
                showAlertComponent = true;

                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task OnRefresh()
        {
            grid.Refresh();
            await Task.CompletedTask;
        }

        async Task onPreviewDone()
        {
            showPreviewDrawer = false;
        }

        private async Task DownloadDataMigrationFileTemplate()
        {
            var fileName = "chevron_data_migration.xlsx";
            var path = Environment.ContentRootPath + "\\StaticFiles\\" + fileName;
            await jsRuntime.InvokeVoidAsync("DownloadDataMigrationFileTemplate", fileName, path);
        }
    }
}