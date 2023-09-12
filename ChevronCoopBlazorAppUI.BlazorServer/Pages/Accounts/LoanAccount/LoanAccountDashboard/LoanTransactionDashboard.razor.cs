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
using AP.ChevronCoop.Entities.Loans.LoanApplications;
namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount.LoanAccountDashboard
{
    public partial class LoanTransactionDashboard
    {
        [Parameter]
        public decimal LoanBalance { get; set; } = 0;
        [Parameter]
        public decimal LoanAmount { get; set; } = 0;
        [Parameter]
        public decimal TotalAmountRepaid { get; set; } = 0;
        [Parameter]
        public decimal NextRepaymentAmount { get; set; } = 0;

        [Parameter]
        public string CustomerID { get; set; }

        [Parameter]
        public string LoanApplicationId { get; set; }

        [Parameter]
        public string LoanAccountNumber { get; set; }
        public bool showDrawal { get; set; } = true;

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public string MembersName { get; set; }

        [Parameter]
        public string MembersNumber { get; set; }
        public bool showCashAddition { get; set; } = false;
        public List<LoanApplicationMasterView> _LoanApplicationMasterViewSrc1 { get; set; }
        public List<LoanApplicationMasterView> _LoanApplicationMasterViewSrc { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await GetLoanApplicationMasterrView();
        }

        public async Task GetLoanApplicationMasterrView()
        {
            var rsp = await DataService.GetValue<List<LoanApplicationMasterView>>(
             nameof(LoanApplicationMasterView), "id", LoanApplicationId);
            if (rsp.IsSuccessStatusCode)
            {
                _LoanApplicationMasterViewSrc1 = new List<LoanApplicationMasterView>();

                _LoanApplicationMasterViewSrc1 = JsonSerializer.Deserialize<List<LoanApplicationMasterView>>(rsp.Content.ToJson());
                _LoanApplicationMasterViewSrc = _LoanApplicationMasterViewSrc1.OrderByDescending(c => c.DateCreated).ToList();

            }
        }

    }
}
