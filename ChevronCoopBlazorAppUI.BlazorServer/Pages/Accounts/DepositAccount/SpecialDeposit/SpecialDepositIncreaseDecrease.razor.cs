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
using System.Text.Json;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using Microsoft.JSInterop; 
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.SpecialDeposit
{
    public partial class SpecialDepositIncreaseDecrease
    {
		[Inject]
		IMasterViews _MasterViews { get; set; }

		[Parameter] public bool IsAdmin { get; set; }
		[Parameter]
        public string CustomerID { get; set; }

        [Parameter]
        public string SpecialDepositAccountID { get; set; }

        [Parameter]
        public string SpecialDepositAccountNumber { get; set; }

        public bool showDrawal { get; set; } = true;

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public string MembersName { get; set; }

        [Parameter] public decimal AccountBalance { get; set; }
        [Parameter] public decimal MonthlyContribution { get; set; }
        

        [Parameter]
        public string MembersNumber { get; set; }

        public bool showCashAddition { get; set; } = false;
        public List<SpecialDepositIncreaseDecreaseMasterView> _SpecialDepositIncreaseDecreaseMasterViewSrc1 { get; set; }
        public List<SpecialDepositIncreaseDecreaseMasterView> _SpecialDepositIncreaseDecreaseMasterViewSrc { get; set; }
        string searchText;

        string selectedTab = "text-gray-500 border-b-2 border-blue-600";

        string allTab, approveTab, pendingTab, rejectedTab = "text-blue-600";

        [Inject]
        NotificationService notificationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            allTab = selectedTab;
            await GetIncreaseDecreaseMasterView();
        }

        public async void resetTab()
        {
            allTab = approveTab = pendingTab = rejectedTab = "text-blue-600";
        }
		public async Task GetIncreaseDecreaseMasterView()
		{
			_SpecialDepositIncreaseDecreaseMasterViewSrc = new List<SpecialDepositIncreaseDecreaseMasterView>();

			_SpecialDepositIncreaseDecreaseMasterViewSrc1 = await _MasterViews.Get2FiltersCustomMasterView<SpecialDepositIncreaseDecreaseMasterView>(nameof(SpecialDepositIncreaseDecreaseMasterView), "SpecialDepositAccountID", SpecialDepositAccountID, "SpecialDepositAccountId_CustomerId", CustomerID, "DateCreated,Description,FundingAmount,ApprovalId_Status");

			_SpecialDepositIncreaseDecreaseMasterViewSrc = _SpecialDepositIncreaseDecreaseMasterViewSrc1
				   .OrderByDescending(c => c.DateCreated).ToList();


		}
         


		private async Task OnShowDrawal()
        {
            showDrawal = true;
            showCashAddition = true;
        }

        private async Task OnUpdateChangedHandler(bool isComplete)
        {
          
            await GetIncreaseDecreaseMasterView();
            await InvokeAsync(StateHasChanged);
        }


        public async Task onFiltering(string filter)
        {
            _SpecialDepositIncreaseDecreaseMasterViewSrc = _SpecialDepositIncreaseDecreaseMasterViewSrc1
                .Where(c => c.ApprovalId_Status == filter).ToList();

            if (filter.Contains("_"))
            {
                _SpecialDepositIncreaseDecreaseMasterViewSrc = _SpecialDepositIncreaseDecreaseMasterViewSrc1
                    .Where(c => c.ApprovalId_Status == filter || c.ApprovalId_Status == filter.Replace("_", " "))
                    .OrderByDescending(c => c.DateCreated).ToList();
                resetTab();
                pendingTab = selectedTab;
            }
            else if (filter == "all")
            {
                _SpecialDepositIncreaseDecreaseMasterViewSrc = _SpecialDepositIncreaseDecreaseMasterViewSrc1.OrderByDescending(c => c.DateCreated)
                    .ToList();

                resetTab();
                allTab = selectedTab;
            }
            else
            {
                _SpecialDepositIncreaseDecreaseMasterViewSrc = _SpecialDepositIncreaseDecreaseMasterViewSrc1
                    .Where(c => c.ApprovalId_Status == filter).OrderByDescending(c => c.DateCreated).ToList();
            }

            if (filter == ApprovalStatus.APPROVED.ToString())
            {
                resetTab();
                approveTab = selectedTab;
            }
            if (filter == ApprovalStatus.REJECTED.ToString())
            {
                resetTab();
                rejectedTab = selectedTab;
            }
        }

        public async void SearchGrid(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                _SpecialDepositIncreaseDecreaseMasterViewSrc = _SpecialDepositIncreaseDecreaseMasterViewSrc1.OrderByDescending(c => c.DateCreated).ToList();
            }
            else
            {
                _SpecialDepositIncreaseDecreaseMasterViewSrc = _SpecialDepositIncreaseDecreaseMasterViewSrc1
                    .Where(c => (c.Description?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.ApprovalId_Status?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ??
                                    false))
                    .Take(5)
                    .OrderByDescending(c => c.DateCreated)
                    .ToList();
            }
        }
    }
}