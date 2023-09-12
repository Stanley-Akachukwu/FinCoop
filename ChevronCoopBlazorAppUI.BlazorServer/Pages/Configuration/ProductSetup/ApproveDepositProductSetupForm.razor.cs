using AntDesign;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.Enrolment;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup
{
    public partial class ApproveDepositProductSetupForm
    {
        public ApproveDepositProductSetupForm()
        {
        }

        string notificationText;
        bool showPopup = false;

        [Parameter]
        public string ActiveTabKey { get; set; }

        [Parameter]
        public EventCallback<string> ActiveTabKeyChanged { get; set; }


        //[Parameter]
        //public ApproveDepositProductCommand Model { get; set; }


        //[Parameter]
        //public EventCallback<ApproveDepositProductCommand> ModelChanged { get; set; }


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

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject] AuthenticationStateProvider _authenticationStateProvider { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        ClaimsPrincipal CurrentUser { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUser();
            await base.OnInitializedAsync();
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        public async Task OnSave()
        {
            //var rsp = await DataService.ApproveDepositProduct<ApproveDepositProductCommand, CommandResult<GetDepositProductViewModel>>(Model);
            //         if (!rsp.IsSuccessStatusCode)
            //         {
            //             var serverErrorMessages = "Server Error.";
            //             var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);
            //             var msg = rsp.ReasonPhrase;

            //             if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
            //             {
            //                 serverErrorMessages += " " + rspContent.ValidationErrors[0].Error;
            //             }
            //             if (!string.IsNullOrEmpty(rspContent.Message))
            //             {
            //                 serverErrorMessages += " " + rspContent.Message;
            //             }

            //             await notificationService.Open(new NotificationConfig()
            //             {
            //                 Message = msg,
            //                 NotificationType = NotificationType.Error,
            //                 Description = rspContent.Message,
            //             });
            //         }
            //         else
            //         {
            //             var payload = JsonSerializer.Serialize(Model);
            //             Logger.LogInformation($"rsp content->{payload}");

            //              await _auditLogService.LogAudit("Approve Deposit Product", "Approved Deposit Product Setup.", "Deposit", payload, CurrentUser);

            //             if (string.IsNullOrEmpty(rsp.Content.Message))
            //             {
            //                 notificationText = $"Deposit Product Setup successfully approved!";
            //             }
            //             else { notificationText = $"{rsp.Content.Message}"; }

            //             await notificationService.Open(new NotificationConfig()
            //             {
            //                 Message = notificationText,
            //                 NotificationType = NotificationType.Success
            //             });
            //             OnCancel();

            //         }
        }


        public async Task OnCancel()
        {
            //ShowModal = false;
            //await ShowModalChanged.InvokeAsync(ShowModal);
            //Model = new ApproveDepositProductCommand();
            //await ModelChanged.InvokeAsync(Model);
            //         _navigationManager.NavigateTo("/ProductSetup/Manage/all", forceLoad: true);
            //         showPopup = false;
        }


        public async Task OnInput(ChangeEventArgs args)
        {
            //await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnChange(ChangeEventArgs args)
        {
            //await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            //wait ModelChanged.InvokeAsync(Model);
        }

        private async Task onFocus(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            //await ModelChanged.InvokeAsync(Model);
        }
    }
}