using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.MasterData.Charges;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.Entities.MasterData.GlobalCodes;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup.EditDepositProduct
{
    public partial class EditDepositProductBasicInformationComponent
    {
        private FluentValidationValidator? _fluentValidationValidator;

        [Parameter]
        public EventCallback<CreateDepositProductDTO> ModelChanged { get; set; }

        [Parameter]
        public CreateDepositProductDTO Model { get; set; }

        [Parameter]
        public EventCallback<CreateDepositProductDTO> OnBasicChanged { get; set; }

        [Parameter]
        public EventCallback<CreateDepositProductDTO> OnBasicPreviousChanged { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        string DROPDOWN_API_RESOURCE_CHARGE { get; set; }
        string DROPDOWN_API_RESOURCE_CHARGE_WORKFLOW { get; set; }
        string combobox_Currency_res;
        string combobox_Loan_Product_Code_res;
        string combobox_Workflow;
        string combobox_Account_res;
        List<string> tenureUnit { get; set; } = new List<string>();
        List<string> depositProductType { get; set; } = new List<string>();
        List<string> interestMethod { get; set; } = new List<string>();

        [Inject]
        WebConfigHelper Config { get; set; }

        private Query Query_Combo;
        string[] preselectedCharges { get; set; }
        string[] preselectedMemberType { get; set; }

        protected override async Task OnInitializedAsync()
        {
            LoadDropDown();
            if (Model?.ProductCharges?.Count > 0)
            {
                preselectedCharges = Model.ProductCharges.ToArray();
            }
        }

        public async Task OnProceed()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                await OnBasicChanged.InvokeAsync(Model);
            }
        }

        public async Task OnPrevious()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                await OnBasicPreviousChanged.InvokeAsync(Model);
            }
        }

        public void LoadDropDown()
        {
            combobox_Loan_Product_Code_res =
                $"{Config.ODATA_VIEWS_HOST}/{nameof(GlobalCodeMasterView)}?$CodeType=LOAN_PRODUCT_TYPE";
            combobox_Currency_res = $"{Config.ODATA_VIEWS_HOST}/{nameof(CurrencyMasterView)}";
            DROPDOWN_API_RESOURCE_CHARGE_WORKFLOW = $"{Config.ODATA_VIEWS_HOST}/{nameof(ApprovalWorkflowMasterView)}";
            DROPDOWN_API_RESOURCE_CHARGE = $"{Config.ODATA_VIEWS_HOST}/{nameof(ChargeMasterView)}";
            tenureUnit = System.Enum.GetNames(typeof(Tenure)).ToList();
            depositProductType = System.Enum.GetNames(typeof(DepositProductType)).ToList();
            interestMethod = System.Enum.GetNames(typeof(InterestMethod)).ToList();
            combobox_Account_res = $"{Config.ODATA_VIEWS_HOST}/{nameof(CompanyBankAccountMasterView)}";
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        private void ValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue.Count() > 0)
            {
                Model.ProductCharges = new List<string>();
                foreach (var item in selectedValue)
                {
                    Model.ProductCharges.Add(item);
                }
            }
        }
    }
}