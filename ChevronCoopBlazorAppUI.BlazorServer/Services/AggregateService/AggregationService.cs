using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using System.Text.Json;
using ChevronCoop.Web.AppUI.BlazorServer.Services.AggregateService.Interface;
using ChevronCoop.Web.AppUI.BlazorServer.Data.AggregateDTO;
using AP.ChevronCoop.Commons;

namespace ChevronCoop.Web.AppUI.BlazorServer.Services.AggregateService
{
    public class AggregationService : IAggregationService
    {
        private readonly IEntityDataService DataService;


        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        public AggregationService(IEntityDataService entityDataService)
        {
            DataService = entityDataService;
        }


        public async Task<decimal> TotalBalanceByAccountType(string entityName, string AccountType, string accountTypeValue, string fieldToSum, string approvalstatus, string approvalstatusValue)
        {
            var totalAmount = new List<TotalAmountResponse>();
            try
            {
                var rsp = await DataService.GetSummation<List<TotalAmountResponse>>(entityName, AccountType, accountTypeValue, fieldToSum, approvalstatus, approvalstatusValue);
                if (rsp.IsSuccessStatusCode)
                {

                    totalAmount = JsonSerializer.Deserialize<List<TotalAmountResponse>>(rsp.Content.ToJson()).ToList();

                }
                else
                {

                }
            }
            catch
            {

            }
            return totalAmount.FirstOrDefault()?.TotalAmount ?? 0;
        }


        public async Task<decimal> TotalBalance(string entityName, string fieldToSum, string approvalstatus, string approvalstatusValue)
        {
            var totalAmount = new List<TotalAmountResponse>();
            try
            {
                var rsp = await DataService.GetAllSummation<List<TotalAmountResponse>>(entityName, fieldToSum, approvalstatus, approvalstatusValue);
                if (rsp.IsSuccessStatusCode)
                {

                    totalAmount = JsonSerializer.Deserialize<List<TotalAmountResponse>>(rsp.Content.ToJson()).ToList();

                }
                else
                {

                }
            }
            catch
            {

            }

            decimal valueToReturn = totalAmount?.FirstOrDefault()?.TotalAmount ?? 0;

            return valueToReturn;
        }


        public async Task<int> TotalRowsByAccountType<T>(string entityName, string FieldType, string FieldTypeValue, string fieldToSelect, string approvalstatus, string approvalstatusValue)
        {
            int totalRows = 0;
            var totalAmount = new List<T>();
            try
            {
                var rsp = await DataService.GetTotalRowsByType(entityName, FieldType, FieldTypeValue, fieldToSelect, approvalstatus, approvalstatusValue);

               
                var odataResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(rsp.ToJson());

                string value = odataResponse["@odata.count"].ToString();

                totalRows = Convert.ToInt32(value);
            }
            catch
            {

            }
            return totalRows;
        }

		public async Task<int> TotalRowsByApprovalStatus<T>(string entityName, string fieldToSelect, string approvalstatus, string approvalstatusValue)
		{
			int totalRows = 0;
			var totalAmount = new List<T>();
			try
			{
				var rsp = await DataService.GetTotalRowsByApprovalStatus(entityName, fieldToSelect, approvalstatus, approvalstatusValue);


				var odataResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(rsp.ToJson());

				string value = odataResponse["@odata.count"].ToString();

				totalRows = Convert.ToInt32(value);
			}
			catch
			{

			}
			return totalRows;
		}

       

    }
}
