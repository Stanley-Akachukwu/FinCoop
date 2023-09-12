using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserLogins;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Auth
{
    public partial class SendNewPasswordForm
    {
        bool showPopup = false;

        private string Password;

        private string Confirmpassword;

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Parameter]
        public ValidateForgetPasswordOTPViewModel Model { get; set; }

        [Parameter]
        public EventCallback<ValidateForgetPasswordOTPViewModel> ModelChanged { get; set; }

        [Parameter]
        public EventCallback<bool> OnValidateOtpChanged { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public ResetPasswordViewModel ResetModel { get; set; }

        private string ButtonText { get; set; } = "Recover Password";
        private bool ButtonDisabled { get; set; } = false;
        private string errorMessage = string.Empty;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                //Model = new ValidateForgetPasswordOTPViewModel();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        public async Task OnClose()
        {
            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);

            showPopup = false;
            NavigationManager.NavigateTo("identity/account/login", true);
        }

        public async Task OnSave()
        {
            ButtonText = "Changing Password...";
            ButtonDisabled = true;
            var message = IsValidPassword(Password);
            if (!string.IsNullOrEmpty(message))
            {
                errorMessage = message;
                ButtonText = "Recover Password";
                ButtonDisabled = false;
                return;
            }

            if (!HasUpperCase(Password))
            {
                errorMessage = "Password must contain an upper case letter.";
                ButtonText = "Recover Password";
                ButtonDisabled = false;
                return;
            }

            if (Password.Length < 8)
            {
                errorMessage = "Password length must not be less than 8.";
                ButtonText = "Recover Password";
                ButtonDisabled = false;
                return;
            }

            if (Confirmpassword != Password)
            {
                errorMessage = "Password does not match";
                ButtonText = "Recover Password";
                ButtonDisabled = false;
                return;
            }

            ResetPasswordCommand command = new()
            {
                Email = Model.Email,
                ConfirmPassword = Confirmpassword,
                NewPassword = Password,
                OneTimePassword = Model.OneTimePasswordCopy
            };
            var rsp = await DataService.ProcessRequest<ResetPasswordCommand, CommandResult<string>>(
                nameof(ApplicationUserLogin), "ResetPassword", command);


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
                JsonSerializer.Deserialize<string>(rsp.Content.Response.ToJson());
                ButtonText = "Recover Password";
                ButtonDisabled = false;
                showPopup = true;
            }
        }

        static string IsValidPassword(string password)
        {
            if (!password.Any(c => IsLetter(c)))
                return "Password must contain alphabet";
            if (!password.Any(c => IsDigit(c)))
                return "Password must contain digit";
            if (!password.Any(c => IsSymbol(c)))
                return "Password must contain atleast a symbols";
            else
                return null;
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
    }
}