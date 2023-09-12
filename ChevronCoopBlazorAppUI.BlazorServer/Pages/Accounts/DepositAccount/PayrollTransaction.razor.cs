using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Payroll.PayrollDeductionScheduleItems;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount
{
	public partial class PayrollTransaction
	{
		public List<PayrollDeductionScheduleItemMasterView> _PayrollDeductionScheduleItemMasterView { get; set; }
		public List<PayrollDeductionScheduleItemMasterView> Model { get; set; } 

		[Parameter] public bool IsAdmin { get; set; }
		[Inject]
		IEntityDataService DataService { get; set; }
		[Parameter]
		public string MembersNumber { get; set; }
		//[Parameter]
		//public string SpecialDepositAccountNumber { get; set; }

		//[Parameter]
		//public string CustomerID { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Model = new List<PayrollDeductionScheduleItemMasterView>();
			await GetDeductions();
		}
	
		public async Task GetDeductions()
		{

			var rsp = await DataService.GetValue<List<PayrollDeductionScheduleItemMasterView>>(
		   nameof(PayrollDeductionScheduleItemMasterView), "memberId", MembersNumber);
			if (rsp.IsSuccessStatusCode)
			{

				_PayrollDeductionScheduleItemMasterView =
					JsonSerializer.Deserialize<List<PayrollDeductionScheduleItemMasterView>>(rsp.Content.ToJson());
				if (_PayrollDeductionScheduleItemMasterView != null && _PayrollDeductionScheduleItemMasterView.Count() > 0)
				{
					Model = _PayrollDeductionScheduleItemMasterView.Where(c => c.CurrentStatus == "SUCCESS")
			  .OrderByDescending(c => c.PayrollDate).ToList();
					 
				}

			}
		 
		}

	}
}
