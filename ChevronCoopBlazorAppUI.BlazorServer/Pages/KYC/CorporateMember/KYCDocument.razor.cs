using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Syncfusion.Blazor.Inputs;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.KYC.CorporateMember
{
    public partial class KYCDocument
    {
        public MemberProfileViewModelResult Model { get; set; }

        [Inject]

        NavigationManager NavigationManager { get; set; }

        [Inject]

        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        string UserSID { get; set; }

        string ProfileImageURL { get; set; }

        string PassportURL { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        ILogger<KYCDocument> Logger { get; set; }

        [Inject]

        IWebHostEnvironment Environment { get; set; }

        public MemberProfileMasterView ProfileModel { get; set; }

        private void OnChange1(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {
                var fileName = UserSID + "photo" + file.FileInfo.Name;

                var path = Environment.ContentRootPath + "\\Pages\\KYC\\CorporateMember\\Photos\\" + fileName;


                ProfileImageURL = $"//Pages//KYC//CorporateMember//Photo//{fileName}";
                FileStream filestream = new FileStream(path, FileMode.Create, FileAccess.Write);
                file.Stream.WriteTo(filestream);
                filestream.Close();
                file.Stream.Close();
            }
        }

        private void OnChange2(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {
                var fileName = UserSID + "id" + file.FileInfo.Name;


                var path = Environment.ContentRootPath + "\\Pages\\KYC\\CorporateMember\\IDs\\" + fileName;

                PassportURL = $"//Pages//KYC//CorporateMember//IDs//{fileName}";
                FileStream filestream = new FileStream(path, FileMode.Create, FileAccess.Write);
                file.Stream.WriteTo(filestream);
                filestream.Close();
                file.Stream.Close();
            }
        }

        public async Task GoToEnd()
        {
            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            if (!string.IsNullOrEmpty(Principal))
            {
                var claims = authenticated.User.Claims;
                if (claims.Any())
                {
                    //if (authenticated.User.IsInRole("Regular") || authenticated.User.IsInRole("Coop Memeber"))
                    // {
                    var sid = claims.ElementAt(2).Value;
                    UserSID = sid;
                    // await sessionStorage.SetAsync("ApplicationUserID", sid);
                    //var rsp = await DataService.GetMemberProfiles<IEnumerable<MemberProfileModel>>();
                    var rsp = await DataService.GetMemberProfileViewResult<IEnumerable<MemberProfileViewModelResult>>();


                    if (rsp.IsSuccessStatusCode)
                    {
                        IEnumerable<MemberProfileViewModelResult> memberProfiles = rsp.Content;

                        if (memberProfiles.Any())
                        {
                            var returnedProfile = memberProfiles.FirstOrDefault(p => p.applicationUserId == sid);

                            if (returnedProfile != null)
                            {
                                Model = returnedProfile;
                                Model.profileImageUrl = ProfileImageURL;
                                var memberProfile = new UpdateBiodataItem();


                                memberProfile.id = Model.id != null ? Model.id : String.Empty;
                                memberProfile.description =
                                    Model.description != null ? Model.description : String.Empty;
                                memberProfile.comments = String.Empty;
                                memberProfile.isActive = Model.isActive == true ? true : false;
                                memberProfile.createdByUserId =
                                    Model.createdByUserId != null ? Model.createdByUserId : UserSID;
                                memberProfile.dateCreated =
                                    Model.dateCreated != null ? Model.dateCreated : DateTime.Now;
                                memberProfile.updatedByUserId = Model.updatedByUserId != null
                                    ? Convert.ToString(Model.updatedByUserId)
                                    : UserSID;
                                memberProfile.dateUpdated = Model.dateUpdated != null
                                    ? Convert.ToDateTime(Model.dateUpdated)
                                    : DateTime.Now;
                                memberProfile.deletedByUserId = Model.deletedByUserId != null
                                    ? Model.deletedByUserId
                                    : String.Empty;
                                memberProfile.isDeleted = Model.isDeleted == true ? true : false;
                                memberProfile.dateDeleted = Model.dateDeleted != null
                                    ? Model.dateDeleted
                                    : new DateTime(1900, 1, 1);
                                memberProfile.rowVersion = Model.rowVersion != null ? Model.rowVersion : String.Empty;
                                memberProfile.fullText = Model.fullText != null ? Model.fullText : String.Empty;
                                memberProfile.tags = Model.tags != null ? Model.tags : String.Empty;
                                memberProfile.caption = Model.caption != null ? Model.caption : String.Empty;
                                memberProfile.lastName = Model.lastName != null ? Model.lastName : String.Empty;
                                memberProfile.firstName = Model.firstName != null ? Model.firstName : String.Empty;
                                memberProfile.middleName = Model.middleName != null ? Model.middleName : String.Empty;
                                memberProfile.gender = Model.gender != null ? Model.gender : String.Empty;
                                memberProfile.primaryEmail =
                                    Model.primaryEmail != null ? Model.primaryEmail : String.Empty;
                                memberProfile.secondaryEmail =
                                    Model.secondaryEmail != null ? Model.secondaryEmail : String.Empty;
                                memberProfile.primaryPhone =
                                    Model.primaryPhone != null ? Model.primaryPhone : String.Empty;
                                memberProfile.secondaryPhone =
                                    Model.secondaryPhone != null ? Model.secondaryPhone : String.Empty;
                                memberProfile.membershipId =
                                    Model.membershipId != null ? Model.membershipId : String.Empty;
                                memberProfile.cai = Model.cai != null ? Model.cai : String.Empty;
                                memberProfile.retireeNumber =
                                    Model.retireeNumber != null ? Model.retireeNumber : String.Empty;
                                memberProfile.residentialAddress = Model.residentialAddress != null
                                    ? Model.residentialAddress
                                    : String.Empty;
                                memberProfile.officeAddress =
                                    Model.officeAddress != null ? Model.officeAddress : String.Empty;
                                memberProfile.rank = Model.rank != null ? Model.rank : String.Empty;
                                memberProfile.departmentId =
                                    Model.departmentId != null ? Model.departmentId : String.Empty;
                                memberProfile.jobRole = Model.jobRole != null ? Model.jobRole : String.Empty;
                                memberProfile.applicationUserId = Model.applicationUserId != null
                                    ? Model.applicationUserId
                                    : String.Empty;
                                memberProfile.status = Model.status != null ? Model.status : String.Empty;
                                memberProfile.profileImageUrl = ProfileImageURL;
                                memberProfile.passportUrl = PassportURL;
                                memberProfile.country = Model.country != null ? Model.country : String.Empty;
                                memberProfile.state = Model.state != null ? Model.state : String.Empty;
                                memberProfile.stateOfOrigin =
                                    Model.stateOfOrigin != null ? Model.stateOfOrigin : String.Empty;
                                memberProfile.isKycStarted = Model.isKycStarted == true ? true : false;

                                memberProfile.kycStartDate = Model.kycStartDate != null ? Model.kycStartDate : null;
                                if (!string.IsNullOrEmpty(ProfileImageURL) && !string.IsNullOrEmpty(PassportURL))
                                {
                                    memberProfile.isKycCompleted = true;
                                    memberProfile.kycCompletedDate = DateTime.Now;
                                }
                                else
                                {
                                    memberProfile.isKycCompleted = false;
                                    memberProfile.kycCompletedDate = null;
                                }


                                var rsp9 = await DataService
                                    .UpdateMemberProfile<UpdateBiodataItem, CommandResult<string>>(memberProfile);


                                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(memberProfile)}");
                                if (!rsp.IsSuccessStatusCode)
                                {
                                    var rspContent =
                                        JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

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
                                    //notificationText = $"Corporate Member Biodata Successfully Updated!";
                                    // showPopup = true;


                                    NavigationManager.NavigateTo("/kyc/kyc-information", true);
                                }


                                NavigationManager.NavigateTo("/kyc/kyc-information", true);


                                // await sessionStorage.SetAsync("UserProfileID", returnedProfile.id);
                            }
                        }
                    }
                    // }
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

            NavigationManager.NavigateTo("/kyc/kyc-information", true);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Model = new MemberProfileViewModelResult();
            ProfileImageURL = "";
            PassportURL = "";

            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            if (!string.IsNullOrEmpty(Principal))
            {
                var claims = authenticated.User.Claims;
                if (claims.Any())
                {
                    await GetProfileDetail();
                    //if (authenticated.User.IsInRole("Regular") || authenticated.User.IsInRole("Coop Memeber"))
                    //{
                    //var sid = claims.ElementAt(2).Value;
                    //UserSID = sid;
                    //// await sessionStorage.SetAsync("ApplicationUserID", sid);
                    ////var rsp = await DataService.GetMemberProfiles<IEnumerable<MemberProfileModel>>();
                    //var rsp = await DataService.GetMemberProfileViewResult<IEnumerable<MemberProfileViewModelResult>>();


                    //if (rsp.IsSuccessStatusCode)
                    //{
                    //    IEnumerable<MemberProfileViewModelResult> memberProfiles = rsp.Content;

                    //    if (memberProfiles.Any())
                    //    {


                    //        var returnedProfile = memberProfiles.FirstOrDefault(p => p.applicationUserId == sid);

                    //        if (returnedProfile != null)
                    //        {
                    //            Model = returnedProfile;


                    //            // await sessionStorage.SetAsync("UserProfileID", returnedProfile.id);


                    //        }
                    //    }
                    //}
                    //}
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

        private async Task OnSave()
        {
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
                ProfileImageUrl = ProfileImageURL,
                PassportUrl = PassportURL,
                DepartmentId = ProfileModel.DepartmentId,
                Rank = ProfileModel.Rank
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


                //NavigationManager.NavigateTo("/Identity/Account/RefreshDashBoard", true);
                //NavigationManager.NavigateTo("/kyc/kyc-information", true);
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