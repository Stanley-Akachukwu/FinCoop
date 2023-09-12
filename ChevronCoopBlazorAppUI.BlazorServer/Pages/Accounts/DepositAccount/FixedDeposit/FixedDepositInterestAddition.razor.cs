using System;
using Blazored.FluentValidation;
using AntDesign;
using Blazored.SessionStorage;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Tailwindcss;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Svg;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Util;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.SharedComponents;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.Drawals;
using Microsoft.AspNetCore.Components; 
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using System.Text.Json;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using Microsoft.JSInterop;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositInterestAdditions;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.FixedDeposit
{
    public partial class FixedDepositInterestAddition
	{
	 

        [Parameter]
        public string AccountNumber { get; set; }
		[Parameter]
		public string AccType { get; set; }
 
        [Inject]
        IEntityDataService DataService { get; set; }

         public List<FixedDepositInterestAdditionMasterView> _FixedDepositInterestAdditionMasterView = new List<FixedDepositInterestAdditionMasterView>();
        public List<FixedDepositInterestAdditionMasterView> Model = new List<FixedDepositInterestAdditionMasterView>();

       

        protected override async Task OnInitializedAsync()
        {
           
            Model = new List<FixedDepositInterestAdditionMasterView>();
			await GetInterestAdditionMasterView();
        }

        public async Task GetInterestAdditionMasterView()
        {
           
			var rsp = await DataService.GetValue<List<FixedDepositInterestAdditionMasterView>>(
	   nameof(FixedDepositInterestAdditionMasterView), "FixedDepositAccountId_AccountNo", AccountNumber);
			if (rsp.IsSuccessStatusCode)
			{

				_FixedDepositInterestAdditionMasterView =
					JsonSerializer.Deserialize<List<FixedDepositInterestAdditionMasterView>>(rsp.Content.ToJson());
				if (_FixedDepositInterestAdditionMasterView != null && _FixedDepositInterestAdditionMasterView.Count() > 0)
				{
					Model = _FixedDepositInterestAdditionMasterView.OrderByDescending(c => c.ProcessedDate).ToList();

				}

			}

		} 
 
    }
}