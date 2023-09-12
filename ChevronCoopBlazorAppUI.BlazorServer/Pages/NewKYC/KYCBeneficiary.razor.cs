using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBeneficiaries;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBeneficiaries;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Tailwindcss;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.KYC.CorporateMember;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Refit;
using Syncfusion.Blazor.Data;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.NewKYC
{
    public partial class KYCBeneficiary
    {
        private FluentValidationValidator? _fluentValidationValidatorAdd;
        private FluentValidationValidator? _fluentValidationValidatorEdit;
        bool showPopup = false;
        public MyBeneficiaryCreateModel Model { get; set; }

        protected List<BeneficiaryData> BeneficiariesList { get; set; }


        private bool DisableAddButton { get; set; } = false;

        private BeneficiaryData UpdateModel { get; set; }
        private bool showEditNextOfKinDrawer { get; set; } = false;

        [Inject]
        BrowserService BrowserService { get; set; }

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]

        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        ILogger<KYCBeneficiary> Logger { get; set; }

        private List<MemberBeneficiaryMasterView> MemberBeneficiaryMasterView { get; set; }

        [Parameter]
        public MemberProfileMasterView MemberProfileModel { get; set; }

        [Parameter]
        public EventCallback<MemberProfileMasterView> MemberProfileModelChanged { get; set; }

        [Parameter]
        public EventCallback<bool> OnUpdateBeneficiaryChanged { get; set; }

        [Parameter]
        public EventCallback<MemberProfileMasterView> UpdateModelChanged { get; set; }

        bool showAddDrawer { get; set; } = false;
        Drawer addDrawer;
        bool showEditDrawer { get; set; } = false;
        Drawer editDrawer;
        BrowserDimension BrowserDimension;
        private bool HasRecords { get; set; } = false;


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            UpdateModel = new BeneficiaryData();
            BeneficiariesList = new List<BeneficiaryData>();

            Model = new MyBeneficiaryCreateModel();

            await GetUserBeneficiary();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                BrowserDimension = await BrowserService.GetDimensions();

                addDrawer.Width = (int)(BrowserDimension.Width * 0.50);
                editDrawer.Width = (int)(BrowserDimension.Width * 0.50);
                //await OnRefresh();
            }
            //StateHasChanged();

            //await jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        private async Task GetUserBeneficiary()
        {
            var payload = MemberProfileModel.Id;
            var rsp = await DataService.GetBeneficiaryValue<List<MemberBeneficiaryMasterView>>(
                nameof(MemberBeneficiaryMasterView), payload);

            if (rsp.IsSuccessStatusCode)
            {
                List<MemberBeneficiaryMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<MemberBeneficiaryMasterView>>(rsp.Content.ToJson());

                if (rspResponse?.Count > 0)
                {
                    MemberBeneficiaryMasterView = new List<MemberBeneficiaryMasterView>();
                    MemberBeneficiaryMasterView = rspResponse;
                    BeneficiariesList = new List<BeneficiaryData>();
                    foreach (var item in rspResponse)
                    {
                        BeneficiaryData eachBeneficiaryDataRecord = new BeneficiaryData()
                        {
                            Address = item.Address,
                            LastName = item.LastName,
                            Id = item.Id,
                            PhoneNumber = item.Phone,
                            FirstName = item.FirstName,
                            Email = item.Email
                        };
                        BeneficiariesList.Add(eachBeneficiaryDataRecord);
                    }

                    if (BeneficiariesList != null && BeneficiariesList.Count >= 5)
                    {
                        onEditDone();
                        DisableAddButton = true;
                    }

                    if (BeneficiariesList?.Count > 0)
                        HasRecords = true;
                    else
                        HasRecords = false;
                }
            }
        }

        private async Task CheckBeneficiaryCount()
        {
            if (BeneficiariesList != null && BeneficiariesList.Count >= 6)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Info",
                    Description = "You can not add more than 5 Next of Kin",
                    NotificationType = NotificationType.Error
                });
            }
        }

        private async Task SaveEditForm()
        {
            if (await _fluentValidationValidatorEdit!.ValidateAsync())
            {
                UpdateMemberBeneficiaryCommand command = new UpdateMemberBeneficiaryCommand()
                {
                    FirstName = UpdateModel.FirstName,
                    LastName = UpdateModel.LastName,
                    Email = UpdateModel.Email,
                    Phone = "+234" + UpdateModel.PhoneNumber,
                    Id = UpdateModel.Id,
                    Address = UpdateModel.Address,
                    ProfileId = MemberProfileModel.Id
                };

                var rsp = await DataService.Beneficiary<UpdateMemberBeneficiaryCommand, CommandResult<string>>("update",
                    command);
                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(Model)}");
                if (!rsp.IsSuccessStatusCode)
                {
                    await DisplayErrorAfterNewBeneficiaryAdditionAsync(rsp);
                }
                else
                {
                    showEditDrawer = false;
                    await GetUserBeneficiary();
                    await InvokeAsync(StateHasChanged);
                }
            }
        }

        private void EditBeneficiary(BeneficiaryData itemToUpdate)
        {
            UpdateModel = new BeneficiaryData();
            UpdateModel = itemToUpdate;
            showEditDrawer = true;
        }

        private async Task DisplayErrorAfterNewBeneficiaryAdditionAsync(ApiResponse<CommandResult<string>> rsp)
        {
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

        private void DeleteBeneficiary(BeneficiaryData itemToDelete)
        {
            UpdateModel = new BeneficiaryData();
            UpdateModel = itemToDelete;
            showPopup = true;
        }

        private async Task ConfirmDeleteBeneficiary()
        {
            var selectedBeneficiary = MemberBeneficiaryMasterView.Where(p => p.Id == UpdateModel.Id).FirstOrDefault();
            if (selectedBeneficiary != null)
            {
                var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var Principal = authenticated.User.Identity.Name;
                var user = authenticated.User;

                if (user.Identity.IsAuthenticated)
                {
                    var userId = user.FindFirstValue(ClaimTypes.Sid);
                    DeleteMemberBeneficiaryCommand deleteCommand = new DeleteMemberBeneficiaryCommand()
                    {
                        Id = selectedBeneficiary.Id,
                        DateDeleted = DateTime.Now,
                        DeletedByUserId = userId
                    };
                    var rsp4 = await DataService
                        .DeleteBeneficiary<DeleteMemberBeneficiaryCommand, CommandResult<string>>(deleteCommand);

                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(deleteCommand)}");
                    if (!rsp4.IsSuccessStatusCode)
                    {
                        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp4.Error.Content);

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
                        await GetUserBeneficiary();
                        StateHasChanged();
                        showPopup = false;
                    }
                }
            }
        }

        private async Task SaveandContinue()
        {
            await OnUpdateBeneficiaryChanged.InvokeAsync(true);
        }

        private async Task Cancel()
        {
            Model = new MyBeneficiaryCreateModel();
        }

        private async Task SubmitValidForm()
        {
            if (await _fluentValidationValidatorAdd!.ValidateAsync())
            {
                if (BeneficiariesList != null && BeneficiariesList.Count >= 6)
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Info",
                        Description = "You can not add more than 5 Beneficiaries",
                        NotificationType = NotificationType.Error
                    });
                }
                else
                {
                    Model.profileId = MemberProfileModel.Id;
                    Model.phone = "+234" + Model.phone;
                    var rsp =
                        await DataService.AddNewBeneficiary<MyBeneficiaryCreateModel, CommandResult<string>>(Model);
                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(Model)}");
                    if (!rsp.IsSuccessStatusCode)
                    {
                        await DisplayErrorAfterNewBeneficiaryAdditionAsync(rsp);
                    }
                    else
                    {
                        showAddDrawer = false;
                        await GetUserBeneficiary();
                        await InvokeAsync(StateHasChanged);
                    }
                }
            }
        }

        async Task onAddDone()
        {
            showAddDrawer = false;
        }

        async Task onEditDone()
        {
            showEditDrawer = false;
        }

        async Task OnAddBeneficiary()
        {
            showAddDrawer = true;
        }

        async Task OnclodeModal()
        {
            showPopup = false;
        }
    }
}