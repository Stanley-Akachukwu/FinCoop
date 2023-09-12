using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Auth
{
    public partial class EmailVerified
    {
        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        ILogger<UploadReceipt> Logger { get; set; }
        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        TempObjectService tempService { get; set; }
        [Inject]
        IEntityDataService DataService { get; set; }
        [Inject]
        NavigationManager _navigationManager { get; set; }
        MemberProfileMasterView MemberProfile { get; set; }
        public RegisterMemberViewModel RegisterMemberViewModel { get; set; }
        string ErrorMessage { get; set; } = string.Empty;
        string Id { get; set; }
        string ApplicationUserId { get; set; }
        public bool isRetiree { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            RegisterMemberViewModel = new RegisterMemberViewModel();

            RegisterMemberViewModel = (RegisterMemberViewModel)tempService.GetTempObject();
            Id = RegisterMemberViewModel.MemberId;
            ApplicationUserId = RegisterMemberViewModel.UserId;

            await GetProfile();
        }
        public async Task GetProfile()
        {
            var rsp = await DataService.GetValue<List<MemberProfileMasterView>>(
                nameof(MemberProfileMasterView), ApplicationUserId);


            if (!rsp.IsSuccessStatusCode)
            {
                if (!string.IsNullOrEmpty(rsp.Error.Content))
                {
                    var rspContent =
                        System.Text.Json.JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

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
                List<MemberProfileMasterView> rspResponse =
                    System.Text.Json.JsonSerializer.Deserialize<List<MemberProfileMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().ApplicationUserId) &&
                    rspResponse.FirstOrDefault().ApplicationUserId == ApplicationUserId)
                {
                    MemberProfile = new MemberProfileMasterView();
                    MemberProfile = rspResponse.FirstOrDefault();
                    MemberProfile = Mapper.Map<MemberProfileMasterView>(rspResponse.FirstOrDefault());
                    var memberType = (MemberType)System.Enum.Parse(typeof(MemberType), MemberProfile.MemberType, true);
                    if (memberType == MemberType.RETIREE)
                    {
                        isRetiree = true;
                    }
                }
            }
        }
        public async Task OnClickReturnToApplication()
        {
            if(isRetiree)
            {
                _navigationManager.NavigateTo("/identity/account/login", forceLoad: true);
            }
            else
            {
                _navigationManager.NavigateTo("/make-payment", forceLoad: true);
            }
        }
    }
}
