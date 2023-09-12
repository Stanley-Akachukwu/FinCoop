using AntDesign;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Accounting.JournalEntries;
using AP.ChevronCoop.AppDomain.Accounting.TransactionJournals;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Accounting.JournalEntries;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Simple.OData.Client;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System.Drawing;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounting.JournalEntry
{
    public partial class ViewEntriesGrid
    {


        [Inject]
        ILogger<JournalEntryGrid> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }


        [Inject]
        ConfirmService ConfirmService { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        ODataClient ODataClient { get; set; }


        [Parameter]
        public string JournalId { get; set; }

        [Parameter]
        public EventCallback<string> JournalIdChanged { get; set; }


        [Parameter]
        public TransactionJournalMasterView MasterView { get; set; }

        [Parameter]
        public EventCallback<TransactionJournalMasterView> MasterViewChanged { get; set; }

        [Parameter]
        public bool ShowCompleted { get; set; }

        [Parameter]
        public EventCallback<bool> ShowCompletedChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }



        SfGrid<JournalEntryMasterView> grid;

        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(JournalEntryMasterView)}";



        private Query QueryGrid;// = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;

        List<string> ToolBar = (new List<string>() { "Search" });

        CreateJournalEntryCommand CreateModel { get; set; }
        UpdateJournalEntryCommand UpdateModel { get; set; }
        DeleteJournalEntryCommand DeleteModel { get; set; }
        List<JournalEntryMasterView> JournalEntries { get; set; } = new();

        JournalEntryCreateForm createForm;
        Drawer createDrawer;
        bool showCreateDrawer { get; set; } = false;

        JournalEntryEditForm editForm;
        Drawer editDrawer;
        bool showEditDrawer { get; set; } = false;


        string editFormActiveTabKey = "1";

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;

        private WhereFilter journalFilter;
        private WhereFilter isApprovedFilter;

        AntDesign.Button btnPost;
        AntDesign.Button btnReverse;
        bool isProcessing;
        string processingMessage;

        public string[] GroupedColumns = new string[] { "EntryType" };

        string testJournalId = "713e8901-f2fc-6b5a-9f5a-89dd59d3f5a0";
        string testId = "01H4Z7400VE2ZG6ATPXX0XD4B9";
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                //createDrawer.Width = (int)(BrowserDimension.Width * 0.40);
                //editDrawer.Width = (int)(BrowserDimension.Width * 0.40);

                Logger.LogInformation($"OnAfterRenderAsync->Journal Id-> {JournalId}");

            }
            else
            {

                if (journalFilter?.value?.ToString() != JournalId && !string.IsNullOrEmpty(JournalId))
                {


                    journalFilter = new WhereFilter
                    {
                        Field = nameof(JournalEntryMasterView.TransactionJournalId),
                        Operator = "equal",
                        //value = $"'{JournalId}'"
                        value = JournalId
                    };

                    QueryGrid = new Query().Where(journalFilter);
                    QueryGrid = new Query();

                    JournalEntries = new List<JournalEntryMasterView>();

                    var entries = await ODataClient.For<JournalEntryMasterView>()
                     .Filter(x => x.TransactionJournalId == JournalId)
                     .FindEntriesAsync();

                    JournalEntries = entries?.ToList();

                    Logger.LogInformation($"total entries found->{entries?.Count()}");

                    await InvokeAsync(StateHasChanged);
                    //await OnRefresh();

                }


            }



            //await jsRuntime.InvokeVoidAsync("initFlowbiteJS");


        }


        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            Logger.LogInformation($"OnParametersSetAsync->Journal Id-> {JournalId}");

            Logger.LogInformation($"OnParametersSetAsync-> Journal Id is null-> {string.IsNullOrEmpty(JournalId)}");
            Logger.LogInformation($"OnParametersSetAsync-> journalFilter is null-> {journalFilter == null}");
            Logger.LogInformation($"OnParametersSetAsync-> journalFilter value is null-> {journalFilter?.value == null}");

            //JournalEntries = new List<JournalEntryMasterView>();

            //var entries = await ODataClient.For<JournalEntryMasterView>()
            // .Filter(x => x.TransactionJournalId == JournalId)
            // .FindEntriesAsync();

            //JournalEntries = entries?.ToList();

            //Logger.LogInformation($"total entries found->{entries?.Count()}");

            //foreach (var entry in entries)
            //{
            //    Logger.LogInformation(entry.ToJson());
            //}

            //if (headerFilter != null && !string.IsNullOrEmpty(HeaderId))
            //{

            //}

            if (MasterView != null)
            {
                //btnPost.Disabled = MasterView.IsPosted;
                //btnReverse.Disabled = MasterView.IsReversed;

            }


        }

        protected override async Task OnInitializedAsync()
        {

            Logger.LogInformation($"OnInitializedAsync->API HOST->{Config.ODATA_VIEWS_HOST}");
            Logger.LogInformation($"OnInitializedAsync->ODATA HOST-> {GRID_API_RESOURCE}");


            CreateModel = new CreateJournalEntryCommand();
            UpdateModel = new UpdateJournalEntryCommand();
            DeleteModel = new DeleteJournalEntryCommand();
            JournalEntries = new List<JournalEntryMasterView>();

            isProcessing = false;
            processingMessage = "Processing...";

            Logger.LogInformation($"OnInitializedAsync->Journal Id-> {JournalId}");



            if (!string.IsNullOrEmpty(JournalId))
            {
                journalFilter = new WhereFilter
                {
                    Field = nameof(JournalEntryMasterView.TransactionJournalId),
                    Operator = "equal",
                    //value = $"'{testJournalId}'"
                    //value = $"'{JournalId}'"
                    //value = $"{JournalId}"
                    value = JournalId
                };


                QueryGrid = new Query().Where(journalFilter);
                QueryGrid = new Query();
            }
            else
            {
                QueryGrid = new Query();
            }

            //journalFilter = new WhereFilter
            //{
            //    Field = nameof(JournalEntryMasterView.TransactionJournalId),
            //    //Field = nameof(JournalEntryMasterView.Id),
            //    Operator = "equal",
            //    //value = $"'{testId}'"
            //    //value = $"guid'{testJournalId}"
            //    //value = $"{testJournalId}"
            //    //value = $"'{JournalId}'"
            //    //value = $"{JournalId}"
            //    value = JournalId
            //};


            //QueryGrid = new Query().Where(journalFilter);
            //QueryGrid = new Query();

        }



        public void ActionCompletedHandler(ActionEventArgs<JournalEntryMasterView> args)
        {
            //jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        public void ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = args.Error.ToString();
            Logger.LogError(this.ErrorDetails);
            StateHasChanged();

        }


        async Task PostEntries()
        {

            try
            {

                if (MasterView.IsPosted)
                {
                    _ = await ConfirmService.Show(
                  "Journal entries have already been posted!",
                  "Cannot post entries",
                  ConfirmButtons.OK,
                  ConfirmIcon.Error);

                    return;
                }


                var confirmPost = await ConfirmService.Show(
                    "Are you sure you want to post these entries?",
                    "Please confirm",
                    ConfirmButtons.YesNo,
                    ConfirmIcon.Warning
                );

                if (confirmPost == ConfirmResult.Yes)
                {

                    var txnPostModel = new PostTransactionJournalCommand
                    {
                        TransactionNo = MasterView.TransactionNo,
                    };

                    var rsp = await DataService
                    .Process<PostTransactionJournalCommand, CommandResult<TransactionJournalViewModel>>(
                        nameof(TransactionJournal), "post", txnPostModel);


                    if (!rsp.IsSuccessStatusCode)
                    {
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                        var msg = rspContent?.Response;
                        if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                        {
                            msg = rspContent.ValidationErrors[0].Error;
                        }


                        //await notificationService.Open(new NotificationConfig()
                        //{
                        //    Message = "Error",
                        //    Description = msg,
                        //    NotificationType = NotificationType.Error
                        //});

                        _ = await ConfirmService.Show(msg, "Error", ConfirmButtons.OK, ConfirmIcon.Error);


                    }
                    else
                    {
                        var notificationText = $"Transaction journal successfully posted, Transaction No is {rsp.Content.Response.TransactionNo}";
                        var showPopup = true;

                        //await notificationService.Open(new NotificationConfig()
                        //{
                        //    Message = "Success",
                        //    Description = notificationText,
                        //    NotificationType = NotificationType.Success
                        //});

                        _ = await ConfirmService.Show(notificationText, "Journal sucessfully posted!", ConfirmButtons.OK, ConfirmIcon.Success);

                        ShowModal = false;
                        await ShowModalChanged.InvokeAsync(ShowModal);


                    }



                }
            }
            catch (Exception ex)
            {
                _ = await ConfirmService.Show(ex.Message, "Error", ConfirmButtons.OK, ConfirmIcon.Error);

                //await notificationService.Open(new NotificationConfig()
                //{
                //    Message = "Error",
                //    Description = msg,
                //    NotificationType = NotificationType.Error
                //});
            }




        }

        async Task ReverseEntries()
        {

            try
            {
                if (!MasterView.IsPosted)
                {
                    _ = await ConfirmService.Show(
                  "Journal entries need to be posted before reversal!",
                  "Cannot reverse entries",
                  ConfirmButtons.OK,
                  ConfirmIcon.Error);

                    return;
                }


                if (MasterView.IsReversed)
                {
                    _ = await ConfirmService.Show(
                  "Journal entries have already been reversed!",
                  "Cannot reverse entries",
                  ConfirmButtons.OK,
                  ConfirmIcon.Error);

                    return;
                }


                var confirmAction = await ConfirmService.Show(
                    "Are you sure you want to reverse these entries?",
                    "Please confirm",
                    ConfirmButtons.YesNo,
                    ConfirmIcon.Warning
                );

                if (confirmAction == ConfirmResult.Yes)
                {

                    isProcessing = true;
                    processingMessage = "Reversing journal entries";

                    //await Task.Delay(1000 * 5);


                    var txnReverseModel = new ReverseTransactionJournalCommand
                    {
                        TransactionNo = MasterView.TransactionNo,
                    };

                    var rsp = await DataService
                    .Process<ReverseTransactionJournalCommand, CommandResult<TransactionJournalViewModel>>(
                        nameof(TransactionJournal), "reverse", txnReverseModel);


                    isProcessing = false;
                    processingMessage = "Processing";

                    if (!rsp.IsSuccessStatusCode)
                    {
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                        var msg = rspContent?.Response;
                        if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                        {
                            msg = rspContent.ValidationErrors[0].Error;
                        }


                        //await notificationService.Open(new NotificationConfig()
                        //{
                        //    Message = "Error",
                        //    Description = msg,
                        //    NotificationType = NotificationType.Error
                        //});

                        _ = await ConfirmService.Show(msg, "Error", ConfirmButtons.OK, ConfirmIcon.Error);


                    }
                    else
                    {
                        var notificationText = $"Transaction journal successfully reversed, Transaction No is {rsp.Content.Response.TransactionNo}";
                        var showPopup = true;

                        //await notificationService.Open(new NotificationConfig()
                        //{
                        //    Message = "Success",
                        //    Description = notificationText,
                        //    NotificationType = NotificationType.Success
                        //});

                        _ = await ConfirmService.Show(notificationText, "Journal sucessfully reversed!", ConfirmButtons.OK, ConfirmIcon.Success);



                        ShowModal = false;
                        await ShowModalChanged.InvokeAsync(ShowModal);


                    }




                }
            }
            catch (Exception ex)
            {
                _ = await ConfirmService.Show(
                               ex.Message,
                               "Error",
                               ConfirmButtons.OK,
                               ConfirmIcon.Error);

                //await notificationService.Open(new NotificationConfig()
                //{
                //    Message = "Error",
                //    Description = msg,
                //    NotificationType = NotificationType.Error
                //});
            }






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

            CreateModel = new CreateJournalEntryCommand();
            await onCreate();

        }



        private async Task OnViewRow(JournalEntryMasterView row)
        {


        }

        private async Task OnEditRow(JournalEntryMasterView row)
        {

            UpdateModel = Mapper.Map<UpdateJournalEntryCommand>(row);

            await onEdit();

        }



        private async Task OnDeleteRow(JournalEntryMasterView row)
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

                var rsp = await DataService.Delete<DeleteJournalEntryCommand, CommandResult<string>>(nameof(JournalEntry), DeleteModel);



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

            Logger.LogInformation($"OnRefresh->Journal Id-> {JournalId}");

            searchText = string.Empty;
            //QueryGrid = new Query().Where(journalFilter);
            QueryGrid = new Query();
            grid.Refresh();

            await InvokeAsync(StateHasChanged);
            //await Task.CompletedTask;

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

                WhereFilter entryTypeFilter = new WhereFilter
                {
                    Field = nameof(JournalEntryMasterView.EntryType),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter nameFilter = new WhereFilter
                {
                    Field = nameof(JournalEntryMasterView.AccountId_Name),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter codeFilter = new WhereFilter
                {
                    Field = nameof(JournalEntryMasterView.AccountId_Code),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter entryNoFilter = new WhereFilter
                {
                    Field = nameof(JournalEntryMasterView.TransactionEntryNo),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter descriptionFilter = new WhereFilter
                {
                    Field = "Description",
                    Operator = "contains",
                    value = searchText
                };


                //QueryGrid = new Query().Where(nameFilter.Or(roleFilter).Or(descriptionFilter));
                //QueryGrid = new Query().Where(journalFilter.And(titleFilter));
                QueryGrid = new Query().Where(entryTypeFilter.Or(nameFilter).Or(codeFilter).Or(entryNoFilter));
                grid.Refresh();

                //await grid.SearchAsync(searchText);
            }
            else
            {
                await OnRefresh();
            }

        }

        public void OnGridSearch(InputEventArgs args)
        {
            this.grid.Search(args.Value);
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