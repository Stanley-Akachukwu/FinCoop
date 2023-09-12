using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.SharedComponents;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Syncfusion.Blazor.DropDowns;
using JsonSerializer = System.Text.Json.JsonSerializer;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Blazored.FluentValidation;
using AP.ChevronCoop.AppDomain.Deposits.FixedDeposits.FixedDepositLiquidations;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.Deposits.DepositApplications;
using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositAccounts;
using AP.ChevronCoop.Entities.Deposits.FixedDeposits.FixedDepositImmediateLiquidations;
using CurrieTechnologies.Razor.SweetAlert2;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Accounts.DepositAccount.Drawals
{
    public partial class FixedDepositImmediateLiquidation
    {
        private FluentValidationValidator? _fluentValidationValidator;

        [Parameter]
        public EventCallback<bool> OnUpdateChanged { get; set; }

        public CreateFixedDepositLiquidationCommand Command { get; set; }

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public bool IsButtonDisabled { get; set; } = false;

        [Parameter] public string CustomerID { get; set; }

        [Parameter] public string FixedDepositAccountID { get; set; }

        [Parameter] public string FixedDepositAccountNumber { get; set; }

        [Parameter] public string MembersName { get; set; }

        [Parameter] public string MembersNumber { get; set; }

        [Parameter]
        public bool showAddDrawer { get; set; } = false;

        Drawer addDrawer;

        BrowserDimension BrowserDimension;

        public bool ShowAlertPage { get; set; } = false;
        private SuccessMessageModal successModal;

        public List<DepositAccountsMasterView> GetDepositAccountsMasterView = new List<DepositAccountsMasterView>();

        public List<DepositAccountsMasterView> GetSpecialDepositAccountsMasterViewSrc = new List<DepositAccountsMasterView>();

        public List<CustomerBankAccountMasterView> GetCustomerBankAccounts = new List<CustomerBankAccountMasterView>();

        public List<FixedDepositAccountMasterView> FixedDepositAccountMasterView = new List<FixedDepositAccountMasterView>();


        private string FundingSourceChecked = "None";

        public string ShowBank = "hidden";

        public string LiquidationPage = "";

        public string LiquidationHeader = "";

        public bool drawalHidden = true;

        public string maturityInstruction { get; set; } = "";

        public string pageToNavigate = "";

        protected override async Task OnInitializedAsync()
        {
            Command = new CreateFixedDepositLiquidationCommand();
            await GetCustomerBankAccount();
            await GetCustomerChevCoopAccount();
            await GetFixedDepositMaturityInstructions();

            pageToNavigate = _NavigationManager.Uri;
        }

        public async Task GetCustomerBankAccount()
        {
            var rsp = await DataService.GetValue<List<CustomerBankAccountMasterView>>(
                nameof(CustomerBankAccountMasterView), "customerId", CustomerID);

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
                nameof(DepositAccountsMasterView), "customerId", CustomerID);

            if (rsp.IsSuccessStatusCode)
            {
                GetSpecialDepositAccountsMasterViewSrc =
                    JsonSerializer.Deserialize<List<DepositAccountsMasterView>>(rsp.Content.ToJson());

                foreach (var item in GetSpecialDepositAccountsMasterViewSrc)
                {
                    item.Caption = $"{MembersName} ({item.AccountNo})";
                }

                GetDepositAccountsMasterView = GetSpecialDepositAccountsMasterViewSrc
                    .Where(ex => ex.AccountType == DepositProductType.SAVINGS.ToString()).ToList();
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

        public async Task GetFixedDepositMaturityInstructions()
        {
            var rsp = await DataService.GetValue<List<FixedDepositAccountMasterView>>(
                nameof(FixedDepositAccountMasterView), "id", FixedDepositAccountID);

            if (rsp.IsSuccessStatusCode)
            {
                FixedDepositAccountMasterView =
                    JsonSerializer.Deserialize<List<FixedDepositAccountMasterView>>(rsp.Content.ToJson());

                maturityInstruction = FixedDepositAccountMasterView.FirstOrDefault().MaturityInstructionType;
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                addDrawer.Width = (int)(BrowserDimension.Width * 0.50);
            }
        }


        public void MapToCommand()
        {
            Command.CustomerId = CustomerID;
            Command.FixedDepositAccountId = FixedDepositAccountID;
            Command.DateCreated = DateTime.UtcNow.AddHours(1);
            Command.CreatedByUserId = CustomerID;
            Command.Description =
                $"({FixedDepositAccountNumber}) Fixed deposit account was liquidated by {MembersName}";
        }

        private async Task OnSave()
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Are you sure?",
                Text = $"You want to liquidate this account?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "Yes, Liquidate",
                CancelButtonText = "No, keep it"
            });

            if (!string.IsNullOrEmpty(result.Value))
            {
                IsButtonDisabled = true;

                MapToCommand();
                //  if (await _fluentValidationValidator.ValidateAsync())
                {
                    var rsp = await DataService
                        .Create<CreateFixedDepositLiquidationCommand, CommandResult<FixedDepositLiquidationViewModel>>(
                            nameof(FixedDepositLiquidation), Command);

                    if (!rsp.IsSuccessStatusCode)
                    {
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                        var msg = rspContent?.Response;
                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
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
                        await OnUpdateChanged.InvokeAsync(true);
                        showAddDrawer = false;
                        ShowAlertPage = true;

                        Command = new CreateFixedDepositLiquidationCommand();
                    }
                }
                IsButtonDisabled = false;
            }
        }

        public async Task ShowSuccessMessage()
        {
            ShowAlertPage = true;
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
        }

        public async Task ShowLiquidation(WithdrawalAccountType enumValue, string header, string pageToShow,
            string accountType)
        {
            LiquidationPage = pageToShow;
            LiquidationHeader = header;
            Command.LiquidationAccountType = enumValue;
            if (accountType == "Savings Account")
            {
                GetDepositAccountsMasterView = GetSpecialDepositAccountsMasterViewSrc
                    .Where(ex => ex.AccountType == DepositProductType.SAVINGS.ToString()).ToList();
            }

            if (accountType == "Special Deposit")
            {
                GetDepositAccountsMasterView = GetSpecialDepositAccountsMasterViewSrc
                    .Where(ex => ex.AccountType == DepositProductType.SPECIAL_DEPOSIT.ToString()).ToList();
            }

            if (accountType == "bank")
            {
                // GetDepositAccountsMasterView = GetDepositAccountsMasterView.Where(ex => ex.AccountType = "Savings Account");
            }
        }

        public async Task GetCoopAccount(ChangeEventArgs<string, DepositAccountsMasterView> args)
        {
            Command.LiquidationAccountId = args.Value;
        }

        public async Task GetRegularCustomerAccount(ChangeEventArgs<string, CustomerBankAccountMasterView> args)
        {
            Command.LiquidationAccountId = args.Value;
        }
    }
}