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
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositInterestAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.SpecialDeposit
{
    public partial class InterestAddition
    {
	 

        [Parameter]
        public string AccountNumber { get; set; }
		[Parameter]
		public string AccType { get; set; }
 
        [Inject]
        IEntityDataService DataService { get; set; }

         public List<SpecialDepositInterestAdditionMasterView> _SpecialDepositInterestAdditionMasterView = new List<SpecialDepositInterestAdditionMasterView>();
        public List<SpecialDepositInterestAdditionMasterView> Model = new List<SpecialDepositInterestAdditionMasterView>();

       

        protected override async Task OnInitializedAsync()
        {
           
            Model = new List<SpecialDepositInterestAdditionMasterView>();
			await GetInterestAdditionMasterView();
        }

        public async Task GetInterestAdditionMasterView()
        {
           
			var rsp = await DataService.GetValue<List<SpecialDepositInterestAdditionMasterView>>(
	   nameof(SpecialDepositInterestAdditionMasterView), "specialDepositAccountId_AccountNo", AccountNumber);
			if (rsp.IsSuccessStatusCode)
			{

				_SpecialDepositInterestAdditionMasterView =
					JsonSerializer.Deserialize<List<SpecialDepositInterestAdditionMasterView>>(rsp.Content.ToJson());
				if (_SpecialDepositInterestAdditionMasterView != null && _SpecialDepositInterestAdditionMasterView.Count() > 0)
				{
					Model = _SpecialDepositInterestAdditionMasterView.OrderByDescending(c => c.ProcessedDate).ToList();

				}

			}

		} 
 
    }
}