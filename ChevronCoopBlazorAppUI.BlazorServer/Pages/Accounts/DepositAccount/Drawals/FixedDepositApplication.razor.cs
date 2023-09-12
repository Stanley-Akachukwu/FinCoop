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
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.AppDomain.Deposits.SpecialDeposits.SpecialDepositAccountApplications;
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositAccountApplications;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositApplications;
using System;
using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductInterestRanges;
using System.Net;
using AP.ChevronCoop.Entities.Accounting.CompanyBankAccounts;
using ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.Drawals
{
    public partial class FixedDepositApplication
    {
        private IJSRuntime jsRuntime;

        private FluentValidationValidator? _fluentValidationValidator;
        public CustomerMasterView Model { get; set; }


        public CreateFixedDepositAccountApplicationCommandFE FixedDepositModel { get; set; }

        public CreateFixedDepositAccountApplicationCommand Command { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        public string bankDepositAccountId { get; set; }
        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        public List<FixedDepositAccountApplicationMasterView> FixedDepositApplicationMasterViews = new List<FixedDepositAccountApplicationMasterView>();

        public List<FixedDepositAccountApplication> FixedDepositApplicationList = new List<FixedDepositAccountApplication>();

        public List<ProductSetupGrid> ProductSetupGrids = new List<ProductSetupGrid>(); 

        string ApplicationUserId { get; set; }

        public MemberProfileMasterView MemberProfile;


        private string FundingSourceChecked = "None";

        public string ShowBank = "hidden";

        public string LiquidationPage = "";

        public string LiquidationHeader = "";

        public bool drawalHidden = true;

        bool showDocumentError { get; set; } = false;
        bool showDocumentSuccess { get; set; } = false;

        bool showAlert { get; set; } = false;

        string ErrorMessage { get; set; } = string.Empty;

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        IMasterViews _MasterViews { get; set; }

        public string interestRate { get; set; }
        public List<InterestRange> InterestRange { get; set; }

        private ElementReference closeButtonRef;

        List<DepositFundingSourceType> FundingSourceType { get; set; }

        List<MaturityInstructionType> MaturityInstructionTypes { get; set; }


        string Uploaded_Document { get; set; } = string.Empty;

        [Parameter]
        public EventCallback<bool> OnUpdateSpecialDepositChanged { get; set; }

        [Parameter]
        public bool showAddDrawer { get; set; } = false;

        Drawer addDrawer;

        BrowserDimension BrowserDimension;

        public bool ShowAlertPage { get; set; } = false;
        private SuccessMessageModal successModal;

        public List<DepositProduct> DepositProductMasterViews = new List<DepositProduct>();

        public List<DepositAccountsMasterView> GetDepositAccountsMasterView = new List<DepositAccountsMasterView>();

        public List<DepositProductInterestRangeMasterView> _DepositProductInterestRangeMasterView = new List<DepositProductInterestRangeMasterView>();
        public List<DepositProductInterestRangeMasterView> _DepositProductInterestRangeMasterViewSrc = new List<DepositProductInterestRangeMasterView>();
      
        public List<DepositAccountsMasterView> _DepositAccountsMasterView = new List<DepositAccountsMasterView>();
        public List<DepositAccountsMasterView> _DepositAccountsMasterViewSrc = new List<DepositAccountsMasterView>();

        //For Mode of Payment
        public List<DepositAccountsMasterView> _SpecialDepositModeofPayment = new List<DepositAccountsMasterView>();
       

        public List<CustomerBankAccountMasterView> GetCustomerBankAccounts = new List<CustomerBankAccountMasterView>();

        private MaturityInstructionType selectedMaturityInstructionType;
        private string SelectedValue;

        public bool ShowSavingsAccount { get; set; } = false;
        public bool ShowSpecialDeposit { get; set; } = false;
        public bool ShowBankTransfer { get; set; } = false;

        public string tenureValue, tenureUnit = "";

        public decimal? minmumContribution = 0;

        public decimal? maximumContribution = 0;
   
        public string BankName { get; set; }

 
        public string AccountNumber { get; set; }
  
        public string AccountName { get; set; }

        [Inject]
        ILogger<FixedDepositApplication> Logger { get; set; }

        public bool IsButtonDisabled { get; set; } = false;

        public string UploadedImage { get; set; } = "";

        protected override async Task OnInitializedAsync()
        {
            //await base.OnInitializedAsync();
            Model = new CustomerMasterView();
            FixedDepositModel = new CreateFixedDepositAccountApplicationCommandFE();
            Command = new CreateFixedDepositAccountApplicationCommand();

            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            // MaturityInstructionTypes = System.Enum.GetValues(typeof(MaturityInstructionType)).Cast<MaturityInstructionType>().ToList();

            MaturityInstructionTypes = System.Enum.GetValues(typeof(MaturityInstructionType))
                .Cast<MaturityInstructionType>().ToList();

            if (!string.IsNullOrEmpty(Principal))
            {
                var user = authenticated.User;

                if (user.Identity.IsAuthenticated)
                {
                    ApplicationUserId = user.FindFirstValue(ClaimTypes.Sid);

                    await GetProfile();
                    await GetDepositProducts();
                    await GetCustomerChevCoopAccount();
                    await GetCustomerBankAccount();
                    await GetInterestRange();
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
            var rsp = await DataService.GetValue<List<DepositProduct>>(
                nameof(DepositProduct), "status", "PUBLISHED", "productType", DepositProductType.FIXED_DEPOSIT.ToString());

            if (rsp.IsSuccessStatusCode)
            {
                DepositProductMasterViews = JsonSerializer.Deserialize<List<DepositProduct>>(rsp.Content.ToJson());
            }
            else if(rsp.StatusCode.Equals(HttpStatusCode.InternalServerError))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = MessageBox.ServerErrorHeader,
                    Description = MessageBox.ServerErrorDescription,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = MessageBox.ServerErrorHeader,
                    Description = MessageBox.ServerErrorDescription,
                    NotificationType = NotificationType.Error
                });
            }
        }

        public async Task GetCustomerBankAccount()
        {
            var rsp = await DataService.GetValue<List<CustomerBankAccountMasterView>>(
                nameof(CustomerBankAccountMasterView), "customerId", Model.Id);

            if (rsp.IsSuccessStatusCode)
            {
                GetCustomerBankAccounts =
                    JsonSerializer.Deserialize<List<CustomerBankAccountMasterView>>(rsp.Content.ToJson());
            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = MessageBox.ServerErrorHeader,
                    Description = MessageBox.ServerErrorDescription,
                    NotificationType = NotificationType.Error
                });
            }
        }

        public async Task GetCustomerChevCoopAccount()
        {
            var rsp = await DataService.GetValue<List<DepositAccountsMasterView>>(
                nameof(DepositAccountsMasterView), "customerId", Model.Id);

            if (rsp.IsSuccessStatusCode)
            {
                _DepositAccountsMasterViewSrc =
                    JsonSerializer.Deserialize<List<DepositAccountsMasterView>>(rsp.Content.ToJson());

                foreach (var item in _DepositAccountsMasterViewSrc)
                {
                    item.Caption = $"{item.AccountType} - {item.AccountNo}";
                }

                _SpecialDepositModeofPayment = _DepositAccountsMasterViewSrc
                    .Where(ex => ex.AccountType == DepositProductType.SPECIAL_DEPOSIT.ToString()).ToList();


            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = MessageBox.ServerErrorHeader,
                    Description = MessageBox.ServerErrorDescription,
                    NotificationType = NotificationType.Error
                });
            }
        }

        public async Task GetInterestRate(ChangeEventArgs<string, DepositProduct> args)
        {
            try
            {
                bankDepositAccountId = args.ItemData.BankDepositAccountId;
                FixedDepositModel.DepositProductId = args.Value;
                var firstorDe = DepositProductMasterViews.FirstOrDefault(ex => ex.Id == args.Value);
                FixedDepositModel.InterestRate = firstorDe?.InterestRanges.FirstOrDefault()?.InterestRate ?? 0;

                tenureUnit = firstorDe?.Tenure.ToString() ?? "";
                tenureValue = firstorDe?.TenureValue.ToString() ?? "0";

                //minmumContribution = DepositProductMasterViews.FirstOrDefault(ex => ex.Id == args.Value)
                //    ?.MinimumContributionRegular ?? 0;

                _DepositProductInterestRangeMasterView = _DepositProductInterestRangeMasterViewSrc.Where(c => c.ProductId == args.Value).ToList();
              
            }
            catch (Exception exp)
            {
            }
        }
        public async Task GetInterestRange()
        {
            var rsp = await DataService.GetMasterView<List<DepositProductInterestRangeMasterView>>(
                nameof(DepositProductInterestRangeMasterView));

            if (rsp.IsSuccessStatusCode)
            {
                _DepositProductInterestRangeMasterViewSrc = JsonSerializer.Deserialize<List<DepositProductInterestRangeMasterView>>(rsp.Content.ToJson());
                foreach(var item in  _DepositProductInterestRangeMasterViewSrc)
                {
                    item.InterestRate = Math.Round(item.InterestRate, 2);
                }
            }
            else
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = MessageBox.ServerErrorHeader,
                    Description = MessageBox.ServerErrorDescription,
                    NotificationType = NotificationType.Error
                });
            }
        }


        public async Task UpdateMinAndMax(ChangeEventArgs<string, DepositProductInterestRangeMasterView> args)
        {
           
            if (_DepositProductInterestRangeMasterView.Count > 0)
            {
                var selectedProduct = _DepositProductInterestRangeMasterViewSrc.FirstOrDefault(d => d.Id == args.Value);
                if (selectedProduct != null)
                {
                    FixedDepositModel.InterestRate = selectedProduct.InterestRate;
                    minmumContribution = selectedProduct.LowerLimit;
                    maximumContribution = selectedProduct.UpperLimit;
                }
            }
            else
            {
                FixedDepositModel.InterestRate = _DepositProductInterestRangeMasterViewSrc.FirstOrDefault().InterestRate;
                minmumContribution = 0;
                maximumContribution = 0;
            }
        }


        private async Task MapToModel()
        {
            FixedDepositModel.CustomerId = Model.CustomerNo;
        }


        public async Task GetProfile()
        {
            var rsp = await DataService.GetValue<List<CustomerMasterView>>(
                nameof(Customer), ApplicationUserId);

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
                        Description = "Oops! Something Went Wrong. Please try again later. Thanks",
                        NotificationType = NotificationType.Error
                    });
                }
            }
            else
            {
                List<CustomerMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<CustomerMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().ApplicationUserId) &&
                    rspResponse.FirstOrDefault().ApplicationUserId == ApplicationUserId)
                {
                    //MemberProfile = new CustomerMasterView();
                    //MemberProfile = rspResponse.FirstOrDefault();
                    Model = Mapper.Map<CustomerMasterView>(rspResponse.FirstOrDefault());
                    ;
                }
            }
        }

        public void MapToCommand()
        {
            Command = new CreateFixedDepositAccountApplicationCommand
            {
                Description = $"{Model.FirstName} {Model.LastName} created fixed deposit application",
                Comments = "",
                CustomerId = Model.Id,
                DepositProductId = FixedDepositModel.DepositProductId,
                Amount = FixedDepositModel.Amount,
                InterestRate = FixedDepositModel.InterestRate,
                ModeOfPayment = FixedDepositModel.ModeOfPayment,
                Document = FixedDepositModel.Document,
                MimeType = FixedDepositModel.MimeType,
                FileName = FixedDepositModel.FileName,
                FileSize = FixedDepositModel.FileSize,
                MaturityInstructionType = FixedDepositModel.MaturityInstructionType,
                LiquidationAccountType = FixedDepositModel.LiquidationAccountType,
                LiquidationAccountId = FixedDepositModel.LiquidationAccountId,
                ModeOfPaymentAccountId = FixedDepositModel.ModeOfPaymentAccountId,
                ApprovalId = FixedDepositModel.ApprovalId,
                CreatedByUserId = Model.ApplicationUserId
            };
        }

        private async Task OnSave()
        {
            IsButtonDisabled = true;
            MapToCommand();

            if (minmumContribution > FixedDepositModel.Amount || maximumContribution < FixedDepositModel.Amount)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = $"Fixed deposit amount must be between {minmumContribution.Value.ToString("N0")} and {maximumContribution.Value.ToString()}",
                    NotificationType = NotificationType.Error
                });
                IsButtonDisabled = false;
                return;
            }



            if (await _fluentValidationValidator.ValidateAsync())
            {
                var rsp = await DataService
                    .Create<CreateFixedDepositAccountApplicationCommand,
                        CommandResult<FixedDepositAccountApplicationViewModel>>(nameof(FixedDepositAccountApplication),
                        Command);

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

                    FixedDepositModel = new CreateFixedDepositAccountApplicationCommandFE();
                    Command = new CreateFixedDepositAccountApplicationCommand();
                    MapToModel();
                }
            }

            IsButtonDisabled = false;
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
                        FixedDepositModel.Document = Uploaded_Document = ImageConverterHelper.ConvertFileToBase64(file);
                        FixedDepositModel.MimeType = file.FileInfo.Type.ToLower();
                        FixedDepositModel.FileSize = (int)file.FileInfo.Size;
                        FixedDepositModel.FileName = file.FileInfo.Name;

                        UploadedImage = FixedDepositModel.Document;
                    }
                    else
                    {
                        byte[] bytes = file.Stream.ToArray();
                        string base64 = Convert.ToBase64String(bytes);

                        //Convert to PDF
                        string base64PDF = @"data:application/pdf;base64," + base64;

                        FixedDepositModel.Document = Uploaded_Document = ImageConverterHelper.ConvertFileToBase64(file);
                    }

                    FixedDepositModel.ModeOfPaymentAccountId = "Bank Transfer";

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

        private string GetDisplayName(MaturityInstructionType maturityInstructionType)
        {
            var displayAttribute = maturityInstructionType.GetType()
                .GetField(maturityInstructionType.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return displayAttribute?.Name ?? maturityInstructionType.ToString();
        }

        public async Task ShowBankMenu(string value, DepositFundingSourceType mode)
        {
            ShowBank = value;

            FixedDepositModel.ModeOfPayment = mode;

            //if bank transfer
            if (mode == DepositFundingSourceType.BANK_TRANSFER)
            {
                if (string.IsNullOrEmpty(bankDepositAccountId))
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = "Select preferred deposit product, before choosing bank transfer",
                        NotificationType = NotificationType.Error
                    });
                }
                var bankrsp = await _MasterViews.GetCustomMasterView<CompanyBankAccountMasterView>(nameof(CompanyBankAccountMasterView), "id", bankDepositAccountId, "AccountNumber,AccountName,BankId_Name");
               var companyBankAccountMasterView = bankrsp.FirstOrDefault();
                BankName = companyBankAccountMasterView?.BankId_Name;
                AccountName = companyBankAccountMasterView?.AccountName;
                AccountNumber = companyBankAccountMasterView?.AccountNumber;
            }

        }

        public async Task ShowLiquidation(WithdrawalAccountType enumValue, string header, string pageToShow,
            string accountType)
        {
            LiquidationPage = pageToShow;
            LiquidationHeader = header;
            FixedDepositModel.LiquidationAccountType = enumValue;
            if (accountType == "Savings_Account")
            {
                GetDepositAccountsMasterView = _DepositAccountsMasterViewSrc
                  .Where(ex => ex.AccountType == DepositProductType.SAVINGS.ToString()).ToList();

            }

            if (accountType == "Special_Deposit")
            {
                GetDepositAccountsMasterView = _DepositAccountsMasterViewSrc
                    .Where(ex => ex.AccountType == DepositProductType.SPECIAL_DEPOSIT.ToString()).ToList();
            }

            //if (accountType == "bank")
            //{
            //    // GetDepositAccountsMasterView = GetDepositAccountsMasterView.Where(ex => ex.AccountType = "Savings Account");
            //}
        }

        public async Task GetCoopAccount(ChangeEventArgs<string, DepositAccountsMasterView> args)
        {
            FixedDepositModel.LiquidationAccountId = args.Value;
        }

        public async Task GetRegularCustomerAccount(ChangeEventArgs<string, CustomerBankAccountMasterView> args)
        {
            FixedDepositModel.LiquidationAccountId = args.Value;
        }


        public async Task ModeofPaymentAccountID(ChangeEventArgs<string, DepositAccountsMasterView> args)
        {
            FixedDepositModel.ModeOfPaymentAccountId = args.Value;
        }

        public async Task SelectMaturityInstruction(ChangeEventArgs<string, MaturityInstructionType> args)
        {
        }

        private async Task OnRemoveHandler(RemovingEventArgs args)
        {
            UploadedImage = Command.Document = "";
            await InvokeAsync(StateHasChanged);

        }

    }
}