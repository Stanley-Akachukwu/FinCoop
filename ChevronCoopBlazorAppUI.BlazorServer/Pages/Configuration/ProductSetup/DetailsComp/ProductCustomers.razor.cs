using AntDesign;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using Syncfusion.Blazor.Data;
using System.Text.Json;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount;
using ChevronCoop.Web.AppUI.BlazorServer.Services.Customers.Interface;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup.DetailsComp
{
    public partial class ProductCustomers
    {

        [Parameter] public DepositProductMasterView Model { get; set; }

        [Inject]
        ILogger<DepositAccountGrid> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        bool showCreateDrawer { get; set; } = false;

        BrowserDimension BrowserDimension;

        [Parameter]
        public string filter { get; set; }

        private WhereFilter statusFilter { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        AntDesign.ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();
        string Active { get; set; } = "text-blue-600 border-b-2 border-blue-600 active";
        string Inactive { get; set; } = "border-b-2 border-transparent";
        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        CustomerMasterView MemberProfile { get; set; }
        string ApplicationUserId { get; set; }
        string All { get; set; } = string.Empty;
        string Savings { get; set; } = string.Empty;
        string SpecialDeposit { get; set; } = string.Empty;
        string FixedDeposit { get; set; } = string.Empty;
        public List<DepositAccountsMasterView> DepositAccountsMasterViewSrc1 { get; set; } = new List<DepositAccountsMasterView>();
        public List<DepositAccountsMasterView> DepositAccountsMasterViewSrc { get; set; } = new List<DepositAccountsMasterView>();

        [Inject]
        WebConfigHelper Config { get; set; }

        SfGrid<DepositAccountsMasterView> grid;

        private Query QueryGrid; // = new Query();

        string searchText;
        string ErrorDetails = "";
        ErrorDetails errors;

        string tabToShow = "depositaccount";


        private string bearToken { get; set; }

        public string CustomerId { get; set; }

        [Inject]
        ICustomersMasterViews customersMasterViews { get; set; }

        [Inject]
        private IMasterViews _MasterView { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);
                if (string.IsNullOrEmpty(bearToken))
                {
                    _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
                }

                HeaderData.Add("Bearer", bearToken);

                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        protected override async Task OnInitializedAsync()
        {
            MemberProfile = new CustomerMasterView();
            await GetCurrentUser();

            await GetDepositAccountMasterView();
        }

        public async Task GetDepositAccountMasterView()
        {
            DepositAccountsMasterViewSrc1 = await _MasterView.GetCustomMasterViewEntityWithFilterAndAllFields<DepositAccountsMasterView>(nameof(DepositAccountsMasterView), "DepositProductId", Model.Id);

            DepositAccountsMasterViewSrc = DepositAccountsMasterViewSrc1.OrderByDescending(ex => ex.DateCreated).ToList();

        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;

        }



        public async void SearchAccount(Microsoft.AspNetCore.Components.ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                DepositAccountsMasterViewSrc = DepositAccountsMasterViewSrc1.OrderByDescending(ex => ex.DateCreated).ToList();
            }
            else
            {
                DepositAccountsMasterViewSrc = DepositAccountsMasterViewSrc1
                    .Where(c => (c.AccountNo?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (c.AccountType?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false))
                    .Take(5)
                    .OrderByDescending(c => c.DateCreated)
                    .ToList();
            }
        }
    }
}
