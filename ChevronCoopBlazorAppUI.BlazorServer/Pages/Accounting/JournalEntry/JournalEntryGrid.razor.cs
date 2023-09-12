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

using AP.ChevronCoop.Commons;

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
using Blazored.SessionStorage;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Tailwindcss;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Svg;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Util;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.SharedComponents;
using CurrieTechnologies.Razor.SweetAlert2;
using AP.ChevronCoop.AppDomain.Accounting.JournalEntries;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounting.JournalEntry
{
    public partial class JournalEntryGrid
    {


        [Inject]
        ILogger<JournalEntryGrid> Logger { get; set; }

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

        }

        protected override Task OnParametersSetAsync()
        {
           
            return base.OnParametersSetAsync();
        }

    }
}