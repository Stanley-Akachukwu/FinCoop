using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserLogins;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Auth
{
    public partial class SendEmailForm
    {
        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Parameter]
        public ForgetPasswordViewModel ForgotModel { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        string Email;

        [Parameter]
        public EventCallback<ForgetPasswordViewModel> OnSendEmailRequestChanged { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject] IClientAuditService _auditLogService { get; set; }
        private string bearToken { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ForgotModel = new ForgetPasswordViewModel();
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
            }

            StateHasChanged();
        }

        private string ButtonText { get; set; } = "Recover Password";
        private bool ButtonDisabled { get; set; } = false;
        private string errorMessage = string.Empty;

        protected async Task OnSendEmail()
        {
            try
            {
                ButtonText = "Sending OTP...";
                ButtonDisabled = true;
                ForgetPasswordCommand command = new()
                {
                    Email = Email
                };
                var rsp = await DataService
                    .ProcessRequest<ForgetPasswordCommand, CommandResult<ForgetPasswordViewModel>>(
                        nameof(ApplicationUserLogin), "ForgetPassword", command);


                if (!rsp.IsSuccessStatusCode)
                {
                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    var msg = rspContent?.Response;
                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        msg = rspContent.ValidationErrors[0].Error;
                    }

                    if (msg == null && rspContent?.Message != null)
                        msg = rspContent.Message;

                    errorMessage = msg;
                    ButtonText = "Recover Password";
                    ButtonDisabled = false;
                }
                else
                {
                    ForgotModel = JsonSerializer.Deserialize<ForgetPasswordViewModel>(rsp.Content.Response.ToJson());
                    await OnSendEmailRequestChanged.InvokeAsync(ForgotModel);
                }

                var payload = JsonSerializer.Serialize(command);
                await _auditLogService.LogAuditByEmail("Request For Password Change.",
                    $"{command.Email} initiated request for password change.", "Security", payload, ForgotModel.Email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task OnSaveClose()
        {
            try
            {
                ForgetPasswordCommand command = new()
                {
                    Email = Email
                };
                var rsp = await DataService
                    .ProcessRequest<ForgetPasswordCommand, CommandResult<ForgetPasswordViewModel>>(
                        nameof(ApplicationUserLogin), "ForgetPassword", command);


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
                    ForgotModel = JsonSerializer.Deserialize<ForgetPasswordViewModel>(rsp.Content.Response.ToJson());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}