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
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.SavingsAccount
{
    public partial class CashAddition
    {
        [Parameter]
        public string CustomerID { get; set; }

		[Parameter] public bool IsAdmin { get; set; }

		[Parameter]
        public string SavingsAccountID { get; set; }

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

        public bool showCashAddition { get; set; } = false;
        public List<SavingsActionsMasterView> _SavingsCashAdditionMasterViewSrc1 { get; set; }
        public List<SavingsActionsMasterView> _SavingsCashAdditionMasterViewSrc { get; set; }
        string searchText;

        string selectedTab = "text-gray-500 border-blue-600";

        string allTab, approveTab, pendingTab, rejectedTab = "text-blue-600";
        string tabtoShow = "othertab1";

        [Inject]
        NotificationService notificationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCashAdditionMasterView();
            allTab = selectedTab;
        }

        public async void resetTab()
        {
            allTab = approveTab = pendingTab = rejectedTab = "text-blue-600";
        }

        public async Task GetCashAdditionMasterView()
        {
            var rsp = await DataService.GetValue<List<SavingsActionsMasterView>>(
                nameof(SavingsActionsMasterView), "savingsAccountId", SavingsAccountID, "customerID", CustomerID,
                "transactionType", "Cash Addition");
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
            
            await GetCashAdditionMasterView();
            await InvokeAsync(StateHasChanged);
        }

        public async Task onFiltering(string filter)
        {
            _SavingsCashAdditionMasterViewSrc =
                _SavingsCashAdditionMasterViewSrc1.Where(c => c.ApprovalId_Status == filter).ToList();

            if (filter.Contains("_"))
            {
                _SavingsCashAdditionMasterViewSrc = _SavingsCashAdditionMasterViewSrc1
                    .Where(c => c.ApprovalId_Status == filter || c.ApprovalId_Status == filter.Replace("_", " "))
                    .OrderByDescending(c => c.TransactionDate).ToList();
                
            }
            else if (filter == "all")
            {
                _SavingsCashAdditionMasterViewSrc = _SavingsCashAdditionMasterViewSrc1
                    .Where(c => c.CustomerID == CustomerID).OrderByDescending(c => c.TransactionDate).ToList();
               
            }
            else
            {
                _SavingsCashAdditionMasterViewSrc = _SavingsCashAdditionMasterViewSrc1
                    .Where(c => c.ApprovalId_Status == filter).OrderByDescending(c => c.TransactionDate).ToList();
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
                                    false))
                    .Take(5)
                    .OrderByDescending(c => c.TransactionDate)
                    .ToList();
            }
        }
    }
}