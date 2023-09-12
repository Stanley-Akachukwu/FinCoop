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
using AntDesign;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.FixedDeposit
{
    public partial class FixedDepositMaturityInstruction
    {
		[Parameter] public bool IsAdmin { get; set; }
		[Parameter]
        public decimal AvailableBalance { get; set; } = 0;

        [Parameter]
        public decimal LedgerBalance { get; set; } = 0;

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

        public bool showFixedDepositLiquidation { get; set; } = false;
        public List<FixedDepositActionsMasterView> _FixedDepositActionsMasterViewSrc1 { get; set; }
        public List<FixedDepositActionsMasterView> _FixedDepositActionsMasterViewSrc { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetFixedDepositMasterView();
        }

        public async Task GetFixedDepositMasterView()
        {
            var rsp = await DataService.GetValue<List<FixedDepositActionsMasterView>>(
                nameof(FixedDepositActionsMasterView), "fixedDepositAccountId", FixedDepositAccountID, "customerID",
                CustomerID, "transactionType", "Change Maturity Instruction");
            if (rsp.IsSuccessStatusCode)
            {
                _FixedDepositActionsMasterViewSrc1 = new List<FixedDepositActionsMasterView>();

                _FixedDepositActionsMasterViewSrc1 =
                    JsonSerializer.Deserialize<List<FixedDepositActionsMasterView>>(rsp.Content.ToJson());
                _FixedDepositActionsMasterViewSrc = _FixedDepositActionsMasterViewSrc1
                    .OrderByDescending(c => c.TransactionDate).ToList();
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
            showFixedDepositLiquidation = true;
        }

        private async Task OnUpdateChangedHandler(bool isComplete)
        {
           
            await GetFixedDepositMasterView();
            await InvokeAsync(StateHasChanged);
        }

        public async Task onFiltering(string filter)
        {
            _FixedDepositActionsMasterViewSrc =
                _FixedDepositActionsMasterViewSrc1.Where(c => c.ApprovalId_Status == filter).ToList();

            if (filter.Contains("_"))
            {
                _FixedDepositActionsMasterViewSrc = _FixedDepositActionsMasterViewSrc1
                    .Where(c => c.ApprovalId_Status == filter || c.ApprovalId_Status == filter.Replace("_", " "))
                    .OrderByDescending(c => c.TransactionDate).ToList();
            }
            else if (filter == "all")
            {
                _FixedDepositActionsMasterViewSrc = _FixedDepositActionsMasterViewSrc1
                    .Where(c => c.CustomerID == CustomerID).OrderByDescending(c => c.TransactionDate).ToList();
            }
            else
            {
                _FixedDepositActionsMasterViewSrc = _FixedDepositActionsMasterViewSrc1
                    .Where(c => c.ApprovalId_Status == filter).OrderByDescending(c => c.TransactionDate).ToList();
            }
        }

        public async void SearchGrid(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                _FixedDepositActionsMasterViewSrc = _FixedDepositActionsMasterViewSrc1
                    .Where(c => c.CustomerID == CustomerID).OrderByDescending(c => c.TransactionDate).ToList();
            }
            else
            {
                _FixedDepositActionsMasterViewSrc = _FixedDepositActionsMasterViewSrc1
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