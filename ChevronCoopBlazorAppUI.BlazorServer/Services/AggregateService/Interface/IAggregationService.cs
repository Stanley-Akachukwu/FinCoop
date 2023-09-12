namespace ChevronCoop.Web.AppUI.BlazorServer.Services.AggregateService.Interface
{
    public interface IAggregationService
    {
        Task<decimal> TotalBalanceByAccountType(string entityName, string AccountType, string accountTypeValue, string fieldToSum, string approvalstatus, string approvalstatusValue);

        Task<decimal> TotalBalance(string entityName, string fieldToSum, string approvalstatus, string approvalstatusValue);

        Task<int> TotalRowsByAccountType<T>(string entityName, string FieldType, string FieldTypeValue, string fieldToSelect, string approvalstatus, string approvalstatusValue);

        Task<int> TotalRowsByApprovalStatus<T>(string entityName, string fieldToSelect, string approvalstatus, string approvalstatusValue);


	}
}
