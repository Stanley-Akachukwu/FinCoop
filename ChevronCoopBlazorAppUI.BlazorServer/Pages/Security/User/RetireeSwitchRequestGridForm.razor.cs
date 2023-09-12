namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User
{
    //public partial class RetireeSwitchRequestGridForm
    //{
    //	string notificationText;
    //	bool showPopup = false;
    //	public int RowCounter = 0;
    //	[Inject]
    //	ILogger<RetireeSwitchRequestGridForm> Logger { get; set; }

    //	[Inject]
    //	BrowserService BrowserService { get; set; }

    //	[Inject]
    //	IEntityDataService DataService { get; set; }

    //	[Inject]
    //	AntDesign.ModalService modalService { get; set; }


    //	[Inject]
    //	protected IJSRuntime jsRuntime { get; set; }

    //	[Inject]
    //	AutoMapper.IMapper Mapper { get; set; }

    //	[Inject]
    //	WebConfigHelper Config { get; set; }


    //	string searchText;


    //	SfGrid<RetireeSwitchMasterView> grid;
    //	string GRID_API_RESOURCE => $"{Config.ODATA_VIEWS_HOST}/{nameof(RetireeSwitchMasterView)}";


    //	private Query QueryGrid;// = new Query();

    //	private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();

    //	BrowserDimension BrowserDimension;
    //	string ErrorDetailMsg = "";
    //	ErrorDetailMsg errors;
    //	string QueryParameter;
    //	[Inject]

    //	NavigationManager NavigationManager { get; set; }

    //	[CascadingParameter]
    //	private Task<AuthenticationState> authenticationStateTask { get; set; }
    //	[Inject]
    //	AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    //	string LoggedInUserId = string.Empty;
    //	[Inject]
    //	NotificationService notificationService { get; set; }
    //	private RetireeSwitchMasterView[] ModelList { get; set; }
    //	protected override async Task OnAfterRenderAsync(bool firstRender)
    //	{
    //		base.OnAfterRender(firstRender);

    //		if (firstRender)
    //		{
    //			BrowserDimension = await BrowserService.GetDimensions();
    //			await OnRefresh();

    //		}
    //		//StateHasChanged();

    //		await jsRuntime.InvokeVoidAsync("initFlowbiteJS");

    //	}


    //	protected override async Task OnInitializedAsync()
    //	{
    //		//Console.WriteLine($"API HOST->{Config.ODATA_VIEWS_HOST}, ODATA HOST-> {GRID_API_RESOURCE}");
    //		//QueryGrid = new Query();
    //		await GetRetireeSwitchMasterView();

    //	}


    //	public void ActionCompletedHandler(ActionEventArgs<RetireeSwitchMasterView> args)
    //	{
    //		jsRuntime.InvokeVoidAsync("initFlowbiteJS");
    //	}
    //	public void CellInfoHandler(QueryCellInfoEventArgs<RetireeSwitchMasterView> Args)
    //	{

    //		Args.Data.Status = Args.Data.Status != null ? Args.Data.Status.Replace("_", " ") : "";
    //	}
    //	public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
    //	{
    //		//this.ErrorDetailMsg = args.Error.ToString();
    //		this.ErrorDetailMsg = "No response for your request. Please refresh your page!";
    //		StateHasChanged();
    //	}


    //	private async Task OnRefresh()
    //	{

    //		searchText = string.Empty;
    //		//QueryGrid = new Query().Where(kycFilter);
    //		grid.Refresh();
    //		await GetRetireeSwitchMasterView();
    //		await Task.CompletedTask;

    //	}

    //	private async Task OnSearchEnter(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
    //	{

    //		if (e.Key == "Enter")
    //		{

    //			if (!string.IsNullOrWhiteSpace(searchText))
    //				await OnSearch();
    //			else
    //				await OnRefresh();


    //		}

    //	}
    //	private async Task onChangeStatus(RetireeSwitchMasterView row, string status)
    //	{
    //		var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
    //		var user = authState.User;

    //		if (user.Identity.IsAuthenticated)
    //		{
    //			LoggedInUserId = user.FindFirstValue(ClaimTypes.Sid);
    //			if (!string.IsNullOrEmpty(LoggedInUserId))
    //			{

    //				ApproveRetireeSwitchCommand command = new ApproveRetireeSwitchCommand()
    //				{
    //					MemberProfileId = row.MemberProfileId,
    //					ApprovedBy = LoggedInUserId,
    //					RetireeSwitchEntityId = row.Id,
    //					Comment = "good"
    //				};
    //				var j = JsonSerializer.Serialize(command);
    //				Logger.LogInformation($"payload->{j}");
    //				var rsp = await DataService.ProcessRequest<ApproveRetireeSwitchCommand, CommandResult<RetireeSwitchViewModel>>(
    //	   "RetireeSwitches", "approve", command);

    //				if (!rsp.IsSuccessStatusCode)
    //				{

    //					var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

    //					var msg = rspContent?.Response;
    //					if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
    //					{
    //						msg = rspContent.ValidationErrors[0].Error;
    //					}
    //					if (msg == null && rspContent?.Message != null)
    //						msg = rspContent.Message;

    //					await notificationService.Open(new NotificationConfig()
    //					{
    //						Message = "Error",
    //						Description = msg,
    //						NotificationType = NotificationType.Error
    //					});
    //				}
    //				else
    //				{
    //					notificationText = $"Operation waas successfull";
    //					showPopup = true;
    //					NavigationManager.NavigateTo("/approval/retirees", true);
    //				}
    //			}
    //		}

    //	}
    //	private async Task OnSearch()
    //	{

    //		if (!string.IsNullOrWhiteSpace(searchText))
    //		{

    //			WhereFilter actionFilter = new WhereFilter
    //			{
    //				Field = nameof(AuditTrailMasterView.Action),
    //				Operator = "contains",
    //				value = searchText
    //			};

    //			WhereFilter moduleFilter = new WhereFilter
    //			{
    //				Field = nameof(AuditTrailMasterView.Module),
    //				Operator = "contains",
    //				value = searchText
    //			};
    //			WhereFilter emailFilter = new WhereFilter
    //			{
    //				Field = nameof(AuditTrailMasterView.ApplicationUserId_Email),
    //				Operator = "contains",
    //				value = searchText
    //			};
    //			WhereFilter ipAddressFilter = new WhereFilter
    //			{
    //				Field = nameof(AuditTrailMasterView.IPAddress),
    //				Operator = "contains",
    //				value = searchText
    //			};
    //			WhereFilter fullTextFilter = new WhereFilter
    //			{
    //				Field = nameof(AuditTrailMasterView.FullText),
    //				Operator = "contains",
    //				value = searchText
    //			};
    //			WhereFilter statusFilter = new WhereFilter
    //			{
    //				Field = nameof(AuditTrailMasterView.Action),
    //				Operator = "contains",
    //				value = searchText
    //			};
    //			QueryGrid = new Query().Where(ipAddressFilter.Or(fullTextFilter).Or(statusFilter).Or(moduleFilter).Or(emailFilter).Or(actionFilter));

    //		}
    //		else
    //		{
    //			await OnRefresh();
    //		}

    //	}

    //	private async Task OnClearSearch()
    //	{

    //		if (!string.IsNullOrWhiteSpace(searchText))
    //		{
    //			await OnRefresh();

    //		}

    //	}
    //	private async Task GetRetireeSwitchMasterView()
    //	{
    //		var rsp = await DataService.GetMasterView<List<RetireeSwitchMasterView>>(nameof(RetireeSwitchMasterView));

    //		if (rsp.IsSuccessStatusCode)
    //		{
    //			List<RetireeSwitchMasterView> rspResponse = JsonSerializer.Deserialize<List<RetireeSwitchMasterView>>(rsp.Content.ToJson());
    //			if (rspResponse != null)
    //			{
    //				if (rspResponse.Any())
    //				{
    //					ModelList = rspResponse.ToArray();

    //				}
    //			}
    //		}
    //		else
    //		{

    //			var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

    //			var msg = rspContent?.Response;
    //			if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
    //			{
    //				msg = rspContent.ValidationErrors[0].Error;
    //			}

    //		}
    //	}

    //}
}