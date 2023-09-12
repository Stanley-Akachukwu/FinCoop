using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.EnrollmentPayments;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalDocuments;
using AP.ChevronCoop.Entities.Security.MemberProfiles.EnrollmentPayments;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Util;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;
using System.Security.Policy;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Auth
{
    public partial class UploadReceipt2
    {
        public UploadReceiptViewModel Model { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        ILogger<UploadReceipt> Logger { get; set; }

        [Parameter]
        public ApprovalDocumentMasterView UpdateModel { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        public CreateEnrollmentPaymentInfoCommand CreateCommand { get; set; }
        public RegisterMemberViewModel RegisterMemberViewModel { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        TempObjectService tempService { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        bool showFileUploadError { get; set; } = false;
        bool showFileUploadSuccess { get; set; } = false;
        bool showDocumentError { get; set; } = false;
        bool showUploadSuccessful { get; set; } = false;
        string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Model = new UploadReceiptViewModel();
            RegisterMemberViewModel = new RegisterMemberViewModel();

            RegisterMemberViewModel = (RegisterMemberViewModel)tempService.GetTempObject();
        }

        public async Task OnChangeDocumentUpload(UploadChangeEventArgs args)
        {
            ErrorMessage = string.Empty;
            showFileUploadError = false;
            // await InvokeAsync(StateHasChanged);

            var file = args.Files.FirstOrDefault();
            if (file != null)
            {
                var response = UploadUtility.ValidateFile(file);
                if (string.IsNullOrEmpty(response))
                {
                    Model.Document = file.Stream.ToArray();
                    Model.FileName = file.FileInfo.Name;
                    Model.MimeType = file.FileInfo.Type;
                    Model.FileSize = file.FileInfo.Size;
                    ErrorMessage = "Upload was successful";
                    showFileUploadSuccess = true;
                }
                else
                {
                    ErrorMessage = response;
                    showFileUploadError = true;
                }
            }
            else
            {
                ErrorMessage = "File not found";
                showFileUploadError = true;
            }

            // await InvokeAsync(StateHasChanged);
        }

        private async Task MapToCommand()
        {
            CreateCommand = new CreateEnrollmentPaymentInfoCommand();
            RegisterMemberViewModel = new RegisterMemberViewModel();
            CreateCommand.FileSize = (int)Model.FileSize;
            CreateCommand.MimeType = Model.MimeType;
            CreateCommand.FileName = Model.FileName;
            CreateCommand.DateCreated = DateTime.UtcNow;
            CreateCommand.ProfileId = RegisterMemberViewModel.MemberId;
            CreateCommand.Document = Model.Document;
        }

        private async Task OnSave()
        {
            await MapToCommand();
            var rsp = await DataService
                .Create<CreateEnrollmentPaymentInfoCommand, CommandResult<CreateEnrollmentPaymentInfoCommand>>(
                    nameof(EnrollmentPaymentInfo), CreateCommand);

            Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(CreateCommand)}");
            if (!rsp.IsSuccessStatusCode)
            {
                var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                var msg = rspContent?.Response;
                if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                {
                    msg = rspContent.ValidationErrors.FirstOrDefault().Error;
                }

                if (msg == null && rspContent?.Message != null)
                    msg = rspContent.Message;
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = msg,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                showUploadSuccessful = true;

            }
        }

        private async Task GoToLogin()
        {
            _navigationManager.NavigateTo("/identity/account/login", true);
        }
    }
}