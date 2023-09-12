using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserLogins;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Auth
{
    public partial class ChangePassword
    {
        private FluentValidationValidator? _fluentValidationValidator;
        bool showPopup = false;

        private string Password;

        private string Confirmpassword;
        private string OldPassword;
        private string Email;

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        private string ButtonText { get; set; } = "Change Password";
        private bool ButtonDisabled { get; set; } = false;

        [Parameter]
        public ChangePasswordViewModel Model { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject] IConfiguration configuration { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }
        string bearToken { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        [Inject]
        ILogger<ChangePassword> Logger { get; set; }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                if (CurrentUser.Identity.IsAuthenticated)
                {
                    Email = CurrentUser.Identity.Name;
                }

                bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);
                if (string.IsNullOrEmpty(bearToken))
                {
                    _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
                }

                StateHasChanged();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetCurrentUser();
            Model = new ChangePasswordViewModel();
            Email = CurrentUser.Identity.Name;
            Model.Email = Email;
        }

        public async Task OnClose()
        {
            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);

            showPopup = false;
            NavigationManager.NavigateTo("Dashboard", true);
        }

        public async Task OnSave()
        {
            ButtonText = "Processing...";
            ButtonDisabled = true;
            if (await _fluentValidationValidator!.ValidateAsync())
            {
                //if (string.IsNullOrEmpty(OldPassword))
                //{
                //    await notificationService.Open(new NotificationConfig()
                //    {
                //        Message = "Error",
                //        Description = "Please enter your current password.",
                //        NotificationType = NotificationType.Error
                //    });
                //    ButtonText = "Change Password";
                //    ButtonDisabled = false;
                //    return;
                //}
                //if (string.IsNullOrEmpty(Password))
                //{
                //    await notificationService.Open(new NotificationConfig()
                //    {
                //        Message = "Error",
                //        Description = "Please enter your new password (it must contain, alphabet, digit,symbol and Uppercase alphabet).",
                //        NotificationType = NotificationType.Error
                //    });
                //    ButtonText = "Change Password";
                //    ButtonDisabled = false;
                //    return;
                //}
                //if (string.IsNullOrEmpty(Confirmpassword))
                //{
                //    await notificationService.Open(new NotificationConfig()
                //    {
                //        Message = "Error",
                //        Description = "Please enter your confirm password (it must contain, alphabet, digit,symbol and Uppercase alphabet).",
                //        NotificationType = NotificationType.Error
                //    });
                //    ButtonText = "Change Password";
                //    ButtonDisabled = false;
                //    return;
                //}
                //if (!IsValidPassword(Password))
                //{
                //    await notificationService.Open(new NotificationConfig()
                //    {
                //        Message = "Error",
                //        Description = "Please enter a valid password (it must contain, alphabet, digit,symbol and Uppercase alphabet).",
                //        NotificationType = NotificationType.Error
                //    });
                //    ButtonText = "Change Password";
                //    ButtonDisabled = false;
                //    return;
                //}
                //if (!HasUpperCase(Password))
                //{
                //    await notificationService.Open(new NotificationConfig()
                //    {
                //        Message = "Error",
                //        Description = "Password must contain an upper case letter.",
                //        NotificationType = NotificationType.Error
                //    });
                //    ButtonText = "Change Password";
                //    ButtonDisabled = false;
                //    return;
                //}
                //if (Password.Length < 8)
                //{
                //    await notificationService.Open(new NotificationConfig()
                //    {
                //        Message = "Error",
                //        Description = "Password length must not be less than 8.",
                //        NotificationType = NotificationType.Error
                //    });
                //    ButtonText = "Change Password";
                //    ButtonDisabled = false;
                //    return;
                //}
                //if (Confirmpassword != Password)
                //{
                //    await notificationService.Open(new NotificationConfig()
                //    {
                //        Message = "Info",
                //        Description = "Password does not match",
                //        NotificationType = NotificationType.Info
                //    });
                //    ButtonText = "Change Password";
                //    ButtonDisabled = false;
                //    return;
                //}
                ChangePasswordCommand command = new()
                {
                    Email = Email,
                    ConfirmPassword = Model.ConfirmPassword,
                    NewPassword = Model.NewPassword,
                    OldPassword = Model.OldPassword
                };
                ButtonText = "Processing...";
                ButtonDisabled = true;

                Model.Email = Email;
                var rsp = await DataService.ProcessRequest<ChangePasswordCommand, CommandResult<string>>(
                    nameof(ApplicationUserLogin), "ChangePassword", command);
                Logger.LogInformation($"rsp payload->{JsonSerializer.Serialize(command)}");

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

                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = msg,
                        NotificationType = NotificationType.Error
                    });
                    ButtonText = "Change Password";
                    ButtonDisabled = false;
                }
                else
                {
                    JsonSerializer.Deserialize<string>(rsp.Content.Response.ToJson());
                    showPopup = true;
                    Model.NewPassword = "xxxxxxxxxx";
                    Model.ConfirmPassword = "xxxxxxxxxx";
                    Model.OldPassword = "xxxxxxxxxx";

                    var payload = JsonSerializer.Serialize(Model);
                    await _auditLogService.LogAudit("Password Update.", $"{Model.Email} changed password.", "Security",
                        payload, CurrentUser);
                }
            }
            else
            {
                ButtonText = "Change Password";
                ButtonDisabled = false;
                return;
            }
        }

        static bool IsValidPassword(string password)
        {
            return
                password.Any(c => IsLetter(c)) &&
                password.Any(c => IsDigit(c)) &&
                password.Any(c => IsSymbol(c));
        }

        static bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        static bool IsSymbol(char c)
        {
            return c > 32 && c < 127 && !IsDigit(c) && !IsLetter(c);
        }

        static bool HasUpperCase(string password)
        {
            var hasUpperCase = password.Any(char.IsUpper);
            return hasUpperCase;
        }

        public async Task OnCloseModal()
        {
            showPopup = false;
            NavigationManager.NavigateTo("/Change-Password", true);
        }
    }
}