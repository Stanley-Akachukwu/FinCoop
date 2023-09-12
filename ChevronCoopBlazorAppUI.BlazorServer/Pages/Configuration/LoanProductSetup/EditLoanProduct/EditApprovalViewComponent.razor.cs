using AntDesign;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup.EditLoanProduct
{
    public partial class EditApprovalViewComponent
    {
        [Parameter]
        public EventCallback<string> ModelChanged { get; set; }

        [Parameter]
        public string Model { get; set; }

        [Parameter]
        public EventCallback<string> OnApprovalChanged { get; set; }

        [Parameter]
        public EventCallback<string> OnApprovalSubmitChanged { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        string DROPDOWN_API_RESOURCE_CHARGE_WORKFLOW { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        private Query Query_Combo;

        [Inject]
        NotificationService notificationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            LoadDropDown();
        }

        public async Task OnPrevious()
        {
            await OnApprovalChanged.InvokeAsync(Model);
        }

        public async Task OnSubmit()
        {
            if (!string.IsNullOrEmpty(Model))
                await OnApprovalSubmitChanged.InvokeAsync(Model);
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = "Please Select Approval workflow to continue",
                    NotificationType = NotificationType.Error
                });
                StateHasChanged();
            }
        }

        public void LoadDropDown()
        {
            DROPDOWN_API_RESOURCE_CHARGE_WORKFLOW = $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalWorkflowMasterView)}";
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }
    }
}