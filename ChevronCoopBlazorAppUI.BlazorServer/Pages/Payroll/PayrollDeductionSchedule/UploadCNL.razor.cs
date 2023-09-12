using AntDesign;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionItems;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Payroll;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using OfficeOpenXml;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Payroll.PayrollDeductionSchedule
{
    public partial class UploadCNL
    {
        [Parameter]
        public string schedulename { get; set; }
        public IBrowserFile uploadFiles { get; set; }
        public List<PayrollDeductionItemViewModel> memberDataUploads { get; set; } = new List<PayrollDeductionItemViewModel>();

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        string LoggedInUserId = string.Empty;

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        //[Inject]
        //ILogger<RetireeSwitchRequestGridForm> Logger { get; set; }

        string ApprovalWorkFlow = string.Empty;

        [Parameter]
        public PayrollDeductionItemViewModel UploadDataModel { get; set; }

        [Parameter]
        public string scheduleId { get; set; }

        [Parameter]
        public EventCallback<ImportPayrollDeductionItemCommand> OnUploadDataChanged { get; set; }

        private bool showError = false;
        private int progressPercentage = 0;
        private bool showProgressbar = false;
        private string errorMessage = string.Empty;
        private string fileName = string.Empty;
        private bool disableUploadButton = true;

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        private string bearToken { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUser();
            UploadDataModel = new PayrollDeductionItemViewModel();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);
                if (string.IsNullOrEmpty(bearToken))
                {
                    _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
                }

                HeaderData.Add("Bearer", bearToken);
            }
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }


        private async Task OnChange(InputFileChangeEventArgs args)
        {
            uploadFiles = null;
            uploadFiles = args.File;
            if (uploadFiles != null)
            {
                if (!string.IsNullOrEmpty(uploadFiles.Name))
                {
                    var fileType = uploadFiles.Name.Split('.');
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
                    }
                }

                disableUploadButton = false;
                fileName = uploadFiles.Name;
                showProgressbar = true;
                StateHasChanged();
            }
        }

        private async Task<List<MemberDataUpload>> ReadCsvFile()
        {
            List<MemberDataUpload> records = new List<MemberDataUpload>();

            return records;
        }

        private async Task<List<PayrollDeductionItemViewModel>> ReadExcel()
        {
            List<PayrollDeductionItemViewModel> records = new List<PayrollDeductionItemViewModel>();

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
                        var record = new PayrollDeductionItemViewModel
                        {
                            MemberId = (worksheet.Cells[row, 1].Value != null)
                                ? worksheet.Cells[row, 1].Value.ToString().Trim()
                                : "",
                            MemberName = worksheet.Cells[row, 2].Value != null
                                ? worksheet.Cells[row, 2].Value.ToString().Trim()
                                : "", 
                            PayrollCode = worksheet.Cells[row, 3].Value != null
                                ? worksheet.Cells[row, 3].Value.ToString().Trim()
                                : "",
                            Amount = worksheet.Cells[row, 4].Value != null
                                ? Convert.ToDecimal(worksheet.Cells[row, 4].Value.ToString().Trim())
                                : 00m,
                            PayrollDate = worksheet.Cells[row, 5].Value != null
                                ? DateTime.Parse(worksheet.Cells[row, 5].Value.ToString().Trim())
                                : DateTime.Now,

                            PayrollDeductionScheduleId = scheduleId
                        };
                        records.Add(record);
                        progressPercentage = (int)(records.Count * 100 / rowCount);
                        StateHasChanged();
                    }
                }
            }

            return records;
        }

        private async Task SendDataToServer()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            try
            {
                if (user.Identity.IsAuthenticated)
                {
                  
                    LoggedInUserId = user.FindFirstValue(ClaimTypes.Sid);
                    if (!string.IsNullOrEmpty(LoggedInUserId))
                    {
                        ImportPayrollDeductionItemCommand command = new ImportPayrollDeductionItemCommand()
                        {
                            PayrollDeductionItems = memberDataUploads,
                            CreatedByUserId = LoggedInUserId
                        };
                        var j = JsonSerializer.Serialize(command);
                      //  Logger.LogInformation($"payload->{j}");
                        var rsp = await DataService
                            .ProcessRequest<ImportPayrollDeductionItemCommand, CommandResult<PayrollDeductionItemViewModel>>(
                               nameof(PayrollDeductionItem), "import", command);
                        if (rsp.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        {
                            await notificationService.Open(new NotificationConfig()
                            {
                                Message = "Error",
                                Description = "Oops! Something went wrong. Please try again",
                                NotificationType = NotificationType.Error
                            });
                        }
                        if (!rsp.IsSuccessStatusCode)
                        {
                            showError = true;
                            var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                            var msg = rspContent?.Response;
                            if (rspContent != null && rspContent.ValidationErrors != null &&
                                rspContent.ValidationErrors.Any())
                            {
                                msg = rspContent.ValidationErrors[0].Error;
                            }

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
                        else
                        {
							progressPercentage = 100;
							StateHasChanged();
							await notificationService.Open(new NotificationConfig()
                            {
                                Message = "Success",
                                Description = "Success",
                                NotificationType = NotificationType.Success
                            });
							//var payload = JsonSerializer.Serialize(command);
							//await _auditLogService.LogAudit("Data Migration Upload.", $"Uploaded data for migration.",
							//    "Security", payload, CurrentUser);
							//UploadDataModel =
							//    JsonSerializer.Deserialize<PayrollDeductionItemViewModel>(rsp.Content.Response.ToJson());

							//  await OnUploadDataChanged.InvokeAsync(UploadDataModel);
							schedulename = schedulename.Replace(" ", "-");
							await InvokeAsync(StateHasChanged);
							_navigationManager.NavigateTo($"/payroll/ImportFromCNL/{schedulename}/{scheduleId}", true);
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
        //public async Task OnClose()
        //{

        //    ShowModal = false;
        //    await ShowModalChanged.InvokeAsync(ShowModal);

        //    showPopup = false;
        //    NavigationManager.NavigateTo("identity/account/login", true);

        //}
    }
}