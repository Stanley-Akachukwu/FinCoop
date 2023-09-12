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
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccounts;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.SavingsAccount
{
    public partial class Dashboard
    {
        //[Parameter]
        //public decimal AvailableBalance { get; set; } = 0;
		[Parameter] public bool IsAdmin { get; set; }

		[Parameter]
        public decimal LedgerBalance { get; set; } = 0;

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

        [Parameter]
        public string MembersNumber { get; set; }

        public bool showCashAddition { get; set; } = false;
        public List<SavingsActionsMasterView> _SavingsCashAdditionMasterViewSrc1 { get; set; } = new List<SavingsActionsMasterView>();
        public List<SavingsActionsMasterView> _SavingsCashAdditionMasterViewSrc { get; set; } = new List<SavingsActionsMasterView>();
        [Parameter]
        public DepositAccountsMasterView AccountsMasterView { get; set; } = new DepositAccountsMasterView();


        [Parameter]
        public CustomerMasterView MemberProfile { get; set; }
        public SavingsAccountMasterView _SavingsAccountMasterView { get; set; } = new SavingsAccountMasterView();

        [Inject]
        private IMasterViews _MasterView { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetCashAdditionMasterView();
        }

        public async Task GetCashAdditionMasterView()
        {
            var rst = await _MasterView.GetCustomMasterViewEntityWithFilterAndAllFields<SavingsAccountMasterView>(nameof(SavingsAccountMasterView), "Id", SavingsAccountID);

            _SavingsAccountMasterView = rst.FirstOrDefault();

            //var rsp = await DataService.GetValue<List<SavingsActionsMasterView>>(
            //    nameof(SavingsActionsMasterView), "savingsAccountId", SavingsAccountID, "customerID", CustomerID);
            //if (rsp.IsSuccessStatusCode)
            //{
            //    _SavingsCashAdditionMasterViewSrc1 = new List<SavingsActionsMasterView>();

            //    _SavingsCashAdditionMasterViewSrc1 =
            //        JsonSerializer.Deserialize<List<SavingsActionsMasterView>>(rsp.Content.ToJson());
            //    _SavingsCashAdditionMasterViewSrc = _SavingsCashAdditionMasterViewSrc1
            //        .OrderByDescending(c => c.TransactionDate).ToList();

            //    var rst = await _MasterView.GetCustomMasterViewEntityWithFilterAndAllFields<SavingsAccountMasterView>(nameof(SavingsAccountMasterView), "Id", SavingsAccountID);

            //    _SavingsAccountMasterView = rst.FirstOrDefault();
            //}
            //else
            //{
            //    await notificationService.Open(new NotificationConfig()
            //    {
            //        Message = MessageBox.ServerErrorHeader,
            //        Description = MessageBox.ServerErrorDescription,
            //        NotificationType = NotificationType.Error
            //    });
            //}
        }



    }
}