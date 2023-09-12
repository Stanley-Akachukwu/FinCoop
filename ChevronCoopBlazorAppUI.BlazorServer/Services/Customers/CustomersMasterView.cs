using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services.Customers.Interface;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using OneOf.Types;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Services.Customers
{
    public class CustomersMasterView : ICustomersMasterViews
    {

        private readonly IEntityDataService DataService;

      
        public CustomerMasterView _CustomerMasterView { set; get; } = new CustomerMasterView();

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        public CustomersMasterView(IEntityDataService entityDataService)
        {
            DataService = entityDataService;
        }


        public async Task<List<DepositAccountsMasterView>> GetDepositAccountMasterView(string CustomerID)
        {
            var customerDepositAccount = new  List<DepositAccountsMasterView>();
            try
            {
                var rsp = await DataService.GetValue<List<DepositAccountsMasterView>>(
               nameof(DepositAccountsMasterView), "CustomerId", CustomerID);
                if (rsp.IsSuccessStatusCode)
                {

                    customerDepositAccount = JsonSerializer.Deserialize<List<DepositAccountsMasterView>>(rsp.Content.ToJson());
                    customerDepositAccount = customerDepositAccount.OrderByDescending(c => c.DateCreated).ToList();

                }
                else
                {

                }
            }
            catch
            {

            }
            return customerDepositAccount;
        }

        public async Task<CustomerMasterView> GetCustomerMasterView(string CustomerID)
        {
            try
            {
                var rsp = await DataService.GetValue<List<CustomerMasterView>>(
               nameof(CustomerMasterView), "id", CustomerID);
                if (rsp.IsSuccessStatusCode)
                {
                    _CustomerMasterView = new CustomerMasterView();

                    var response =
                        JsonSerializer.Deserialize<List<CustomerMasterView>>(rsp.Content.ToJson());

                    _CustomerMasterView = response.FirstOrDefault();
                }
                else
                {

                }
            }
            catch
            {

            }
            return _CustomerMasterView;
        }

        public async Task<List<DepositAccountsMasterView>> GetAllDepositAccountMasterView()
        {
            var result = new List<DepositAccountsMasterView>();
            try
            {
                var rsp = await DataService.GetApprovedValue<List<DepositAccountsMasterView>>(nameof(DepositAccountsMasterView), "status", "APPROVED");
                if (rsp.IsSuccessStatusCode)
                {
                   
                    result = JsonSerializer.Deserialize<List<DepositAccountsMasterView>>(rsp.Content.ToJson());
                    return result = result.OrderByDescending(c => c.DateCreated).ToList();

                }
                else
                {
                    return result;
                }
            }
            catch
            {
                return result;
            }
          
        }

        public async Task<List<DepositAccountsMasterView>> GetCustomerDepositActions(string customerID, string orderbyColumn, int count, string asc_or_desc)
        {
            var response = new List<DepositAccountsMasterView>();
            try
            {
                var rsp = await DataService.GetValueWithOrderAndCount<List<DepositAccountsMasterView>>(
               nameof(DepositAccountsMasterView), "customerId", customerID, orderbyColumn, count, asc_or_desc);

                response = new List<DepositAccountsMasterView>();
                if (!rsp.IsSuccessStatusCode)
                {

                }
                else
                {
                    response = JsonSerializer.Deserialize<List<DepositAccountsMasterView>>(rsp.Content.ToJson());

                }
            }
            catch
            {

            }  
            return response;
        }

        public async Task<List<LoanAccountMasterView>> GetAllCustomersLoansAccountMasterView()
        {
            var result = new List<LoanAccountMasterView>();
            try
            {
                var rsp = await DataService.GetApprovedValue<List<LoanAccountMasterView>>(
                                 nameof(LoanAccountMasterView), "loanApplicationId_Status", "APPROVED");

                if (rsp.IsSuccessStatusCode)
                {
                    
                    result =
                        JsonSerializer.Deserialize<List<LoanAccountMasterView>>(rsp.Content.ToJson());
                    return result = result.OrderByDescending(c => c.DateCreated).ToList();

                }
              
            }
            catch { 
            
            }

            return result;


        }

        public async Task<string> GetCurrentUser()
        {
            string getCustomer = "";
            try
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                CurrentUser = authState.User;
                if (CurrentUser != null)
                {
                    var checker = CurrentUser.Claims.Where(f => f.Type == "CustomerId").FirstOrDefault();
                    getCustomer =  (checker != null) ? checker.Value : "";
                }
            }
            catch { }

            return getCustomer;
        }

        public async Task<List<LoanAccountMasterView>> GetCustomerLoanProducts(string CustomerId)
        {
            var response = new List<LoanAccountMasterView>();
            try
            {
                var rsp = await DataService.GetValue<List<LoanAccountMasterView>>(
                nameof(LoanAccountMasterView), "customerId", CustomerId);

                if (rsp.IsSuccessStatusCode)
                {
                    response = JsonSerializer.Deserialize<List<LoanAccountMasterView>>(rsp.Content.ToJson());
                }
            }
            catch { }

            return response;
        }


	}
}
