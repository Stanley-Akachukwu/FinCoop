using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.CommonViews;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using AP.ChevronCoop.Commons;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AntDesign;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.SpecialDeposit
{
    public partial class SpecialDashboard
    {
		[Parameter] public bool IsAdmin { get; set; }
		[Parameter]
        public decimal AvailableBalance { get; set; } = 0;

        [Parameter]
        public decimal LedgerBalance { get; set; } = 0;

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public string CustomerID { get; set; }

		[Parameter]
		public string SpecialDepositAccountID { get; set; }

		[Parameter]
		public string SpecialDepositAccountNumber { get; set; }

		[Parameter]
		public DepositAccountsMasterView AccountsMasterView { get; set; }


		[Parameter]
		public CustomerMasterView MemberProfile { get; set; }

		[Parameter]
        public string MembersName { get; set; }

        [Parameter]
        public string MembersNumber { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }
        public SpecialDepositAccountMasterView _SpecialDepositAccountMasterView { get; set; } = new SpecialDepositAccountMasterView();

        public List<SpecialDepositActionsMasterView> _SpecialDepositActionsMasterView { get; set; }
        public List<SpecialDepositActionsMasterView> _SpecialDepositActionsMasterViewSrc { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _SpecialDepositAccountMasterView = new SpecialDepositAccountMasterView();
            await GetSpecialDepositAccountMasterView();
        }


        public async Task GetSpecialDepositAccountMasterView()
        {
            var rsp = await DataService.GetValue<List<SpecialDepositAccountMasterView>>(
                nameof(SpecialDepositAccountMasterView), "Id", SpecialDepositAccountID, "customerID",
                CustomerID);
            if (rsp.IsSuccessStatusCode)
            {


                _SpecialDepositAccountMasterView = JsonSerializer.Deserialize<List<SpecialDepositAccountMasterView>>(rsp.Content.ToJson()).FirstOrDefault();
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
    }
}