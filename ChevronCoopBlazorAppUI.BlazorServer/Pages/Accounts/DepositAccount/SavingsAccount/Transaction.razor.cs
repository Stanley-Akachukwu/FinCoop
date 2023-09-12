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

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.SavingsAccount
{
    public partial class Transaction
    {
        [Parameter]
        public string CustomerID { get; set; }

		[Parameter] public bool IsAdmin { get; set; }

		[Parameter]
        public string SavingsAccountID { get; set; }

        [Parameter]
        public string SavingsAccountNumber { get; set; }

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
        public List<SavingsActionsMasterView> _SavingsCashAdditionMasterViewSrc1 { get; set; }
        public List<SavingsActionsMasterView> _SavingsCashAdditionMasterViewSrc { get; set; }

        public string selectedTab = "text-gray-500 border-blue-600";

        public string allTab, cashadditionTab, increaseTab = "text-blue-600";

        protected override async Task OnInitializedAsync()
        {
            await GetCashAdditionMasterView();
            
        }

        public async Task GetCashAdditionMasterView()
        {
            var rsp = await DataService.GetValue<List<SavingsActionsMasterView>>(
                nameof(SavingsActionsMasterView), "savingsAccountId", SavingsAccountID, "customerID", CustomerID);
            if (rsp.IsSuccessStatusCode)
            {
                _SavingsCashAdditionMasterViewSrc1 = new List<SavingsActionsMasterView>();

                _SavingsCashAdditionMasterViewSrc1 =
                    JsonSerializer.Deserialize<List<SavingsActionsMasterView>>(rsp.Content.ToJson());
                _SavingsCashAdditionMasterViewSrc = _SavingsCashAdditionMasterViewSrc1
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
            showCashAddition = true;
        }

        private async Task OnUpdateChangedHandler(bool isComplete)
        {
            StateHasChanged();
            showDrawal = false;
            await GetCashAdditionMasterView();
            await InvokeAsync(StateHasChanged);
        }

        public async void clearTabCSS()
        {
            allTab = cashadditionTab = increaseTab = "text-blue-600";
        }

        public async Task onFiltering(string filter)
        {
            if (filter.ToLowerInvariant() == "all")
            {
                _SavingsCashAdditionMasterViewSrc = _SavingsCashAdditionMasterViewSrc1.OrderByDescending(c => c.TransactionDate).ToList();

            }
            else
            {
                _SavingsCashAdditionMasterViewSrc = _SavingsCashAdditionMasterViewSrc1
                    .Where(c => c.TransactionType == filter).OrderByDescending(c => c.TransactionDate).ToList();
            }
            if(filter == "Cash Addition")
            {
               
            }
            else if (filter == "Increase/Decrease")
            {
               
            }
        }

        public async void SearchGrid(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                _SavingsCashAdditionMasterViewSrc = _SavingsCashAdditionMasterViewSrc1
                    .Where(c => c.CustomerID == CustomerID).OrderByDescending(c => c.TransactionDate).ToList();
            }
            else
            {
                _SavingsCashAdditionMasterViewSrc = _SavingsCashAdditionMasterViewSrc1
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