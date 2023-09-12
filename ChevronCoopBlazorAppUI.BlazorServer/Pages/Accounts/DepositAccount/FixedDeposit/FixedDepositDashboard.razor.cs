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
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.FixedDeposit
{
    public partial class FixedDepositDashboard
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
		public DepositAccountsMasterView AccountsMasterView { get; set; }


		[Parameter]
		public CustomerMasterView MemberProfile { get; set; }
		


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
        public List<FixedDepositActionsMasterView> _FixedDepositActionsMasterViewSrc1 { get; set; } = new List<FixedDepositActionsMasterView>();
        public FixedDepositActionsMasterView _FixedDepositActionsMasterViewSrc { get; set; } 

        [Inject]
        NotificationService notificationService { get; set; }

        public FixedDepositAccountMasterView _FixedDepositAccountMasterView { get; set; } = new FixedDepositAccountMasterView();
        protected override async Task OnInitializedAsync()
        {
            _FixedDepositAccountMasterView = new FixedDepositAccountMasterView();
            await GetFixedDepositAccountMasterView();
        }
      

        public async Task GetFixedDepositAccountMasterView()
        {
            var rsp = await DataService.GetValue<List<FixedDepositAccountMasterView>>(
                nameof(FixedDepositAccountMasterView), "Id", FixedDepositAccountID, "customerID",
                CustomerID);
            if (rsp.IsSuccessStatusCode)
            {
            

                _FixedDepositAccountMasterView = JsonSerializer.Deserialize<List<FixedDepositAccountMasterView>>(rsp.Content.ToJson()).FirstOrDefault();
                _FixedDepositAccountMasterView.MaturityInstructionType = _FixedDepositAccountMasterView?.MaturityInstructionType.Replace('_', ' ');

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