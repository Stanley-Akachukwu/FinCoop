using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.AppDomain;
using System.Text.Json;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedule;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedules;
using AP.ChevronCoop.Entities; 
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities;
using ChevronCoop.Web.AppUI.BlazorServer.Data.DepositApplication;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.SharedComponents;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using System.Net.Http.Headers;
using System.Security.Claims;
using JsonSerializer = System.Text.Json.JsonSerializer;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Payroll.PayrollDeductionSchedule
{
	public partial class PayrollDeductionScheduleCreateForm
	{



		[Inject]
		AuthenticationStateProvider _authenticationStateProvider { get; set; }
		private Query Query_Combo;// = new Query();

		string notificationText;
		bool showPopup = false;


		[Inject]
		IEntityDataService DataService { get; set; }

		[Inject]
		NotificationService notificationService { get; set; }
		[Parameter]
		public CreatePayrollDeductionScheduleCommand Model { get; set; }
        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Parameter]
		public EventCallback<CreatePayrollDeductionScheduleCommand> ModelChanged { get; set; }

		[Parameter]
		public bool ShowModal { get; set; }
		ClaimsPrincipal CurrentUser { get; set; }

		[Parameter]
		public EventCallback<bool> ShowModalChanged { get; set; }

		[Inject]
		ILogger<PayrollDeductionScheduleCreateForm> Logger { get; set; }

		[Inject]
		protected IJSRuntime JsRuntime { get; set; }

		[Inject]
		ModalService modalService { get; set; }

		public List<string> GetScheduleTypesSrc { get; set; }
		public int PayrollType { get; set; }
        public List<CompanyBankAccountMasterView> GetCompanyBankAccountMasterViews { get; set; }
        public string GetScheduleType { get; set; }

        public PayrollDeductionScheduleCreateForm()
		{
		}
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await base.OnAfterRenderAsync(firstRender);

			if (firstRender)
			{


			}


		}


		protected override async Task OnInitializedAsync()
		{
			await GetCurrentUser();
			await base.OnInitializedAsync();

			Query_Combo = new Query();
			await FetchCompanyBankAccountMasterViews();

			 GetScheduleTypesSrc = System.Enum.GetNames(typeof(PayrollScheduleType)).ToList();

		}
		private async Task GetCurrentUser()
		{
			var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
			CurrentUser = authState.User;
		}
		 
			
 
	private async Task FetchCompanyBankAccountMasterViews()
		{
			var rsp = await DataService.GetMasterView<List<CompanyBankAccountMasterView>>(
				nameof(CompanyBankAccountMasterView));
			GetCompanyBankAccountMasterViews = new List<CompanyBankAccountMasterView>();
			if (rsp.IsSuccessStatusCode)
			{
				GetCompanyBankAccountMasterViews =
					JsonSerializer.Deserialize<List<CompanyBankAccountMasterView>>(rsp.Content.ToJson());
			}
			 
		}

		//public async Task GetScheduleType(ChangeEventArgs<string, PayrollScheduleType> args)
		//{
		//	Model.PayrollScheduleType = int.Parse(args.Value);
		//}
		public async Task GetBankAccountPayroll(ChangeEventArgs<string, CompanyBankAccountMasterView> args)
		{
			Model.BankAccountId = args.Value;
		}
        public async Task GetBankAccountSpecialDeposit(ChangeEventArgs<string, CompanyBankAccountMasterView> args)
        {
            Model.SpecialDepositBankAccountId = args.Value;
        }
        public async Task GetBankAccountFixed(ChangeEventArgs<string, CompanyBankAccountMasterView> args)
        {
            Model.FixedDepositBankAccountId = args.Value;
        }
        public async Task OnSaveClose()
		{
			Model.CreatedByUserId =  CurrentUser.FindFirstValue(ClaimTypes.Sid);

			var rsp = await DataService.Create<CreatePayrollDeductionScheduleCommand, CommandResult<PayrollDeductionScheduleViewModel>>(
			 nameof(PayrollDeductionSchedule), Model);

			var modelString = JsonSerializer.Serialize(Model);
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
			 
				var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

				var msg = rspContent?.Response;
				if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
				{
					msg = rspContent.ValidationErrors[0].Error;
				}


				await notificationService.Open(new NotificationConfig()
				{
					Message = "Error",
					Description = msg,
					NotificationType = NotificationType.Error
				});

			}
			else
			{
				notificationText = $"Record successfully created";
				showPopup = true;

				await notificationService.Open(new NotificationConfig()
				{
					Message = "Success",
					Description = notificationText,
					NotificationType = NotificationType.Success
				});

				OnCancel();
                _navigationManager.NavigateTo($"/schedule/payrollScheduleList/", forceLoad: true);
            }


		}

		public async Task OnCancel()
		{

			ShowModal = false;
			await ShowModalChanged.InvokeAsync(ShowModal);

			Model = new CreatePayrollDeductionScheduleCommand();
			await ModelChanged.InvokeAsync(Model);

			showPopup = false;
		}


		private void OnFilterCombo(FilteringEventArgs args)
		{

			WhereFilter filter1 = new WhereFilter
			{
				Field = "Description",
				Operator = "contains",
				value = args.Text
			};

			WhereFilter filter2 = new WhereFilter
			{
				Field = "Name",
				Operator = "contains",
				value = args.Text
			};



		}


		private void OnFileUploaded(UploadChangeEventArgs args)
		{

		}

		public async Task OnInput(Microsoft.AspNetCore.Components.ChangeEventArgs args)
		{
			await ModelChanged.InvokeAsync(Model);
		}

		public async Task OnChange(Microsoft.AspNetCore.Components.ChangeEventArgs args)
		{
			await ModelChanged.InvokeAsync(Model);

		}

		public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
		{
			await ModelChanged.InvokeAsync(Model);

		}

		private async Task onFocus(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
		{
			await ModelChanged.InvokeAsync(Model);
		}




	}
}

