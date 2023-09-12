using AntDesign;
using AP.ChevronCoop.AppCore.Services.Helpers;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using OfficeOpenXml;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.NewDataMigration
{
    public partial class MigratedUserGrid
    {
        Drawer uploadDrawer;
        Drawer previewDrawer;
        Drawer errorDrawer;
        string ValidActive { get; set; } = string.Empty;
        string InValidActive { get; set; } = string.Empty;
        string Active { get; set; } = "text-blue-600 border-b-2 border-blue-600 active";
        string Inactive { get; set; } = "border-b-2 border-transparent";
        public IBrowserFile uploadFiles { get; set; }
        public MemberDataUpload ErrorRecord { get; set; } = new MemberDataUpload();
        public List<MemberDataUpload> memberDataUploads { get; set; } = new List<MemberDataUpload>();

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        string LoggedInUserId = string.Empty;

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        ILogger<RetireeSwitchRequestGridForm> Logger { get; set; }

        string ApprovalWorkFlow = string.Empty;

        [Parameter]
        public MemberBulkUploadViewModel UploadDataModel { get; set; }

        [Parameter]
        public EventCallback<MemberBulkUploadViewModel> OnUploadDataChanged { get; set; }

        public List<MemberProfileMasterView> BulkUpload { get; set; }
        public MemberProfileMasterView[] BulkUploadList { get; set; }

        private bool showError = false;
        private int progressPercentage = 0;
        private bool showProgressbar = false;
        private string errorMessage = string.Empty;
        private string fileName = string.Empty;
        private string fileSize = string.Empty;
        private bool disableUploadButton = true;
        private bool showCompleteUpload = false;
        bool showUploadDrawer { get; set; } = false;
        bool showPreviewDrawer { get; set; } = false;
        bool showErrorDrawer { get; set; } = false;
        bool isValidRecord { get; set; } = false;
        bool isInValidRecord { get; set; } = false;
        bool showNotification { get; set; } = false;

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        public IFileExportService FileExportService { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        BrowserDimension BrowserDimension;
        private string CommitButtonText { get; set; } = "Commit";
        private bool CommitButtonDisabled { get; set; } = false;
        string searchText;
        private WhereFilter kycFilter;
        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();

        [Inject]
        WebConfigHelper Config { get; set; }

        SfGrid<MemberProfileViaUploadMasterView> grid;
        string ErrorDetails = "";
        private Query QueryGrid; // = new Query();
        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(MemberProfileViaUploadMasterView)}";

        [Inject]

        NavigationManager NavigationManager { get; set; }
        string SessionId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            ErrorRecord = new MemberDataUpload();
            QueryGrid = new Query();

            //await GetApprovedBulkUpload();
        }

        public void ActionCompletedHandler(ActionEventArgs<MemberProfileViaUploadMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<MemberProfileViaUploadMasterView> Args)
        {
            if (Args.Data.Status == "PENDING_APPROVAL ")
            {
                Args.Data.Status = "PENDING APPROVAL";
            }
        }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            //this.ErrorDetailMsg = args.Error.ToString();
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                uploadDrawer.Width = (int)(BrowserDimension.Width * 0.50);
                previewDrawer.Width = (int)(BrowserDimension.Width * 0.50);
                errorDrawer.Width = (int)(BrowserDimension.Width * 0.30);
                //await OnRefresh();
                ErrorRecord = new MemberDataUpload();
            }
            //StateHasChanged();

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        async Task onUploadDone()
        {
            showUploadDrawer = false;
        }

        async Task onPreviewDone()
        {
            showPreviewDrawer = false;
        }

        async Task OnShowUpload()
        {
            showUploadDrawer = true;
        }

        async Task LoadValidEntry()
        {
            isValidRecord = true;
            isInValidRecord = false;
            ValidActive = Active;
            InValidActive = Inactive;
            StateHasChanged();
        }

        async Task onErrorDone()
        {
            showErrorDrawer = false;
        }

        async Task LoadInValidEntry()
        {
            isValidRecord = false;
            isInValidRecord = true;
            InValidActive = Active;
            ValidActive = Inactive;
            StateHasChanged();
        }

        private async Task OnChange(InputFileChangeEventArgs args)
        {
            uploadFiles = null;
            uploadFiles = args.File;
            if (uploadFiles != null)
            {
                if (!string.IsNullOrEmpty(uploadFiles.Name))
                {
                    fileName = uploadFiles.Name;
                    var fileType = uploadFiles.Name.Split('.');
                    fileSize = $"{(uploadFiles.Size / 1024)} kb";
                    // var progress = new FileUploadProgress(uploadFiles.Name, uploadFiles.Size);
                    //uploadedFiles.Add(progress);
                    if (fileType[1] != "xlsx" && fileType[1] != "csv")
                    {
                        showError = true;
                        errorMessage = "Invalid file type (.csv, and excel only)";
                        StateHasChanged();
                        return;
                    }
                    else
                    {
                        showError = false;
                        StateHasChanged();
                        await OnUploadFile();
                    }
                }

                disableUploadButton = false;
                fileName = uploadFiles.Name;
                showProgressbar = true;
                StateHasChanged();
            }
        }

        private async Task<List<MemberDataUpload>> ReadExcel()
        {
            List<MemberDataUpload> records = new List<MemberDataUpload>();

            using (MemoryStream ms = new MemoryStream())
            {
                showProgressbar = true;
                StateHasChanged();
                //copy data from the file to memory stream
                await uploadFiles.OpenReadStream().CopyToAsync(ms);
                //position the cursor at the beginning of the memory stream
                ms.Position = 0;
                // LicenseContext of the ExcelPackage class :
                //ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(ms))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;
                    var colCount = worksheet.Dimension.Columns;
                    if (rowCount == 0)
                    {
                        return records;
                    }

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var record = new MemberDataUpload
                        {
                            FirstName = (worksheet.Cells[row, 1].Value != null)
                                ? worksheet.Cells[row, 1].Value.ToString().Trim()
                                : "",
                            LastName = worksheet.Cells[row, 2].Value != null
                                ? worksheet.Cells[row, 2].Value.ToString().Trim()
                                : "",
                            Gender = worksheet.Cells[row, 3].Value != null
                                ? worksheet.Cells[row, 3].Value.ToString().Trim()
                                : "",
                            Email = worksheet.Cells[row, 4].Value != null
                                ? worksheet.Cells[row, 4].Value.ToString().Trim()
                                : "",
                            PhoneNumber = worksheet.Cells[row, 5].Value != null
                                ? worksheet.Cells[row, 5].Value.ToString().Trim()
                                : "",
                            MembershipNumber = worksheet.Cells[row, 6].Value != null
                                ? worksheet.Cells[row, 6].Value.ToString().Trim()
                                : "",
                            MemberType = worksheet.Cells[row, 7].Value != null
                                ? worksheet.Cells[row, 7].Value.ToString().Trim()
                                : "",
                            Status = "Active"

                        };
                        if (!string.IsNullOrEmpty(record.Email) && !string.IsNullOrEmpty(record.FirstName) &&
                            !string.IsNullOrEmpty(record.LastName))
                            records.Add(record);
                        progressPercentage = (int)(records.Count * 100 / rowCount);
                        StateHasChanged();
                    }
                }
            }

            return records;
        }

        public async Task OnUploadFile()
        {
            try
            {
                // Do any additional processing here (e.g. check if a file was selected)
                if (uploadFiles == null)
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Info",
                        Description = "No file was selected",
                        NotificationType = NotificationType.Success
                    });
                    return;
                }

                memberDataUploads = await ReadExcel();
                if (memberDataUploads?.Count() > 0)
                {
                    await SendDataToServer();
                }
                else
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Info",
                        Description = "No record found on the uploaded file!",
                        NotificationType = NotificationType.Info
                    });
                    return;
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

        private async Task SendDataToServer()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            try
            {
                if (user.Identity.IsAuthenticated)
                {
                    SessionId = NHiloHelper.GetNextKey(nameof(MemberBulkUploadSession)).ToString();
                    await GetApprovalWorkFlow();
                    LoggedInUserId = user.FindFirstValue(ClaimTypes.Sid);
                    if (!string.IsNullOrEmpty(LoggedInUserId))
                    {
                        ValidateMemberBulkUploadCommand command = new ValidateMemberBulkUploadCommand()
                        {
                            MemberDataUploads = memberDataUploads,
                            UploadedByUserId = LoggedInUserId,
                            ApprovalWorkflowId = ApprovalWorkFlow,
                            SessionId = SessionId
                        };
                        var j = JsonSerializer.Serialize(command);
                        Logger.LogInformation($"payload->{j}");
                        var rsp = await DataService
                            .ProcessRequest<ValidateMemberBulkUploadCommand, CommandResult<MemberBulkUploadViewModel>>(
                                "MemberBulkUploads", "validate", command);

                        if (!rsp.IsSuccessStatusCode)
                        {
                            showError = true;
                            var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                            var msg = rspContent?.Response;
                            if (rspContent != null && rspContent.ValidationErrors != null &&
                                rspContent.ValidationErrors.Any())
                            {
                                UploadDataModel = GetValidationError(rspContent.ValidationErrors);
                                progressPercentage = 100;
                                showCompleteUpload = true;
                                StateHasChanged();
                                await OnUploadDataChanged.InvokeAsync(UploadDataModel);
                            }
                            else
                            {
                                if (msg == null && rspContent?.Message != null)
                                    msg = rspContent.Message;

                                errorMessage = msg;
                                await notificationService.Open(new NotificationConfig()
                                {
                                    Message = "Error",
                                    Description = msg,
                                    NotificationType = NotificationType.Error
                                });
                            }


                        }
                        else
                        {
                            UploadDataModel =
                                JsonSerializer.Deserialize<MemberBulkUploadViewModel>(rsp.Content.Response.ToJson());

                            progressPercentage = 100;
                            showCompleteUpload = true;
                            StateHasChanged();
                            await OnUploadDataChanged.InvokeAsync(UploadDataModel);
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
                    else
                    {
                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Error",
                            Description = "No approval workflow found",
                            NotificationType = NotificationType.Error
                        });
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

        public async Task ViewRecords()
        {
            isValidRecord = true;
            showUploadDrawer = false;
            isInValidRecord = false;
            ValidActive = Active;
            InValidActive = Inactive;
            showPreviewDrawer = true;
        }

        private async Task OnExport()
        {
            if (UploadDataModel != null && UploadDataModel.AcceptedMemberDataUpload.Count > 0)
            {
                await FileExportService.ExportDataMigrationToCSV(UploadDataModel.AcceptedMemberDataUpload, true);
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

        public async Task OnViewRow(MemberProfileViaUploadMasterView row)
        {
            NavigationManager.NavigateTo($"/data-migration-profile/{row.ApplicationUserId}");
        }

        private async Task OnDownload()
        {
            if (UploadDataModel != null && UploadDataModel.RejectedMemberDataUpload.Count > 0)
            {
                await FileExportService.ExportDataMigrationToCSV(UploadDataModel.RejectedMemberDataUpload, false);
            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "No record to Download",
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
                            MemberDataUploads = UploadDataModel.AcceptedMemberDataUpload,
                            UploadedByUserId = LoggedInUserId,
                            ApprovalWorkflowId = ApprovalWorkFlow,
                            SessionId = UploadDataModel.SessionId != null ? UploadDataModel.SessionId : Guid.NewGuid().ToString()
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
                            Logger.LogInformation($"payload->{JsonSerializer.Serialize(msg)}");
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
                            showNotification = true;
                            isValidRecord = false;
                            isInValidRecord = false;
                            StateHasChanged();
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

        private async Task OnShowMemberDataUploadErrorChangedHandler(MemberDataUpload row)
        {
            if (row != null)
            {
                ErrorRecord = row;
                showErrorDrawer = true;
                await InvokeAsync(StateHasChanged); // trigger a UI refresh
            }
        }

        public async Task GetApprovedBulkUpload()
        {
            try
            {
                var rsp = await DataService.GetRecord<List<MemberProfileMasterView>>(
                    nameof(MemberBulkUploadSessionMasterView), "getUploadedMemebers");

                if (rsp.IsSuccessStatusCode)
                {
                    List<MemberProfileMasterView> rspResponse =
                        JsonSerializer.Deserialize<List<MemberProfileMasterView>>(rsp.Content.ToJson());

                    if (rspResponse?.Count > 0)
                    {
                        BulkUpload = rspResponse;
                        BulkUploadList = rspResponse.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                WhereFilter firstNameFilter = new WhereFilter
                {
                    Field = nameof(MemberProfileViaUploadMasterView.FirstName),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter caiFilter = new WhereFilter
                {
                    Field = nameof(MemberProfileViaUploadMasterView.CAI),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter memberShipFilter = new WhereFilter
                {
                    Field = nameof(MemberProfileViaUploadMasterView.MemberBulkUploadTempId_MembershipNumber),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter emailFilter = new WhereFilter
                {
                    Field = nameof(MemberProfileViaUploadMasterView.PrimaryEmail),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter addressFilter = new WhereFilter
                {
                    Field = nameof(MemberProfileViaUploadMasterView.Address),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter lastNameFilter = new WhereFilter
                {
                    Field = nameof(MemberProfileViaUploadMasterView.LastName),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter phoneNoFilter = new WhereFilter
                {
                    Field = nameof(MemberProfileViaUploadMasterView.PrimaryPhone),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter statusNoFilter = new WhereFilter
                {
                    Field = nameof(MemberProfileViaUploadMasterView.Status),
                    Operator = "contains",
                    value = searchText
                };
                QueryGrid = new Query().Where(firstNameFilter.Or(lastNameFilter).Or(caiFilter).Or(addressFilter)
                    .Or(emailFilter).Or(phoneNoFilter).Or(statusNoFilter).Or(memberShipFilter));
            }
            else
            {
                await OnRefresh();
            }

            //if (!string.IsNullOrWhiteSpace(searchText))
            //{
            //    BulkUpload.Where(f => (f.LastName != null && f.LastName.Contains(searchText)) || (f.FirstName != null && f.FirstName.Contains(searchText)) || (f.Status != null && f.Status.Contains(searchText)) || (f.ApplicationUserId_PhoneNumber != null && f.ApplicationUserId_PhoneNumber.Contains(searchText)) || (f.CAI != null && f.CAI.Contains(searchText)) || (f.Address != null && f.Address.Contains(searchText)) || (f.MembershipId != null && f.MembershipId.Contains(searchText))).ToList();
            //    BulkUploadList = BulkUpload.ToArray();

            //}
            //else
            //{
            //    await GetApprovedBulkUpload();
            //}
        }

        private async Task OnRefresh()
        {
            searchText = string.Empty;
            QueryGrid = new Query().Where(kycFilter);
            grid.Refresh();

            await Task.CompletedTask;
        }

        private async Task OnSearchEnter(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                if (!string.IsNullOrWhiteSpace(searchText))
                    await OnSearch();
                else
                    await GetApprovedBulkUpload();
            }
        }

        public async Task OnPage() => NavigationManager.NavigateTo("/security/data-migration", true);
        private async Task OnGetTemplate()
        {
            List<MemberUploadTemplateModelDTO> MemberDataUploadList = new List<MemberUploadTemplateModelDTO>();
            MemberUploadTemplateModelDTO memberDataUpload = new MemberUploadTemplateModelDTO()
            {
                SN = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                Gender = "Male",
                Email = "example@cemcs.com",
                PhoneNumber = "09030141414",
                MembershipNumber = "998912",
                MemberType = "Regular",
            };
            MemberDataUploadList.Add(memberDataUpload);
            await FileExportService.DownloadTemplateDataMigrationToCSV(MemberDataUploadList);


        }
        private MemberBulkUploadViewModel GetValidationError(List<ModelValidationError> errors)
        {
            MemberBulkUploadViewModel memberBulkUploadViewModel = new MemberBulkUploadViewModel();
            memberBulkUploadViewModel.RejectedMemberDataUpload = new List<MemberDataUpload>();
            memberBulkUploadViewModel.AcceptedMemberDataUpload = new List<MemberDataUpload>();
            memberBulkUploadViewModel.SessionId = SessionId;


            var uploads = memberDataUploads.ToArray();



            var Properties = typeof(MemberDataUpload).GetProperties().Select(x => x.Name).ToList();



            foreach (var d in errors)
            {
                var fieldName = d.FieldName.Substring(d.FieldName.LastIndexOf('.') + 1);
                var error = d.Error;
                //var index = GetIndex(d.FieldName);
                var digit = Regex.Replace(d.FieldName, @"[^\d]", "");
                int index = Convert.ToInt32(digit);
                uploads[index].IsValid = false;
                if (uploads[index].Messages == null)
                    uploads[index].Messages = new List<ValidationMessage>();
                uploads[index].Messages.Add(new ValidationMessage { FieldName = fieldName, ErrorReport = "Invalid data" });
            }
            memberDataUploads = uploads.ToList();
            memberBulkUploadViewModel.RejectedMemberDataUpload = uploads.Where(f => f.Messages != null).ToList();
            memberBulkUploadViewModel.AcceptedMemberDataUpload = uploads.Where(f => f.Messages == null).ToList();
            foreach (var d in memberBulkUploadViewModel.AcceptedMemberDataUpload)
            {
                d.IsValid = true;
            }
            return memberBulkUploadViewModel;
        }
        public int GetIndex(string fieldname)
        {
            int result = 0;
            for (int i = 0; i < fieldname.Length; i++)
            {
                if (Char.IsDigit(fieldname[i]))
                    result = fieldname[i];
            }
            return result;
        }
    }
}