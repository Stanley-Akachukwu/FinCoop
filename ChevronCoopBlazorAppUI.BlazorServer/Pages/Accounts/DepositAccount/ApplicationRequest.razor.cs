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
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Util;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount
{
    public partial class ApplicationRequest
    {
        [Inject]
        ILogger<DepositAccountGrid> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        bool showCreateDrawer { get; set; } = false;

        BrowserDimension BrowserDimension;

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

        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        CustomerMasterView MemberProfile { get; set; }
        string ApplicationUserId { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        SfGrid<DepositApplicationsMasterView> grid;

        // string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(DepositApplicationsMasterView)}?$filter=CustomerId eq '{MemberProfile.Id}'";
        public List<DepositApplicationsMasterView> DepositApplicationMasterViewSrc1 = new List<DepositApplicationsMasterView>();
        public List<DepositApplicationsMasterView> DepositApplicationMasterViewSrc = new List<DepositApplicationsMasterView>();
        private Query QueryGrid; // = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;
        string ErrorDetails = "";
        ErrorDetails errors;

        public CustomerMasterView Model { get; set; }

        private string bearToken { get; set; }

        public string CustomerId { get; set; }

        private ElementReference dropdownButtonRef;

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

                //   await OnRefresh();
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        protected override async Task OnInitializedAsync()
        {
            MemberProfile = new CustomerMasterView();
            await GetCurrentUser();
            QueryGrid = new Query();
            await GetProfile();
            await GetDepositAccountMasterView();
        }

        public async Task GetDepositAccountMasterView()
        {
            var rsp = await DataService.GetValue<List<DepositApplicationsMasterView>>(
                nameof(DepositApplicationsMasterView), "CustomerId", CustomerId);
            if (rsp.IsSuccessStatusCode)
            {
                DepositApplicationMasterViewSrc1 =
                    JsonSerializer.Deserialize<List<DepositApplicationsMasterView>>(rsp.Content.ToJson());
                DepositApplicationMasterViewSrc =
                    DepositApplicationMasterViewSrc1.OrderByDescending(c => c.DateCreated).ToList();
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

        public async Task onFiltering(string filter)
        {
            DepositApplicationMasterViewSrc =
                DepositApplicationMasterViewSrc1.Where(c => c.ApprovalId_Status == filter).ToList();

            if (filter.Contains("_"))
            {
                DepositApplicationMasterViewSrc = DepositApplicationMasterViewSrc1
                    .Where(c => c.ApprovalId_Status == filter || c.ApprovalId_Status == filter.Replace("_", " "))
                    .OrderByDescending(c => c.DateCreated).ToList();
            }

            if (filter == "all")
            {
                DepositApplicationMasterViewSrc = DepositApplicationMasterViewSrc1
                    .Where(c => c.CustomerId == CustomerId).OrderByDescending(c => c.DateCreated).ToList();
            }

            await jsRuntime.InvokeVoidAsync("eval", "document.getElementById('dropdownDefaultButton').click();");
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
                ExportProperties.FileName = "Deposit_Application.xlsx";
                await this.grid.ExportToExcelAsync(ExportProperties);
            }

            if (args.Item.Id == "gridLoanApplication_pdfexport") //Id is combination of Grid's ID and itemname
            {
                PdfExportProperties ExportProperties = new PdfExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Deposit_Application.pdf";
                await this.grid.ExportToPdfAsync();
            }
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
                        Message = MessageBox.ServerErrorHeader,
                        Description = MessageBox.ServerErrorDescription,
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

        private async Task OnRefresh()
        {
            searchText = string.Empty;
            QueryGrid = new Query();
            // grid.Refresh();

            await Task.CompletedTask;
        }

        private async Task OnSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                WhereFilter roleFilter = new WhereFilter
                {
                    Field = nameof(CurrencyMasterView.Code),
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter nameFilter = new WhereFilter
                {
                    Field = "Name",
                    Operator = "contains",
                    value = searchText
                };

                WhereFilter descriptionFilter = new WhereFilter
                {
                    Field = "Description",
                    Operator = "contains",
                    value = searchText
                };


                QueryGrid = new Query().Where(nameFilter.Or(roleFilter).Or(descriptionFilter));
            }
            else
            {
                await OnRefresh();
            }
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<DepositApplicationsMasterView> Args)
        {
            Args.Data.ApprovalId_Status =
                Args.Data.ApprovalId_Status != null ? Args.Data.ApprovalId_Status.Replace("_", " ") : "";
        }

        public async void SearchApplication(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                DepositApplicationMasterViewSrc =
                    DepositApplicationMasterViewSrc1.Where(c => c.CustomerId == CustomerId).ToList();
            }
            else
            {
                DepositApplicationMasterViewSrc = DepositApplicationMasterViewSrc1
                    .Where(c => (c.ApplicationNo?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (c.AccountType?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.ProductId_Name?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.ApprovalId_Status?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ??
                                    false))
                    .Take(5)
                    .OrderByDescending(c => c.DateCreated)
                    .ToList();
            }
        }
    }
}