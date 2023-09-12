using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup.EditLoanProduct
{
    public partial class EditGuarantorInfoViewComponent
    {
        private FluentValidationValidator? _fluentValidationValidator;

        [Parameter]
        public EventCallback<CreateLoanProductGuarantorDTO> ModelChanged { get; set; }

        [Parameter]
        public CreateLoanProductGuarantorDTO Model { get; set; }

        [Parameter]
        public EventCallback<CreateLoanProductGuarantorDTO> OnGuarantorChanged { get; set; }

        [Parameter]
        public EventCallback<CreateLoanProductGuarantorDTO> OnGuarantorPreviousChanged { get; set; }

        bool showGuarantorInfoForm { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            if (Model != null)
            {
                if (Model.IsGuarantorRequired)
                    showGuarantorInfoForm = true;
            }
        }

        public async Task OnProceed()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                await OnGuarantorChanged.InvokeAsync(Model);
            }
        }

        public async Task OnPrevious()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                await OnGuarantorPreviousChanged.InvokeAsync(Model);
            }
        }

        public void ShowGuarantorForm()
        {
            Model.IsGuarantorRequired = true;
            showGuarantorInfoForm = true;
            StateHasChanged();
        }

        public void HideGuarantorForm()
        {
            showGuarantorInfoForm = false;
            Model.EmployeeGuarantorCount = 0;
            Model.IsGuarantorRequired = false;
            Model.GuarantorMinYear = 0;
            Model.NonEmployeeGuarantorCount = 0;
            Model.GuarantorAmountLimit = 0;
            StateHasChanged();
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }
    }
}