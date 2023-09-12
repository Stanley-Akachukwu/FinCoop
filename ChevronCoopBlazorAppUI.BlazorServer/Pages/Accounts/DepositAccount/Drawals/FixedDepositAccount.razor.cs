using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data.DepositApplication;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Configuration.ProductSetup;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;
using System.Net.Http.Headers;
using System.Security.Claims;
using JsonSerializer = System.Text.Json.JsonSerializer;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.SharedComponents;
using Microsoft.JSInterop;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AP.ChevronCoop.Entities.Deposits.SpecialDeposits.SpecialDepositAccountApplications;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.Drawals
{
    public partial class FixedDepositAccount
    {
        private IJSRuntime jsRuntime;

        private FluentValidationValidator? _fluentValidationValidator;
        public MemberProfileMasterView Model { get; set; }

        public SpecialDepositApplicationViewModel SpecialDepositModel { get; set; }

        public CreateSpecialDepositApplicationCommand Command { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        public List<DepositProductMasterView> DepositProductMasterViews { get; set; }

        public List<DepositProduct> DepositProductsList { get; set; }

        public List<ProductSetupGrid> ProductSetupGrids { get; set; }

        string ApplicationUserId { get; set; }

        public MemberProfileMasterView MemberProfile;



        private string FundingSourceChecked = "None";

        public string ShowBank = "hidden";

        public bool drawalHidden = true;

        bool showDocumentError { get; set; } = false;
        bool showDocumentSuccess { get; set; } = false;

        bool showAlert { get; set; } = false;

        string ErrorMessage { get; set; } = string.Empty;

        [Inject]
        WebConfigHelper Config { get; set; }

        public string interestRate { get; set; }
        public List<InterestRange> InterestRange { get; set; }

        private ElementReference closeButtonRef;

        List<DepositFundingSourceType> FundingSourceType { get; set; }


        string Uploaded_Document { get; set; } = string.Empty;

        [Parameter]
        public EventCallback<bool> OnUpdateSpecialDepositChanged { get; set; }

        [Parameter]
        public bool showAddDrawer { get; set; } = false;
        Drawer addDrawer;

        BrowserDimension BrowserDimension;

        public bool ShowAlertPage { get; set; } = false;
        private SuccessMessageModal successModal;

        protected override async Task OnInitializedAsync()
        {

            //await base.OnInitializedAsync();
            Model = new MemberProfileMasterView();
            SpecialDepositModel = new SpecialDepositApplicationViewModel();
            Command = new CreateSpecialDepositApplicationCommand();

            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            if (!string.IsNullOrEmpty(Principal))
            {
                var user = authenticated.User;

                if (user.Identity.IsAuthenticated)
                {
                    ApplicationUserId = user.FindFirstValue(ClaimTypes.Sid);

                    await GetProfile();
                    await GetDepositProducts();
                }

            }
            else
            {

            }

            await MapToModel();

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                addDrawer.Width = (int)(BrowserDimension.Width * 0.50);

            }

        }

        public async Task GetDepositProducts()
        {
            var rsp = await DataService.GetProduct<List<DepositProductMasterView>>(
            nameof(DepositProductMasterView), "");

            if (rsp.IsSuccessStatusCode)
            {
                DepositProductMasterViews = JsonSerializer.Deserialize<List<DepositProductMasterView>>(rsp.Content.ToJson());

            }
        }

        public async Task GetGetInterestRate(ChangeEventArgs<string, DepositProductMasterView> args)
        {

            try
            {
                string endpoint = $"{Config.API_HOST}/{nameof(DepositProduct)}/{args.Value}";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        SpecialDepositModel.DepositProductId = args.Value;
                        var jsonContent = response.Content.ReadAsStringAsync();
                        var content = JsonConvert.DeserializeObject<RootObject>(jsonContent.Result);

                        InterestRange = content.response.interestRanges;
                        SpecialDepositModel.InterestRate = (decimal)(InterestRange.FirstOrDefault()?.InterestRate);
                    }
                }

            }
            catch (Exception exp)
            {
            }


        }

        private async Task MapToModel()
        {

            SpecialDepositModel.MemberProfileId = Model.MembershipId;

        }


        public async Task GetProfile()
        {

            var rsp = await DataService.GetValue<List<MemberProfileMasterView>>(
             nameof(MemberProfileMasterView), ApplicationUserId);

            if (!rsp.IsSuccessStatusCode)
            {
                if (!string.IsNullOrEmpty(rsp.Error.Content))
                {
                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    var msg = rspContent?.Response;
                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        msg = rspContent.ValidationErrors[0].Error;
                    }
                    if (msg == null && rspContent?.Message != null)
                        msg = rspContent.Message;
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = msg,
                        NotificationType = NotificationType.Error
                    });
                }
                else
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = "Internal Server Error",
                        NotificationType = NotificationType.Error
                    });
                }

            }
            else
            {
                List<MemberProfileMasterView> rspResponse = JsonSerializer.Deserialize<List<MemberProfileMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 && !string.IsNullOrEmpty(rspResponse.FirstOrDefault().ApplicationUserId) && rspResponse.FirstOrDefault().ApplicationUserId == ApplicationUserId)
                {
                    MemberProfile = new MemberProfileMasterView();
                    MemberProfile = rspResponse.FirstOrDefault();
                    Model = Mapper.Map<MemberProfileMasterView>(rspResponse.FirstOrDefault()); ;

                }

            }
        }

        public void MapToCommand()
        {
            Command = new CreateSpecialDepositApplicationCommand()
            {
                MemberProfileId = Model.Id,
                DepositProductId = SpecialDepositModel.DepositProductId,
                Amount = SpecialDepositModel.Amount,
                InterestRate = SpecialDepositModel.InterestRate,
                ModeOfPayment = SpecialDepositModel.ModeOfPayment,
                Document = SpecialDepositModel.Document,
                MimeType = SpecialDepositModel.MimeType,
                FileName = SpecialDepositModel.FileName,
                FileSize = SpecialDepositModel.FileSize,
                CreatedByUserId = Model.ApplicationUserId

            };


        }
        private async Task OnSave()
        {
            MapToCommand();
            if (await _fluentValidationValidator.ValidateAsync())
            {

                var rsp = await DataService.Create<CreateSpecialDepositApplicationCommand, CommandResult<SpecialDepositApplicationViewModel>>(nameof(SpecialDepositAccountApplicationMasterView), Command);

                if (!rsp.IsSuccessStatusCode)
                {
                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    var msg = rspContent?.Response;
                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        msg = rspContent.ValidationErrors[0].Error;
                    }
                    if (msg == null && rspContent?.Message != null)
                        msg = rspContent.Message;
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = msg,
                        NotificationType = NotificationType.Error
                    });
                }
                else
                {
                    showAddDrawer = false;
                    ShowAlertPage = true;
                    await onAddDone();
                    StateHasChanged();

                    SpecialDepositModel = new SpecialDepositApplicationViewModel();
                    Command = new CreateSpecialDepositApplicationCommand();
                    MapToModel();
                }
            }

        }

        public async Task ShowBankMenu(string value, int mode)
        {
            ShowBank = value;

            SpecialDepositModel.ModeOfPayment = mode;

        }

        public async Task OnChangeDocumentUpload(UploadChangeEventArgs args)
        {
            ErrorMessage = string.Empty;
            showDocumentError = false;
            await InvokeAsync(StateHasChanged);
            var file = args.Files[0];
            if (file != null)
            {
                var response = ImageConverterHelper.ValidateDocuments(file);
                if (string.IsNullOrEmpty(response))
                {
                    if (file.FileInfo.Type.ToLower() != "pdf")
                    {
                        SpecialDepositModel.Document = Uploaded_Document = ImageConverterHelper.ConvertFileToBase64(file);
                        SpecialDepositModel.MimeType = file.FileInfo.Type.ToLower();
                        SpecialDepositModel.FileSize = (int)file.FileInfo.Size;
                        SpecialDepositModel.FileName = file.FileInfo.Name;
                    }
                    else
                    {
                        byte[] bytes = file.Stream.ToArray();
                        string base64 = Convert.ToBase64String(bytes);

                        //Convert to PDF
                        string base64PDF = @"data:application/pdf;base64," + base64;

                        SpecialDepositModel.Document = Uploaded_Document = ImageConverterHelper.ConvertFileToBase64(file);

                    }

                    ErrorMessage = "Upload was successfull";
                    showDocumentError = false;
                    showDocumentSuccess = true;
                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    ErrorMessage = response;
                    showDocumentError = true;
                    showDocumentSuccess = false;
                    await InvokeAsync(StateHasChanged);

                }

            }
            else
            {
                ErrorMessage = "File not found";
                showDocumentError = true;
                showDocumentSuccess = false;
                await InvokeAsync(StateHasChanged);
            }
        }




        async Task onAddDone()
        {
            showAddDrawer = false;
        }

    }
}