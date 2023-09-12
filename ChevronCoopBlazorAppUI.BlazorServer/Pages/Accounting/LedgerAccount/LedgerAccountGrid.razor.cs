using AntDesign;
using AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounting.LedgerAccount
{
    public partial class LedgerAccountGrid
    {
        [Inject]
        ILogger<LedgerAccountGrid> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        ModalService modalService { get; set; }

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


        SfGrid<LedgerAccountMasterView> grid;

        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(LedgerAccountMasterView)}";


        private Query QueryGrid; // = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;

        private List<LedgerAccountMasterView> _LedgerAccountMasterView { get; set; } = new List<LedgerAccountMasterView>();

        private List<LedgerAccountMasterView> _LedgerAccountMasterViewSrc { get; set; } = new List<LedgerAccountMasterView>();

        CreateLedgerAccountCommand CreateModel { get; set; }
        UpdateLedgerAccountCommand UpdateModel { get; set; }
        DeleteLedgerAccountCommand DeleteModel { get; set; }

        LedgerAccountCreateForm createForm;
        Drawer createDrawer;
        bool showCreateDrawer { get; set; } = false;

        LedgerAccountEditForm editForm;
        Drawer editDrawer;
        bool showEditDrawer { get; set; } = false;


        string editFormActiveTabKey = "1";

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;

        [Inject]
        private IMasterViews _MasterView { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                createDrawer.Width = (int)(BrowserDimension.Width * 0.40);
                editDrawer.Width = (int)(BrowserDimension.Width * 0.40);

                await OnRefresh();
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        protected override async Task OnInitializedAsync()
        {
            //Console.WriteLine($"API HOST->{Config.ODATA_VIEWS_HOST}, ODATA HOST-> {GRID_API_RESOURCE}");

            CreateModel = new CreateLedgerAccountCommand
            {
                UOM = LedgerBalanceUOM.CURRENCY.ToString(),
                AccountType = COAType.ASSET.ToString(),
                IsClosed = false,
            };

            UpdateModel = new UpdateLedgerAccountCommand();
            DeleteModel = new DeleteLedgerAccountCommand();

            _LedgerAccountMasterViewSrc = await _MasterView.GetCustomMasterViewEntityAllFields<LedgerAccountMasterView>(nameof(LedgerAccountMasterView));
            _LedgerAccountMasterView = _LedgerAccountMasterViewSrc.OrderByDescending(dt => dt.DateCreated).ToList();

        }

        public void ActionCompletedHandler(ActionEventArgs<LedgerAccountMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }
        public void ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            ErrorDetails = args.Error.ToString();
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
            CreateModel = new CreateLedgerAccountCommand();
            await onCreate();
        }


        private async Task OnViewRow(LedgerAccountMasterView row)
        {
        }

        private async Task OnEditRow(LedgerAccountMasterView row)
        {
            UpdateModel = Mapper.Map<UpdateLedgerAccountCommand>(row);

            await onEdit();
        }


        private async Task OnDeleteRow(LedgerAccountMasterView row)
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

                var rsp = await DataService.Delete<DeleteLedgerAccountCommand, CommandResult<string>>(
                    nameof(LedgerAccount), DeleteModel);


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

       
        private async Task OnSearch(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                _LedgerAccountMasterView = _LedgerAccountMasterViewSrc.OrderByDescending(c => c.DateCreated).ToList();
            }
            else
            {
                _LedgerAccountMasterView = _LedgerAccountMasterViewSrc
                    .Where(c => (c.Description?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.Code?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ??
                                    false)
                                || (c.Name?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ??
                                    false)
                                || (c.LedgerBalance.ToString().Contains(inputValue, StringComparison.OrdinalIgnoreCase))
                                || (c.AvailableBalance.ToString().Contains(inputValue, StringComparison.OrdinalIgnoreCase))
                                || (c.AccountType.ToString()
                                    .Contains(inputValue, StringComparison.OrdinalIgnoreCase))
                                || (c.CurrencyId_Code.ToString().Contains(inputValue, StringComparison.OrdinalIgnoreCase)))
                    .Take(10)
                    .OrderByDescending(c => c.DateCreated)
                    .ToList();
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