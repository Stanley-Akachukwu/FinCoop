using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Auth
{
    public partial class Forgot_Password
    {
        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        //[Parameter]
        //public ForgetPasswordViewModel SendEmailModel { get; set; }
        public ForgetPasswordViewModel SendEmailModel;

        [Parameter]
        public ValidateForgetPasswordOTPViewModel ValidateModel { get; set; }

        string Email;
        bool showSendEmailForm { get; set; } = false;
        bool showValidateOtpForm { get; set; } = false;
        bool showSendNewPasswordForm { get; set; } = false;
        bool showSuccessfulPasswordChange { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            showSendEmailForm = true;
        }

        private async Task OnSendEmailRequestChangedHandler(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            SendEmailModel = forgetPasswordViewModel;
            if (SendEmailModel != null)
            {
                showValidateOtpForm = true;
                showSendEmailForm = false;
                await InvokeAsync(StateHasChanged); // trigger a UI refresh
            }
        }

        private async Task OnValidateOtpChangedHandler(
            ValidateForgetPasswordOTPViewModel validateForgetPasswordOTPViewModel)
        {
            ValidateModel = validateForgetPasswordOTPViewModel;
            if (ValidateModel != null)
            {
                showValidateOtpForm = false;
                showSendNewPasswordForm = true;
                await InvokeAsync(StateHasChanged); // trigger a UI refresh
            }
        }
    }
}