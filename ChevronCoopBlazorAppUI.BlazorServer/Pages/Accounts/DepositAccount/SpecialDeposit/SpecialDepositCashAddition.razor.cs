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
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositCashAdditions;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.SpecialDeposit
{
    public partial class SpecialDepositCashAddition
    {
		[Parameter] public bool IsAdmin { get; set; }
		[Parameter]
        public string CustomerID { get; set; }

        [Parameter]
        public string SpecialDepositAccountId { get; set; }

        [Parameter]
        public string SavingsAccountNumber { get; set; }
        [Parameter]
        public string BankName { get; set; }

        [Parameter]
        public string AccountNumber { get; set; }
        [Parameter]
        public string AccountName { get; set; }

        public bool showDrawal { get; set; } = true;

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public string MembersName { get; set; }

        [Parameter]
        public string MembersNumber { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public bool showCashAddition { get; set; } = false;
        public List<SpecialDepositActionsMasterView> _SpecialDepositActionsMasterView = new List<SpecialDepositActionsMasterView>();
        public List<SpecialDepositActionsMasterView> _SpecialDepositActionsMasterViewSrc = new List<SpecialDepositActionsMasterView>();

        string searchText;

        [Parameter] public decimal AvailableBalance { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {
            await GetCashAdditionMasterView();
        }

        public async Task GetCashAdditionMasterView()
        {
            var rsp = await DataService.GetValue<List<SpecialDepositActionsMasterView>>(
                nameof(SpecialDepositActionsMasterView), "specialDepositAccountId", SpecialDepositAccountId, "customerID", CustomerID, "transactionType", "Cash Addition");
            if (rsp.IsSuccessStatusCode)
            {
                _SpecialDepositActionsMasterViewSrc = new List<SpecialDepositActionsMasterView>();

                _SpecialDepositActionsMasterViewSrc =
                    JsonSerializer.Deserialize<List<SpecialDepositActionsMasterView>>(rsp.Content.ToJson());
                _SpecialDepositActionsMasterView = _SpecialDepositActionsMasterViewSrc.OrderByDescending(c => c.TransactionDate).ToList();
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

        public async void SearchGrid(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                _SpecialDepositActionsMasterView = _SpecialDepositActionsMasterViewSrc
                    .OrderByDescending(c => c.TransactionDate).ToList();
            }
            else
            {
                _SpecialDepositActionsMasterView = _SpecialDepositActionsMasterViewSrc
                    .Where(c => (c.Description?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.ApprovalId_Status?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ??
                                    false))
                    .Take(5)
                    .OrderByDescending(c => c.TransactionDate)
                    .ToList();
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

        public async Task onFiltering(string filter)
        {
            await InvokeAsync(StateHasChanged);
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