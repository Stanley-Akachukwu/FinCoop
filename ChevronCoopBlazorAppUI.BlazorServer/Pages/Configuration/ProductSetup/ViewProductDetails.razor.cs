using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Services.AggregateService.Interface;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup
{
    public partial class ViewProductDetails
    {
        [Parameter]
        public string Id { get; set; }

        public DepositProductMasterView Model { get; set; }
       
        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        private IMasterViews _MasterView { get; set; }

        [Inject]
        private IAggregationService _AggregationService { get; set; }

        public int TotalAccounts { get; set; }

        public decimal TotalMembersBalance { get; set; }

        public List<DepositProductMasterView> _DepositProductMasterView { get; set; } = new List<DepositProductMasterView>();
        [Inject]
        NavigationManager _navigationManager { get; set; }

        public string SelectedMenu =
           "items-start bg-blue-200 hover:bg-blue-200 cursor-pointer justify-center p-4 flex flex-col xl:block 2xl:flex sm:space-x-4 xl:space-x-0 2xl:space-x-4";
        public string DefaultSelection = "items-start hover:bg-blue-200 cursor-pointer justify-center p-4 flex flex-col xl:block 2xl:flex sm:space-x-4 xl:space-x-0 2xl:space-x-4";

        public string DefaultMenu1 { get; set; }
        public string DefaultMenu2 { get; set; }
        public string DefaultMenu3 { get; set; }

        public string DefaultMenu4 { get; set; }
        public string DefaultMenu5 { get; set; }

        public string MenuToShow { get; set; } = "Transaction";


        protected override async Task OnInitializedAsync()
        {
            DefaultMenu1 = SelectedMenu;
            MenuToShow = "dashboard";
            await GetProductDetails();
        }

        public async Task GetProductDetails()
        {
            Model = new DepositProductMasterView();
            var rsp = await DataService.GetValue<List<DepositProductMasterView>>(
               nameof(DepositProductMasterView), "id", Id);
            if (rsp.IsSuccessStatusCode)
            {
                _DepositProductMasterView = await _MasterView.GetCustomMasterViewEntityWithFilterAndAllFields<DepositProductMasterView>(nameof(DepositProductMasterView), "id", Id);
                Model = JsonSerializer.Deserialize<List<DepositProductMasterView>>(rsp.Content.ToJson()).FirstOrDefault();

                TotalAccounts = await _AggregationService.TotalRowsByAccountType<DepositAccountsMasterView>(nameof(DepositAccountsMasterView), "DepositProductId", Id, "LedgerBalance", "Status", "APPROVED");

                TotalMembersBalance = await _AggregationService.TotalBalanceByAccountType(nameof(DepositAccountsMasterView), "DepositProductId", Id, "LedgerBalance", "Status", "APPROVED");

            }
        
        }


        public async Task ResetSideButton()
        {
            DefaultMenu1 = DefaultMenu2 =
                DefaultMenu3 = DefaultMenu4 = DefaultMenu5 = 
                                "items-start hover:bg-blue-200 cursor-pointer justify-center p-4 flex flex-col xl:block 2xl:flex sm:space-x-4 xl:space-x-0 2xl:space-x-4";
        }

        public async void ChangeMenu(string value, string buttonName)
        {
            ResetSideButton();
            MenuToShow = value;

            switch (buttonName)
            {
                case "DefaultMenu1":
                    DefaultMenu1 = SelectedMenu;
                    break;
                case "DefaultMenu2":
                    DefaultMenu2 = SelectedMenu;
                    break;
                case "DefaultMenu3":
                    DefaultMenu3 = SelectedMenu;
                    break;
                case "DefaultMenu4":
                    DefaultMenu4 = SelectedMenu;
                    break;
                case "DefaultMenu5":
                    DefaultMenu5 = SelectedMenu;
                    break;
               
            }
            await InvokeAsync(StateHasChanged);

        }


    }
}
