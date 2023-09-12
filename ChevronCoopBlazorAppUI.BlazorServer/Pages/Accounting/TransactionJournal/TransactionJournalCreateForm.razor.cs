using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.AppDomain;
using System.Text.Json;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using AntDesign;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.AppDomain.Accounting.TransactionJournals;
using Syncfusion.Blazor.PivotView;
using AP.ChevronCoop.AppDomain.Accounting.JournalEntries;
using AP.ChevronCoop.Entities.Accounting.LedgerAccounts;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.JournalEntries;
using Syncfusion.Blazor.Grids;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;
using ChevronCoop.Web.AppUI.BlazorServer.Data.Accounting;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounting.TransactionJournal
{
    public partial class TransactionJournalCreateForm
    {
        public TransactionJournalCreateForm()
        {
        }


        private Query Query_Combo; // = new Query();

        string notificationText;
        bool showPopup = false;


        [Parameter]
        public CreateTransactionJournalCommand Model { get; set; }


        [Parameter]
        public EventCallback<CreateTransactionJournalCommand> ModelChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Inject]
        ILogger<TransactionJournalCreateForm> Logger { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        ModalService ModalService { get; set; }

        [Inject]
        ConfirmService ConfirmService { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }


        //SfGrid<CreateJournalEntryCommand> gridEntries;
        SfGrid<JournalEntryDTO> gridEntries;

        string combobox_Ledger_API => $"{Config.ODATA_VIEWS_HOST}/{nameof(LedgerAccountMasterView)}";

        //public List<CreateJournalEntryCommand> Items { get; set; }
        //public List<JournalEntryDTO> Items { get; set; }
        public JournalEntryItems Items { get; set; }

        private Query Query_Combo_Ledger; // = new Query();
        List<string> entryTypes => System.Enum.GetNames(typeof(TransactionEntryType)).ToList();

        BrowserDimension BrowserDimension;
        private DialogSettings DialogParams = new DialogSettings { MinHeight = "25%", Width = "40%" };

        string ErrorDetails = "";
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                //gridEntries.Width = $"{(int)(BrowserDimension.Width * 0.40)}";

                //StateHasChanged();
            }

            // await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            //combobox_Ledger_API = $"{Config.ODATA_VIEWS_HOST}/{nameof(LedgerAccountMasterView)}";

            Query_Combo = new Query();
            Query_Combo_Ledger = new Query();
            //Items = new List<CreateJournalEntryCommand>(); 
            //Items = new List<JournalEntryDTO>();
            Items = new JournalEntryItems();

            //Model.TransactionNo = NHiloHelper.GetNextKey(nameof(TransactionJournal));
            Model.TransactionDate = DateTime.Now;
            //Model.IsReversed = false;
            //Model.IsPosted = false;
            Model.TransactionType = TransactionType.GENERAL_TRANSACTION.ToString();

            //Model.ReversalDate = DateTime.Now;
            //Model.DatePosted = DateTime.Now;
        }


        public void ActionCompletedHandler(ActionEventArgs<JournalEntryDTO> args)
        {
            // jsRuntime.InvokeVoidAsync("initFlowbiteJS");
            Logger.LogInformation($"action completed->type->{args.Type}, action->{args.Action}");

            if (args.Action == "Add")
            {
                Logger.LogInformation($"index of new item->{Items.IndexOf(args.Data)}");
                Logger.LogInformation($"entry no of new item->{args.Data?.TransactionEntryNo}");
            }

            ////args.Data.TransactionEntryNo = $"{Items.IndexOf(args.Data) + 1}";
            //foreach (var item in Items.OrderBy(p => p.Timestamp))
            //{
            //    item.TransactionEntryNo = $"{Items.IndexOf(item) + 1}";
            //}

            //InvokeAsync(StateHasChanged);
            //gridEntries.Refresh();

        }

        public void ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            ErrorDetails = args.Error.ToString();
            //Logger.LogError(ErrorDetails);
            StateHasChanged();

            ConfirmService.Show(
                                ErrorDetails,
                                "Error",
                                ConfirmButtons.OK,
                                ConfirmIcon.Error);
        }


        public int NextEntryNo
        {
            get
            {
                if (Items.Count == 0) return 1;

                return Items.Count + 1;
            }
        }

        public async Task OnSaveClose()
        {

            try
            {



                Model.PostEntries = false;
                Model.DocumentRef = "MANUAL";
                Model.EntityRef = "MANUAL";
                //Model.TransactionDate= DateTime.Now;
                //Model.TransactionType= TransactionType.GENERAL_TRANSACTION.ToString();

                if (!Items.Any())
                {
                    _ = await ConfirmService.Show(
                               "Journal entries are empty!",
                               "Cannot create journal",
                               ConfirmButtons.OK,
                               ConfirmIcon.Error);

                    return;
                }




                Model.JournalEntries = new List<CreateJournalEntryCommand>();
                foreach (var item in Items)
                {
                    Model.JournalEntries.Add(new CreateJournalEntryCommand
                    {
                        AccountCode = item.AccountCode,
                        EntryType = item.EntryType,
                        Debit = item.Debit,
                        Credit = item.Credit,
                        TransactionDate = DateTime.Now,
                    });
                }

                if (!Model.IsBalanced)
                {
                    _ = await ConfirmService.Show(
                             $"Journal entries are not balanced! {Model.AllDebits} <> {Model.AllDebits}",
                             "Cannot create journal",
                             ConfirmButtons.OK,
                             ConfirmIcon.Error);

                    return;
                }


                var rsp = await DataService
                    .Create<CreateTransactionJournalCommand, CommandResult<TransactionJournalViewModel>>(
                        nameof(TransactionJournal), Model);


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
                    notificationText = $"Transaction journal successfully created, Transaction No is {rsp.Content.Response.TransactionNo}";
                    showPopup = true;

                    //await notificationService.Open(new NotificationConfig()
                    //{
                    //    Message = "Success",
                    //    Description = notificationText,
                    //    NotificationType = NotificationType.Success
                    //});

                    _ = await ConfirmService.Show(
             notificationText,
             "Journal sucessfully created!",
             ConfirmButtons.OK,
             ConfirmIcon.Success);

                    await OnCancel();


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




        public async Task OnSaveAndPost()
        {
            try
            {



                Model.PostEntries = true;
                Model.DocumentRef = "MANUAL";
                Model.EntityRef = "MANUAL";
                //Model.TransactionDate= DateTime.Now;
                //Model.TransactionType= TransactionType.GENERAL_TRANSACTION.ToString();

                if (!Items.Any())
                {
                    _ = await ConfirmService.Show(
                               "Journal entries are empty!",
                               "Cannot create journal",
                               ConfirmButtons.OK,
                               ConfirmIcon.Error);

                    return;
                }




                Model.JournalEntries = new List<CreateJournalEntryCommand>();
                foreach (var item in Items)
                {
                    Model.JournalEntries.Add(new CreateJournalEntryCommand
                    {
                        AccountCode = item.AccountCode,
                        EntryType = item.EntryType,
                        Debit = item.Debit,
                        Credit = item.Credit,
                        TransactionDate = DateTime.Now,
                    });
                }

                if (!Model.IsBalanced)
                {
                    _ = await ConfirmService.Show(
                             $"Journal entries are not balanced! {Model.AllDebits} <> {Model.AllDebits}",
                             "Cannot create journal",
                             ConfirmButtons.OK,
                             ConfirmIcon.Error);

                    return;
                }


                var rsp = await DataService
                    .Create<CreateTransactionJournalCommand, CommandResult<TransactionJournalViewModel>>(
                        nameof(TransactionJournal), Model);


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
                    notificationText = $"Transaction journal successfully created, Transaction No is {rsp.Content.Response.TransactionNo}";
                    showPopup = true;

                    //await notificationService.Open(new NotificationConfig()
                    //{
                    //    Message = "Success",
                    //    Description = notificationText,
                    //    NotificationType = NotificationType.Success
                    //});

                    _ = await ConfirmService.Show(
             notificationText,
             "Journal sucessfully created!",
             ConfirmButtons.OK,
             ConfirmIcon.Success);

                    await OnCancel();


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



        public async Task OnCancel()
        {
            Model = new CreateTransactionJournalCommand();
            await ModelChanged.InvokeAsync(Model);

            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);

            showPopup = false;
        }


        private void OnFilterCombo(FilteringEventArgs args)
        {
            WhereFilter filter1 = new WhereFilter
            {
                Field = "Description",
                Operator = "contains",
                value = args.Text
            };

            WhereFilter filter2 = new WhereFilter
            {
                Field = "Name",
                Operator = "contains",
                value = args.Text
            };
        }


        private void OnFileUploaded(UploadChangeEventArgs args)
        {
        }

        public async Task OnInput(Microsoft.AspNetCore.Components.ChangeEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnChange(Microsoft.AspNetCore.Components.ChangeEventArgs args)
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