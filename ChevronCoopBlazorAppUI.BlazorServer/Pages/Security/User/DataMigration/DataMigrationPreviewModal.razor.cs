using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Commons;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.DataMigration
{
    public partial class DataMigrationPreviewModal
    {
        [Parameter]
        public MemberBulkUploadViewModel Model { get; set; }

        private bool showValidData = false;
        private bool showInValidData = false;

        [Parameter]
        public bool showAlertComponent { get; set; } = false;

        [Parameter]
        public List<MemberDataUpload> ValidDataModel { get; set; }

        [Parameter]
        public List<MemberDataUpload> InValidDataModel { get; set; }

        [Parameter]
        public EventCallback<MemberBulkUploadViewModel> ModelChanged { get; set; }

        [Parameter]
        public EventCallback<List<MemberDataUpload>> OnSuccessfulUploadedDataChanged { get; set; }

        [Parameter]
        public EventCallback<bool> OnSuccessfulCommitChanged { get; set; }

        [Parameter]
        public EventCallback<bool> showAlertComponentChanged { get; set; }


        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        public IFileExportService FileExportService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        string ApprovalWorkFlow = string.Empty;

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        private string CommitButtonText { get; set; } = "Commit";
        private bool CommitButtonDisabled { get; set; } = false;

        [Inject]
        ILogger<DataMigrationPreviewModal> Logger { get; set; }

        protected override async Task OnInitializedAsync()
        {
            showValidData = false;
            showInValidData = false;
            Model = new MemberBulkUploadViewModel();
            ValidDataModel = new List<MemberDataUpload>();
            ValidDataModel = Model.AcceptedMemberDataUpload;
            InValidDataModel = Model.RejectedMemberDataUpload;
            showAlertComponent = false;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                ValidDataModel = Model.AcceptedMemberDataUpload;
                //await OnRefresh();
            }
        }

        public async Task OnShowValidEntry()
        {
            ValidDataModel = Model.AcceptedMemberDataUpload;
            showInValidData = false;
            showValidData = true;
            showAlertComponent = false;
            await OnSuccessfulUploadedDataChanged.InvokeAsync(ValidDataModel);
        }

        public async Task OnShowInValidEntry()
        {
            InValidDataModel = Model.RejectedMemberDataUpload;
            showInValidData = true;
            showValidData = false;
            showAlertComponent = false;
            await OnSuccessfulUploadedDataChanged.InvokeAsync(InValidDataModel);
        }

        //Exporting the data to csv or excel
        private async Task OnExport()
        {
            if (showInValidData)
            {
                if (InValidDataModel != null && InValidDataModel.Count > 0)
                {
                    await FileExportService.ExportDataMigrationToCSV(InValidDataModel, false);
                }
                else
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = "No record to Export",
                        NotificationType = NotificationType.Error
                    });
                }
            }

            if (showValidData)
                if (ValidDataModel != null && ValidDataModel.Count > 0)
                {
                    await FileExportService.ExportDataMigrationToCSV(ValidDataModel, true);
                }
                else
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = "No record to Export",
                        NotificationType = NotificationType.Error
                    });
                }
        }

        //committing the valid Data to the server
        private async Task OnCommit()
        {
            CommitButtonText = "Saving...";
            CommitButtonDisabled = true;
            StateHasChanged();
            try
            {
                await OnSuccessfulCommitChanged.InvokeAsync(true);
                var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                if (user.Identity.IsAuthenticated)
                {
                    await GetApprovalWorkFlow();
                    var LoggedInUserId = user.FindFirstValue(ClaimTypes.Sid);
                    if (!string.IsNullOrEmpty(LoggedInUserId))
                    {
                        CreateMemberBulkUploadCommand command = new CreateMemberBulkUploadCommand()
                        {
                            MemberDataUploads = ValidDataModel,
                            UploadedByUserId = LoggedInUserId,
                            ApprovalWorkflowId = ApprovalWorkFlow,
                            SessionId = Model.SessionId
                        };
                        var j = JsonSerializer.Serialize(command);
                        Logger.LogInformation($"payload->{j}");
                        var rsp = await DataService
                            .ProcessRequest<CreateMemberBulkUploadCommand, CommandResult<MemberBulkUploadViewModel>>(
                                "MemberBulkUploads", "save", command);

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

                            await notificationService.Open(new NotificationConfig()
                            {
                                Message = "Error",
                                Description = msg,
                                NotificationType = NotificationType.Error
                            });


                            CommitButtonText = "Commit";
                            CommitButtonDisabled = false;
                            StateHasChanged();
                        }
                        else
                        {
                            CommitButtonText = "Commit";
                            CommitButtonDisabled = false;
                            await notificationService.Open(new NotificationConfig()
                            {
                                Message = "Success",
                                Description = "File uploaded for Approval",
                                NotificationType = NotificationType.Success
                            });
                            StateHasChanged();

                            //UploadDataModel = JsonSerializer.Deserialize<MemberBulkUploadViewModel>(rsp.Content.Response.ToJson());
                            //await OnUploadDataChanged.InvokeAsync(UploadDataModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = ex.Message,
                    NotificationType = NotificationType.Error
                });
            }
        }

        private async Task GetApprovalWorkFlow()
        {
            var rsp = await DataService.GetMasterView<List<ApprovalWorkflowViewModel>>("ApprovalWorkflows");

            if (rsp.IsSuccessStatusCode)
            {
                List<ApprovalWorkflowViewModel> rspResponse =
                    JsonSerializer.Deserialize<List<ApprovalWorkflowViewModel>>(rsp.Content.ToJson());
                if (rspResponse != null)
                {
                    if (rspResponse.Any())
                    {
                        ApprovalWorkFlow = rspResponse.FirstOrDefault().Id;
                    }
                }
            }
            else
            {
                var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                var msg = rspContent?.Response;
                if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                {
                    msg = rspContent.ValidationErrors[0].Error;
                }
            }
        }
    }
}