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
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using AP.ChevronCoop.Entities.Loans.LoanProducts;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount.LoanAccountDashboard
{
    public partial class LoanApplicationDashboard
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
        public string LoanAccountId { get; set; }

        [Parameter]
        public string LoanAccountNumber { get; set; }
        public bool showDrawal { get; set; } = true;
        [Inject]
        AutoMapper.IMapper Mapper { get; set; }
        [Inject]
        IEntityDataService DataService { get; set; }
        [Inject]
        IMasterViews MasterViews { get; set; }
        [Parameter]
        public string MembersName { get; set; }
        public decimal PrincipalPaid { get; set; } = 0.0m;
        public bool IsAccountOpened { get; set; }

        [Parameter]
        public string MembersNumber { get; set; }
        public bool showCashAddition { get; set; } = false;
        public List<LoanApplicationMasterView> _LoanApplicationMasterViewSrc1 { get; set; }
        public List<LoanApplicationMasterView> _LoanApplicationMasterViewSrc { get; set; }
        public LoanAccountMasterView LoanAccountMasterView { get; set; }
        List<LoanProductMasterView> LoanProductMasterViews = new List<LoanProductMasterView>();
        LoanProductMasterView LoanProductMasterView = new LoanProductMasterView();
        protected override async Task OnInitializedAsync()
        {
            LoanAccountMasterView = new LoanAccountMasterView() { InterestBalanceAccountId_LedgerBalance  = 0.0m};
            await GetLoanAccountDetails();
        }

        //public async Task GetLoanApplicationMasterrView()
        //{
        //    var rsp = await DataService.GetValue<List<LoanApplicationMasterView>>(
        //     nameof(LoanApplicationMasterView), "id", LoanApplicationId);
        //    if (rsp.IsSuccessStatusCode)
        //    {
        //        _LoanApplicationMasterViewSrc1 = new List<LoanApplicationMasterView>();

        //        _LoanApplicationMasterViewSrc1 = JsonSerializer.Deserialize<List<LoanApplicationMasterView>>(rsp.Content.ToJson());
        //        _LoanApplicationMasterViewSrc = _LoanApplicationMasterViewSrc1.OrderByDescending(c => c.DateCreated).ToList();

        //    }
        //}

        public async Task GetLoanAccountDetails()
        {
            var rsp = await DataService.GetValue<List<LoanAccountMasterView>>(
                nameof(LoanAccountMasterView), "id", LoanAccountId);
            if (rsp.IsSuccessStatusCode)
            {
                List<LoanAccountMasterView> rspResponseLoanAccount = JsonSerializer.Deserialize<List<LoanAccountMasterView>>(rsp.Content.ToJson());
                if (rspResponseLoanAccount != null && rspResponseLoanAccount.Count > 0 && !string.IsNullOrEmpty(rspResponseLoanAccount.FirstOrDefault().Id) && rspResponseLoanAccount.FirstOrDefault().Id == LoanAccountId)
                {
                    LoanAccountMasterView = Mapper.Map<LoanAccountMasterView>(rspResponseLoanAccount.FirstOrDefault());
                    if (LoanAccountMasterView.PrincipalBalanceAccountId_LedgerBalance <= 0)
                    {
                        PrincipalPaid = 0.0m;
                    }
                    else { PrincipalPaid = LoanAccountMasterView.Principal - LoanAccountMasterView.PrincipalBalanceAccountId_LedgerBalance; }
                    if (LoanAccountMasterView.IsClosed)
                    {
                        IsAccountOpened = false;
                    }
                    else
                    {
                        IsAccountOpened = true;
                    }

                    //get loan product details 
                    LoanProductMasterViews = await MasterViews.GetCustomMasterView<LoanProductMasterView>(nameof(LoanProductMasterView), "Id", LoanAccountMasterView.LoanApplicationId_LoanProductId, DatabaseFields.LoanProductProperties);
                    LoanProductMasterView = LoanProductMasterViews.FirstOrDefault();
                }
            }
        }

    }
}
