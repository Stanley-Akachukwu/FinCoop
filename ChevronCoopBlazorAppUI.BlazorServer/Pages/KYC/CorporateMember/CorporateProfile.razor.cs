using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.KYC.CorporateMember
{
    public partial class CorporateProfile
    {
        [Inject]

        ProtectedSessionStorage sessionStorage { get; set; }

        //public UpdateMemberProfileCommand Model { get; set; }
        string notificationText;
        bool showPopup = false;

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        ILogger<CorporateProfile> Logger { get; set; }

        protected string UserProfileID { get; set; }

        public MemberProfileMasterView Model { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }


        [Inject]

        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]

        NavigationManager NavigationManager { get; set; }

        string UserSID { get; set; }

        public List<GenderViewModel> Genders { get; set; }


        private async Task OnSave()
        {
            UpdateMemberProfileCommand command = new UpdateMemberProfileCommand()
            {
                FirstName = Model.FirstName,
                LastName = Model.LastName,
                Gender = Model.Gender,
                Country = Model.Country,
                State = Model.State,
                StateOfOrigin = Model.StateOfOrigin,
                SecondaryEmail = Model.SecondaryEmail,
                SecondaryPhone = Model.SecondaryPhone,
                PrimaryPhone = Model.PrimaryPhone,
                MiddleName = Model.MiddleName,
                MembershipId = Model.MembershipId,
                CAI = Model.CAI,
                OfficeAddress = Model.OfficeAddress,
                ResidentialAddress = Model.ResidentialAddress,
                JobRole = Model.JobRole,
                Id = Model.Id,
                ApplicationUserId = Model.ApplicationUserId,
                Status = Model.Status,
                DepartmentId = Model.DepartmentId,
                Rank = Model.Rank,
                ProfileImageUrl = Model.ProfileImageUrl,
                PassportUrl = Model.PassportUrl,
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


                NavigationManager.NavigateTo("/KYC/NOK-SWITCH", true);
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
                        // if (authenticated.User.IsInRole("Regular") || authenticated.User.IsInRole("Coop Memeber"))
                        //{
                        var sid = claims.ElementAt(2).Value;
                        await sessionStorage.SetAsync("ApplicationUserID", sid);
                        UserProfileID = Model.Id;


                        //}
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
            Model = new MemberProfileMasterView();

            Genders = new List<GenderViewModel>();
            Genders.Add(new GenderViewModel { Code = "MALE", Name = "MALE" });
            Genders.Add(new GenderViewModel { Code = "FEMALE", Name = "FEMALE" });

            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            if (!string.IsNullOrEmpty(Principal))
            {
                var claims = authenticated.User.Claims;
                if (claims.Any())
                {
                    // if (authenticated.User.IsInRole("Regular")|| authenticated.User.IsInRole("Coop Memeber"))
                    // {
                    var sid = claims.ElementAt(2).Value;
                    UserSID = sid;
                    //var payload = $"filter=applicationUserId eq '{sid}'";
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
                            Model = rspResponse.FirstOrDefault();
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