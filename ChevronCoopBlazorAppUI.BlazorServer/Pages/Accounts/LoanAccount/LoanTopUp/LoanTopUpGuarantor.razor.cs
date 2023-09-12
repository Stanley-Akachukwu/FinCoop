using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.LoanAccount.LoanTopUp
{
    public partial class LoanTopUpGuarantor
    {
        public LoanTopUpGuarantor()
        {
        }

        private EditContext editContext;
        private Query Query_Combo; // = new Query();

        string notificationText;

        [Parameter]
        public EventCallback<CreateLoanApplicationGuarantorCommand> ModelChanged { get; set; }

        [Parameter]
        public CreateLoanApplicationGuarantorCommand Model { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public bool showPopup { get; set; } = false;

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }
    }
}