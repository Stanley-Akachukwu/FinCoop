using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.MasterData.Charges;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup.EditLoanProduct
{
    public partial class EditWhenDueViewComponent
    {
        private FluentValidationValidator? _fluentValidationValidator;
        bool showEnableChargeWaitingForm { get; set; } = false;
        bool showEnableWaitingForm { get; set; } = false;

        [Parameter]
        public EventCallback<CreateLoanProductWhenDueDTO> ModelChanged { get; set; }

        [Parameter]
        public CreateLoanProductWhenDueDTO Model { get; set; }

        [Parameter]
        public EventCallback<CreateLoanProductWhenDueDTO> OnWhenDueChanged { get; set; }

        [Parameter]
        public EventCallback<CreateLoanProductWhenDueDTO> OnWhenDuePreviousChanged { get; set; }

        string[] preselectedPeriodCharges { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        string DROPDOWN_API_RESOURCE_CHARGE { get; set; }
        string combobox_Currency_res;
        List<string> tenureUnit { get; set; } = new List<string>();
        List<string> interestMethod { get; set; } = new List<string>();

        [Inject]
        WebConfigHelper Config { get; set; }

        private Query Query_Combo;

        protected override async Task OnInitializedAsync()
        {
            LoadDropDown();
            if (Model != null)
            {
                if (Model.EnableWaitingPeriod)
                    showEnableWaitingForm = true;
                if (Model?.WaitingPeriodCharges?.Count > 0)
                {
                    preselectedPeriodCharges = Model.WaitingPeriodCharges.ToArray();
                }
            }
        }

        public async Task OnProceed()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                await OnWhenDueChanged.InvokeAsync(Model);
            }
        }

        public async Task OnPrevious()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                await OnWhenDuePreviousChanged.InvokeAsync(Model);
            }
        }

        public void ShowWaitingChargeForm()
        {
            Model.EnableWaitingPeriodCharge = true;
            showEnableChargeWaitingForm = true;
            StateHasChanged();
        }

        public void HideWaitingChargeForm()
        {
            showEnableChargeWaitingForm = false;
            Model.WaitingPeriodCharges = new List<string>();
            Model.EnableWaitingPeriodCharge = false;
            StateHasChanged();
        }

        public void ShowWaitingForm()
        {
            Model.EnableWaitingPeriod = true;
            showEnableWaitingForm = true;
            StateHasChanged();
        }

        public void HideWaitingForm()
        {
            showEnableWaitingForm = false;
            Model.EnableWaitingPeriod = false;
            Model.WaitingPeriodUnit = string.Empty;
            Model.WaitingPeriodValue = 0;
            StateHasChanged();
        }

        public void LoadDropDown()
        {
            combobox_Currency_res = $"{Config.ODATA_VIEWS_HOST}/{nameof(CurrencyMasterView)}";
            DROPDOWN_API_RESOURCE_CHARGE = $"{Config.ODATA_VIEWS_HOST}/{nameof(ChargeMasterView)}";
            tenureUnit = System.Enum.GetNames(typeof(Tenure)).ToList();
            interestMethod = System.Enum.GetNames(typeof(InterestMethod)).ToList();
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        private void WaitingPeriodValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue.Count() > 0)
            {
                Model.WaitingPeriodCharges = new List<string>();
                foreach (var item in selectedValue)
                {
                    Model.WaitingPeriodCharges.Add(item);
                }
            }
        }
    }
}