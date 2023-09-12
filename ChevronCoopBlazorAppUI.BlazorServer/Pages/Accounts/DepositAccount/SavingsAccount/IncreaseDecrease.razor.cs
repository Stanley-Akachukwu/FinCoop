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
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using System.Text.Json;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using Microsoft.JSInterop;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsIncreaseDecreases;
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.SavingsAccount
{
    public partial class IncreaseDecrease
    {
		[Parameter] public bool IsAdmin { get; set; }
		[Parameter]
        public string CustomerID { get; set; }

        [Parameter]
        public string SavingsAccountID { get; set; }

        [Parameter]
        public string SavingsAccountNumber { get; set; }

        public bool showDrawal { get; set; } = true;

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public string MembersName { get; set; }

        [Parameter] public decimal AccountBalance { get; set; }

        [Parameter]
        public string MembersNumber { get; set; }

        public bool showCashAddition { get; set; } = false;
        public List<SavingsIncreaseDecreaseMasterView> _SavingsIncreaseDecreaseMasterViewSrc1 { get; set; }
        public List<SavingsIncreaseDecreaseMasterView> _SavingsIncreaseDecreaseMasterViewSrc { get; set; }
        string searchText;

        string selectedTab = "text-gray-500 border-b-2 border-blue-600";

        string allTab, approveTab, pendingTab, rejectedTab = "text-blue-600";

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        IMasterViews _MasterViews { get; set; }

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
			_SavingsIncreaseDecreaseMasterViewSrc1 = new List<SavingsIncreaseDecreaseMasterView>();

			_SavingsIncreaseDecreaseMasterViewSrc1  = await _MasterViews.Get2FiltersCustomMasterView<SavingsIncreaseDecreaseMasterView>(nameof(SavingsIncreaseDecreaseMasterView), "savingsAccountId", SavingsAccountID, "savingsAccountId_CustomerId", CustomerID, "DateCreated,Description,Amount,ApprovalId_Status");

			_SavingsIncreaseDecreaseMasterViewSrc = _SavingsIncreaseDecreaseMasterViewSrc1
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
            _SavingsIncreaseDecreaseMasterViewSrc = _SavingsIncreaseDecreaseMasterViewSrc1
                .Where(c => c.ApprovalId_Status == filter).ToList();

            if (filter.Contains("_"))
            {
                _SavingsIncreaseDecreaseMasterViewSrc = _SavingsIncreaseDecreaseMasterViewSrc1
                    .Where(c => c.ApprovalId_Status == filter || c.ApprovalId_Status == filter.Replace("_", " "))
                    .OrderByDescending(c => c.DateCreated).ToList();
                resetTab();
                pendingTab = selectedTab;
            }
            else if (filter == "all")
            {
                _SavingsIncreaseDecreaseMasterViewSrc = _SavingsIncreaseDecreaseMasterViewSrc1.OrderByDescending(c => c.DateCreated)
                    .ToList();

                resetTab();
                allTab = selectedTab;
            }
            else
            {
                _SavingsIncreaseDecreaseMasterViewSrc = _SavingsIncreaseDecreaseMasterViewSrc1
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
                _SavingsIncreaseDecreaseMasterViewSrc = _SavingsIncreaseDecreaseMasterViewSrc1.OrderByDescending(c => c.DateCreated).ToList();
            }
            else
            {
                _SavingsIncreaseDecreaseMasterViewSrc = _SavingsIncreaseDecreaseMasterViewSrc1
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