using AntDesign;
using AP.ChevronCoop.AppDomain.Loans.LoanOffsets;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount
{
    public partial class LoanRequestHistory
    {
        [Inject]
        ILogger<LoanRequestHistory> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        private FluentValidationValidator? _fluentValidationValidator;

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

        CreateLoanOffsetCommand Command { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        [Inject] NavigationManager _navigationManager { get; set; }
        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        MemberProfileMasterView MemberProfile { get; set; }
        string ApplicationUserId { get; set; }
        bool AccountIsProvided { get; set; }

        string combobox_preferredSpecialAccounts;
        string combobox_preferredAccounts;
        bool showSpecialDepositAccount { get; set; } = false;
        bool showPreferredAccount { get; set; } = false;

        [Inject]
        WebConfigHelper Config { get; set; }

        SfGrid<LoanApplicationMasterView> grid;
        public LoanOffSetDTO Model { get; set; }
        public EventCallback<LoanOffSetDTO> ModelChanged { get; set; }

        string GRID_API_RESOURCE =>
            $"{Config.ODATA_VIEWS_HOST}/{nameof(LoanApplicationMasterView)}?$filter=CustomerId eq '{CustomerId}'";

        private Query QueryGrid; // = new Query();

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;
        string ErrorDetails = "";
        ErrorDetails errors;

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

                await OnRefresh();
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        protected override async Task OnInitializedAsync()
        {
            Model = new LoanOffSetDTO();
            MemberProfile = new MemberProfileMasterView();
            await GetCurrentUser();
            QueryGrid = new Query();
            await GetProfile();
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task LoadDropDown()
        {
            combobox_preferredAccounts =
                $"{Config.ODATA_VIEWS_HOST}/{nameof(CustomerBankAccountMasterView)}?$filter=CustomerId eq '{CustomerId}'";
            Logger.LogInformation($"url->{System.Text.Json.JsonSerializer.Serialize(combobox_preferredAccounts)}");
            combobox_preferredSpecialAccounts =
                $"{Config.ODATA_VIEWS_HOST}/{nameof(SpecialDepositAccountMasterView)}?$filter=CustomerId eq '{CustomerId}'";
        }

        public void ShowSpecialDepositAccounts()
        {
            showSpecialDepositAccount = true;
            showPreferredAccount = false;
            StateHasChanged();
        }

        public void ShowPreferredAccounts()
        {
            showSpecialDepositAccount = false;
            showPreferredAccount = true;
            StateHasChanged();
        }

        private void SpecialBankValueChangeHandler(ChangeEventArgs<string, SpecialDepositAccountMasterView> args)
        {
            Model.SpecialDepositId = args.Value;
            Model.DestinationAccountId = null;
            Model.UseSpecialDeposit = true;
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

        public void ActionCompletedHandler(ActionEventArgs<LoanApplicationMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Id == "gridLoanApplication_excelexport") //Id is combination of Grid's ID and itemname
            {
                ExcelExportProperties ExportProperties = new ExcelExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Loan_Application.xlsx";
                await this.grid.ExportToExcelAsync(ExportProperties);
            }

            if (args.Item.Id == "gridLoanApplication_pdfexport") //Id is combination of Grid's ID and itemname
            {
                PdfExportProperties ExportProperties = new PdfExportProperties();
                ExportProperties.IncludeHiddenColumn = true;
                ExportProperties.FileName = "Loan_Application.pdf";
                await this.grid.ExportToPdfAsync();
            }
        }

        public async Task GetProfile()
        {
            ApplicationUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrEmpty(ApplicationUserId))
            {
                _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
            }

            var rsp = await DataService.GetValue<List<MemberProfileMasterView>>(
                nameof(MemberProfileMasterView), ApplicationUserId);


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
                List<MemberProfileMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<MemberProfileMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().ApplicationUserId) &&
                    rspResponse.FirstOrDefault().ApplicationUserId == ApplicationUserId)
                {
                    MemberProfile = new MemberProfileMasterView();
                    MemberProfile = rspResponse.FirstOrDefault();
                    MemberProfile = Mapper.Map<MemberProfileMasterView>(rspResponse.FirstOrDefault());
                    ;
                }
            }
        }

        public void ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            this.ErrorDetails = args.Error.ToString();
            StateHasChanged();
        }

        private async Task ProceedToOffsetAsync()
        {
        }

        private async Task OnRefresh()
        {
            searchText = string.Empty;
            QueryGrid = new Query();
            grid.Refresh();

            await Task.CompletedTask;
        }

        private async Task OnSearchEnter(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                if (!string.IsNullOrWhiteSpace(searchText))
                    await OnSearch();
                else
                    await OnRefresh();
            }
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

        private async Task OnClearSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                await OnRefresh();
            }
        }

        async Task onCreate()
        {
        }

        private async Task OnProceedToTopUp(LoanApplicationMasterView row)
        {
            var Id = row.Id;
            var url = $"/account/loanTopUp?id={Id}";
            _navigationManager.NavigateTo(url, true);
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<LoanApplicationMasterView> Args)
        {
            Args.Data.LoanProductId_LoanProductType = Args.Data.LoanProductId_LoanProductType != null
                ? Args.Data.LoanProductId_LoanProductType.Replace("_", " ")
                : "";
        }
    }
}