using AntDesign;
using AP.ChevronCoop.AppDomain.MasterData.Currencies;
using AP.ChevronCoop.Commons;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Inputs;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.MasterData.Currency
{
    public partial class CurrencyEditForm
    {
        private Query Query_Combo; // = new Query();

        string notificationText; // = $"name->{Model.Name}";
        bool showPopup = false;


        AntDesign.Tabs editTab;

        [Parameter]
        public string ActiveTabKey { get; set; }

        [Parameter]
        public EventCallback<string> ActiveTabKeyChanged { get; set; }


        [Parameter]
        public UpdateCurrencyCommand Model { get; set; }


        [Parameter]
        public EventCallback<UpdateCurrencyCommand> ModelChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        ILogger<CurrencyEditForm> Logger { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }

        private WhereFilter baseFilter;
        [Inject]
        NavigationManager NavigationManager { get; set; }
        bool reloadGrid = false;

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
        }


        public async Task OnSave()
        {
            var rsp = await DataService.Update<UpdateCurrencyCommand, CommandResult<CurrencyViewModel>>(
                nameof(Currency), Model);

            Logger.LogInformation($"rsp content->{rsp?.Content}");
            Logger.LogInformation($"error->{rsp?.Error?.Content}");

            if (!rsp.IsSuccessStatusCode)
            {
                var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);
                //Console.WriteLine($"rsp content->{rsp.Content}");
                //Console.WriteLine($"error->{rsp.Error.Content}");


                //Console.WriteLine($"error content->{rspContent}");
                //Console.WriteLine($"error msg->{rspContent.Response}");

                var msg = rspContent?.Response;
                if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                {
                    msg = rspContent.ValidationErrors[0].Error;
                }


                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = msg,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                notificationText = $"Record successfully updated, Id->{rsp.Content.Response.Id}";
                showPopup = true;
                reloadGrid = true;
            }
        }


        public async Task OnCancel()
        {
            if (reloadGrid)
            {
                NavigationManager.NavigateTo("/setup/currency", true);
                reloadGrid = false;
            }
            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);

            Model = new UpdateCurrencyCommand();
            await ModelChanged.InvokeAsync(Model);

            showPopup = false;
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