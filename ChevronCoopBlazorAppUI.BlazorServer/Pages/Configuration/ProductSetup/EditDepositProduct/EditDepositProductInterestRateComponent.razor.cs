using AntDesign;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges;
using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup.EditDepositProduct
{
    public partial class EditDepositProductInterestRateComponent
    {
        private FluentValidationValidator? _fluentValidationValidator;
        bool showInterestRate { get; set; } = false;
        bool showAddRateDrawer { get; set; } = false;
        bool showPopup = false;

        [Inject]
        BrowserService BrowserService { get; set; }

        [Parameter]
        public EventCallback<GetDepositProductViewModel> ModelChanged { get; set; }

        [Parameter]
        public GetDepositProductViewModel Model { get; set; }

        [Parameter]
        public EventCallback<GetDepositProductViewModel> OnInterestChanged { get; set; }

        [Parameter]
        public EventCallback<GetDepositProductViewModel> OnInterestPreviousChanged { get; set; }

        public List<CreateDepositProductInterestDTO> InterestRates { get; set; }
        public CreateDepositProductInterestDTO CreateInterestRate { get; set; }
        BrowserDimension BrowserDimension;
        bool showInterestGrid { get; set; } = false;

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        bool showInterestConfiguration { get; set; } = false;
        Drawer createDrawer;

        protected override async Task OnInitializedAsync()
        {
            InterestRates = new List<CreateDepositProductInterestDTO>();
            CreateInterestRate = new CreateDepositProductInterestDTO();
            if (Model != null)
            {
                if (Model.IsInterestEnabled)
                {
                    showInterestRate = true;
                    int count = 0;
                    foreach (var item in Model.InterestRanges)
                    {
                        count++;
                        CreateDepositProductInterestDTO createDepositProductInterestDTO =
                            new CreateDepositProductInterestDTO()
                            {
                                Id = count.ToString(),
                                InterestId = item.Id,
                                InterestRate = item.InterestRate,
                                LowerLimit = item.LowerLimit,
                                ProductId = item.ProductId,
                                UpperLimit = item.UpperLimit,
                            };
                        InterestRates.Add(createDepositProductInterestDTO);
                    }
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();
                createDrawer.Width = (int)(BrowserDimension.Width * 0.40);
            }

            StateHasChanged();
        }

        public async Task OnProceed()
        {
            MapToModel();
            await OnInterestChanged.InvokeAsync(Model);
        }

        public async Task OnPrevious()
        {
            MapToModel();
            await OnInterestPreviousChanged.InvokeAsync(Model);
        }

        public void ShowInterestRate()
        {
            showInterestRate = true;
            Model.IsInterestEnabled = true;
            StateHasChanged();
        }

        public void hideInterestRate()
        {
            Model.IsInterestEnabled = false;
            showInterestRate = false;
            //Model.BenefitCode = string.Empty;
            StateHasChanged();
        }

        public async Task OnBlur(Microsoft.AspNetCore.Components.Web.FocusEventArgs args)
        {
            await ModelChanged.InvokeAsync(Model);
        }

        async Task onAddRateDone()
        {
            showAddRateDrawer = false;
        }

        async Task onShowAddInterestRateDone()
        {
            showAddRateDrawer = true;
        }

        public async Task CreateRate()
        {
            if (await _fluentValidationValidator.ValidateAsync())
            {
                int Id = InterestRates.Count + 1;
                CreateInterestRate.Id = Id.ToString();
                InterestRates.Add(CreateInterestRate);
                CreateInterestRate = new CreateDepositProductInterestDTO();
                showInterestConfiguration = true;
                showAddRateDrawer = false;
                StateHasChanged();
            }
        }

        public void DeleteRate(string Id)
        {
            InterestRates.Remove(InterestRates.Where(f => f.Id == Id).FirstOrDefault());

            showAddRateDrawer = false;
            StateHasChanged();
        }

        public async Task OnCancel()
        {
            showAddRateDrawer = false;
        }

        public void MapToModel()
        {
            Model.InterestRanges = new List<DepositProductInterestRangeViewModel>();
            foreach (var interestRange in InterestRates)
            {
                DepositProductInterestRangeViewModel interestRangeViewModel = new DepositProductInterestRangeViewModel()
                {
                    Id = interestRange.InterestId,
                    InterestRate = interestRange.InterestRate,
                    UpperLimit = interestRange.UpperLimit,
                    LowerLimit = interestRange.LowerLimit,
                    ProductId = Model.Id,
                };
                Model.InterestRanges.Add(interestRangeViewModel);
            }
        }
    }
}