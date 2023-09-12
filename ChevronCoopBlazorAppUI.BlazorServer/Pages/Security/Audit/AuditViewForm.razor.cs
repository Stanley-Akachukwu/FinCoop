using AntDesign;
using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.AuditTrails;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Roles;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Audit
{
    public partial class AuditViewForm
    {
        [Parameter]
        public AuditTrailMasterView Model { get; set; }

        [Parameter]
        public EventCallback<AuditTrailMasterView> ModelChanged { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public string ActiveTabKey { get; set; }

        [Parameter]
        public EventCallback<string> ActiveTabKeyChanged { get; set; }

        bool showPopup = false;

        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();


            var data = Model;
        }

        public async Task OnCancel()
        {
            NavigationManager.NavigateTo("/audittrail/list", true);
        }
    }
}