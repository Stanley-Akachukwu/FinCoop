using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup.EditLoanProduct
{
    public partial class EditTargetSetUpViewComponent
    {
        private FluentValidationValidator? _fluentValidationValidator;
        bool showBenefitCode { get; set; } = false;

        [Parameter]
        public EventCallback<CreateLoanProductTargetSetupDTO> ModelChanged { get; set; }

        [Parameter]
        public CreateLoanProductTargetSetupDTO Model { get; set; }

        [Parameter]
        public EventCallback<CreateLoanProductTargetSetupDTO> OnTargetChanged { get; set; }

        [Parameter]
        public EventCallback<CreateLoanProductTargetSetupDTO> OnTargetPreviousChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Model != null)
            {
                if (Model.IsTargetLoan)
                    showBenefitCode = true;
            }
        }

        public async Task OnProceed()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                await OnTargetChanged.InvokeAsync(Model);
            }
        }

        public async Task OnPrevious()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                await OnTargetPreviousChanged.InvokeAsync(Model);
            }
        }

        public void ShowBenefitCode()
        {
            showBenefitCode = true;
            Model.IsTargetLoan = true;
            StateHasChanged();
        }

        public void hideBenefitCode()
        {
            Model.IsTargetLoan = false;
            showBenefitCode = false;
            Model.BenefitCode = string.Empty;
            StateHasChanged();
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }
    }
}