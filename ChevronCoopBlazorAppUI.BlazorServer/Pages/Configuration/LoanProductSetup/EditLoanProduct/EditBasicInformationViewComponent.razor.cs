using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using AP.ChevronCoop.Entities.MasterData.Charges;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.Entities.MasterData.GlobalCodes;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup.EditLoanProduct
{
    public partial class EditBasicInformationViewComponent
    {
        private FluentValidationValidator? _fluentValidationValidator;

        [Parameter]
        public EventCallback<CreateLoanProductBasicInfoDTO> ModelChanged { get; set; }

        [Parameter]
        public CreateLoanProductBasicInfoDTO Model { get; set; }

        [Parameter]
        public EventCallback<CreateLoanProductBasicInfoDTO> OnBasicChanged { get; set; }

        [Parameter]
        public EventCallback<CreateLoanProductBasicInfoDTO> OnBasicPreviousChanged { get; set; }

        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        string DROPDOWN_API_RESOURCE_CHARGE { get; set; }
        string DROPDOWN_API_RESOURCE_CHARGE_WORKFLOW { get; set; }
        string combobox_Currency_res;
        string combobox_Loan_Product_Code_res;
        string combobox_Workflow;
        List<string> tenureUnit { get; set; } = new List<string>();
        List<string> memberType { get; set; } = new List<string>();
        List<string> depositProductType { get; set; } = new List<string>();
        List<string> interestMethod { get; set; } = new List<string>();
        List<string> loanProductType { get; set; } = new List<string>();
        List<string> BindMemberType { get; set; } = new List<string>();
        List<string> daysInYear { get; set; } = new List<string>();
        List<string> interestCalculationMethod { get; set; } = new List<string>();

        [Inject]
        WebConfigHelper Config { get; set; }

        private Query Query_Combo;
        string[] preselectedAdminCharges { get; set; }
        string[] preselectedMemberType { get; set; }
        string DROPDOWN_API_RESOURCE_COMPANY_ACCOUNT { get; set; }

        protected override async Task OnInitializedAsync()
        {
            LoadDropDown();
            if (Model?.AdminCharges?.Count > 0)
            {
                preselectedAdminCharges = Model.AdminCharges.ToArray();
            }

            if (Model?.MemberTypes?.Count > 0)
            {
                preselectedMemberType = Model.MemberTypes.ToArray();
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
            memberType = System.Enum.GetNames(typeof(MemberType)).ToList();
            memberType.Remove("ADMIN");
            memberType.Remove("MFB");
            depositProductType = System.Enum.GetNames(typeof(DepositProductType)).ToList();
            interestMethod = System.Enum.GetNames(typeof(InterestMethod)).ToList();
            loanProductType = System.Enum.GetNames(typeof(LoanProductType)).ToList();
            DROPDOWN_API_RESOURCE_COMPANY_ACCOUNT = $"{Config.ODATA_VIEWS_HOST}/{nameof(CompanyBankAccountMasterView)}";
            daysInYear = System.Enum.GetNames(typeof(DaysInYear)).ToList();
            interestCalculationMethod = System.Enum.GetNames(typeof(InterestCalculationMethod)).ToList();
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
                Model.MemberTypes = new List<string>();
                foreach (var item in selectedValue)
                {
                    Model.MemberTypes.Add(item);
                }
            }
        }

        private void AdminChargeValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue.Count() > 0)
            {
                Model.AdminCharges = new List<string>();
                foreach (var item in selectedValue)
                {
                    Model.AdminCharges.Add(item);
                }
            }
        }
    }
}