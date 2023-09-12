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
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using System.Text.Json;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using Microsoft.JSInterop;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositWithdrawals;
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.SpecialDeposit
{
    public partial class Withdrawal
    {
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

        [Inject]
        NotificationService notificationService { get; set; }

        [Parameter]
        public string MembersNumber { get; set; }

        public bool showWithdrawal { get; set; } = false;
        public List<SpecialDepositActionsMasterView> _SpecialDepositActionsMasterViewSrc = new List<SpecialDepositActionsMasterView>();
        public List<SpecialDepositActionsMasterView> _SpecialDepositActionsMasterView = new List<SpecialDepositActionsMasterView>();

        [Parameter] public decimal AvailableBalance { get; set; }
        [Parameter] public decimal LedgerBalance { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetWithdrawalMasterView();
        }

        public async Task GetWithdrawalMasterView()
        {
            var rsp = await DataService.GetValue<List<SpecialDepositActionsMasterView>>(
                nameof(SpecialDepositActionsMasterView), "specialDepositAccountId", SpecialDepositAccountID, "customerID", CustomerID, "transactionType", "Withdrawal");
            if (rsp.IsSuccessStatusCode)
            {
                _SpecialDepositActionsMasterViewSrc = new List<SpecialDepositActionsMasterView>();

                _SpecialDepositActionsMasterViewSrc =
                    JsonSerializer.Deserialize<List<SpecialDepositActionsMasterView>>(rsp.Content.ToJson());
                _SpecialDepositActionsMasterView = _SpecialDepositActionsMasterViewSrc
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


        private async Task OnShowDrawal()
        {
            showDrawal = true;
            showWithdrawal = true;
        }

        private async Task OnUpdateChangedHandler(bool isComplete)
        {
           
            await GetWithdrawalMasterView();
            await InvokeAsync(StateHasChanged);
        }

        public async Task onFiltering(string filter)
        {
            _SpecialDepositActionsMasterView = _SpecialDepositActionsMasterViewSrc
                .Where(c => c.ApprovalId_Status == filter).ToList();

            if (filter.Contains("_"))
            {
                _SpecialDepositActionsMasterView = _SpecialDepositActionsMasterViewSrc
                    .Where(c => c.ApprovalId_Status == filter || c.ApprovalId_Status == filter.Replace("_", " "))
                    .OrderByDescending(c => c.TransactionDate).ToList();
            }

            if (filter == "all")
            {
                _SpecialDepositActionsMasterView = _SpecialDepositActionsMasterViewSrc
                    .OrderByDescending(c => c.TransactionDate).ToList();
            }
        }
    }
}