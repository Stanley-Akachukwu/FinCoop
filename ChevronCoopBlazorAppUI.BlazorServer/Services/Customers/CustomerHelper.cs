using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AutoMapper;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services.Customers.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Services.Customers
{
    public class CustomerHelper
    {
        public string Id { get; set; }
        public string PageType { get; set; }
        public string PageTitle { get; set; }
        public string MenuToShow { get; set; } = "Transaction";
        public bool ShowSavings { get; set; } = false;
        public bool ShowSpecialDeposit { get; set; } = false;
        public bool ShowFixedDeposit { get; set; } = false;
        public string ApplicationUserId { get; set; }
        public CustomerMasterView MemberProfile;
        public ClaimsPrincipal CurrentUser;
        public string CustomerId;

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public async Task<string> GetCustomerID()
        {
            MemberProfile = new CustomerMasterView();

            var authenticated = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            string userID = "";

            if (!string.IsNullOrEmpty(Principal))
            {
                var user = authenticated.User;

                if (user.Identity.IsAuthenticated)
                {
                    ApplicationUserId = user.FindFirstValue(ClaimTypes.Sid);
                    userID = ApplicationUserId;
                }
                return userID;
            }
            else
            {
                return userID = "";
            }


        }


        public async Task<CustomerMasterView> GetProfile(string CustomerId)
        {

            var rsp = await DataService.GetValue<List<CustomerMasterView>>(
             nameof(Customer), CustomerId);

            if (!rsp.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                List<CustomerMasterView> rspResponse = JsonSerializer.Deserialize<List<CustomerMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 && !string.IsNullOrEmpty(rspResponse.FirstOrDefault().ApplicationUserId) && rspResponse.FirstOrDefault().ApplicationUserId == ApplicationUserId)
                {
                    MemberProfile = Mapper.Map<CustomerMasterView>(rspResponse.FirstOrDefault());
                    return MemberProfile;
                }
            }
            return null;
        }

    }
}
