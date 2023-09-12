using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Syncfusion.Blazor.Inputs;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.NewKYC
{
    public partial class KYCDocument
    {
        private FluentValidationValidator? _fluentValidationValidator;

        [Parameter]
        public EventCallback<bool> OnUpdateDocumentChanged { get; set; }

        [Parameter]
        public MemberProfileMasterView UpdateModel { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public EventCallback<MemberProfileMasterView> UpdateModelChanged { get; set; }

        public KYCDocumentViewModel Model { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        ILogger<KYCDocument> Logger { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        public UpdateMemberProfileCommand UpdateCommand { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Parameter]
        public MemberProfileViewModel MemberProfileModel { get; set; }

        bool showPassportError { get; set; } = false;
        bool showPassportSuccess { get; set; } = false;
        bool showDocumentError { get; set; } = false;
        string ErrorMessage { get; set; } = string.Empty;

        string Passport { get; set; } = string.Empty;

        string Member_Document { get; set; } = string.Empty;

        public List<DropDown> DocumentType { get; set; }


        string ApplicationUserId { get; set; }


        [Inject]

        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]

        NavigationManager NavigationManager { get; set; }

        string IsApproved { get; set; }
        string SubmitBtn { get; set; } = "Submit";
        bool Disable { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            UpdateModel = new MemberProfileMasterView();
            MemberProfileModel = new MemberProfileViewModel();
            Model = new KYCDocumentViewModel();

            DocumentType = new List<DropDown>
            {
                new DropDown { Name = "Driver's Licence", Code = "Driver's Licence" },
                new DropDown { Name = "International Passport", Code = "International Passport" },
                new DropDown { Name = "National ID", Code = "National ID" },
                new DropDown { Name = "Voters' Card", Code = "Voters' Card" },

            };


            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            if (!string.IsNullOrEmpty(Principal))
            {
                var user = authenticated.User;

                if (user.Identity.IsAuthenticated)
                {
                    ApplicationUserId = user.FindFirstValue(ClaimTypes.Sid);

                    await GetProfile();
                }
                else
                {
                    NavigationManager.NavigateTo("/Identity/Account/LogIn");
                }
            }
            else
            {
                NavigationManager.NavigateTo("/Identity/Account/LogIn");
            }

            await MapToModel();
        }

        private async Task OnSave()
        {
            if (await _fluentValidationValidator!.ValidateAsync())
            {
                await MapToCommand();
                SubmitBtn = "Submitting ...";
                Disable = true;
                StateHasChanged();
                var rsp = await DataService
                    .UpdateMemberProfile<UpdateMemberProfileCommand, CommandResult<MemberProfileViewModel>>(
                        UpdateCommand);

                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(UpdateCommand)}");
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
                    SubmitBtn = "Submit";
                    Disable = false;
                    StateHasChanged();
                }
                else
                {
                    await OnUpdateDocumentChanged.InvokeAsync(true);
                }
            }
        }

        private async Task MapToCommand()
        {
            UpdateCommand = new UpdateMemberProfileCommand();
            UpdateCommand = Mapper.Map<UpdateMemberProfileCommand>(UpdateModel);
            UpdateCommand.ProfileImageUrl = Model.PassportPhotograph;
            UpdateCommand.IdentificationUrl = Model.Document;
            UpdateCommand.IdentificationType = Model.DocumentTypeId;
            UpdateCommand.IdentificationNumber = Model.DocumentNumber;
            UpdateCommand.SubmitKyc = true;
            UpdateCommand.KycCompletedDate = DateTime.UtcNow;
        }

        private async Task MapToModel()
        {
            //UpdateCommand = new UpdateMemberProfileCommand();
            //UpdateCommand = Mapper.Map<UpdateMemberProfileCommand>(UpdateModel);
            Model.PassportPhotograph = UpdateModel.ProfileImageUrl;

            Passport = UpdateModel.ProfileImageUrl;
            Member_Document = UpdateModel.IdentificationUrl;

            Model.DocumentTypeId = UpdateModel.IdentificationType;
            Model.DocumentNumber = UpdateModel.IdentificationNumber;
            IsApproved = UpdateModel.Status;
        }

        public async Task OnChangePassportUpload(UploadChangeEventArgs args)
        {
            ErrorMessage = string.Empty;
            showPassportError = false;
            await InvokeAsync(StateHasChanged);
            var file = args.Files[0];
            if (file != null)
            {
                var response = ValidatePassport(file);
                if (string.IsNullOrEmpty(response))
                {
                    Model.PassportPhotograph = Passport = ConvertFileToBase64(file);
                    ErrorMessage = "Upload was successfull";
                    showPassportSuccess = true;
                    showPassportError = false;
                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    ErrorMessage = response;
                    showPassportError = true;
                    showPassportSuccess = false;
                    await InvokeAsync(StateHasChanged);
                }
            }
            else
            {
                ErrorMessage = "File not found";
                showPassportError = true;
                showPassportSuccess = false;
                await InvokeAsync(StateHasChanged);
            }
        }

        public async Task OnChangeDocumentUpload(UploadChangeEventArgs args)
        {
            ErrorMessage = string.Empty;
            showPassportError = false;
            await InvokeAsync(StateHasChanged);
            var file = args.Files[0];
            if (file != null)
            {
                var response = ValidatePassport(file);
                if (string.IsNullOrEmpty(response))
                {
                    if (file.FileInfo.Type.ToLower() != "pdf")
                    {
                        Model.Document = Member_Document = ConvertFileToBase64(file);
                    }
                    else
                    {
                        byte[] bytes = file.Stream.ToArray();
                        string base64 = Convert.ToBase64String(bytes);

                        //Convert to PDF
                        string base64PDF = @"data:application/pdf;base64," + base64;

                        Model.Document = Member_Document = base64PDF;
                    }

                    ErrorMessage = "Upload was successfull";
                    showPassportSuccess = true;
                    showPassportError = false;
                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    ErrorMessage = response;
                    showPassportError = true;
                    showPassportSuccess = false;
                    await InvokeAsync(StateHasChanged);
                }
            }
            else
            {
                ErrorMessage = "File not found";
                showPassportError = true;
                showPassportSuccess = false;
                await InvokeAsync(StateHasChanged);
            }
        }

        private string ConvertFileToBase64(UploadFiles file)
        {
            byte[] bytes = file.Stream.ToArray();

            string base64 = Convert.ToBase64String(bytes);

            return @"data:image/" + file.FileInfo.Type + ";base64," + base64;
        }

        private string ValidatePassport(UploadFiles file)
        {
            List<string> allowedFileType = new List<string>();
            allowedFileType.Add("jpg");
            allowedFileType.Add("jpeg");
            allowedFileType.Add("png");
            allowedFileType.Add("pdf");
            if (!allowedFileType.Contains(file.FileInfo.Type.ToLower()))
                return $"This file type {file.FileInfo.Type} is not allowed";
            if (file.FileInfo.Size > (10 * 1024 * 1024))
                return $"This file size {file.FileInfo.Size * (1024 * 1024)} is not allowed";
            return "";
        }

        public async Task GetProfile()
        {
            var rsp = await DataService.GetValue<List<MemberProfileMasterView>>(
                nameof(MemberProfileMasterView), ApplicationUserId);


            if (!rsp.IsSuccessStatusCode)
            {
                if (!string.IsNullOrEmpty(rsp.Error.Content))
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
                }
                else
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = "Oops! Something Went Wrong. Please try again later. Thanks",
                        NotificationType = NotificationType.Error
                    });
                }
            }
            else
            {
                List<MemberProfileMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<MemberProfileMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().ApplicationUserId) &&
                    rspResponse.FirstOrDefault().ApplicationUserId == ApplicationUserId)
                {
                    UpdateModel = new MemberProfileMasterView();
                    UpdateModel = rspResponse.FirstOrDefault();
                    UpdateModel = Mapper.Map<MemberProfileMasterView>(rspResponse.FirstOrDefault());
                    ;
                }
            }
        }
    }

    public class DropDown
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}