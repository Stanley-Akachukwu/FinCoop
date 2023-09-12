using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Shared;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting;
using AP.ChevronCoop.Entities.Documents;
using AP.ChevronCoop.Entities.Loans;
using AP.ChevronCoop.Entities.MasterData;
using AP.ChevronCoop.AppDomain;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Buttons;
using Syncfusion.Blazor.SplitButtons;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor.Calendars;
using BlazorPro.Spinkit;
using Blazored.FluentValidation;
using AntDesign;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.Commons;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounting.JournalEntry;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using AP.ChevronCoop.AppDomain.Accounting.TransactionJournals;
using AP.ChevronCoop.Entities.Accounting.JournalEntries;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using ChangeEventArgs = Microsoft.AspNetCore.Components.ChangeEventArgs;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounting.TransactionJournal
{
    public partial class TransactionJournalGrid
    {
        [Inject]
        ILogger<TransactionJournalGrid> Logger { get; set; }

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


        SfGrid<TransactionJournalMasterView> grid;

        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(TransactionJournalMasterView)}";


        private Query QueryGrid; // = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;

        List<string> ToolBar = (new List<string>() { "Search" });
        TransactionJournalMasterView MasterView { get; set; }
        CreateTransactionJournalCommand CreateModel { get; set; }
        UpdateTransactionJournalCommand UpdateModel { get; set; }
        DeleteTransactionJournalCommand DeleteModel { get; set; }

        TransactionJournalCreateForm createForm;
        Drawer createDrawer;
        bool showCreateDrawer { get; set; } = false;

        TransactionJournalEditForm editForm;
        Drawer editDrawer;
        bool showEditDrawer { get; set; } = false;

        JournalEntryQuickGrid quickGrid;
        ViewEntriesGrid entriesGrid;
        Drawer quickGridDrawer;
        Drawer entriesGridDrawer;
        bool showQuickGridDrawer { get; set; } = false;
        bool showEntriesDrawer { get; set; } = false;
        string entriesGridDrawerTitle = $"View entries";

        //TransactionJournalMasterView selectedJournalMasterView;

        string editFormActiveTabKey = "1";

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;

        List<TransactionJournalMasterView> _TransactionJournalMasterViewSrc { get; set; } = new List<TransactionJournalMasterView>();
        List<TransactionJournalMasterView> _TransactionJournalMasterView { get; set; } = new List<TransactionJournalMasterView>();

        [Inject]
        private IMasterViews _MasterView { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                createDrawer.Width = (int)(BrowserDimension.Width * 0.55);
                editDrawer.Width = (int)(BrowserDimension.Width * 0.40);
                quickGridDrawer.Width = (int)(BrowserDimension.Width * 0.50);
                entriesGridDrawer.Width = (int)(BrowserDimension.Width * 0.60);


                await OnRefresh();
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        protected override async Task OnInitializedAsync()
        {
            //Console.WriteLine($"API HOST->{Config.ODATA_VIEWS_HOST}, ODATA HOST-> {GRID_API_RESOURCE}");


            CreateModel = new CreateTransactionJournalCommand();
            UpdateModel = new UpdateTransactionJournalCommand();
            DeleteModel = new DeleteTransactionJournalCommand();

            MasterView = new TransactionJournalMasterView();

            _TransactionJournalMasterViewSrc = await _MasterView.GetCustomMasterViewEntityAllFields<TransactionJournalMasterView>(nameof(TransactionJournalMasterView));
            _TransactionJournalMasterView = _TransactionJournalMasterViewSrc.OrderByDescending(dt => dt.TransactionDate).ToList();

            QueryGrid = new Query();
        }


        public void ActionCompletedHandler(ActionEventArgs<TransactionJournalMasterView> args)
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
            //var txnNo = NHiloHelper.GetNextKey(nameof(AP.ChevronCoop.Entities.Accounting.TransactionJournals
            //    .TransactionJournal));
            var txnNo = NUlid.Ulid.NewUlid().ToString();

            CreateModel = new CreateTransactionJournalCommand
            {
                TransactionDate = DateTime.Now,
                //IsReversed = false,
                //IsPosted = false,
                TransactionType = TransactionType.GENERAL_TRANSACTION.ToString(),
                TransactionNo = $"{txnNo}"
            };
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

        async Task onViewEntriesDone()
        {
            showEntriesDrawer = false;
            // editForm.Errors = null;
        }

        async Task onViewQuickGridDone()
        {
            showQuickGridDrawer = false;
            // editForm.Errors = null;
        }

        private async Task OnAddRow()
        {
            CreateModel = new CreateTransactionJournalCommand();
            await onCreate();
        }


        private async Task OnViewRow(TransactionJournalMasterView row)
        {
        }

        private async Task OnViewEntries(TransactionJournalMasterView row)
        {
            MasterView = row;
            //entriesGrid.JournalId = row.Id;
            entriesGridDrawerTitle = $"View entries for Journal No {MasterView.TransactionNo}";
            InvokeAsync(StateHasChanged);

            //showQuickGridDrawer = true;
            showEntriesDrawer = true;
        }

        private async Task OnEditRow(TransactionJournalMasterView row)
        {
            UpdateModel = Mapper.Map<UpdateTransactionJournalCommand>(row);

            await onEdit();
        }


        private async Task OnDeleteRow(TransactionJournalMasterView row)
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

                var rsp = await DataService.Delete<DeleteTransactionJournalCommand, CommandResult<string>>(
                    nameof(TransactionJournal), DeleteModel);


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
                _TransactionJournalMasterView = _TransactionJournalMasterViewSrc.OrderByDescending(c => c.DateCreated).ToList();
            }
            else
            {
                _TransactionJournalMasterView = _TransactionJournalMasterViewSrc
                    .Where(c => (c.TransactionNo?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.TransactionType?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.Title?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.Title?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.DatePosted.ToString().Contains(inputValue, StringComparison.OrdinalIgnoreCase))
                                || (c.ReversalDate.ToString().Contains(inputValue, StringComparison.OrdinalIgnoreCase))
                                || (c.IsReversed.ToString().Contains(inputValue, StringComparison.OrdinalIgnoreCase))
                                || (c.IsPosted.ToString().Contains(inputValue, StringComparison.OrdinalIgnoreCase)))
                    .Take(10)
                    .OrderByDescending(c => c.DateCreated)
                    .ToList();
            }
        }

    }
}