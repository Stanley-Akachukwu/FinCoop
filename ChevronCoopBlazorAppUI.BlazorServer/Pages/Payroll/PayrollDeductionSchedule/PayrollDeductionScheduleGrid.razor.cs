using AntDesign;
using AP.ChevronCoop.Commons; 
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Text.Json;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionSchedules;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedule;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedules;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Payroll.PayrollDeductionSchedule
{


	public partial class PayrollDeductionScheduleGrid
	{
        [Inject]
        NavigationManager _navigationManager { get; set; }
        [Inject]
		ILogger<PayrollDeductionScheduleGrid> Logger { get; set; }

		[Inject]
		BrowserService BrowserService { get; set; }

		[Inject]
		IEntityDataService DataService { get; set; }

		[Inject]
		AntDesign.ModalService modalService { get; set; }

		[Inject]
		NotificationService notificationService { get; set; }

		[Inject]
		protected IJSRuntime jsRuntime { get; set; }

		[Inject]
		AutoMapper.IMapper Mapper { get; set; }

		[Inject]
		WebConfigHelper Config { get; set; }

		//[Inject]
		//SessionService SessionService { get; set; }



		SfGrid<PayrollDeductionScheduleMasterView> grid;

		string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(PayrollDeductionScheduleMasterView)}?$orderby=DateCreated desc";



		private Query QueryGrid;// = new Query();

		private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
		string searchText;


		CreatePayrollDeductionScheduleCommand CreateModel { get; set; }
		UpdatePayrollDeductionScheduleCommand UpdateModel { get; set; }
		DeletePayrollDeductionScheduleCommand DeleteModel { get; set; }

		PayrollDeductionScheduleCreateForm createForm;
		Drawer createDrawer;
		bool showCreateDrawer { get; set; } = false;

		PayrollDeductionScheduleEditForm editForm;
		Drawer editDrawer;
		bool showEditDrawer { get; set; } = false;


		string editFormActiveTabKey = "1";

		BrowserDimension BrowserDimension;
		string ErrorDetailMsg = "";
		ErrorDetails errors;

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			base.OnAfterRender(firstRender);

			if (firstRender)
			{

				BrowserDimension = await BrowserService.GetDimensions();

				createDrawer.Width = (int)(BrowserDimension.Width * 0.40);
				editDrawer.Width = (int)(BrowserDimension.Width * 0.40);

				await OnRefresh();
				StateHasChanged();
			}
		

			await jsRuntime.InvokeVoidAsync("initFlowbiteJS");

		}


		protected override async Task OnInitializedAsync()
		{

			//Console.WriteLine($"API HOST->{Config.ODATA_VIEWS_HOST}, ODATA HOST-> {GRID_API_RESOURCE}");


			CreateModel = new CreatePayrollDeductionScheduleCommand() { MinDecimalPlace =0, AdviseDate = DateTime.Now, ExpectedDate = DateTime.Now, PayrollDate = DateTime.Now};

			UpdateModel = new UpdatePayrollDeductionScheduleCommand();

			DeleteModel = new DeletePayrollDeductionScheduleCommand();

			QueryGrid = new Query();
		}



		public void ActionCompletedHandler(ActionEventArgs<PayrollDeductionScheduleMasterView> args)
		{
			jsRuntime.InvokeVoidAsync("initFlowbiteJS");
		}


		private string removeSpaces(string name)
        {
           return name.Replace(" ", "-");
        }
		public void ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
		{
			this.ErrorDetailMsg = args.Error.ToString();
			StateHasChanged();

		}



		async Task onCreate()
		{
			showCreateDrawer = true;
		}

		async Task onCreateDone()
		{
			showCreateDrawer = false;
			//createForm.Errors = null;
		}

		async Task onEdit()
		{
			showEditDrawer = true;
		}

		async Task onEditDone()
		{
			showEditDrawer = false;
			// editForm.Errors = null;
		}



		private async Task OnAddRow()
		{

			//CreateModel = new CreatePayrollDeductionScheduleCommand();
			await onCreate();

		}



		//private async Task OnMatchDeduction(PayrollDeductionScheduleMasterView row)
		//{
		//	var newURL = removeSpaces(row.ScheduleName);
  //          _navigationManager.NavigateTo($"/payroll/matchDeduction/{newURL}/{row.Id}/", forceLoad: true);

  //      }

		private async Task OnEditRow(PayrollDeductionScheduleMasterView row)
		{

			UpdateModel = Mapper.Map<UpdatePayrollDeductionScheduleCommand>(row);

			await onEdit();

		}


        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "gridPayrollDeductionSchedule_excelexport") //Id is combination of Grid's ID and itemname
            {
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "ExportPayrollSchedule.xlsx";
                await this.grid.ExportToExcelAsync(ExportProperties);
            }

            if (args.Item.Id == "gridPayrollDeductionSchedule_pdfexport") //Id is combination of Grid's ID and itemname
            {
                PdfExportProperties ExportProperties = new PdfExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "ExportPayrollSchedule.pdf";
                await this.grid.ExportToPdfAsync();
            }
        }

        private async Task OnDeleteRow(PayrollDeductionScheduleMasterView row)
		{



			bool isOk = false;

			isOk = await modalService.ConfirmAsync(new ConfirmOptions()
			{
				Title = "Are you sure?",
				//Icon = icon,
				Content = "You will not be able to recover this record after deleting!",
			});



			if (isOk)
			{

				DeleteModel.Id = row.Id;
				var rsp = await DataService.Delete<DeletePayrollDeductionScheduleCommand, CommandResult<string>>(nameof(PayrollDeductionSchedule), DeleteModel);



				if (!rsp.IsSuccessStatusCode)
				{

					//Console.WriteLine($"rsp content->{rsp.Content}");
					//Console.WriteLine($"error->{rsp.Error.Content}");

					var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

					//Console.WriteLine($"error content->{rspContent}");
					//Console.WriteLine($"error msg->{rspContent.Response}");

					var msg = rspContent?.Response;
					if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
					{
						msg = rspContent.ValidationErrors[0].Error;
					}


					await notificationService.Open(new NotificationConfig()
					{
						Message = "Error deleting record",
						Description = msg,
						NotificationType = NotificationType.Error
					});


				}
				else
				{

					await notificationService.Open(new NotificationConfig()
					{
						Message = "Deleted",
						Description = rsp.Content.Response, //"Record deleted.",
						NotificationType = NotificationType.Success
					});
					await OnRefresh();
				}


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

				//WhereFilter roleFilter = new WhereFilter
				//{
				//	Field = nameof(PayrollDeductionScheduleMasterView.Code),
				//	Operator = "contains",
				//	value = searchText
				//};

				WhereFilter nameFilter = new WhereFilter
				{
					Field = "ScheduleName",
					Operator = "contains",
					value = searchText
				};

				WhereFilter descriptionFilter = new WhereFilter
				{
					Field = "ScheduleType",
					Operator = "contains",
					value = searchText
				};


				QueryGrid = new Query().Where(nameFilter.Or(descriptionFilter));

				//this.grid.Refresh();
			}
			else
			{
				await OnRefresh();
			}

		}
 

	}


}



