using AntDesign;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.MasterData.Departments;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using MailKit.Search;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Query = Syncfusion.Blazor.Data.Query;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.AdminUserAccountManagement.DepositAccounts
{
	public partial class AllDepositApplications
	{
		
		string notificationText;
		bool showPopup = false;

		[Inject]
		ILogger<ProductSetupGrid> Logger { get; set; }

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

		[Inject]
		AuthenticationStateProvider _authenticationStateProvider { get; set; }


		SfGrid<DepositProductMasterView> grid;

		private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
		string searchText;

	
		ClaimsPrincipal CurrentUser { get; set; }
		private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

		[Inject]
		NavigationManager _navigationManager { get; set; }

		[Inject] IClientAuditService _auditLogService { get; set; }
		private string bearToken { get; set; }
		string ErrorDetails = "";

		ErrorDetails errors;

		//ApproveDepositProductSetupForm approveForm;
		Drawer approveDrawer;
		Drawer specificDrawer;
		Drawer departmentDrawer;
		
		BrowserDimension BrowserDimension;

		[Parameter]
		public string filter { get; set; }

		string Active { get; set; } = "text-blue-600 border-b-2 border-blue-600 active";
		string Inactive { get; set; } = "border-b-2 border-transparent";
		string All { get; set; } = string.Empty;
		string Pending { get; set; } = string.Empty;
		string Approved { get; set; } = string.Empty;
		string Rejected { get; set; } = string.Empty;
		string Published { get; set; } = string.Empty;
		string DepositProductId { get; set; } = string.Empty;

		public string combobox_department_res { get; set; }

		public List<DepositApplicationsMasterView> _DepositApplicationsMasterView { get; set; } = new List<DepositApplicationsMasterView>();
		public List<DepositApplicationsMasterView> _DepositApplicationsMasterViewSrc { get; set; } = new List<DepositApplicationsMasterView>();

		IMasterViews _MasterViews { get; set; }

		protected override async Task OnInitializedAsync()
		{
			try
			{
				searchText = "";
				
				await GetCurrentUser();

				await GetDepositProducts();

				await _auditLogService.LogAudit("Accessed Deposit Product list", "Accessed Deposit Product list",
					"Security", "NA, readonly request", CurrentUser);
			}
			catch (Exception ex)
			{
				
			}
		}

		public async Task GetDepositProducts()
		{
       
                  var rsp = await DataService.GetCustomValuesEntityAllFields<List<DepositApplicationsMasterView>>(nameof(DepositApplicationsMasterView));
			if (rsp.IsSuccessStatusCode)
			{ 
                _DepositApplicationsMasterViewSrc = JsonSerializer.Deserialize<List<DepositApplicationsMasterView>>(rsp.Content.ToJson());
                _DepositApplicationsMasterView = _DepositApplicationsMasterViewSrc.OrderByDescending(x => x.DateCreated).ToList();
				UpdateSerial();
			}
		}

		public async void UpdateSerial()
		{
			int serial = 1;
			foreach (var sn in _DepositApplicationsMasterView)
			{
				sn.Caption = serial.ToString();
				serial++;
			}
		}

		public async void SearchProducts(ChangeEventArgs args)
		{
			string inputValue = args.Value?.ToString();
			if (string.IsNullOrWhiteSpace(inputValue))
			{
				_DepositApplicationsMasterView = _DepositApplicationsMasterViewSrc.OrderByDescending(x => x.DateCreated).ToList();
			}
			else
			{
				_DepositApplicationsMasterView = _DepositApplicationsMasterViewSrc
					.Where(c => (c.ApplicationNo?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false) ||
								(c.ProductId_Name?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
								|| (c.AccountType?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
								|| (c.ApprovalId_Status?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
								|| (c.Description?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ??
									false))
					.Take(5)
					.OrderByDescending(c => c.DateCreated)
					.ToList();
			}
			UpdateSerial();

		}

		async Task onFiltering(string filter)
		{
			if (filter == "all")
			{
				_DepositApplicationsMasterView = _DepositApplicationsMasterViewSrc.OrderByDescending(x => x.DateCreated).ToList();
			}
			else if (filter.ToLowerInvariant() == "pending_approval" || filter.ToLowerInvariant() == "pending approval")
			{
				_DepositApplicationsMasterView = _DepositApplicationsMasterViewSrc.Where(x => x.ApprovalId_Status.ToLowerInvariant() == filter || x.ApprovalId_Status.ToLowerInvariant() == "pending approval").OrderByDescending(x => x.DateCreated).ToList();
			}
			else
			{
				_DepositApplicationsMasterView = _DepositApplicationsMasterViewSrc.Where(x => x.ApprovalId_Status == filter).OrderByDescending(x => x.DateCreated).ToList();
			}
			UpdateSerial();
		}

		public void CellInfoHandler(QueryCellInfoEventArgs<DepositApplicationsMasterView> Args)
		{
			Args.Data.ApprovalId_Status = Args.Data.ApprovalId_Status != null ? Args.Data.ApprovalId_Status.Replace("_", " ") : "";
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			base.OnAfterRender(firstRender);

			if (firstRender)
			{
				
			}

			await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
		}


		private async Task GetCurrentUser()
		{
			var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
			CurrentUser = authState.User;
		}

		public void ActionCompletedHandler(ActionEventArgs<DepositApplicationsMasterView> args)
		{
			jsRuntime.InvokeVoidAsync("initFlowbiteJS");
		}



	}
}
