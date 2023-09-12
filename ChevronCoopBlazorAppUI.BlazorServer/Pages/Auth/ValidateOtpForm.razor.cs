using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserLogins;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Auth
{
    public partial class ValidateOtpForm
    {
        string Code_1;
        string Code_2;
        string Code_3;
        string Code_4;
        string Code_5;
        string Code_6;
        bool disableCode_2 = true;
        bool disableCode_3 = true;
        bool disableCode_4 = true;
        bool disableCode_5 = true;
        bool disableCode_6 = true;
        private string Otp = string.Empty;
        bool showPopup = false;
        string notificationText = string.Empty;
        bool disableButton = true;

        [Inject]
        NotificationService notificationService { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Parameter]
        public ForgetPasswordViewModel ForgetPassModel { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public ValidateForgetPasswordOTPViewModel ValidateModel { get; set; }

        [Parameter]
        public EventCallback<ValidateForgetPasswordOTPViewModel> OnValidateOtpChanged { get; set; }

        [Parameter]
        public EventCallback<ForgetPasswordViewModel> ForgetPassModelChanged { get; set; }

        private string ButtonText { get; set; } = "Recover Password";
        private bool ButtonDisabled { get; set; } = false;
        private string errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            //await base.OnInitializedAsync();
            ValidateModel = new ValidateForgetPasswordOTPViewModel();
        }

        protected async Task OnSaveClose()
        {
            try
            {
                ButtonText = "Verifying OTP...";
                ButtonDisabled = true;
                Otp = await GetOtpEntered();
                if (Otp == string.Empty)
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Info",
                        Description = "Please enter you OTP to continue",
                        NotificationType = NotificationType.Info
                    });
                }

                if (ValidateEnteredOtp())
                {
                    ValidateForgetPasswordOTPCommand command = new()
                    {
                        Email = ForgetPassModel.Email,
                        OneTimePassword = Otp,
                        OneTimePasswordCopy = ForgetPassModel.OneTimePasswordCopy
                    };
                    var rsp = await DataService
                        .ProcessRequest<ValidateForgetPasswordOTPCommand,
                            CommandResult<ValidateForgetPasswordOTPViewModel>>(nameof(ApplicationUserLogin),
                            "ValidateOTP", command);


                    if (!rsp.IsSuccessStatusCode)
                    {
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                        var msg = rspContent?.Response;
                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
                        {
                            msg = rspContent.ValidationErrors[0].Error;
                        }

                        if (msg == null && rspContent?.Message != null)
                            msg = rspContent.Message;

                        //await notificationService.Open(new NotificationConfig()
                        //{
                        //    Message = "Error",
                        //    Description = msg,
                        //    NotificationType = NotificationType.Error
                        //});
                        errorMessage = msg;
                        ButtonText = "Recover Password";
                        ButtonDisabled = false;
                    }
                    else
                    {
                        ValidateModel =
                            JsonSerializer.Deserialize<ValidateForgetPasswordOTPViewModel>(
                                rsp.Content.Response.ToJson());
                        await OnValidateOtpChanged.InvokeAsync(ValidateModel);
                    }
                }
                else
                {
                    //await notificationService.Open(new NotificationConfig()
                    //{
                    //    Message = "Error",
                    //    Description = "Invalid OTP, you can click on resend to get a new copy",
                    //    NotificationType = NotificationType.Error
                    //});
                    errorMessage = "Invalid OTP, you can click on resend to get a new copy";
                    ButtonText = "Recover Password";
                    ButtonDisabled = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task OnCancel()
        {
            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);

            showPopup = false;
        }

        public async Task<string> GetOtpEntered()
        {
            Otp = string.Empty;
            Otp = $"{Code_1}{Code_2}{Code_3}{Code_4}{Code_5}{Code_6}";
            return Otp;
        }

        public async Task<bool> OnResendOtp()
        {
            try
            {
                ForgetPasswordCommand command = new()
                {
                    Email = ForgetPassModel.Email
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

                    //await notificationService.Open(new NotificationConfig()
                    //{
                    //    Message = "Error",
                    //    Description = msg,
                    //    NotificationType = NotificationType.Error
                    //});
                    errorMessage = msg;
                }
                else
                {
                    ForgetPassModel =
                        JsonSerializer.Deserialize<ForgetPasswordViewModel>(rsp.Content.Response.ToJson());
                    //await notificationService.Open(new NotificationConfig()
                    //{
                    //    Message = "Info",
                    //    Description = "Please, check your email, for the new OTP",
                    //    NotificationType = NotificationType.Info
                    //});
                    errorMessage = "Please, check your email, for the new OTP";
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ShowMessage(string message)
        {
            await notificationService.Open(new NotificationConfig()
            {
                Message = "Info",
                Description = message,
                NotificationType = NotificationType.Info
            });
            return false;
        }

        private async Task OnInput_1(ChangeEventArgs e)
        {
            Code_1 = e.Value.ToString();
            if (!string.IsNullOrEmpty(e.Value.ToString()))
            {
                disableCode_2 = false;
            }
        }

        private void OnInput_2(ChangeEventArgs e)
        {
            Code_2 = e.Value.ToString();
            if (!string.IsNullOrEmpty(e.Value.ToString()))
                disableCode_3 = false;
        }

        private void OnInput_3(ChangeEventArgs e)
        {
            Code_3 = e.Value.ToString();
            if (!string.IsNullOrEmpty(e.Value.ToString()))
                disableCode_4 = false;
        }

        private void OnInput_4(ChangeEventArgs e)
        {
            Code_4 = e.Value.ToString();
            if (!string.IsNullOrEmpty(e.Value.ToString()))
                disableCode_5 = false;
        }

        private void OnInput_5(ChangeEventArgs e)
        {
            Code_5 = e.Value.ToString();
            if (!string.IsNullOrEmpty(e.Value.ToString()))
                disableCode_6 = false;
        }

        private void OnInput_6(ChangeEventArgs e)
        {
            Code_6 = e.Value.ToString();
            if (!string.IsNullOrEmpty(e.Value.ToString()))
                disableButton = false;
        }

        public bool ValidateEnteredOtp()
        {
            if (Otp.ToLower() != ForgetPassModel.OneTimePasswordCopy.ToLower()) return false;
            else return true;
        }
    }
}