using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.AppDomain;
using System.Text.Json;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Inputs;
using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedule;
using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedules;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using Syncfusion.Blazor.DropDowns;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using AP.ChevronCoop.Entities.MasterData.Currencies;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Payroll.PayrollDeductionSchedule
{
    public partial class PayrollDeductionScheduleEditForm
    {


        public PayrollDeductionScheduleEditForm()
        {
        }

		[Inject]
		AuthenticationStateProvider _authenticationStateProvider { get; set; }
		private Query Query_Combo;
		ClaimsPrincipal CurrentUser { get; set; }

		string notificationText;
        bool showPopup = false;


        AntDesign.Tabs editTab;

        [Parameter]
        public string ActiveTabKey { get; set; }

		public string combobox_Department_res { get; set; }
		public List<CompanyBankAccountMasterView> GetCompanyBankAccountMasterViews { get; set; }
		[Parameter]
        public EventCallback<string> ActiveTabKeyChanged { get; set; }


        [Parameter]
        public UpdatePayrollDeductionScheduleCommand Model { get; set; }


        [Parameter]
        public EventCallback<UpdatePayrollDeductionScheduleCommand> ModelChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        ILogger<PayrollDeductionScheduleEditForm> Logger { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }

        private WhereFilter baseFilter;


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
           //  LoadDropDown();
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
		//public void LoadDropDown()
		//{
		//	combobox_Department_res = $"{Config.ODATA_VIEWS_HOST}/{nameof(CompanyBankAccountMasterView)}";
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

		public async Task OnSave()
        {
			Model.UpdatedByUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
			var rsp = await DataService.Update<UpdatePayrollDeductionScheduleCommand, CommandResult<PayrollDeductionScheduleViewModel>>(
           nameof(PayrollDeductionSchedule), Model);

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
                notificationText = "Record successfully updated";
                showPopup = true;
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Success",
                    Description = notificationText,
                    NotificationType = NotificationType.Success
                });

                OnCancel();
            }

        }


        public async Task OnCancel()
        {

            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);

            Model = new UpdatePayrollDeductionScheduleCommand();
            await ModelChanged.InvokeAsync(Model);

            showPopup = false;

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

