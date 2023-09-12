using AntDesign;
using AP.ChevronCoop.AppDomain.Customers.CustomerBankAccounts;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Syncfusion.Blazor.Data;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User
{

    public partial class MyBankAccountGrid
    {
        private FluentValidationValidator? _fluentValidationValidator;
        private Query Query_Combo;

        [Inject]
        BrowserService BrowserService { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }

        public MemberAccountViewModel Model { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        ILogger<MyBankAccountGrid> Logger { get; set; }

        public bool showAddAccountDrawer { get; set; } = false;
        public bool showEditAccountDrawer { get; set; } = false;
        public bool showDeleteAccountModal { get; set; } = false;
        private bool HasRecords { get; set; } = false;
        public List<CustomerBankAccountMasterView> CustomerBankAccounts { get; set; }
        bool showAddDrawer { get; set; } = false;
        Drawer addDrawer;
        bool showEditDrawer { get; set; } = false;
        Drawer editDrawer;
        BrowserDimension BrowserDimension;
        string ConfirmDeleteAccountBtn = "Yes, Proceed";
        string SaveBtn = "Save";
        bool ConfirmDeleteAccountDisabled { get; set; } = false;
        bool SaveDisabled { get; set; } = false;
        ClaimsPrincipal CurrentUser { get; set; }
        [Inject]
        AuthenticationStateProvider _authenticationStateProvider { get; set; }

        [Inject] IClientAuditService _auditLogService { get; set; }
        public string CustomerId { get; set; }
        public CustomerBankAccountMasterView EditCustomerBankAccount { get; set; }
        protected override async Task OnInitializedAsync()
        {
            EditCustomerBankAccount = new CustomerBankAccountMasterView();
            Model = new MemberAccountViewModel();
            CustomerBankAccounts = new List<CustomerBankAccountMasterView>();
            await GetCurrentUser();
            await GetUserBankAccount();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                addDrawer.Width = (int)(BrowserDimension.Width * 0.50);
                editDrawer.Width = (int)(BrowserDimension.Width * 0.50);
            }

            ConfirmDeleteAccountBtn = "Yes, Proceed";
            SaveBtn = "Save";
            SaveDisabled = false;
            ConfirmDeleteAccountDisabled = false;
        }

        private async Task GetUserBankAccount()
        {
            var rsp = await DataService.GetCustomerBankAccountValue<ODataResponse<CustomerBankAccountMasterView>>(
                nameof(CustomerBankAccountMasterView), "CustomerId", CustomerId);

            if (rsp.IsSuccessStatusCode)
            {
                var rspResponse = rsp.Content.Data;
                if (rspResponse?.Count > 0)
                {
                    CustomerBankAccounts = rspResponse;
                    if (CustomerBankAccounts.Count > 0)
                        HasRecords = true;
                    else
                        HasRecords = false;
                    StateHasChanged();
                }
            }
        }

        private async Task OnSave()
        {
            SaveBtn = "Saving...";
            SaveDisabled = true;
            StateHasChanged();
            if (await _fluentValidationValidator!.ValidateAsync())
            {
                if (!string.IsNullOrEmpty(Model.Id))
                {
                    UpdateCustomerBankAccountCommand command = new UpdateCustomerBankAccountCommand()
                    {
                        AccountName = Model.AccountName,
                        AccountNumber = Model.AccountNumber,
                        BankId = Model.BankId,
                        Branch = Model.Branch,
                        CustomerId = CustomerId,
                        Id = Model.Id,
                        //SortCode = Model.SortCode,
                    };
                    var rsp = await DataService.PostCommand<UpdateCustomerBankAccountCommand, CommandResult<string>>(
                        nameof(CustomerBankAccount), "update", command);
                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(command)}");
                    if (!rsp.IsSuccessStatusCode)
                    {
                        SaveBtn = "Save";
                        SaveDisabled = false;
                        StateHasChanged();
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                        var msg = rspContent?.Response;
                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
                        {
                            msg = rspContent.ValidationErrors[0].Error;
                        }

                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Error",
                            Description = msg,
                            NotificationType = NotificationType.Error
                        });
                    }
                    else
                    {
                        showEditDrawer = false;
                        showAddDrawer = false;
                        await GetUserBankAccount();
                    }
                }
                else
                {
                    CreateCustomerBankAccountCommand command = new CreateCustomerBankAccountCommand()
                    {
                        AccountName = Model.AccountName,
                        AccountNumber = Model.AccountNumber,
                        BankId = Model.BankId,
                        Branch = Model.Branch,
                        CustomerId = CustomerId,
                        SortCode = Model.SortCode,
                    };
                    var rsp = await DataService
                        .PostCommand<CreateCustomerBankAccountCommand, CommandResult<CustomerBankAccountViewModel>>(
                            nameof(CustomerBankAccount), "create", command);

                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(command)}");
                    if (!rsp.IsSuccessStatusCode)
                    {
                        SaveBtn = "Save";
                        SaveDisabled = false;
                        StateHasChanged();
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                        var msg = rspContent?.Response;
                        if (rspContent != null && rspContent.ValidationErrors != null &&
                            rspContent.ValidationErrors.Any())
                        {
                            msg = rspContent.ValidationErrors[0].Error;
                        }

                        await notificationService.Open(new NotificationConfig()
                        {
                            Message = "Error",
                            Description = msg,
                            NotificationType = NotificationType.Error
                        });
                    }
                    else
                    {
                        showEditDrawer = false;
                        showAddDrawer = false;
                        await GetUserBankAccount();
                    }
                }
            }
        }

        public void OnAddBankAccount()
        {
            Model = new MemberAccountViewModel();
            SaveBtn = "Save";
            SaveDisabled = false;
            showAddDrawer = true;
        }

        private void OnEditBankAccount(CustomerBankAccountMasterView editAccount)
        {
            EditCustomerBankAccount = new CustomerBankAccountMasterView();
            EditCustomerBankAccount = editAccount;
            Model = new MemberAccountViewModel()
            {
                AccountName = editAccount.AccountName,
                AccountNumber = editAccount.AccountNumber,
                //SortCode = editAccount.SortCode,
                BankId = editAccount.BankId,
                Branch = editAccount.Branch,
                Bvn = editAccount.BVN,
                Id = editAccount.Id,
            };
            showEditDrawer = true;
        }

        private void OnDeleteBankAccount(CustomerBankAccountMasterView deleteAccount)
        {
            Model = new MemberAccountViewModel();
            Model.Id = deleteAccount.Id;
            showDeleteAccountModal = true;
        }

        private async Task ConfirmDeleteAccount()
        {
            //Call delete endPoint Here
            await DeleteAccount();
        }

        async Task onAddDone()
        {
            Model = new MemberAccountViewModel();
            SaveBtn = "Save";
            SaveDisabled = false;
            showAddDrawer = false;
        }

        async Task onEditDone()
        {
            Model = new MemberAccountViewModel();
            SaveBtn = "Save";
            SaveDisabled = false;
            showEditDrawer = false;
        }


        public async Task DeleteAccount()
        {
            if (!string.IsNullOrEmpty(Model.Id))
            {
                ConfirmDeleteAccountBtn = "Deleting...";
                ConfirmDeleteAccountDisabled = true;
                StateHasChanged();
                DeleteCustomerBankAccountCommand command = new DeleteCustomerBankAccountCommand()
                {
                    Id = Model.Id
                };
                var rsp = await DataService.PostCommand<DeleteCustomerBankAccountCommand, CommandResult<string>>(
                    nameof(CustomerBankAccount), "delete", command);

                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(command)}");
                if (!rsp.IsSuccessStatusCode)
                {
                    ConfirmDeleteAccountBtn = "Yes, Proceed";
                    ConfirmDeleteAccountDisabled = false;
                    StateHasChanged();
                    var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

                    var msg = rspContent?.Response;
                    if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
                    {
                        msg = rspContent.ValidationErrors[0].Error;
                    }

                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Error",
                        Description = msg,
                        NotificationType = NotificationType.Error
                    });
                }
                else
                {
                    showDeleteAccountModal = false;
                    await GetUserBankAccount();
                }
            }
        }

        async Task onDeleteDone()
        {
            Model = new MemberAccountViewModel();
            ConfirmDeleteAccountBtn = "Yes, Proceed";
            ConfirmDeleteAccountDisabled = false;
            showDeleteAccountModal = false;
        }
        private async Task GetCurrentUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            CurrentUser = authState.User;
            if (CurrentUser != null)
            {
                CustomerId = CurrentUser.Claims.Where(f => f.Type == "CustomerId").FirstOrDefault().Value;
            }
        }
    }
}
