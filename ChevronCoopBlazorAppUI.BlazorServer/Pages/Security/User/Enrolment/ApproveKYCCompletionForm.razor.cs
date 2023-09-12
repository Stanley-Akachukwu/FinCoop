using AntDesign;
using AP.ChevronCoop.Commons;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using System.Text.Json;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.Enrolment
{
    public partial class ApproveKYCCompletionForm
    {
        public ApproveKYCCompletionForm()
        {
        }

        string notificationText;
        bool showPopup = false;

        [Parameter]
        public string ActiveTabKey { get; set; }

        [Parameter]
        public EventCallback<string> ActiveTabKeyChanged { get; set; }


        [Parameter]
        public ApproveKYCCommand Model { get; set; }


        [Parameter]
        public EventCallback<ApproveKYCCommand> ModelChanged { get; set; }


        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Parameter]
        public EventCallback<bool> OnApproveActionRefreshGrid { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }


        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        ILogger<ApproveEnrolmentForm> Logger { get; set; }

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

        public async Task OnSave()
        {
            //var approveKyc = new ApproveKYCCommand {  };

            var rsp = await DataService.ApproveKYC<ApproveKYCCommand, CommandResult<string>>(Model);

            Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(Model)}");
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
                notificationText = $"KYC successfully approved!";
                showPopup = true;
            }
        }


        public async Task OnCancel()
        {
            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);
            Model = new ApproveKYCCommand();
            await ModelChanged.InvokeAsync(Model);
            showPopup = false;

            //await OnApproveActionRefreshGrid.InvokeAsync(true);
            //navigationManager.NavigateTo("/security/kyc-approvals", true);
        }


        private async Task OnDownloadPaymentReciept()
        {
            //navigationManager.NavigateTo("/enrolment/download", true);
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