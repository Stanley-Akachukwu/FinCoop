using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.Permissions;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Roles
{
    public partial class RoleEditForm
    {
        [Inject]

        ProtectedSessionStorage sessionStorage { get; set; }

        private Query Query_Combo;

        string notificationText;
        bool showPopup = false;
        bool reloadGrid = false;

        Tabs editTab;

        [Parameter]
        public string ActiveTabKey { get; set; }

        [Parameter]
        public EventCallback<string> ActiveTabKeyChanged { get; set; }


        [Parameter]
        public UpdateApplicationRoleCommand Model { get; set; }


        [Parameter]
        public EventCallback<UpdateApplicationRoleCommand> ModelChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Inject]

        NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        ModalService modalService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        ILogger<RoleEditForm> Logger { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }

        private WhereFilter baseFilter;

        [Parameter]
        public string[] PreviouslySelectedPermissionIds { get; set; }

        public string[] SelectedPermissionIds { get; set; }

        [Parameter]
        public EventCallback<string[]> PreviouslySelectedPermissionIdsChanged { get; set; }


        ClaimsPrincipal CurrentUser { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        private IDictionary<string, string> HeaderData = new Dictionary<string, string>();

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject] IConfiguration _configuration { get; set; }
        [Inject] IClientAuditService _auditLogService { get; set; }

        private string bearToken { get; set; }
        public List<PermissionMasterView> RolePermissions = new List<PermissionMasterView>();

        protected override async Task OnInitializedAsync()
        {
            await GetCurrentUser();

            await base.OnInitializedAsync();
            DROPDOWN_API_RESOURCE = $"{Config.ODATA_VIEWS_HOST}/{nameof(PermissionMasterView)}";
            Query_Combo = new Query();

            var data = Model;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(DROPDOWN_API_RESOURCE);
            Stream stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsAsync<OdataPermissionResponse>();
            if (content != null)
                RolePermissions = content.permissions;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                bearToken = CurrentUser.FindFirstValue(ClaimTypes.Hash);
                if (string.IsNullOrEmpty(bearToken))
                {
                    _navigationManager.NavigateTo("/Identity/Account/Logout", forceLoad: true);
                }

                HeaderData.Add("Bearer", bearToken);
            }

            if (PreviouslySelectedPermissionIds != null && PreviouslySelectedPermissionIds.Length > 0)
            {
                if (SelectedPermissionIds == null || SelectedPermissionIds.Length == 0)
                    SelectedPermissionIds = PreviouslySelectedPermissionIds;
            }
        }

        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
        }

        public async Task OnSave()
        {
            if (SelectedPermissionIds != null && SelectedPermissionIds.Any())
            {
                Model.PermissionIds = new List<string>();
                Model.PermissionIds = SelectedPermissionIds.ToList();
            }

            var rsp = await DataService.Update<UpdateApplicationRoleCommand, CommandResult<ApplicationRoleViewModel>>(
                nameof(ApplicationRole), Model);


            if (!rsp.IsSuccessStatusCode)
            {
                var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                var msg = rspContent?.Response;
                if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                {
                    msg = rspContent.ValidationErrors[0].Error;
                }


                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = msg,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                var payload = JsonSerializer.Serialize(Model);
                await _auditLogService.LogAudit("Application Role Update.", $"Updated application role", "Security",
                    payload, CurrentUser);
                notificationText = $"Record successfully updated, Id->{Model.Id}";
                showPopup = true;
                reloadGrid = true;
                await OnCancel();
            }
        }

        string DROPDOWN_API_RESOURCE { get; set; }

        private void ValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue.Count() > 0)
            {
                Model.PermissionIds = new List<string>();
                Model.PermissionIds.AddRange(selectedValue);
                SelectedPermissionIds = new string[] { };
                SelectedPermissionIds = selectedValue;
            }
        }


        public async Task OnCancel()
        {
            NavigationManager.NavigateTo("/security/roles", true);
            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);

            Model = new UpdateApplicationRoleCommand();
            await ModelChanged.InvokeAsync(Model);
            SelectedPermissionIds = new string[] { };
            PreviouslySelectedPermissionIds = new string[] { };
            showPopup = false;
        }


        private void OnFileUploaded(UploadChangeEventArgs args)
        {
        }


        public async Task OnInput(ChangeEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnChange(ChangeEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        private async Task onFocus(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public class OdataPermissionResponse
        {
            [Newtonsoft.Json.JsonProperty("@odata.context")]
            public string Odatacontext { get; set; }

            [Newtonsoft.Json.JsonProperty("value")]
            public List<PermissionMasterView> permissions { get; set; }
        }
    }
}