using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.AppDomain;
using System.Text.Json;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.AppDomain.Security.Auth;
using AP.ChevronCoop.AppDomain.Security.Auth.Permissions;
using AP.ChevronCoop.Entities.Security.Auth;
using AP.ChevronCoop.Entities.Security.Auth.Permissions;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Permissions
{
    public partial class PermissionCreateForm
    {
        public PermissionCreateForm()
        {
        }


        private Query Query_Combo; // = new Query();

        string notificationText;
        bool showPopup = false;


        [Parameter]
        public CreatePermissionCommand Model { get; set; }


        [Parameter]
        public EventCallback<CreatePermissionCommand> ModelChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Inject]
        ILogger<PermissionCreateForm> Logger { get; set; }

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

            Query_Combo = new Query();
        }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        public async Task OnSaveClose()
        {
            var rsp = await DataService.Create<CreatePermissionCommand, CommandResult<PermissionViewModel>>(
                nameof(Permission), Model);


            if (!rsp.IsSuccessStatusCode)
            {
                var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

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
                NavigationManager.NavigateTo("/security/permission", true);
                //notificationText = $"Record successfully created, Id->{rsp.Content.Response.Id}";
                //showPopup = true;

                //await notificationService.Open(new NotificationConfig()
                //{
                //    Message = "Success",
                //    Description = notificationText,
                //    NotificationType = NotificationType.Success
                //});

                OnCancel();
            }
        }

        public async Task OnCancel()
        {
            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);

            Model = new CreatePermissionCommand();
            await ModelChanged.InvokeAsync(Model);

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

        public async Task OnInput(ChangeEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnChange(ChangeEventArgs args)
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