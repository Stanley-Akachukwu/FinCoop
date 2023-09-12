using AntDesign;
using AP.ChevronCoop.AppDomain.Members.MemberBankAccounts;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBankAccounts;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBankAccounts;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.NewKYC
{
    public partial class KYCMemberAccount
    {
        private FluentValidationValidator? _fluentValidationValidator;
        private Query Query_Combo;

        [Inject]
        BrowserService BrowserService { get; set; }

        [Parameter]
        public EventCallback<bool> OnUpdateMemberAccountChanged { get; set; }

        [Parameter]
        public MemberProfileMasterView UpdateModel { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public EventCallback<MemberProfileMasterView> UpdateModelChanged { get; set; }

        public MemberAccountViewModel Model { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        public UpdateMemberProfileCommand UpdateCommand { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Parameter]
        public MemberProfileViewModel MemberProfileModel { get; set; }

        [Inject]
        ILogger<KYCMemberAccount> Logger { get; set; }

        public bool showAddAccountDrawer { get; set; } = false;
        public bool showEditAccountDrawer { get; set; } = false;
        public bool showDeleteAccountModal { get; set; } = false;
        private bool HasRecords { get; set; } = false;
        public List<MemberBankAccountMasterView> MemberBankAccounts { get; set; }
        bool showAddDrawer { get; set; } = false;
        Drawer addDrawer;
        bool showEditDrawer { get; set; } = false;
        Drawer editDrawer;
        BrowserDimension BrowserDimension;
        string ConfirmDeleteAccountBtn = "Yes, Proceed";
        string SaveBtn = "Save";
        bool ConfirmDeleteAccountDisabled { get; set; } = false;
        bool SaveDisabled { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            MemberProfileModel = new MemberProfileViewModel();
            Model = new MemberAccountViewModel();
            MemberBankAccounts = new List<MemberBankAccountMasterView>();
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
            var payload = UpdateModel.Id;
            var rsp = await DataService.GetCustomerBankAccountValue<ODataResponse<MemberBankAccountMasterView>>(
                nameof(MemberBankAccountMasterView), "profileId", payload);

            if (rsp.IsSuccessStatusCode)
            {
                var rspResponse = rsp.Content.Data;
                if (rspResponse?.Count > 0)
                {
                    MemberBankAccounts = rspResponse;
                    if (MemberBankAccounts.Count > 0)
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
                    UpdateMemberBankAccountCommand command = new UpdateMemberBankAccountCommand()
                    {
                        AccountName = Model.AccountName,
                        AccountNumber = Model.AccountNumber,
                        BankId = Model.BankId,
                        Branch = Model.Branch,
                        ProfileId = UpdateModel.Id,
                        Id = Model.Id,
                        //SortCode = Model.SortCode,
                    };
                    var rsp = await DataService.PostCommand<UpdateMemberBankAccountCommand, CommandResult<string>>(
                        nameof(MemberBankAccount), "update", command);
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
                    CreateMemberBankAccountCommand command = new CreateMemberBankAccountCommand()
                    {
                        AccountName = Model.AccountName,
                        AccountNumber = Model.AccountNumber,
                        BankId = Model.BankId,
                        Branch = Model.Branch,
                        ProfileId = UpdateModel.Id,
                        SortCode = Model.SortCode,
                    };
                    var rsp = await DataService
                        .PostCommand<CreateMemberBankAccountCommand, CommandResult<MemberBankAccountViewModel>>(
                            nameof(MemberBankAccount), "create", command);

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

        private void OnEditBankAccount(MemberBankAccountMasterView editAccount)
        {
            Model = new MemberAccountViewModel()
            {
                AccountName = editAccount.AccountName,
                AccountNumber = editAccount.AccountNumber,
                //SortCode = editAccount.SortCode,
                BankId = editAccount.BankId,
                Branch = editAccount.Branch,
                Bvn = editAccount.BVN,
                Id = editAccount.Id,
                ProfileId = editAccount.ProfileId
            };
            showEditDrawer = true;
        }

        private void OnDeleteBankAccount(MemberBankAccountMasterView deleteAccount)
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

        private async Task SaveandContinue()
        {
            await OnUpdateMemberAccountChanged.InvokeAsync(true);
        }

        public async Task DeleteAccount()
        {
            if (!string.IsNullOrEmpty(Model.Id))
            {
                ConfirmDeleteAccountBtn = "Deleting...";
                ConfirmDeleteAccountDisabled = true;
                StateHasChanged();
                DeleteMemberBankAccountCommand command = new DeleteMemberBankAccountCommand()
                {
                    Id = Model.Id
                };
                var rsp = await DataService.PostCommand<DeleteMemberBankAccountCommand, CommandResult<string>>(
                    nameof(MemberBankAccount), "delete", command);

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
    }
}