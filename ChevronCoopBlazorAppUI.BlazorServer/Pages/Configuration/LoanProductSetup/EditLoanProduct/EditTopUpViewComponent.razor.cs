using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.MasterData.Charges;
using AP.ChevronCoop.Entities.MasterData.Currencies;
using AP.ChevronCoop.Entities.MasterData.GlobalCodes;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.LoanProductSetup.EditLoanProduct
{
    public partial class EditTopUpViewComponent
    {
        private FluentValidationValidator? _fluentValidationValidator;

        [Parameter]
        public EventCallback<CreateLoanProductTopUpDTO> ModelChanged { get; set; }

        [Parameter]
        public CreateLoanProductTopUpDTO Model { get; set; }

        [Parameter]
        public EventCallback<CreateLoanProductTopUpDTO> OnTopUpSetChanged { get; set; }

        [Parameter]
        public EventCallback<CreateLoanProductTopUpDTO> OnTopUpSetPreviousChanged { get; set; }

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

        [Inject]
        WebConfigHelper Config { get; set; }

        private Query Query_Combo;
        string[] preselectedCharges { get; set; }
        bool showTopUpInfoForm { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            LoadDropDown();
            if (Model != null)
            {
                if (Model.EnableTopUpCharges)
                    showTopUpInfoForm = true;
                if (Model?.TopUpCharges?.Count > 0)
                {
                    preselectedCharges = Model.TopUpCharges.ToArray();
                }
            }
        }

        public async Task OnProceed()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                await OnTopUpSetChanged.InvokeAsync(Model);
            }
        }

        public async Task OnPrevious()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                await OnTopUpSetPreviousChanged.InvokeAsync(Model);
            }
        }

        private void TopupValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue.Count() > 0)
            {
                Model.TopUpCharges = new List<string>();
                foreach (var item in selectedValue)
                {
                    Model.TopUpCharges.Add(item);
                }
            }
        }

        public void ShowTopUpForm()
        {
            Model.EnableTopUpCharges = true;
            showTopUpInfoForm = true;
            StateHasChanged();
        }

        public void HideTopUpForm()
        {
            showTopUpInfoForm = false;
            Model.EnableTopUpCharges = false;
            Model.TopUpCharges = new List<string>();
            StateHasChanged();
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
            depositProductType = System.Enum.GetNames(typeof(DepositProductType)).ToList();
            interestMethod = System.Enum.GetNames(typeof(InterestMethod)).ToList();
        }
    }
}