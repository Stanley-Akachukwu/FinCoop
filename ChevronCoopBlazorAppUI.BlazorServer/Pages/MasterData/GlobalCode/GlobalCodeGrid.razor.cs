using AntDesign;
using AP.ChevronCoop.AppDomain.MasterData.GlobalCodes;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.MasterData.GlobalCodes;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.MasterData.GlobalCode
{
    public partial class GlobalCodeGrid
    {
        [Inject]
        ILogger<GlobalCodeGrid> Logger { get; set; }

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

        //[Inject]
        //SessionService SessionService { get; set; }


        SfGrid<GlobalCodeMasterView> grid;

        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(GlobalCodeMasterView)}";


        private Query QueryGrid; // = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;


        CreateGlobalCodeCommand CreateModel { get; set; }
        UpdateGlobalCodeCommand UpdateModel { get; set; }
        DeleteGlobalCodeCommand DeleteModel { get; set; }

        GlobalCodeCreateForm createForm;
        Drawer createDrawer;
        bool showCreateDrawer { get; set; } = false;

        GlobalCodeEditForm editForm;
        Drawer editDrawer;
        bool showEditDrawer { get; set; } = false;


        string editFormActiveTabKey = "1";

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                createDrawer.Width = (int)(BrowserDimension.Width * 0.40);
                editDrawer.Width = (int)(BrowserDimension.Width * 0.40);

                await OnRefresh();
            }
            //StateHasChanged();

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        protected override async Task OnInitializedAsync()
        {
            //Console.WriteLine($"API HOST->{Config.ODATA_VIEWS_HOST}, ODATA HOST-> {GRID_API_RESOURCE}");


            CreateModel = new CreateGlobalCodeCommand();

            UpdateModel = new UpdateGlobalCodeCommand();

            DeleteModel = new DeleteGlobalCodeCommand();

            QueryGrid = new Query();
        }


        public void ActionCompletedHandler(ActionEventArgs<GlobalCodeMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        public void ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = args.Error.ToString();
            StateHasChanged();
        }


        async Task onCreate()
        {
            showCreateDrawer = true;
        }

        async Task onCreateDone()
        {
            showCreateDrawer = false;
            //createForm.Errors = null;
        }

        async Task onEdit()
        {
            showEditDrawer = true;
        }

        async Task onEditDone()
        {
            showEditDrawer = false;
            // editForm.Errors = null;
        }


        private async Task OnAddRow()
        {
            CreateModel = new CreateGlobalCodeCommand();
            await onCreate();
        }


        private async Task OnViewRow(GlobalCodeMasterView row)
        {
        }

        private async Task OnEditRow(GlobalCodeMasterView row)
        {
            UpdateModel = Mapper.Map<UpdateGlobalCodeCommand>(row);

            await onEdit();
        }


        private async Task OnDeleteRow(GlobalCodeMasterView row)
        {
            bool isOk = false;

            isOk = await modalService.ConfirmAsync(new ConfirmOptions()
            {
                Title = "Are you sure?",
                //Icon = icon,
                Content = "You will not be able to recover this record after deleting!",
            });


            if (isOk)
            {
                DeleteModel.Id = row.Id;
                //DeleteModel.DeleteKey = EntityUtils.GenerateSequentialId().ToString(); ;

                var rsp = await DataService.Delete<DeleteGlobalCodeCommand, CommandResult<string>>(nameof(GlobalCode),
                    DeleteModel);


                if (!rsp.IsSuccessStatusCode)
                {
                    //Console.WriteLine($"rsp content->{rsp.Content}");
                    //Console.WriteLine($"error->{rsp.Error.Content}");

                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    //Console.WriteLine($"error content->{rspContent}");
                    //Console.WriteLine($"error msg->{rspContent.Response}");

                    var msg = rspContent?.Response;
                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        msg = rspContent.ValidationErrors[0].Error;
                    }


                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error deleting record",
                        Description = msg,
                        NotificationType = NotificationType.Error
                    });
                }
                else
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Deleted",
                        Description = rsp.Content.Response, //"Record deleted.",
                        NotificationType = NotificationType.Success
                    });
                    await OnRefresh();
                }
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
                    Field = nameof(GlobalCodeMasterView.Code),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter nameFilter = new WhereFilter
                {
                    Field = "Name",
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter descriptionFilter = new WhereFilter
                {
                    Field = "Description",
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