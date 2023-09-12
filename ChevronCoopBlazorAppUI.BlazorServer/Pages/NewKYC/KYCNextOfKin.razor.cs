using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberNextOfKins;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberNextOfKins;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using ChevronCoop.Web.AppUI.BlazorServer.Helper.Tailwindcss;
using ChevronCoop.Web.AppUI.BlazorServer.Pages.KYC.CorporateMember;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Refit;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.NewKYC
{
    public partial class KYCNextOfKin
    {
        private FluentValidationValidator? _fluentValidationValidatorAdd;
        private FluentValidationValidator? _fluentValidationValidatorEdit;
        bool showPopup = false;
        public NextOfKinCreateModel Model { get; set; }

        List<NextOfKinRelationships> KinRelationships { get; set; }

        protected List<NextOfKinData> NextOfKinList { get; set; }
        protected NextOfKinData[] NextOfKins { get; set; }

        protected string UserSID { get; set; }
        private bool DisableAddButton { get; set; } = false;

        private NextOfKinData UpdateModel { get; set; }

        //public MemberProfileMasterView ProfileModel { get; set; }
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
        ILogger<KYCNextOfKin> Logger { get; set; }

        private List<MemberNextOfKinMasterView> NextOfKinMasterViews { get; set; }

        [Parameter]
        public MemberProfileMasterView MemberProfileModel { get; set; }

        [Parameter]
        public EventCallback<MemberProfileMasterView> MemberProfileModelChanged { get; set; }

        [Parameter]
        public EventCallback<bool> OnUpdateNextOfKinChanged { get; set; }

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
            UpdateModel = new NextOfKinData();
            NextOfKinList = new List<NextOfKinData>();


            Model = new NextOfKinCreateModel();

            KinRelationships = new List<NextOfKinRelationships>
            {
                new NextOfKinRelationships { Name = "Brother", Code = "Brother" },
                new NextOfKinRelationships { Name = "Sister", Code = "Sister" },
                new NextOfKinRelationships { Name = "Father", Code = "Father" },
                new NextOfKinRelationships { Name = "Mother", Code = "Mother" },
                new NextOfKinRelationships { Name = "Uncle", Code = "Uncle" },
                new NextOfKinRelationships { Name = "Aunty", Code = "Aunty" },
                new NextOfKinRelationships { Name = "Spouse", Code = "Spouse" },
                new NextOfKinRelationships { Name = "Other", Code = "Other" },
            };
            await GetUserNextOfKin();
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

        private async Task GetUserNextOfKin()
        {
            var payload = MemberProfileModel.Id;
            var rsp = await DataService.GetNextOfKinValue<List<MemberNextOfKinMasterView>>(
                nameof(MemberNextOfKinMasterView), payload);

            if (rsp.IsSuccessStatusCode)
            {
                List<MemberNextOfKinMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<MemberNextOfKinMasterView>>(rsp.Content.ToJson());

                if (rspResponse?.Count > 0)
                {
                    NextOfKinMasterViews = new List<MemberNextOfKinMasterView>();
                    NextOfKinMasterViews = rspResponse;
                    NextOfKinList = new List<NextOfKinData>();
                    foreach (var item in rspResponse)
                    {
                        NextOfKinData eachNextOfKinRecord = new NextOfKinData()
                        {
                            Address = item.Address,
                            LastName = item.LastName,
                            Id = item.Id,
                            Relationship = item.Relationship,
                            Email = item.Email,
                            PhoneNumber = item.Phone,
                            FirstName = item.FirstName
                        };
                        NextOfKinList.Add(eachNextOfKinRecord);
                    }

                    NextOfKins = NextOfKinList.ToArray();
                    if (NextOfKinList != null && NextOfKinList.Count >= 5)
                        DisableAddButton = true;
                    if (NextOfKinList?.Count > 0)
                        HasRecords = true;
                    else
                        HasRecords = false;
                }
            }
        }

        private async Task CheckNextOfKinCount()
        {
            if (NextOfKinList != null && NextOfKinList.Count >= 6)
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
                UpdateMemberNextOfKinCommand command = new UpdateMemberNextOfKinCommand()
                {
                    FirstName = UpdateModel.FirstName,
                    LastName = UpdateModel.LastName,
                    Email = UpdateModel.Email,
                    Relationship = UpdateModel.Relationship,
                    Phone = CountryPhoneCode.Nigeria + UpdateModel.PhoneNumber,
                    Id = UpdateModel.Id,
                    Address = UpdateModel.Address,
                    ProfileId = MemberProfileModel.Id
                };

                var rsp = await DataService.NextOfKin<UpdateMemberNextOfKinCommand, CommandResult<string>>("update",
                    command);
                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(Model)}");
                if (!rsp.IsSuccessStatusCode)
                {
                    await DisplayErrorAfterNewNextOfKinAdditionAsync(rsp);
                }
                else
                {
                    showEditDrawer = false;
                    await GetUserNextOfKin();
                    await InvokeAsync(StateHasChanged);
                }
            }
        }

        private void EditNextOfKin(NextOfKinData itemToUpdate)
        {
            UpdateModel = new NextOfKinData();
            itemToUpdate.PhoneNumber = itemToUpdate.PhoneNumber.StartsWith("+234")
                ? itemToUpdate.PhoneNumber.Replace("+234", "")
                : itemToUpdate.PhoneNumber;
            UpdateModel = itemToUpdate;
            //showEditNextOfKinDrawer = true;
            showEditDrawer = true;
        }

        private async Task DisplayErrorAfterNewNextOfKinAdditionAsync(ApiResponse<CommandResult<string>> rsp)
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

        private void DeleteNextOfKin(NextOfKinData itemToDelete)
        {
            UpdateModel = new NextOfKinData();
            UpdateModel = itemToDelete;
            showPopup = true;
        }

        private async Task ConfirmDeleteNextOfKin()
        {
            var selectedNextOfKin = NextOfKinMasterViews.Where(p => p.Id == UpdateModel.Id).FirstOrDefault();
            if (selectedNextOfKin != null)
            {
                var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var Principal = authenticated.User.Identity.Name;
                var user = authenticated.User;

                if (user.Identity.IsAuthenticated)
                {
                    var userId = user.FindFirstValue(ClaimTypes.Sid);
                    DeleteMemberNextOfKinCommand deleteCommand = new DeleteMemberNextOfKinCommand()
                    {
                        Id = selectedNextOfKin.Id,
                        DateDeleted = DateTime.Now,
                        DeletedByUserId = userId
                    };
                    var rsp4 =
                        await DataService.DeleteNextOfKin<DeleteMemberNextOfKinCommand, CommandResult<string>>(
                            deleteCommand);

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
                        await GetUserNextOfKin();
                        StateHasChanged();
                        showPopup = false;
                    }
                }
            }
        }

        private async Task SaveandContinue()
        {
            await OnUpdateNextOfKinChanged.InvokeAsync(true);
        }

        private async Task Cancel()
        {
            Model = new NextOfKinCreateModel();
        }

        private async Task SubmitValidForm()
        {
            if (await _fluentValidationValidatorAdd!.ValidateAsync())
            {
                if (NextOfKinList != null && NextOfKinList.Count >= 6)
                {
                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Info",
                        Description = "You can not add more than 5 Next of Kin",
                        NotificationType = NotificationType.Error
                    });
                }
                else
                {
                    Model.profileId = MemberProfileModel.Id;
                    var rsp = await DataService.AddNewNextOfKin<NextOfKinCreateModel, CommandResult<string>>(Model);
                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(Model)}");
                    if (!rsp.IsSuccessStatusCode)
                    {
                        await DisplayErrorAfterNewNextOfKinAdditionAsync(rsp);
                    }
                    else
                    {
                        showAddDrawer = false;
                        await GetUserNextOfKin();
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

        async Task OnAddNextOfKin()
        {
            showAddDrawer = true;
        }

        async Task OnclodeModal()
        {
            showPopup = false;
        }
    }
}