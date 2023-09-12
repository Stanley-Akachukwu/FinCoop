
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Syncfusion.Blazor.Data;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.SharedComponents
{
    public partial class CurrentUserComp
    {
        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }
        public string ApplicationUserId { get; set; }
        public string CustomerId { get; set; }

        public string Fullname { get; set; }

        MemberProfileMasterView MemberProfile { get; set; }

        public List<CustomerMasterView> _CustomerMasterView { get; set; }

        [Inject]
        private UserService _UserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
            if (CurrentUser != null)
            {
                _UserService.ApplicationUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);

                var rsp = await DataService.GetValue<List<CustomerMasterView>>(
               nameof(CustomerMasterView), "applicationUserId", _UserService.ApplicationUserId);

                if (rsp.IsSuccessStatusCode)
                {
                    _CustomerMasterView = JsonSerializer.Deserialize<List<CustomerMasterView>>(rsp.Content.ToJson());
                    if (_CustomerMasterView.Count > 0)
                    {
                        var customerClaims = CurrentUser.Claims.Where(f => f.Type == "CustomerId").FirstOrDefault();
                        if (customerClaims != null)
                        {
                            _UserService.CustomerId = customerClaims.Value;
                        }

                        _UserService.Fullname = _CustomerMasterView.FirstOrDefault().FullName ?? string.Empty;
                        _UserService.MemberId = _CustomerMasterView.FirstOrDefault().MemberId ?? string.Empty;
                    }

                }


            }

        }
    }
}