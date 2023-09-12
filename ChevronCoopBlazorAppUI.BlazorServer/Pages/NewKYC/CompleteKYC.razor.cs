using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.NewKYC
{
    public partial class CompleteKYC
    {
        [Inject]

        ProtectedSessionStorage sessionStorage { get; set; }

        //public UpdateMemberProfileCommand Model { get; set; }
        string notificationText;
        bool showPopup = false;

        [Inject]
        NotificationService notificationService { get; set; }

        protected string UserProfileID { get; set; }

        public MemberProfileMasterView Model { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }


        [Inject]

        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]

        NavigationManager NavigationManager { get; set; }

        public UpdateMemberProfileCommand UpdateModel;
        public MemberProfileMasterView MemberProfile;
        bool showKYCBiodata { get; set; } = false;
        bool showKYCBeneficiary { get; set; } = false;
        bool showKYCCompanyData { get; set; } = false;
        bool showKYCMemberAccount { get; set; } = false;
        bool showKYCDocument { get; set; } = false;
        bool showKYCNextOfKin { get; set; } = false;
        bool showKYCInformation { get; set; } = false;
        bool showBackButton { get; set; } = false;

        bool KYCBiodataIsComplete { get; set; } = false;
        bool KYCBeneficiaryIsComplete { get; set; } = false;
        bool KYCCompanyDataIsComplete { get; set; } = false;
        bool KYCMemberAccountIsComplete { get; set; } = false;
        bool KYCDocumentIsComplete { get; set; } = false;
        bool KYCNextOfKinIsComplete { get; set; } = false;
        string ApplicationUserId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //await base.OnInitializedAsync();
            Model = new MemberProfileMasterView();


            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            if (!string.IsNullOrEmpty(Principal))
            {
                var user = authenticated.User;

                if (user.Identity.IsAuthenticated)
                {
                    ApplicationUserId = user.FindFirstValue(ClaimTypes.Sid);
                    await GetProfile();
                }
                else
                {
                    NavigationManager.NavigateTo("/Identity/Account/LogIn");
                }
            }
            else
            {
                NavigationManager.NavigateTo("/Identity/Account/LogIn");
            }

            showKYCBiodata = true;
        }

        private async Task OnUpdateKYCBioDataChangedHandler(MemberProfileViewModel UpdateBioDataViewModel)
        {
            //UpdateModel = Mapper.Map<UpdateMemberProfileCommand>(UpdateBioDataViewModel);

            //if (UpdateModel != null)
            //{
            await GetProfile();
            showKYCNextOfKin = true;
            showKYCBiodata = false;
            showKYCBeneficiary = false;
            showKYCCompanyData = false;
            showKYCDocument = false;
            showKYCInformation = false;
            showKYCMemberAccount = false;
            showBackButton = true;
            KYCBiodataIsComplete = true;

            await InvokeAsync(StateHasChanged); // trigger a UI refresh
            //}
        }

        private async Task OnUpdateKYCNextOfKinChangedHandler(bool isComplete)
        {
            if (isComplete)
            {
                showKYCNextOfKin = false;
                showKYCBiodata = false;
                //showKYCBeneficiary = true;
                showKYCCompanyData = true;
                showKYCDocument = false;
                showKYCInformation = false;
                showKYCMemberAccount = false;
                showBackButton = true;
                KYCNextOfKinIsComplete = true;
                await InvokeAsync(StateHasChanged); // trigger a UI refresh
            }
        }

        //private async Task OnUpdateKYCBeneficiaryChangedHandler(bool isComplete)
        //{
        //    if (isComplete)
        //    {
        //        showKYCNextOfKin = false;
        //        showKYCBiodata = false;
        //        showKYCBeneficiary = false;
        //        showKYCCompanyData = true;
        //        showKYCDocument = false;
        //        showKYCInformation = false;
        //        showKYCMemberAccount = false;
        //        showBackButton = true;
        //        KYCBeneficiaryIsComplete = true;
        //        await InvokeAsync(StateHasChanged); // trigger a UI refresh
        //    }
        //}

        private async Task OnUpdateCompanyDataChangedHandler(bool isComplete)
        {
            if (isComplete)
            {
                showKYCNextOfKin = false;
                showKYCBiodata = false;
                showKYCBeneficiary = false;
                showKYCCompanyData = false;
                showKYCDocument = false;
                showKYCInformation = false;
                showKYCMemberAccount = true;
                showBackButton = true;
                KYCCompanyDataIsComplete = true;
                await InvokeAsync(StateHasChanged); // trigger a UI refresh
            }
        }

        private async Task OnUpdateMemberAccountChangedHandler(bool isComplete)
        {
            if (isComplete)
            {
                showKYCNextOfKin = false;
                showKYCBiodata = false;
                showKYCBeneficiary = false;
                showKYCCompanyData = false;
                showKYCDocument = true;
                showKYCInformation = false;
                showKYCMemberAccount = false;
                showBackButton = true;
                KYCMemberAccountIsComplete = true;
                await InvokeAsync(StateHasChanged); // trigger a UI refresh
            }
        }

        private async Task OnUpdateDocumentChangedHandler(bool isComplete)
        {
            if (isComplete)
            {
                showKYCNextOfKin = false;
                showKYCBiodata = false;
                showKYCBeneficiary = false;
                showKYCCompanyData = false;
                showKYCDocument = false;
                //showKYCInformation = true;
                showKYCMemberAccount = false;
                showBackButton = true;
                KYCDocumentIsComplete = true;
                showPopup = true;
                await InvokeAsync(StateHasChanged); // trigger a UI refresh
            }
        }

        private async Task BackAction()
        {
            if (showKYCInformation)
            {
                showKYCNextOfKin = false;
                showKYCBiodata = false;
                showKYCBeneficiary = false;
                showKYCCompanyData = false;
                showKYCDocument = true;
                showKYCInformation = false;
                showKYCMemberAccount = false;
            }
            else if (showKYCMemberAccount)
            {
                showKYCNextOfKin = false;
                showKYCBiodata = false;
                showKYCBeneficiary = false;
                showKYCCompanyData = true;
                showKYCDocument = false;
                showKYCInformation = false;
                showKYCMemberAccount = false;
            }
            else if (showKYCDocument)
            {
                showKYCNextOfKin = false;
                showKYCBiodata = false;
                showKYCBeneficiary = false;
                showKYCCompanyData = false;
                showKYCDocument = false;
                showKYCInformation = false;
                showKYCMemberAccount = true;
            }
            else if (showKYCCompanyData)
            {
                showKYCNextOfKin = true;
                showKYCBiodata = false;
                //showKYCBeneficiary = true;
                showKYCCompanyData = false;
                showKYCDocument = false;
                showKYCInformation = false;
                showKYCMemberAccount = false;
            }
            else if (showKYCBeneficiary)
            {
                showKYCNextOfKin = true;
                showKYCBiodata = false;
                showKYCBeneficiary = false;
                showKYCCompanyData = false;
                showKYCDocument = false;
                showKYCInformation = false;
                showKYCMemberAccount = false;
            }
            else if (showKYCNextOfKin)
            {
                showKYCNextOfKin = false;
                showKYCBiodata = true;
                showKYCBeneficiary = false;
                showKYCCompanyData = false;
                showKYCDocument = false;
                showKYCInformation = false;
                showKYCMemberAccount = false;
            }

            await InvokeAsync(StateHasChanged); // trigger a UI refresh
        }

        private async Task Done()
        {
            showPopup = false;
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var CurrentUser = authState.User;
            if (CurrentUser != null)
            {
                var admin = CurrentUser.Claims.Where(f => f.Type == "IsAdmin").FirstOrDefault();
                if (admin != null && admin.Value.ToLower() == "false")
                    NavigationManager.NavigateTo("/Dashboard", true);
                if (admin != null && admin.Value.ToLower() == "true")
                    NavigationManager.NavigateTo("/admin/dashboard", true);
            }
            await InvokeAsync(StateHasChanged);
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
                        Description = "Oops! Something Went Wrong. Please try again later. Thanks",
                        NotificationType = NotificationType.Error
                    });
                }
            }
            else
            {
                List<MemberProfileMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<MemberProfileMasterView>>(rsp.Content.ToJson());
                if (rspResponse != null && rspResponse.Count > 0 &&
                    !string.IsNullOrEmpty(rspResponse.FirstOrDefault().ApplicationUserId) &&
                    rspResponse.FirstOrDefault().ApplicationUserId == ApplicationUserId)
                {
                    MemberProfile = new MemberProfileMasterView();
                    MemberProfile = rspResponse.FirstOrDefault();
                    Model = Mapper.Map<MemberProfileMasterView>(rspResponse.FirstOrDefault());
                    ;
                }
            }
        }
    }
}