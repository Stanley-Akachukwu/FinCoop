using AP.ChevronCoop.Entities.Security.AuditTrails;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Audit
{
    public partial class AuditViewForm2
    {
        [Parameter]
        public AuditTrailMasterViewResult Model { get; set; }

        [Parameter]
        public EventCallback<AuditTrailMasterViewResult> ModelChanged { get; set; }

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

            //if(Model==null)
            //{
            //	Model = new AuditTrailMasterViewResult();
            //}
        }

        public async Task OnCancel()
        {
            //Refresh fix
            NavigationManager.NavigateTo("/Audit/Audit-trail-2", true);

            //ShowModal = false;
            //await ShowModalChanged.InvokeAsync(ShowModal);

            //Model = new AuditTrailMasterView();
            //await ModelChanged.InvokeAsync(Model);

            //showPopup = false;
        }
    }
}