using System;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using Microsoft.IdentityModel.Tokens;
using AP.ChevronCoop.Entities.Loans.LoanOffSetTransactions;
using Syncfusion.Blazor.Grids;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount.LoanAccountDashboard
{
    public partial class LoanRepaymentSchedule
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
        public string LoanApplicationId { get; set; }

        [Parameter]
        public string LoanAccountNumber { get; set; }
        public bool showDrawal { get; set; } = true;
        [Inject]
        AutoMapper.IMapper Mapper { get; set; }
        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public string MembersName { get; set; }

        [Parameter]
        public string MembersNumber { get; set; }
        public bool showCashAddition { get; set; } = false;
        public List<LoanRepaymentScheduleMasterView> LoanRepaymentScheduleMasterViews { get; set; }
        string searchText;
        SfGrid<AmortizationSchedule> grid;
        protected override async Task OnInitializedAsync()
        {
            await GetLoanAccountDetails();
        }

        public async Task GetLoanAccountDetails()
        {
            var rsp = await DataService.GetValue<List<LoanRepaymentScheduleMasterView>>(
                nameof(LoanRepaymentScheduleMasterView), "loanAccountId", LoanAccountID);
            if (!rsp.IsSuccessStatusCode)
            {
                LoanRepaymentScheduleMasterViews = new List<LoanRepaymentScheduleMasterView>();
            }
            else
            {
                LoanRepaymentScheduleMasterViews = JsonSerializer.Deserialize<List<LoanRepaymentScheduleMasterView>>(rsp.Content.ToJson());
                LoanRepaymentScheduleMasterViews = LoanRepaymentScheduleMasterViews.OrderBy(c => c.RepaymentNo).ToList();
            }
        }
        public async void SearchGrid(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                LoanRepaymentScheduleMasterViews = LoanRepaymentScheduleMasterViews
                    .Where(c => c.LoanAccountId_CustomerId == CustomerID).OrderByDescending(c => c.DateCreated).ToList();
            }
            else
            {
                LoanRepaymentScheduleMasterViews = LoanRepaymentScheduleMasterViews
                    .Where(c => (c.Description?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false))
                    .Take(5)
                    .OrderByDescending(c => c.DateCreated)
                    .ToList();
            }
        }
        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "gridrepayment_excelexport") //Id is combination of Grid's ID and itemname
            {
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Repayment_Schedule.xlsx";
                await this.grid.ExportToExcelAsync(ExportProperties);
            }
        }
    }
}
