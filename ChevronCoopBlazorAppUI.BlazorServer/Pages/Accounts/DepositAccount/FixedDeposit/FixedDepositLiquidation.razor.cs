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
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;
using DocumentFormat.OpenXml.Wordprocessing;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.FixedDeposit
{
    public partial class FixedDepositLiquidation
    {
		[Parameter] public bool IsAdmin { get; set; }
		[Parameter]
        public string CustomerID { get; set; }

        [Parameter]
        public string FixedDepositAccountID { get; set; }

        [Parameter]
        public string FixedDepositAccountNumber { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public bool showDrawal { get; set; } = true;

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public string MembersName { get; set; }

        [Parameter]
        public string MembersNumber { get; set; }

        public bool showFixedDepositLiquidation { get; set; } = false;
        public List<FixedDepositActionsMasterView> _FixedDepositMasterViewSrc1 { get; set; } = new List<FixedDepositActionsMasterView>();
        public List<FixedDepositActionsMasterView> _FixedDepositMasterViewSrc { get; set; } = new List<FixedDepositActionsMasterView>();

        string searchText;

        protected override async Task OnInitializedAsync()
        {
            await GetFixedDepositMasterView();
        }

        protected override async Task OnParametersSetAsync()
        {
            await GetFixedDepositMasterView();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetFixedDepositMasterView();
            }
        }

        public async Task GetFixedDepositMasterView()
        {
            var rsp = await DataService.GetValue<List<FixedDepositActionsMasterView>>(
                nameof(FixedDepositActionsMasterView), "customerID", CustomerID, "fixedDepositAccountId",
                FixedDepositAccountID, "transactionType", "Liquidation");
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
                                    false))
                    .Take(5)
                    .OrderByDescending(c => c.TransactionDate)
                    .ToList();
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
            _FixedDepositMasterViewSrc = _FixedDepositMasterViewSrc1.Where(c => c.ApprovalId_Status == filter).ToList();

            if (filter.Contains("_"))
            {
                _FixedDepositMasterViewSrc = _FixedDepositMasterViewSrc1
                    .Where(c => c.ApprovalId_Status == filter || c.ApprovalId_Status == filter.Replace("_", " "))
                    .OrderByDescending(c => c.TransactionDate).ToList();
            }

            if (filter == "all")
            {
                _FixedDepositMasterViewSrc = _FixedDepositMasterViewSrc1.Where(c => c.CustomerID == CustomerID)
                    .OrderByDescending(c => c.TransactionDate).ToList();
            }
        }
    }
}