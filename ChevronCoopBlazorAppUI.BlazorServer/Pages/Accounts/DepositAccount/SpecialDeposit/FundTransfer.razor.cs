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
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositFundTransfer;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using System.Text.Json;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using Microsoft.JSInterop;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.SpecialDeposit
{
    public partial class FundTransfer
    {
		[Parameter] public bool IsAdmin { get; set; }
		[Parameter]
        public string CustomerID { get; set; }

		[Parameter]
		public string SpecialDepositAccountID { get; set; }

		[Parameter]
		public string SpecialDepositAccountNumber { get; set; }

		[Parameter] public decimal AvailableBalance { get; set; } = 0;
        [Parameter] public decimal LedgerBalance { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public bool showDrawal { get; set; } = true;

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public string MembersName { get; set; }

        [Parameter]
        public string MembersNumber { get; set; }

        public bool showCashAddition { get; set; } = false;
        public List<SpecialDepositActionsMasterView> _SpecialDepositActionsMasterView = new List<SpecialDepositActionsMasterView>();
        public List<SpecialDepositActionsMasterView> _SpecialDepositActionsMasterViewSrc = new List<SpecialDepositActionsMasterView>();

        string selectedTab = "text-gray-500 border-b-2 border-blue-600";

        string allTab, approveTab, pendingTab, rejectedTab = "text-blue-600";


        protected override async Task OnInitializedAsync()
        {
            await GetCashAdditionMasterView();
            resetTab();
            allTab = selectedTab;

        }

        public async Task GetCashAdditionMasterView()
        {
            try
            {
                var rsp = await DataService.GetValue<List<SpecialDepositActionsMasterView>>(
               nameof(SpecialDepositActionsMasterView), "specialDepositAccountId", SpecialDepositAccountID, "customerID", CustomerID, "transactionType", "Fund Transfer");
                if (rsp.IsSuccessStatusCode)
                {
                    _SpecialDepositActionsMasterView = new List<SpecialDepositActionsMasterView>();

                    _SpecialDepositActionsMasterView =
                        JsonSerializer.Deserialize<List<SpecialDepositActionsMasterView>>(rsp.Content.ToJson());
                    _SpecialDepositActionsMasterViewSrc = _SpecialDepositActionsMasterView
                        .OrderByDescending(c => c.TransactionDate).ToList();
                    // ActivateTab();
                }
                else
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = MessageBox.ServerErrorHeader,
                        Description = MessageBox.ServerErrorDescription,
                        NotificationType = NotificationType.Error
                    });
                }
            }
            catch
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = $"Error connecting to server",
                    NotificationType = NotificationType.Error
                });
            }
           
        }


        private async Task OnShowDrawal()
        {
            showDrawal = true;
            showCashAddition = true;
        }

        private async Task OnUpdateChangedHandler(bool isComplete)
        {
           
            await GetCashAdditionMasterView();
            await InvokeAsync(StateHasChanged);
        }

        public async void resetTab()
        {
            allTab = approveTab = pendingTab = rejectedTab = "text-blue-600";
        }

        public async Task onFiltering(string filter)
        {
            _SpecialDepositActionsMasterViewSrc = _SpecialDepositActionsMasterView
                .Where(c => c.ApprovalId_Status == filter).ToList();

            if (filter.Contains("_"))
            {
                _SpecialDepositActionsMasterViewSrc = _SpecialDepositActionsMasterView
                    .Where(c => c.ApprovalId_Status == filter || c.ApprovalId_Status == filter.Replace("_", " "))
                    .OrderByDescending(c => c.TransactionDate).ToList();
               
            }

            if (filter == "all")
            {
                _SpecialDepositActionsMasterViewSrc = _SpecialDepositActionsMasterView
                    .OrderByDescending(c => c.TransactionDate).ToList();
               
            }

          
        }

        public async void SearchGrid(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                _SpecialDepositActionsMasterViewSrc = _SpecialDepositActionsMasterView.OrderByDescending(c => c.TransactionDate).ToList();
            }
            else
            {
                _SpecialDepositActionsMasterViewSrc = _SpecialDepositActionsMasterView
                    .Where(c => (c.Description?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.ApprovalId_Status?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ??
                                    false))
                    .Take(5)
                    .OrderByDescending(c => c.TransactionDate)
                    .ToList();
            }
        }

    }
}