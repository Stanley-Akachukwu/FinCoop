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
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount
{
    public partial class DepositAccountGrid
    {
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
        public List<DepositAccountsMasterView> DepositAccountsMasterViewSrc1 = new List<DepositAccountsMasterView>();
        public List<DepositAccountsMasterView> DepositAccountsMasterViewSrc = new List<DepositAccountsMasterView>();    

        [Inject]
        WebConfigHelper Config { get; set; }

        SfGrid<DepositAccountsMasterView> grid;

        private Query QueryGrid; // = new Query();

        string searchText;
        string ErrorDetails = "";
        ErrorDetails errors;

        string tabToShow = "depositaccount";

        public CustomerMasterView Model { get; set; }

        private string bearToken { get; set; }

        public string CustomerId { get; set; }


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

            await GetProfile();

            await GetDepositAccountMasterView();
        }

        public async Task GetDepositAccountMasterView()
        {
            var rsp = await DataService.GetValue<List<DepositAccountsMasterView>>(
                nameof(DepositAccountsMasterView), "CustomerId", CustomerId);
            if (rsp.IsSuccessStatusCode)
            {
                DepositAccountsMasterViewSrc1 = new List<DepositAccountsMasterView>();

                DepositAccountsMasterViewSrc1 =
                    JsonSerializer.Deserialize<List<DepositAccountsMasterView>>(rsp.Content.ToJson());
                DepositAccountsMasterViewSrc =
                    DepositAccountsMasterViewSrc1.OrderByDescending(c => c.DateCreated).ToList();
                ActivateTab();
                All = Active;
            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = MessageBox.ServerErrorHeader,
                    Description = MessageBox.ServerErrorDescription,
                    NotificationType = NotificationType.Error
                });
            }
        }

        public void ActivateTab()
        {
            All = Inactive;
            SpecialDeposit = Inactive;
            FixedDeposit = Inactive;
            Savings = Inactive;
        }

        public async Task ShowPendingApprovals()
        {
        }

        async Task onFiltering(string filter)
        {
            DepositAccountsMasterViewSrc = new List<DepositAccountsMasterView>();

            DepositAccountsMasterViewSrc = DepositAccountsMasterViewSrc1.Where(c => c.AccountType == filter)
                .OrderByDescending(c => c.DateCreated).ToList();

           
            if (filter == "all")
            {
                DepositAccountsMasterViewSrc =
                    DepositAccountsMasterViewSrc1.Where(c => c.CustomerId == CustomerId).ToList();
                
            }
           
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
            if (CurrentUser != null)
            {
                CustomerId = CurrentUser.Claims.Where(f => f.Type == "CustomerId").FirstOrDefault().Value;
            }
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "gridLoanApplication_excelexport") //Id is combination of Grid's ID and itemname
            {
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "DepositProducts.xlsx";
                await this.grid.ExportToExcelAsync(ExportProperties);
            }

            if (args.Item.Id == "gridLoanApplication_pdfexport") //Id is combination of Grid's ID and itemname
            {
                PdfExportProperties ExportProperties = new PdfExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "DepositProducts.pdf";
                await this.grid.ExportToPdfAsync();
            }
        }


        public void ActionCompletedHandler(ActionEventArgs<DepositAccountsMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        public async Task GetProfile()
        {
            var rsp = await DataService.GetValue<List<CustomerMasterView>>(
                nameof(Customer), ApplicationUserId);

            if (!rsp.IsSuccessStatusCode)
            {
                if (!string.IsNullOrEmpty(rsp.Error.Content))
                {
                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    var msg = rspContent?.Response;
                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        msg = rspContent.ValidationErrors[0].Error;
                    }

                    if (msg == null && rspContent?.Message != null)
                        msg = rspContent.Message;
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = msg,
                        NotificationType = NotificationType.Error
                    });
                }
                else
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = "Oops! Something Went Wrong. Please try again later. Thanks",
                        NotificationType = NotificationType.Error
                    });
                }
            }
            else
            {
                List<CustomerMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<CustomerMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().ApplicationUserId) &&
                    rspResponse.FirstOrDefault().ApplicationUserId == ApplicationUserId)
                {
                    MemberProfile = new CustomerMasterView();
                    MemberProfile = rspResponse.FirstOrDefault();
                    Model = Mapper.Map<CustomerMasterView>(rspResponse.FirstOrDefault());
                    ;
                }
            }
        }


        private async Task OnSearchEnter(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                if (!string.IsNullOrWhiteSpace(searchText))
                    await OnSearch();
            }
        }

        private async Task OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
            }
        }

        private async Task OnClearSearch()
        {
        }

        public void ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = args.Error.ToString();
            StateHasChanged();
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<DepositAccountsMasterView> Args)
        {
            Args.Data.Status = Args.Data.Status != null ? Args.Data.Status.Replace("_", " ") : "";
        }


        public async void SearchAccount(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                DepositAccountsMasterViewSrc = DepositAccountsMasterViewSrc1.Where(c => c.CustomerId == CustomerId)
                    .OrderByDescending(c => c.DateCreated).ToList();
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