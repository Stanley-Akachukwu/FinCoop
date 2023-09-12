using AntDesign;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.MasterData.Departments;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using System.Security.Claims;
using System.Text.Json;
using Query = Syncfusion.Blazor.Data.Query;


namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup
{
    public partial class ProductSetupGrid
    {
        public string combobox_Department_res { get; set; }

        string notificationText;
        bool showPopup = false;

        [Inject]
        ILogger<ProductSetupGrid> Logger { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

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

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }


        SfGrid<DepositProductMasterView> grid;

        string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(DepositProductMasterView)}";
        //string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(MemberProfileMasterView)}?$orderby=DateCreated desc";


        private Query QueryGrid;
        private Query Query_Combo;

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        ClaimsPrincipal CurrentUser { get; set; }
        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }
        private string bearToken { get; set; }
        string ErrorDetails = "";

        ErrorDetails errors;

        //ApproveDepositProductSetupForm approveForm;
        Drawer approveDrawer;
        Drawer specificDrawer;
        Drawer departmentDrawer;
        bool showApproveDrawer { get; set; } = false;
        bool showSpecificDrawer { get; set; } = false;
        bool showDepartmentDrawer { get; set; } = false;
        BrowserDimension BrowserDimension;

        [Parameter]
        public string filter { get; set; }

        private WhereFilter statusFilter { get; set; }
        private WhereFilter searchApprovedFilter { get; set; }
        private WhereFilter searchRejectedFilter { get; set; }
        private WhereFilter searchPendingApprovalFilter { get; set; }
        private WhereFilter searchPublishedFilter { get; set; }
        string Active { get; set; } = "text-blue-600 border-b-2 border-blue-600 active";
        string Inactive { get; set; } = "border-b-2 border-transparent";
        string All { get; set; } = string.Empty;
        string Pending { get; set; } = string.Empty;
        string Approved { get; set; } = string.Empty;
        string Rejected { get; set; } = string.Empty;
        string Published { get; set; } = string.Empty;
        string DepositProductId { get; set; } = string.Empty;

        public bool disableCreatePermission { get; set; } = false;
        public List<DepartmentMasterView> departmentList { get; set; }
        public CustomerMasterView AddRow { get; set; }

        public DepartmentMasterView AddDepartmentRow { get; set; }

        //public ProductPublicationCommand productPublicationCommand { get; set; }
        public string combobox_users_res { get; set; }
        public string combobox_department_res { get; set; }
        public List<CustomerMasterView> userList { get; set; }
        public PublishDepositProductCommand command { get; set; }

        public List<DepositProductMasterView> DepositProductMasterViews { get; set; } = new List<DepositProductMasterView>();
        public List<DepositProductMasterView> DepositProductMasterViewsSrc { get; set; } = new List<DepositProductMasterView>();


        private async Task CheckState()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                if (user.HasClaim(ClaimTypes.Role, PermissionConfig.DepositsProductCreate))
                    disableCreatePermission = true;
            }
        }



        private void OnValueSelecthandler(SelectEventArgs<CustomerMasterView> args)
        {
            AddRow = new CustomerMasterView();
            AddRow = args.ItemData;
        }



        protected override async Task OnInitializedAsync()
        {
            try
            {
                searchText = "";
                //AddRow = new MembersToPublishTo();
                LoadDropDown();
                userList = new List<CustomerMasterView>();
                await CheckState();
                await GetCurrentUser();

                AddDepartmentRow = new DepartmentMasterView();
                //productPublicationCommand = new ProductPublicationCommand();
                departmentList = new List<DepartmentMasterView>();
                command = new PublishDepositProductCommand();

                //ApproveCommand = new ApproveDepositProductCommand();

                QueryGrid = new Query();
                await ExecuteFilteringAsync();

                await GetDepositProducts();

                await _auditLogService.LogAudit("Accessed Deposit Product list", "Accessed Deposit Product list",
                    "Security", "NA, readonly request", CurrentUser);
            }
            catch (Exception ex)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = ex.Message,
                    NotificationType = NotificationType.Error,
                    Description = "Error"
                });
            }
        }

        public void LoadDropDown()
        {
            combobox_Department_res = $"{Config.ODATA_VIEWS_HOST}/{nameof(MemberProfileMasterView)}";
            combobox_users_res = $"{Config.ODATA_VIEWS_HOST}/{nameof(CustomerMasterView)}";
            combobox_department_res = $"{Config.ODATA_VIEWS_HOST}/{nameof(DepartmentMasterView)}";
        }

        public async Task GetDepositProducts()
        {

            var rsp = await DataService.GetMasterView<List<DepositProductMasterView>>(nameof(DepositProductMasterView));

            if (rsp.IsSuccessStatusCode)
            {
                DepositProductMasterViewsSrc =
                    JsonSerializer.Deserialize<List<DepositProductMasterView>>(rsp.Content.ToJson());

                DepositProductMasterViews = DepositProductMasterViewsSrc.OrderBy(x => x.Name).ToList();
                UpdateSerial();
            }
        }

        public async void UpdateSerial()
        {
            int serial = 1;
            foreach (var sn in DepositProductMasterViews)
            {
                sn.Caption = serial.ToString();
                serial++;
            }
        }

        public async void SearchProducts(ChangeEventArgs args)
        {
            string inputValue = args.Value?.ToString();
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                DepositProductMasterViews = DepositProductMasterViewsSrc.OrderBy(x => x.Name).ToList();
            }
            else
            {
                DepositProductMasterViews = DepositProductMasterViewsSrc
                    .Where(c => (c.Name?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (c.Code?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.Status?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ?? false)
                                || (c.Description?.Contains(inputValue, StringComparison.OrdinalIgnoreCase) ??
                                    false))
                    .Take(5)
                    .OrderByDescending(c => c.DateCreated)
                    .ToList();
            }
            UpdateSerial();

        }

        async Task onFiltering(string filter)
        {
            if (filter == "all")
            {
                DepositProductMasterViews = DepositProductMasterViewsSrc.OrderBy(x => x.Name).ToList();
            }
            else if (filter.ToLowerInvariant() == "pending_approval" || filter.ToLowerInvariant() == "pending approval")
            {
                DepositProductMasterViews = DepositProductMasterViewsSrc.Where(x => x.Status.ToLowerInvariant() == filter || x.Status.ToLowerInvariant() == "pending approval").OrderBy(x => x.Name).ToList();
            }
            else
            {
                DepositProductMasterViews = DepositProductMasterViewsSrc.Where(x => x.Status == filter).OrderBy(x => x.Name).ToList();
            }
            UpdateSerial();
        }

        public void CellInfoHandler(QueryCellInfoEventArgs<DepositProductMasterView> Args)
        {
            Args.Data.Status = Args.Data.Status != null ? Args.Data.Status.Replace("_", " ") : "";
        }

        private async Task ExecuteFilteringAsync()
        {
            switch (filter)
            {
                case "approved":
                    statusFilter = new WhereFilter
                    {
                        Field = nameof(DepositProductMasterView.Status),
                        Operator = "equal",
                        value = nameof(ProductStatus.APPROVED)
                    };

                    QueryGrid = new Query().Where(statusFilter);
                    ActivateTab();
                    Approved = Active;
                    break;
                case "rejected":
                    statusFilter = new WhereFilter
                    {
                        Field = nameof(DepositProductMasterView.Status),
                        Operator = "equal",
                        value = nameof(ProductStatus.REJECTED)
                    };
                    QueryGrid = new Query().Where(statusFilter);
                    ActivateTab();
                    Rejected = Active;
                    break;
                case "pending_approval":
                    statusFilter = new WhereFilter
                    {
                        Field = nameof(DepositProductMasterView.Status),
                        Operator = "equal",
                        value = nameof(ProductStatus.PENDING_APPROVAL)
                    };
                    var pendingstatusFilter = new WhereFilter
                    {
                        Field = nameof(DepositProductMasterView.Status),
                        Operator = "equal",
                        value = nameof(ProductStatus.PENDING)
                    };
                    QueryGrid = new Query().Where(statusFilter.Or(pendingstatusFilter));
                    ActivateTab();
                    Pending = Active;
                    break;
                case "published":
                    statusFilter = new WhereFilter
                    {
                        Field = nameof(DepositProductMasterView.Status),
                        Operator = "equal",
                        value = "PUBLISHED"
                    };
                    QueryGrid = new Query().Where(statusFilter);
                    ActivateTab();
                    Published = Active;
                    break;
                default:
                case "all":
                    var approvedFilter = new WhereFilter
                    {
                        Field = nameof(DepositProductMasterView.Status),
                        Operator = "equal",
                        value = nameof(ProductStatus.APPROVED)
                    };
                    var rejectedFilter = new WhereFilter
                    {
                        Field = nameof(DepositProductMasterView.Status),
                        Operator = "equal",
                        value = nameof(ProductStatus.DEACTIVATED)
                    };
                    var pendingApprovalFilter = new WhereFilter
                    {
                        Field = nameof(DepositProductMasterView.Status),
                        Operator = "equal",
                        value = nameof(ProductStatus.PENDING_APPROVAL)
                    };
                    var publishedFilter = new WhereFilter
                    {
                        Field = nameof(DepositProductMasterView.Status),
                        Operator = "equal",
                        value = nameof(ProductStatus.PUBLISHED)
                    };

                    statusFilter = new WhereFilter
                    {
                        Field = nameof(DepositProductMasterView.Status),
                        Operator = "equal",
                        value = nameof(ProductStatus.REJECTED)
                    };

                    QueryGrid = new Query().Where(
                        approvedFilter.Or(
                            rejectedFilter.Or(statusFilter.Or(pendingApprovalFilter.Or(publishedFilter)))));
                    ActivateTab();
                    All = Active;
                    break;
            }
        }

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

                BrowserDimension = await BrowserService.GetDimensions();
                //approveDrawer.Width = (int)(BrowserDimension.Width * 0.25);
                specificDrawer.Width = (int)(BrowserDimension.Width * 0.40);
                departmentDrawer.Width = (int)(BrowserDimension.Width * 0.40);
                StateHasChanged();
            }

            await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private async Task onApprovingProductSetup(DepositProductMasterView row)
        {
            //ApproveCommand = new ApproveDepositProductCommand();
            //ApproveCommand.Status = row.Status;
            //ApproveCommand.DepositProductId = row.Id;
            //ApproveCommand.ApprovalId = row.ApprovalId;
            //ApproveCommand.ApprovedBy = CurrentUser.FindFirstValue(ClaimTypes.Sid);
            //var payload = JsonSerializer.Serialize(ApproveCommand);
            //await _auditLogService.LogAudit("Deposit Product Setup Approval.", $"Approved Deposit Product Setup with ID- {row.Id}.", "Security", payload, CurrentUser);
            //await onApprove();
        }

        async Task onApprove()
        {
            showApproveDrawer = true;
        }


        async Task onApproveDone()
        {
            showApproveDrawer = false;
        }


        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        public void ActionCompletedHandler(ActionEventArgs<DepositProductMasterView> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }


        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            //this.ErrorDetailMsg = args.Error.ToString();
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }

        string nameTextField = "";
        string valueTextField = "";

        public async Task OnSaveClose()
        {
            try
            {
                var checkIsSuccessfull = await ValidateEntry();
                if (checkIsSuccessfull)
                {
                    command.PublicationType = PublicationType.CUSTOMER;
                    command.Targets = userList.Select(x => x.Id).ToList();
                    command.PublishedByUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
                    var rsp = await DataService
                        .ProcessRequest<PublishDepositProductCommand, CommandResult<DepositProductViewModel>>(
                            nameof(DepositProduct), "publish", command);
                    Logger.LogInformation($"error->{JsonSerializer.Serialize(command)}");

                    if (!rsp.IsSuccessStatusCode)
                    {
                        Logger.LogInformation($"error->{JsonSerializer.Serialize(rsp?.Error?.Content)}");
                        var serverErrorMessages = "Server Error.";
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);
                        var msg = rsp.ReasonPhrase;

                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
                        {
                            serverErrorMessages += " " + rspContent.ValidationErrors[0].Error;
                        }

                        if (!string.IsNullOrEmpty(rspContent.Message))
                        {
                            serverErrorMessages += " " + rspContent.Message;
                        }

                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = msg,
                            NotificationType = NotificationType.Error,
                            Description = serverErrorMessages
                        });
                    }
                    else
                    {
                        var payload = System.Text.Json.JsonSerializer.Serialize(command);
                        await _auditLogService.LogAudit("Publish Deposit product to specific user Creation.", "ID",
                            "Security", payload, CurrentUser);


                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Success",
                            NotificationType = NotificationType.Success,
                            Description = rsp.Content.Message
                        });
                        await onSpecificDone();

                        await GetDepositProducts();
                    }
                }
            }
            catch (Exception ex)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = ex.Message,
                    Description = "Request could not be processed.",
                    NotificationType = NotificationType.Error
                });
            }
        }

        public async Task<bool> ValidateEntry()
        {
            if (userList.Count() < 1)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "No user selected for publish",
                    NotificationType = NotificationType.Error
                });

                return false;
            }

            return true;
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
            await SearchFiltering();
        }

        private async Task OnClearSearch()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                await OnRefresh();
            }
        }

        private async Task OnRefresh()
        {
            searchText = string.Empty;
            //ExecuteFilteringAsync();
            grid.Refresh();

            await Task.CompletedTask;
        }

        public void ActivateTab()
        {
            All = Inactive;
            Approved = Inactive;
            Pending = Inactive;
            Rejected = Inactive;
            Published = Inactive;
        }

        private async Task SearchFiltering()
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                WhereFilter NameFilter = new WhereFilter
                {
                    Field = nameof(DepositProductMasterView.Name),
                    Operator = "contains",
                    value = searchText
                };


                WhereFilter tenureFilter = new WhereFilter
                {
                    Field = nameof(DepositProductMasterView.TenureValue),
                    Operator = "contains",
                    value = searchText
                };
                WhereFilter statusFilter = new WhereFilter
                {
                    Field = nameof(DepositProductMasterView.Status),
                    Operator = "contains",
                    value = searchText
                };
                switch (filter)
                {
                    case "approved":
                        statusFilter = new WhereFilter
                        {
                            Field = nameof(DepositProductMasterView.Status),
                            Operator = "equal",
                            value = nameof(ProductStatus.APPROVED)
                        };
                        QueryGrid = new Query().Where(statusFilter.And(NameFilter.Or(tenureFilter).Or(statusFilter)));
                        ActivateTab();
                        Approved = Active;
                        break;
                    case "rejected":
                        statusFilter = new WhereFilter
                        {
                            Field = nameof(DepositProductMasterView.Status),
                            Operator = "equal",
                            value = nameof(ProductStatus.REJECTED)
                        };
                        QueryGrid = new Query().Where(statusFilter.And(NameFilter.Or(tenureFilter).Or(statusFilter)));
                        ActivateTab();
                        Rejected = Active;
                        break;
                    case "pending_approval":
                        statusFilter = new WhereFilter
                        {
                            Field = nameof(DepositProductMasterView.Status),
                            Operator = "equal",
                            value = nameof(ProductStatus.PENDING_APPROVAL)
                        };
                        QueryGrid = new Query().Where(statusFilter.And(NameFilter.Or(tenureFilter).Or(statusFilter)));
                        ActivateTab();
                        Pending = Active;
                        break;
                    case "published":
                        statusFilter = new WhereFilter
                        {
                            Field = nameof(DepositProductMasterView.Status),
                            Operator = "equal",
                            value = "PUBLISHED"
                        };
                        QueryGrid = new Query().Where(statusFilter.And(NameFilter.Or(tenureFilter).Or(statusFilter)));
                        ActivateTab();
                        Published = Active;
                        break;
                    default:
                    case "all":
                        searchApprovedFilter = new WhereFilter
                        {
                            Field = nameof(DepositProductMasterView.Status),
                            Operator = "equal",
                            value = nameof(ProductStatus.APPROVED)
                        };
                        searchRejectedFilter = new WhereFilter
                        {
                            Field = nameof(DepositProductMasterView.Status),
                            Operator = "equal",
                            value = nameof(ProductStatus.REJECTED)
                        };
                        searchPendingApprovalFilter = new WhereFilter
                        {
                            Field = nameof(DepositProductMasterView.Status),
                            Operator = "equal",
                            value = nameof(ProductStatus.PENDING_APPROVAL)
                        };
                        searchPublishedFilter = new WhereFilter
                        {
                            Field = nameof(DepositProductMasterView.Status),
                            Operator = "equal",
                            value = "PUBLISHED"
                        };

                        QueryGrid = new Query();
                        ActivateTab();
                        All = Active;
                        break;
                }
            }
            else
            {
                await OnRefresh();
            }
        }

        private async Task DeactivateRecored(DepositProductMasterView row)
        {
            DepositProductId = row.Id;
            showPopup = true;
        }

        private async Task ConfirmDeactivateRecored()
        {
            try
            {
                if (!string.IsNullOrEmpty(DepositProductId))
                {
                    UpdateDepositProductStatusCommand command = new UpdateDepositProductStatusCommand()
                    {
                        DepositProductId = DepositProductId,
                        Status = ProductStatus.DEACTIVATED
                    };
                    var rsp = await DataService
                        .ProcessRequest<UpdateDepositProductStatusCommand, DepositProductViewModel>(
                            nameof(DepositProduct), "updateStatus", command);
                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(command)}");
                    if (rsp.IsSuccessStatusCode)
                    {
                        showPopup = false;
                        //_navigationManager.NavigateTo($"/ProductSetup/Manage/{filter}", forceLoad: true);
                        GetDepositProducts();
                    }
                    else
                    {
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                        var msg = rspContent?.Response;
                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
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
                        StateHasChanged();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task Cancel()
        {
            DepositProductId = string.Empty;
            showPopup = false;
            StateHasChanged();
        }

        public async Task OnViewRow(DepositProductMasterView row)
        {
            _navigationManager.NavigateTo($"/ProductSetup/view/{row.Id}");
        }

        public async Task OnEditRow(DepositProductMasterView row)
        {
            _navigationManager.NavigateTo($"/ProductSetup/edit/{row.Id}");
        }

        async Task onSpecificDone()
        {
            //productPublicationCommand = new ProductPublicationCommand();
            showSpecificDrawer = false;
        }

        async Task onDepartmentDone()
        {
            //productPublicationCommand = new ProductPublicationCommand();
            showDepartmentDrawer = false;
        }

        private void OnValueSelectDepartmenthandler(SelectEventArgs<DepartmentMasterView> args)
        {
            AddDepartmentRow = new DepartmentMasterView();
            AddDepartmentRow = args.ItemData;
        }

        public void AddDepartmentList()
        {
            if (AddDepartmentRow == null || string.IsNullOrEmpty(AddDepartmentRow?.Id))
            {
                notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    NotificationType = NotificationType.Error,
                    Description = "No department selected. Please select and Try again"
                });
            }
            else if (departmentList.Any(c => c.Id == AddDepartmentRow.Id))
            {
                notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    NotificationType = NotificationType.Error,
                    Description = "Department already added"
                });
            }
            else
            {
                departmentList.Add(AddDepartmentRow);
                StateHasChanged();
            }
        }

        public void AddToUsersList()
        {
            if (AddRow == null || string.IsNullOrEmpty(AddRow?.Id))
            {
                notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    NotificationType = NotificationType.Error,
                    Description = "No user selected. Please select and Try again"
                });
            }
            else if (userList.Any(c => c.Id == AddRow.Id))
            {
                notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    NotificationType = NotificationType.Error,
                    Description = "User already added"
                });
            }
            else
            {
                userList.Add(AddRow);
                StateHasChanged();
            }
        }

        private async Task onPublishToAllUser(DepositProductMasterView row)
        {
            command = new PublishDepositProductCommand();
            command.ProductId = row.Id;
            command.PublicationType = PublicationType.ALL;
            command.PublishedByUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
            var rsp = await DataService
                .ProcessRequest<PublishDepositProductCommand, CommandResult<DepositProductViewModel>>(
                    nameof(DepositProduct), "publish", command);
            Logger.LogInformation($"error->{JsonSerializer.Serialize(command)}");

            if (!rsp.IsSuccessStatusCode)
            {
                Logger.LogInformation($"error->{JsonSerializer.Serialize(rsp?.Error?.Content)}");
                var serverErrorMessages = "Server Error.";
                var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);
                var msg = rsp.ReasonPhrase;

                if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                {
                    serverErrorMessages += " " + rspContent.ValidationErrors[0].Error;
                }

                if (!string.IsNullOrEmpty(rspContent.Message))
                {
                    serverErrorMessages += " " + rspContent.Message;
                }

                await notificationService.Open(new NotificationConfig()
                {
                    Message = msg,
                    NotificationType = NotificationType.Error,
                    Description = serverErrorMessages
                });
            }
            else
            {
                var payload = JsonSerializer.Serialize(command);
                await _auditLogService.LogAudit("Publish deposit product to all user Creation.", "ID", "Security",
                    payload, CurrentUser);


                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Success",
                    NotificationType = NotificationType.Success,
                    Description = rsp.Content.Message
                });
                await onSpecificDone();
                await GetDepositProducts();
            }
        }

        private async Task onPublishToDepartment(string Id)
        {
            try
            {
                command = new PublishDepositProductCommand();
                command.ProductId = Id;

                showDepartmentDrawer = true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task OnSaveDepartmentClose()
        {
            try
            {
                var checkIsSuccessfull = await ValidateDepartment();
                if (checkIsSuccessfull)
                {
                    command.PublicationType = PublicationType.DEPARTMENT;
                    command.Targets = departmentList.Select(x => x.Id).ToList();
                    command.PublishedByUserId = CurrentUser.FindFirstValue(ClaimTypes.Sid);
                    var payload = JsonSerializer.Serialize(command);
                    var rsp = await DataService
                        .ProcessRequest<PublishDepositProductCommand, CommandResult<DepositProductViewModel>>(
                            nameof(DepositProduct), "publish", command);
                    Logger.LogInformation($"error->{JsonSerializer.Serialize(command)}");


                    if (!rsp.IsSuccessStatusCode)
                    {
                        Logger.LogInformation($"error->{JsonSerializer.Serialize(rsp?.Error?.Content)}");
                        var serverErrorMessages = "Server Error.";
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);
                        var msg = rsp.ReasonPhrase;

                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
                        {
                            serverErrorMessages += " " + rspContent.ValidationErrors[0].Error;
                        }

                        if (!string.IsNullOrEmpty(rspContent.Message))
                        {
                            serverErrorMessages += " " + rspContent.Message;
                        }

                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = msg,
                            NotificationType = NotificationType.Error,
                            Description = serverErrorMessages
                        });
                    }
                    else
                    {
                        await _auditLogService.LogAudit("Publish deposit product to specific department Creation.",
                            "ID", "Security", payload, CurrentUser);


                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Success",
                            NotificationType = NotificationType.Success,
                            Description = rsp.Content.Message
                        });
                        await onDepartmentDone();

                        await grid.Refresh();

                        _navigationManager.NavigateTo($"/ProductSetup/view/All");
                        //InvokeAsync(StateHasChanged);
                        await GetDepositProducts();
                    }
                }
            }
            catch (Exception ex)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = ex.Message,
                    Description = "Request could not be processed.",
                    NotificationType = NotificationType.Error
                });
            }
        }

        public async Task<bool> ValidateDepartment()
        {
            //if (departmentList.Count() < 1)
            //{
            //    await notificationService.Open(new NotificationConfig()
            //    {
            //        Message = "Error",
            //        Description = "list cannot be less than 1",
            //        NotificationType = NotificationType.Error
            //    });

            //    return false;
            //}
            return true;
        }

        private async Task onPublishToSpecificUser(string Id)
        {
            try
            {
                command = new PublishDepositProductCommand();
                command.ProductId = Id;

                showSpecificDrawer = true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}