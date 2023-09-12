
using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Payroll;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Text.Json;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionScheduleItems;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionScheduleItems;
using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalWorkflows;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals.Approvalworkflows;
using DocumentFormat.OpenXml.Office2010.Excel;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionMatch;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionItems;
using AP.ChevronCoop.AppCore.Payroll.PayrollDeductionMatch;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Payroll.PayrollCronJobConfigurations;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using CurrieTechnologies.Razor.SweetAlert2;
namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Payroll
{


    public partial class MatchDeduction
    {
        [Parameter]
        public string schedulename { get; set; }
        [Inject]
        ILogger<PayrollDeductionScheduleItemGrid> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Parameter]
        public string scheduleId { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        //[Inject]
        //SessionService SessionService { get; set; }

        private WhereFilter statusFilter { get; set; }
        string Active { get; set; } = "text-blue-600 border-b-2 border-blue-600 active";
        string Inactive { get; set; } = "border-b-2 border-transparent";
        string All { get; set; } = string.Empty;
        string Complete { get; set; } = string.Empty;
        string Failed { get; set; } = string.Empty;
        string Started { get; set; } = string.Empty;
        string Pending { get; set; } = string.Empty;

        SfGrid<PayrollDeductionScheduleItemMasterView> grid;

        public List<PayrollDeductionScheduleItemMasterView> PayrollDeductionScheduleItemMasterViewSrc1 = new List<PayrollDeductionScheduleItemMasterView>();
        public List<PayrollDeductionScheduleItemMasterView> PayrollDeductionScheduleItemMasterViewSrc = new List<PayrollDeductionScheduleItemMasterView>();
       
		private Query QueryGrid;// = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;

        BrowserDimension BrowserDimension;
        string ErrorDetails = "";
        ErrorDetails errors;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {


                await OnRefresh();

            }
            //StateHasChanged();

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");

        }

        async Task onFiltering(string filter)
        {
            ActivateTab();
            if (filter == PayrollErrorType.PENDING.ToString())
            {
                Pending = Active;
            }
            else if (filter == "SUCCESS")
            {
                Started = Active;
            }
          
            else if (filter == "ERROR")
            {
                Failed = Active;
            }
            else
            {
                All = Active;
            }


            if (filter == "all")
            {
                PayrollDeductionScheduleItemMasterViewSrc = PayrollDeductionScheduleItemMasterViewSrc1
                    .OrderByDescending(c => c.DateCreated).ToList();
            }
            else if(filter =="ERROR")
            {
                PayrollDeductionScheduleItemMasterViewSrc = PayrollDeductionScheduleItemMasterViewSrc1
                    .Where(c => c.CurrentStatus != "MATCHED" && c.CurrentStatus != PayrollErrorType.COMPLETE.ToString() && c.CurrentStatus != PayrollErrorType.PENDING.ToString() && c.CurrentStatus != "SUCCESS").ToList();
            }
            else
            {
                PayrollDeductionScheduleItemMasterViewSrc = PayrollDeductionScheduleItemMasterViewSrc1
             .Where(c => c.CurrentStatus == filter).ToList();

            }
            await InvokeAsync(StateHasChanged);

            // _navigationManager.NavigateTo($"/schedule/jobsProcessing/{filter}/{scheduleId}", forceLoad: true);
        }

        public async Task GetPayrollDeductionScheduleItemMasterView()
        {
            //var rsp = await DataService.GetValue<List<PayrollDeductionScheduleItemMasterView>>(
            //    nameof(PayrollDeductionScheduleItemMasterView), "matchPayroll", scheduleId);
            var rsp = await DataService.GetAllValue<List<PayrollDeductionScheduleItemMasterView>>($"PayrollDeductionScheduleItemMasterView/matchPayroll/{scheduleId}");
            if (rsp.IsSuccessStatusCode)
            {
                PayrollDeductionScheduleItemMasterViewSrc = new List<PayrollDeductionScheduleItemMasterView>();

                PayrollDeductionScheduleItemMasterViewSrc1 =
                    JsonSerializer.Deserialize<List<PayrollDeductionScheduleItemMasterView>>(rsp.Content.ToJson());
                PayrollDeductionScheduleItemMasterViewSrc = PayrollDeductionScheduleItemMasterViewSrc1
                    .OrderByDescending(c => c.DateCreated).ToList();
                // ActivateTab();
            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = $"Error connecting to server {rsp.Error} - {rsp.Content} - {rsp.RequestMessage}",
                    NotificationType = NotificationType.Error
                });
            }
        }
        public void ActivateTab()
        {
            All = Inactive;
            Complete = Inactive;
            Pending = Inactive;
            Failed = Inactive;
            Started = Inactive;
        }
        private async Task removeHyphen()
        {
            schedulename = schedulename.Replace("-", " ");
        }

        protected override async Task OnInitializedAsync()
        {
            await removeHyphen();

            QueryGrid = new Query();
            await GetPayrollDeductionScheduleItemMasterView();
        }

        public async Task GetMacthPayroll()
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Are you sure?",
                Text = $"You want to Proceed with this action?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "Proceed",
                CancelButtonText = "Cancel"
            });

            if (string.IsNullOrEmpty(result.Value))
            {
                return;
            }
                var rsp = await DataService
                    .PostCommand<CreatePayrollDeductionMatchCommand, CommandResult<bool>>(
                        nameof(PayrollDeductionItem), "matchpayroll", new CreatePayrollDeductionMatchCommand { ScheduleId = scheduleId });
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
                var rspContent = JsonSerializer.Deserialize<CommandResult<bool>>(rsp.Content.ToJson());
                if (rspContent.Response)
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Success",
                        Description = "Matching Succesful",
                        NotificationType = NotificationType.Success
                    });

                    await GetPayrollDeductionScheduleItemMasterView();
                  // await StateHasChanged();
                }
                else
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = "Matching Not Succesful, try again",
                        NotificationType = NotificationType.Error
                    });
                }
          

            
             //   grid.Refresh();
            }
        }
		//public async Task MatchNewPayroll()
		//{
		//	var rsp = await DataService
		//			.GetValue<CreatePayrollDeductionMatchCommand, CommandResult<PayrollDeductionItemViewModel>>(
		//				nameof(PayrollDeductionItem), "matchpayroll", new CreatePayrollDeductionMatchCommand { ScheduleId = scheduleId });
		//	if (!rsp.IsSuccessStatusCode)
		//	{
		//		if (!string.IsNullOrEmpty(rsp.Error.Content))
		//		{
		//			var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

		//			var msg = rspContent?.Response;
		//			if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
		//			{
		//				msg = rspContent.ValidationErrors[0].Error;
		//			}

		//			if (msg == null && rspContent?.Message != null)
		//				msg = rspContent.Message;
		//			await notificationService.Open(new NotificationConfig()
		//			{
		//				Message = "Error",
		//				Description = msg,
		//				NotificationType = NotificationType.Error
		//			});
		//		}
		//		else
		//		{
		//			await notificationService.Open(new NotificationConfig()
		//			{
		//				Message = "Error",
		//				Description = "Oops! Something Went Wrong. Please try again later. Thanks",
		//				NotificationType = NotificationType.Error
		//			});
		//		}
		//	}
		//	else
		//	{
		//		var rspResponse =
		//			JsonSerializer.Deserialize<PayrollDeductionItemViewModel>(rsp.Content.Response.ToJson());
		//		if (rspResponse != null)
		//		{

		//		}
		//	}
		//}

		public void ActionCompletedHandler(ActionEventArgs<PayrollDeductionScheduleItemMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        public void ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = args.Error.ToString();
            StateHasChanged();

        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "gridPayrollDeductionScheduleItem_excelexport") //Id is combination of Grid's ID and itemname
            {
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Staff_Users.xlsx";
                await this.grid.ExportToExcelAsync(ExportProperties);
            }

            if (args.Item.Id == "gridPayrollDeductionScheduleItem_pdfexport") //Id is combination of Grid's ID and itemname
            {
                PdfExportProperties ExportProperties = new PdfExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Staff_Users.pdf";
                await this.grid.ExportToPdfAsync();
            }
        }



        private async Task OnRefresh()
        {

            //Console.WriteLine($"OnRefresh(), searchText->{searchText}");

            searchText = string.Empty;
            QueryGrid = new Query();
            grid.Refresh();

            await Task.CompletedTask;

        }

        private async Task OnSearchEnter(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            //Console.WriteLine($"OnSearchEnter, searchText->{searchText}");

            //Console.WriteLine($"code->{e.Code}, key-> {e.Key}");

            if (e.Key == "Enter")
            {

                if (!string.IsNullOrWhiteSpace(searchText))
                    await OnSearch();
                else
                    await OnRefresh();


            }

        }

        private async Task OnSearch()
        {

            //Console.WriteLine($"OnSearch, searchText->{searchText}");

            if (!string.IsNullOrWhiteSpace(searchText))
            {

                WhereFilter roleFilter = new WhereFilter
                {
                    Field = nameof(PayrollDeductionScheduleItemMasterView.AccountNo),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter nameFilter = new WhereFilter
                {
                    Field = "Name",
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter descriptionFilter = new WhereFilter
                {
                    Field = "Description",
                    Operator = "contains",
                    value = searchText
                };


                QueryGrid = new Query().Where(nameFilter.Or(roleFilter).Or(descriptionFilter));

                //this.grid.Refresh();
            }
            else
            {
                await OnRefresh();
            }

        }

        private async Task OnClearSearch()
        {
            //Console.WriteLine($"OnClearSearch, searchText->{searchText}");

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                await OnRefresh();

            }

        }


    }


}



