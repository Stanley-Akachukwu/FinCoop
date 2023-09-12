using System;
using Microsoft.AspNetCore.Components;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsCashAdditions;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using System.Text.Json;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using AntDesign.Core.Extensions;
using AntDesign;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.FixedDeposit
{
    public partial class FixedDepositTransactions
    {
		[Parameter] public bool IsAdmin { get; set; }
		[Parameter]
        public string CustomerID { get; set; }

        [Parameter]
        public string FixedDepositAccountID { get; set; }

        [Parameter]
        public string FixedDepositAccountNumber { get; set; }

        public bool showDrawal { get; set; } = true;

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public string MembersName { get; set; }

        [Parameter]
        public string MembersNumber { get; set; }

        public bool showCashAddition { get; set; } = false;
        public List<FixedDepositActionsMasterView> _FixedDepositMasterViewSrc1 { get; set; }
        public List<FixedDepositActionsMasterView> _FixedDepositMasterViewSrc { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCashAdditionMasterView();
        }

        public async Task GetCashAdditionMasterView()
        {
            var rsp = await DataService.GetValue<List<FixedDepositActionsMasterView>>(
                nameof(FixedDepositActionsMasterView), "fixedDepositAccountId", FixedDepositAccountID, "customerID",
                CustomerID);
            if (rsp.IsSuccessStatusCode)
            {
                _FixedDepositMasterViewSrc1 = new List<FixedDepositActionsMasterView>();

                _FixedDepositMasterViewSrc1 =
                    JsonSerializer.Deserialize<List<FixedDepositActionsMasterView>>(rsp.Content.ToJson());
                _FixedDepositMasterViewSrc =
                    _FixedDepositMasterViewSrc1.OrderByDescending(c => c.TransactionDate).ToList();
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
            showCashAddition = true;
        }

        private async Task OnUpdateChangedHandler(bool isComplete)
        {
            StateHasChanged();
            showDrawal = false;
            await GetCashAdditionMasterView();
            await InvokeAsync(StateHasChanged);
        }

        public async Task onFiltering(string filter)
        {
            if (filter.ToLowerInvariant() == "all")
            {
                _FixedDepositMasterViewSrc = _FixedDepositMasterViewSrc1
                   .OrderByDescending(c => c.TransactionDate)
                    .ToList();
            }
            else
            {
                _FixedDepositMasterViewSrc = _FixedDepositMasterViewSrc1.Where(c => c.TransactionType == filter)
                    .OrderByDescending(c => c.TransactionDate).ToList();
            }
        }

        public async void SearchGrid(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                _FixedDepositMasterViewSrc = _FixedDepositMasterViewSrc1.Where(c => c.CustomerID == CustomerID)
                    .OrderByDescending(c => c.TransactionDate).ToList();
            }
            else
            {
                _FixedDepositMasterViewSrc = _FixedDepositMasterViewSrc1
                    .Where(c => (c.Description?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.ApprovalId_Status?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ??
                                    false)
                                || (c.TransactionType?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ??
                                    false)
                                || (c.TransactionDate.ToString()
                                    .Contains(inputValue, StringComparison.OrdinalIgnoreCase))
                                || (c.Amount.ToString().Contains(inputValue, StringComparison.OrdinalIgnoreCase)))
                    .Take(5)
                    .OrderByDescending(c => c.TransactionDate)
                    .ToList();
            }
        }
    }
}