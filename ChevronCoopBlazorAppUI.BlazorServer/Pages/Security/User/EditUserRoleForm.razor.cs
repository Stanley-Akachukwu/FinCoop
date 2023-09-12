using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUserRoles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using System.Text.Json;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User
{
    public partial class EditUserRoleForm
    {
        public EditUserRoleForm()
        {
        }

        private EditContext editContext;
        private Query Query_Combo;
        string notificationText;
        public string[] SelectedRoleIds { get; set; }

        [Parameter]
        public UpdateRolesForUser UpdateRolesForUserModel { get; set; }

        [Parameter]
        public UpdateRolesForUser Model { get; set; }

        [Parameter]
        public EventCallback<UpdateRolesForUser> ModelChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public bool showPopup { get; set; } = false;

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        string USER_EXISTING_ROLES => $"{Config.ODATA_VIEWS_HOST}/{nameof(ApplicationRoleMasterView)}";

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject]
        ILogger<EditUserRoleForm> Logger { get; set; }


        public List<ApplicationRoleMasterView> RolePermissions = new List<ApplicationRoleMasterView>();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(USER_EXISTING_ROLES);
            Stream stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsAsync<OdataRoleResponse>();
            if (content != null)
                RolePermissions = content.Roles;
        }

        public async Task OnCancel()
        {
            ShowModal = false;
            await ShowModalChanged.InvokeAsync(ShowModal);
            showPopup = false;
            //_navigationManager.NavigateTo("/security/users", forceLoad: true);
        }

        public async Task OnSave()
        {
            var selectedPermission = SelectedRoleIds;

            var model = new UpdateApplicationUserRoleCommand
            {
                RoleId = Model.RoleId.ToList(),
                UserId = Model.UserId
            };

            Logger.LogInformation($"error->{JsonSerializer.Serialize(model)}");
            var rsp = await DataService
                .Update<UpdateApplicationUserRoleCommand, CommandResult<ApplicationUserRoleViewModel>>(
                    nameof(ApplicationUserRole), model);

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
                    Message = rspContent.Message != null ? rspContent.Message : "Request could not be processed.",
                    Description = msg,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                notificationText = $"Operation was successfull";
                showPopup = true;
            }
        }

        public class OdataRoleResponse
        {
            [Newtonsoft.Json.JsonProperty("@odata.context")]
            public string Odatacontext { get; set; }

            [Newtonsoft.Json.JsonProperty("value")]
            public List<ApplicationRoleMasterView> Roles { get; set; }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
            }
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
    }

    public class UpdateRolesForUser
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string[] RoleId { get; set; }
    }
}