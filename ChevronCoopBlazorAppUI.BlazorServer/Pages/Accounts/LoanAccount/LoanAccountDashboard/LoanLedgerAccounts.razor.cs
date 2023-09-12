using AP.ChevronCoop.AppDomain.Accounting.LedgerAccounts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanTopupTransactions;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount.LoanAccountDashboard
{
    public partial class LoanLedgerAccounts
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
        public string LoanAccountID { get; set; }

        [Parameter]
        public string LonaAccountNumber { get; set; }
        public bool showDrawal { get; set; } = true;
        string searchText;
        [Inject]
        IEntityDataService DataService { get; set; }
        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Parameter]
        public string MembersName { get; set; }
        public List<LoanAccountMasterView> LoanAccountMasterViews { get; set; }
        public List<LoanAccountMasterView> LoanAccountMasterView_Src { get; set; }
        public List<LedgerAccountViewModel> LedgerAccounts { get; set; }
        [Parameter]
        public string MembersNumber { get; set; }
        [Inject] NavigationManager _navigationManager { get; set; }
        private bool isAdmin { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            LedgerAccounts = new List<LedgerAccountViewModel>();
            await GetLedgerTransactions();
        }
        public async Task GetLedgerTransactions()
        {
            var rsp = await DataService.GetValue<List<LoanAccountMasterView>>(
             nameof(LoanAccountMasterView), "id", LoanAccountID);
            if (rsp.IsSuccessStatusCode)
            {
                LoanAccountMasterViews = new List<LoanAccountMasterView>();

                LoanAccountMasterViews = JsonSerializer.Deserialize<List<LoanAccountMasterView>>(rsp.Content.ToJson());
                LoanAccountMasterView_Src = LoanAccountMasterViews.OrderByDescending(c => c.DateCreated).ToList();

                if(LoanAccountMasterView_Src != null)
                {
                    foreach(var loanAccount in LoanAccountMasterView_Src)
                    {
                        if (!string.IsNullOrEmpty(loanAccount.PrincipalBalanceAccountId_CurrencyId))
                        {
                            LedgerAccounts.Add(new LedgerAccountViewModel {
                                Code = loanAccount.PrincipalBalanceAccountId_Code,
                                Name = loanAccount.PrincipalBalanceAccountId_Name,
                                AccountType = loanAccount.PrincipalBalanceAccountId_AccountType,
                                Description = loanAccount.PrincipalBalanceAccountId_Caption,
                                LedgerBalance = loanAccount.PrincipalBalanceAccountId_LedgerBalance,
                                AvailableBalance = loanAccount.PrincipalBalanceAccountId_AvailableBalance
                            });
                        }
                        if (!string.IsNullOrEmpty(loanAccount.EarnedInterestAccountId_CurrencyId))
                        {
                            LedgerAccounts.Add(new LedgerAccountViewModel
                            {
                                Code = loanAccount.EarnedInterestAccountId_Code,
                                Name = loanAccount.EarnedInterestAccountId_Name,
                                AccountType = loanAccount.EarnedInterestAccountId_AccountType,
                                Description = loanAccount.EarnedInterestAccountId_Caption,
                                LedgerBalance = loanAccount.EarnedInterestAccountId_LedgerBalance,
                                AvailableBalance = loanAccount.EarnedInterestAccountId_AvailableBalance.GetValueOrDefault()
                            });
                        }
                        if (!string.IsNullOrEmpty(loanAccount.InterestBalanceAccountId_CurrencyId))
                        {
                            LedgerAccounts.Add(new LedgerAccountViewModel
                            {
                                Code = loanAccount.InterestBalanceAccountId_Code,
                                Name = loanAccount.InterestBalanceAccountId_Name,
                                AccountType = loanAccount.InterestBalanceAccountId_AccountType,
                                Description = loanAccount.InterestBalanceAccountId_Caption,
                                LedgerBalance = loanAccount.InterestBalanceAccountId_LedgerBalance.GetValueOrDefault(),
                                AvailableBalance = loanAccount.InterestBalanceAccountId_AvailableBalance.GetValueOrDefault()
                            });
                        }
                        if (!string.IsNullOrEmpty(loanAccount.UnearnedInterestAccountId_CurrencyId))
                        {
                            LedgerAccounts.Add(new LedgerAccountViewModel
                            {
                                Code = loanAccount.UnearnedInterestAccountId_Code,
                                Name = loanAccount.UnearnedInterestAccountId_Name,
                                AccountType = loanAccount.UnearnedInterestAccountId_AccountType,
                                Description = loanAccount.UnearnedInterestAccountId_Caption,
                                LedgerBalance = loanAccount.UnearnedInterestAccountId_LedgerBalance.GetValueOrDefault(),
                                AvailableBalance = loanAccount.UnearnedInterestAccountId_AvailableBalance.GetValueOrDefault()
                            });
                        }
                        if (!string.IsNullOrEmpty(loanAccount.InterestLossAccountId_CurrencyId))
                        {
                            LedgerAccounts.Add(new LedgerAccountViewModel
                            {
                                Code = loanAccount.InterestLossAccountId_Code,
                                Name = loanAccount.InterestLossAccountId_Name,
                                AccountType = loanAccount.InterestLossAccountId_AccountType,
                                Description = loanAccount.InterestLossAccountId_Caption,
                                LedgerBalance = loanAccount.InterestLossAccountId_LedgerBalance.GetValueOrDefault(),
                                AvailableBalance = loanAccount.InterestLossAccountId_AvailableBalance.GetValueOrDefault()
                            });
                        }
                        if (!string.IsNullOrEmpty(loanAccount.InterestWaivedAccountId_CurrencyId))
                        {
                            LedgerAccounts.Add(new LedgerAccountViewModel
                            {
                                Code = loanAccount.InterestWaivedAccountId_Code,
                                Name = loanAccount.InterestWaivedAccountId_Name,
                                AccountType = loanAccount.InterestWaivedAccountId_AccountType,
                                Description = loanAccount.InterestWaivedAccountId_Caption,
                                LedgerBalance = loanAccount.InterestWaivedAccountId_LedgerBalance.GetValueOrDefault(),
                                AvailableBalance = loanAccount.InterestWaivedAccountId_AvailableBalance.GetValueOrDefault()
                            });
                        }
                        if (!string.IsNullOrEmpty(loanAccount.ChargesWaivedAccountId_CurrencyId))
                        {
                            LedgerAccounts.Add(new LedgerAccountViewModel
                            {
                                Code = loanAccount.ChargesWaivedAccountId_Code,
                                Name = loanAccount.ChargesWaivedAccountId_Name,
                                AccountType = loanAccount.ChargesWaivedAccountId_AccountType,
                                Description = loanAccount.ChargesWaivedAccountId_Caption,
                                LedgerBalance = loanAccount.ChargesWaivedAccountId_LedgerBalance.GetValueOrDefault(),
                                AvailableBalance = loanAccount.ChargesWaivedAccountId_AvailableBalance.GetValueOrDefault()
                            });
                        }
                        if (!string.IsNullOrEmpty(loanAccount.ChargesAccruedAccountId_CurrencyId))
                        {
                            LedgerAccounts.Add(new LedgerAccountViewModel
                            {
                                Code = loanAccount.ChargesAccruedAccountId_Code,
                                Name = loanAccount.ChargesAccruedAccountId_Name,
                                AccountType = loanAccount.ChargesAccruedAccountId_AccountType,
                                Description = loanAccount.ChargesAccruedAccountId_Caption,
                                LedgerBalance = loanAccount.ChargesAccruedAccountId_LedgerBalance.GetValueOrDefault(),
                                AvailableBalance = loanAccount.ChargesAccruedAccountId_AvailableBalance.GetValueOrDefault()
                            });
                        }
                        if (!string.IsNullOrEmpty(loanAccount.EarnedInterestAccountId_CurrencyId))
                        {
                            LedgerAccounts.Add(new LedgerAccountViewModel
                            {
                                Code = loanAccount.EarnedInterestAccountId_Code,
                                Name = loanAccount.EarnedInterestAccountId_Name,
                                AccountType = loanAccount.EarnedInterestAccountId_AccountType,
                                Description = loanAccount.EarnedInterestAccountId_Caption,
                                LedgerBalance = loanAccount.EarnedInterestAccountId_LedgerBalance,
                                AvailableBalance = loanAccount.EarnedInterestAccountId_AvailableBalance.GetValueOrDefault()
                            });
                        }
                    }
                }

            }
        }
        public async void SearchGrid(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                LoanAccountMasterView_Src = LoanAccountMasterView_Src
                    .Where(c => c.CustomerId == CustomerID).OrderByDescending(c => c.DateCreated).ToList();
            }
        }
    }
}
