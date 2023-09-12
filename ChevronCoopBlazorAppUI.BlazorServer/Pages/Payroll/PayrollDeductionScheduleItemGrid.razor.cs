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
using AP.ChevronCoop.Entities.Deposits.CommonViews;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Payroll
{
    public partial class PayrollDeductionScheduleItemGrid
    {
        [Inject]
		ILogger<PayrollDeductionScheduleItemGrid> Logger { get; set; }

		[Inject]
		BrowserService BrowserService { get; set; }

		[Inject]
		IEntityDataService DataService { get; set; }
		[Parameter]
		public string schedulename { get; set; }

		[Inject]
		AntDesign.ModalService modalService { get; set; }

		[Inject]
		NotificationService notificationService { get; set; }

		[Parameter]
		public string jobId { get; set; }

      
        public string desc { get; set; }

        [Inject]
		protected IJSRuntime jsRuntime { get; set; }

		[Inject]
		AutoMapper.IMapper Mapper { get; set; }

		[Inject]
		WebConfigHelper Config { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }


        SfGrid<PayrollDeductionScheduleItemMasterView> grid;

		// GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(PayrollDeductionScheduleItemMasterView)}?$filter=PayrollCronJobConfigId eq '{jobId}'";

        //string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(PayrollDeductionScheduleItemMasterView)}";

        public List<PayrollDeductionScheduleItemMasterView> _PayrollDeductionScheduleItemMasterView { get; set; }
        public List<PayrollDeductionScheduleItemMasterView> _PayrollDeductionScheduleItemMasterViewSrc { get; set; }

        private Query QueryGrid;// = new Query();

		private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
		string searchText;
		 
		BrowserDimension BrowserDimension;
		string ErrorDetails = "";
		ErrorDetails errors;
        public int TotalCount { get; set; }
        public string TotalDeduction { get; set; }
        public DateTime DateCompleted { get; set; }
     

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
 
private async Task GoBack()
        {
            await jsRuntime.InvokeVoidAsync("history.back");
        }
        private async Task removeHyphen()
		{
			var split = schedulename.Split(';');
            schedulename = split[0];
            desc = split[1].Replace("-", " ");
            desc.Replace("_", "/");
        }

		protected override async Task OnInitializedAsync()
		{
        
            //var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);

            //if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("initialCount", out initCount))
            //{
            //    currentCount = Convert.ToInt32(initCount);
            //}

            //_navigationManager.TryGet
            //NavManager.TryGetQueryString<int>("initialCount", out CurrentCount)

            await removeHyphen();

			QueryGrid = new Query();

			 
        await GetDeductions();
    }

    public async Task GetDeductions()
    {
         
            var rsp = await DataService.GetValue<List<PayrollDeductionScheduleItemMasterView>>(
           nameof(PayrollDeductionScheduleItemMasterView), "PayrollCronJobConfigId", jobId);
        if (rsp.IsSuccessStatusCode)
        {
            _PayrollDeductionScheduleItemMasterView = new List<PayrollDeductionScheduleItemMasterView>();

            _PayrollDeductionScheduleItemMasterView =
                JsonSerializer.Deserialize<List<PayrollDeductionScheduleItemMasterView>>(rsp.Content.ToJson());
				if (_PayrollDeductionScheduleItemMasterView != null && _PayrollDeductionScheduleItemMasterView.Count() > 0)
				{
                    _PayrollDeductionScheduleItemMasterViewSrc = _PayrollDeductionScheduleItemMasterView
              .OrderByDescending(c => c.PayrollDate).ToList();

					TotalCount = _PayrollDeductionScheduleItemMasterView.Count;
					TotalDeduction = _PayrollDeductionScheduleItemMasterView.Sum(c => c.Amount).ToString("N");
					DateCompleted = _PayrollDeductionScheduleItemMasterView.FirstOrDefault().PayrollDate;
                }
          

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
				ExportProperties.FileName = "ExportPayrollDeductionItems.xlsx";
				await this.grid.ExportToExcelAsync(ExportProperties);
			}

			if (args.Item.Id == "gridPayrollDeductionScheduleItem_pdfexport") //Id is combination of Grid's ID and itemname
			{
				PdfExportProperties ExportProperties = new PdfExportProperties();
				ExportProperties.IncludeHiddenColumn = true;
				ExportProperties.FileName = "ExportPayrollDeductionItems.pdf";
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



