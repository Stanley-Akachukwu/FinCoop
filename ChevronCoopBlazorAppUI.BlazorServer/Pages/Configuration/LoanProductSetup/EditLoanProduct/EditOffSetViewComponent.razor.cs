using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
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
    public partial class EditOffSetViewComponent
    {
        private FluentValidationValidator? _fluentValidationValidator;
        bool showOffset { get; set; } = false;
        bool showOffsetForm { get; set; } = false;

        [Parameter]
        public EventCallback<CreateLoanProductOffSetInfoDTO> ModelChanged { get; set; }

        [Parameter]
        public CreateLoanProductOffSetInfoDTO Model { get; set; }

        [Parameter]
        public EventCallback<CreateLoanProductOffSetInfoDTO> OnOffSetChanged { get; set; }

        [Parameter]
        public EventCallback<CreateLoanProductOffSetInfoDTO> OnOffSetPreviousChanged { get; set; }

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
        string[] preselectedSaving { get; set; }
        string[] preselectedWaiving { get; set; }
        bool showOffsetAdmin { get; set; } = false;
        bool showAdminCharge { get; set; } = false;
        string DROPDOWN_API_RESOURCE_DEPOSIT_PRODUCT { get; set; }

        protected override async Task OnInitializedAsync()
        {
            LoadDropDown();
            if (Model != null)
            {
                if (Model.AllowedOffsetType != null)
                    showOffsetForm = true;
                if (Model?.SavingsOffSets?.Count > 0)
                {
                    preselectedSaving = Model.SavingsOffSets.ToArray();
                }

                if (Model?.WaivedCharges?.Count > 0)
                {
                    preselectedWaiving = Model.WaivedCharges.ToArray();
                }

                if (Model.AllowedOffsetType == AllowedOffsetType.NONE.ToString())
                    hideOffsetForm();
            }
        }

        public async Task OnProceed()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                await OnOffSetChanged.InvokeAsync(Model);
            }
        }

        public async Task OnPrevious()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                await OnOffSetPreviousChanged.InvokeAsync(Model);
            }
        }

        public void ShowOffsetForm()
        {
            showOffsetForm = true;
            Model.AllowedOffsetType = AllowedOffsetType.NONE.ToString();
            StateHasChanged();
        }

        public void ShowOffsetForm_Partial()
        {
            showOffsetForm = true;
            Model.AllowedOffsetType = AllowedOffsetType.PARTIAL.ToString();
            StateHasChanged();
        }

        public void ShowOffsetForm_Full()
        {
            showOffsetForm = true;
            Model.AllowedOffsetType = AllowedOffsetType.FULL.ToString();
            StateHasChanged();
        }

        public void EnableOffsetSaving()
        {
            Model.EnableSavingsOffset = true;
            StateHasChanged();
        }

        public void DisableOffsetSaving()
        {
            Model.EnableSavingsOffset = false;
            StateHasChanged();
        }

        public void EnableOffsetWaiver()
        {
            Model.EnableChargeWaiver = true;
            StateHasChanged();
        }

        public void DisableOffsetWaiver()
        {
            Model.EnableChargeWaiver = false;
            StateHasChanged();
        }


        public void hideOffsetForm()
        {
            showOffsetForm = false;
            Model.AllowedOffsetType = AllowedOffsetType.NONE.ToString();
            Model.OffsetPeriodValue = 0;
            Model.OffsetPeriodUnit = Tenure.NONE.ToString();
            Model.EnableSavingsOffset = false;
            Model.EnableChargeWaiver = false;
            Model.EnableSavingsOffset = false;
            StateHasChanged();
        }

        private void SavingValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue.Count() > 0)
            {
                Model.SavingsOffSets = new List<string>();
                foreach (var item in selectedValue)
                {
                    Model.SavingsOffSets.Add(item);
                }
            }
        }

        private void WaiverValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue.Count() > 0)
            {
                Model.WaivedCharges = new List<string>();
                foreach (var item in selectedValue)
                {
                    Model.WaivedCharges.Add(item);
                }
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
            depositProductType = System.Enum.GetNames(typeof(DepositProductType)).ToList();
            interestMethod = System.Enum.GetNames(typeof(InterestMethod)).ToList();
            DROPDOWN_API_RESOURCE_DEPOSIT_PRODUCT = $"{Config.ODATA_VIEWS_HOST}/{nameof(DepositProductMasterView)}";
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        public void EnableOffsetAdminCharges()
        {
            Model.EnableOffSetAdminCharges = true;
            showOffsetAdmin = true;
            StateHasChanged();
        }

        public void DisableOffsetAdminCharges()
        {
            Model.EnableOffSetAdminCharges = false;
            showOffsetAdmin = false;
            StateHasChanged();
        }

        private void OffsetAdminValueChangeHandler(MultiSelectChangeEventArgs<string[]> args)
        {
            var selectedValue = args.Value;
            if (selectedValue.Count() > 0)
            {
                Model.OffSetsAdminCharges = new List<string>();
                foreach (var item in selectedValue)
                {
                    Model.OffSetsAdminCharges.Add(item);
                }
            }
        }
    }
}