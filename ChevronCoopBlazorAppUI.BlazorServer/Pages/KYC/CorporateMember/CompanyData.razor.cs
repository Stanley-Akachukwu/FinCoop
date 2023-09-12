using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Syncfusion.Blazor.Data;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.KYC.CorporateMember
{
    public partial class CompanyData
    {
        [Inject]

        ProtectedSessionStorage sessionStorage { get; set; }

        private Query Query_Combo; // = new Query();

        //public UpdateMemberProfileCommand Model { get; set; }
        string notificationText;
        bool showPopup = false;

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        ILogger<CorporateProfile> Logger { get; set; }

        protected string UserProfileID { get; set; }

        public MemberProfileViewModelResult Model { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }


        [Inject]

        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]

        NavigationManager NavigationManager { get; set; }

        string UserSID { get; set; }
        public MemberProfileMasterView ProfileModel { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        private async Task OnSave()
        {
            bool hasBeenDeleted = false;
            if (Model.isDeleted == true)
            {
                hasBeenDeleted = true;
            }

            UpdateMemberProfileCommand command = new UpdateMemberProfileCommand()
            {
                FirstName = ProfileModel.FirstName,
                LastName = ProfileModel.LastName,
                Gender = ProfileModel.Gender,
                Country = ProfileModel.Country,
                State = ProfileModel.State,
                StateOfOrigin = ProfileModel.StateOfOrigin,
                SecondaryEmail = ProfileModel.SecondaryEmail,
                SecondaryPhone = ProfileModel.SecondaryPhone,
                //PrimaryEmail=Model.PrimaryEmail,
                PrimaryPhone = ProfileModel.PrimaryPhone,
                MiddleName = ProfileModel.MiddleName,
                MembershipId = ProfileModel.MembershipId,
                CAI = ProfileModel.CAI,
                OfficeAddress = ProfileModel.OfficeAddress,
                ResidentialAddress = ProfileModel.ResidentialAddress,
                JobRole = ProfileModel.JobRole,
                Id = ProfileModel.Id,
                ApplicationUserId = ProfileModel.ApplicationUserId,
                Status = ProfileModel.Status,
                DepartmentId = ProfileModel.DepartmentId,
                Rank = ProfileModel.Rank,
            };
            // await sessionStorage.SetAsync("UserProfileID", memberProfile.id);
            var rsp = await DataService
                .UpdateMemberProfile<UpdateMemberProfileCommand, CommandResult<MemberProfileViewModel>>(command);

            Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(command)}");
            if (!rsp.IsSuccessStatusCode)
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
            else
            {
                //notificationText = $"Corporate Member Biodata Successfully Updated!";
                // showPopup = true;


                NavigationManager.NavigateTo("/kyc/member-account-data");
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var Principal = authenticated.User.Identity.Name;
                if (!string.IsNullOrEmpty(Principal))
                {
                    var claims = authenticated.User.Claims;
                    if (claims.Any())
                    {
                        //if (!authenticated.User.IsInRole("Regular") || authenticated.User.IsInRole("Coop Memeber"))
                        //{
                        var sid = claims.ElementAt(2).Value;
                        await sessionStorage.SetAsync("ApplicationUserID", sid);
                        UserProfileID = Model.id;


                        // }
                    }
                    else
                    {
                        NavigationManager.NavigateTo("/Identity/Account/LogIn");
                    }
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Model = new MemberProfileViewModelResult();
            ProfileModel = new MemberProfileMasterView();

            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            if (!string.IsNullOrEmpty(Principal))
            {
                var claims = authenticated.User.Claims;
                if (claims.Any())
                {
                    await GetProfileDetail();
                    //// if (authenticated.User.IsInRole("Regular") || authenticated.User.IsInRole("Coop Memeber"))
                    // //{
                    //     var sid = claims.ElementAt(2).Value;
                    //     UserSID = sid;
                    //     // await sessionStorage.SetAsync("ApplicationUserID", sid);
                    //     //var rsp = await DataService.GetMemberProfiles<IEnumerable<MemberProfileModel>>();
                    //     var rsp = await DataService.GetMemberProfileViewResult<IEnumerable<MemberProfileViewModelResult>>();


                    //     if (rsp.IsSuccessStatusCode)
                    //     {
                    //         IEnumerable<MemberProfileViewModelResult> memberProfiles = rsp.Content;

                    //         if (memberProfiles.Any())
                    //         {


                    //             var returnedProfile = memberProfiles.FirstOrDefault(p => p.applicationUserId == sid);

                    //             if (returnedProfile != null)
                    //             {
                    //                 Model = returnedProfile;

                    //                 if(Model.fullName.ToLower().Trim()=="string")
                    //                 {
                    //                     Model.fullName = "";
                    //                 }

                    //                 if (Model.jobRole.ToLower().Trim() == "string")
                    //                 {
                    //                     Model.jobRole = "";
                    //                 }

                    //                 if (Model.cai.ToLower().Trim() == "string")
                    //                 {
                    //                     Model.cai = "";
                    //                 }

                    //                 if (Model.rank.ToLower() == "string")
                    //                 {
                    //                     Model.rank = "";
                    //                 }

                    //                 if (Model.departmentId_Name.ToLower() == "string")
                    //                 {
                    //                     Model.departmentId_Name = "";
                    //                 }


                    //                 // await sessionStorage.SetAsync("UserProfileID", returnedProfile.id);


                    //             }
                    //         }
                    //     }
                    // //}
                }
            }
            else
            {
                NavigationManager.NavigateTo("/Identity/Account/LogIn");
            }
        }

        private async Task GetProfileDetail()
        {
            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            if (!string.IsNullOrEmpty(Principal))
            {
                var claims = authenticated.User.Claims;
                if (claims.Any())
                {
                    var sid = claims.ElementAt(2).Value;
                    UserSID = sid;
                    var payload = sid;
                    var rsp = await DataService.GetValue<List<MemberProfileMasterView>>(
                        nameof(MemberProfileMasterView), payload);


                    if (rsp.IsSuccessStatusCode)
                    {
                        List<MemberProfileMasterView> rspResponse =
                            JsonSerializer.Deserialize<List<MemberProfileMasterView>>(rsp.Content.ToJson());
                        if (rspResponse != null && rspResponse.Count > 0 &&
                            !string.IsNullOrEmpty(rspResponse.FirstOrDefault().ApplicationUserId) &&
                            rspResponse.FirstOrDefault().ApplicationUserId == sid)
                        {
                            ProfileModel = rspResponse.FirstOrDefault();
                        }
                    }
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
        }
    }
}