using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using AP.ChevronCoop.Commons;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AntDesign;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.SpecialDeposit
{
    public partial class SpecialDepositTransactions
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

        [Parameter]
        public string MembersNumber { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public bool showCashAddition { get; set; } = false;
        public List<SpecialDepositActionsMasterView> _SpecialDepositActionsMasterView = new List<SpecialDepositActionsMasterView>();
        public List<SpecialDepositActionsMasterView> _SpecialDepositActionsMasterViewSrc = new List<SpecialDepositActionsMasterView>();


        protected override async Task OnInitializedAsync()
        {
            await GetTransactions();
        }

        public async Task GetTransactions()
        {
           
            var rsp = await DataService.GetValue<List<SpecialDepositActionsMasterView>>(
               nameof(SpecialDepositActionsMasterView), "specialDepositAccountId", SpecialDepositAccountID, "customerID", CustomerID);
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


        private async Task OnShowDrawal()
        {
            showDrawal = true;
            showCashAddition = true;
        }


        public async Task onFiltering(string filter)
        {
            _SpecialDepositActionsMasterViewSrc =
                _SpecialDepositActionsMasterView.Where(c => c.ApprovalId_Status == filter).ToList();

            if (filter.Contains("_"))
            {
                _SpecialDepositActionsMasterViewSrc = _SpecialDepositActionsMasterView
                    .Where(c => c.ApprovalId_Status == filter || c.ApprovalId_Status == filter.Replace("_", " "))
                    .OrderByDescending(c => c.TransactionDate).ToList();
            }

            if (filter == "all")
            {
                _SpecialDepositActionsMasterViewSrc = _SpecialDepositActionsMasterViewSrc
                    .Where(c => c.CustomerID == CustomerID).OrderByDescending(c => c.TransactionDate).ToList();
            }
        }
    }
}