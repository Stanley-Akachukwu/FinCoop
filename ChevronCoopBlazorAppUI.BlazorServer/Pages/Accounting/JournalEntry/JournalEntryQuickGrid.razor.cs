using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using ChevronCoop.Web.AppUI.BlazorServer;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Pages;
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
using AP.ChevronCoop.AppDomain.Accounting.JournalEntries;
using AP.ChevronCoop.Entities.Accounting.TransactionJournals;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounting.JournalEntry
{
    public partial class JournalEntryQuickGrid
    {
        public JournalEntryQuickGrid()
        {
            Items = new List<AP.ChevronCoop.Entities.Accounting.JournalEntries.JournalEntry>();
        }

        [Parameter]
        public string HeaderId { get; set; }

        [Parameter]
        public EventCallback<string> HeaderIdChanged { get; set; }


        private Query Query_Combo; // = new Query();


        [Parameter]
        public CreateJournalEntryCommand Model { get; set; }


        [Parameter]
        public EventCallback<CreateJournalEntryCommand> ModelChanged { get; set; }


        [Parameter]
        public TransactionJournalMasterView JournalMasterView { get; set; }


        [Parameter]
        public EventCallback<TransactionJournalMasterView> JournalMasterViewChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Parameter]
        public List<AP.ChevronCoop.Entities.Accounting.JournalEntries.JournalEntry> Items { get; set; }

        [Parameter]
        public EventCallback<List<AP.ChevronCoop.Entities.Accounting.JournalEntries.JournalEntry>> ItemsChanged
        {
            get;
            set;
        }


        [Inject]
        ILogger<JournalEntryCreateForm> Logger { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }


        SfGrid<AP.ChevronCoop.Entities.Accounting.JournalEntries.JournalEntry> grid;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
            }
        }


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Query_Combo = new Query();
            Items = new List<AP.ChevronCoop.Entities.Accounting.JournalEntries.JournalEntry>();
            Model = new CreateJournalEntryCommand();
        }

        protected override Task OnParametersSetAsync()
        {
            Items?.Clear();
            return base.OnParametersSetAsync();
        }


        private async void AddEntry()
        {
            var newEntry = Mapper.Map<AP.ChevronCoop.Entities.Accounting.JournalEntries.JournalEntry>(Model);

            Items.Add(newEntry);
            await ItemsChanged.InvokeAsync(Items);

            Model = new CreateJournalEntryCommand();
            await ModelChanged.InvokeAsync(Model);

            grid.Refresh();

            StateHasChanged();
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