using AntDesign;
using AP.ChevronCoop.Commons;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Refit;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.KYC.CorporateMember
{
    public partial class Beneficiary
    {
        [Inject]

        ProtectedSessionStorage sessionStorage { get; set; }

        //public UpdateMemberProfileCommand Model { get; set; }
        string notificationText;
        bool showPopup = false;

        [Inject]
        NotificationService notificationService { get; set; }

        [Inject]
        ILogger<NextOfKin> Logger { get; set; }

        public BeneficiaryViewResult Model { get; set; }

        public MyBeneficiaryCreateModel CreateModel { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }


        [Inject]

        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]

        NavigationManager NavigationManager { get; set; }

        protected string UserSID { get; set; }

        protected string ProfileID { get; set; }

        List<NextOfKinRelationships> KinRelationships { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                //var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                //var Principal = authenticated.User.Identity.Name;

                //if (!string.IsNullOrEmpty(Principal))
                //{

                //    var claims = authenticated.User.Claims;
                //    if (claims.Any())
                //    {
                //        if (authenticated.User.IsInRole("Regular"))
                //        {
                //            var sid = claims.ElementAt(2).Value;
                //            UserSID = sid;
                //           // CreateModel.createdByUserId = sid;
                //            //var rsp = await DataService.GetMemberProfiles<IEnumerable<MemberProfileModel>>();

                //            await ReloadGridAsync(sid);
                //        }
                //        else
                //        {
                //            //NavigationManager.NavigateTo("/dashboard");
                //        }

                //    }
                //}
            }
        }


        private async Task OnSave()
        {
            if (!BeneficiariesList.Any())
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Input Validation Error!",
                    Description = "You Can Not Proceed To The Company Data Screen Until You Have Entered A Next Of Kin",
                    NotificationType = NotificationType.Error
                });
                return;
            }
            else
            {
                var rsp1 = await DataService.GetMemberProfileViewResult<IEnumerable<MemberProfileViewModelResult>>();
                if (rsp1.IsSuccessStatusCode)
                {
                    IEnumerable<MemberProfileViewModelResult> memberProfiles = rsp1.Content;

                    if (memberProfiles.Any())
                    {
                        var returnedProfile = memberProfiles.FirstOrDefault(p => p.applicationUserId == UserSID);

                        if (returnedProfile != null)
                        {
                            CreateModel.profileId = returnedProfile.id;


                            // await sessionStorage.SetAsync("UserProfileID", returnedProfile.id);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(CreateModel.firstName) && !string.IsNullOrEmpty(CreateModel.lastName) &&
                    !string.IsNullOrEmpty(CreateModel.phone) && !string.IsNullOrEmpty(CreateModel.address))
                {
                    var rsp =
                        await DataService.AddNewBeneficiary<MyBeneficiaryCreateModel, CommandResult<string>>(
                            CreateModel);
                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(CreateModel)}");
                    if (!rsp.IsSuccessStatusCode)
                    {
                        await DisplayErrorAfterNewBeneficiaryAdditionAsync(rsp);
                    }
                    else
                    {
                        //notificationText = $"Corporate Member Biodata Successfully Updated!";
                        // showPopup = true;

                        //await ReloadGridAsync(UserSID);
                        NavigationManager.NavigateTo("/kyc/company-data");
                    }

                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Input Validation Error!",
                        Description = "All The Fields On The Beneficiary Form Are Compulsory!",
                        NotificationType = NotificationType.Error
                    });
                    return;
                }

                else
                {
                    NavigationManager.NavigateTo("/kyc/company-data", true);
                }
            }
        }

        private async Task OnAddNewBeneficiary()
        {
            if (string.IsNullOrEmpty(CreateModel.firstName) || string.IsNullOrEmpty(CreateModel.lastName) ||
                string.IsNullOrEmpty(CreateModel.phone) || string.IsNullOrEmpty(CreateModel.address))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Input Validation Error!",
                    Description = "All The Fields On The Beneficiary Screen Are Compulsory!",
                    NotificationType = NotificationType.Error
                });
                return;
            }
            else
            {
                var rsp1 = await DataService.GetMemberProfileViewResult<IEnumerable<MemberProfileViewModelResult>>();
                if (rsp1.IsSuccessStatusCode)
                {
                    IEnumerable<MemberProfileViewModelResult> memberProfiles = rsp1.Content;

                    if (memberProfiles.Any())
                    {
                        var returnedProfile = memberProfiles.FirstOrDefault(p => p.applicationUserId == UserSID);

                        if (returnedProfile != null)
                        {
                            CreateModel.profileId = returnedProfile.id;


                            // await sessionStorage.SetAsync("UserProfileID", returnedProfile.id);
                        }
                    }
                }

                var rsp =
                    await DataService.AddNewBeneficiary<MyBeneficiaryCreateModel, CommandResult<string>>(CreateModel);
                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(CreateModel)}");
                if (!rsp.IsSuccessStatusCode)
                {
                    await DisplayErrorAfterNewBeneficiaryAdditionAsync(rsp);
                }
                else
                {
                    //notificationText = $"Corporate Member Biodata Successfully Updated!";
                    // showPopup = true;

                    await ReloadGridAsync(CreateModel.profileId);
                    CreateModel.firstName = String.Empty;
                    CreateModel.lastName = String.Empty;
                    CreateModel.phone = String.Empty;
                    CreateModel.address = String.Empty;
                    //CreateModel.relationship = String.Empty;
                }
            }
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

        protected BeneficiaryData[] Beneficiaries { get; set; }

        protected List<BeneficiaryData> BeneficiariesList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            BeneficiariesList = new List<BeneficiaryData>();


            CreateModel = new MyBeneficiaryCreateModel();
            CreateModel.fullText = "";
            CreateModel.caption = "";
            CreateModel.comments = "";
            CreateModel.tags = "";
            CreateModel.dateCreated = DateTime.Now;
            CreateModel.description = "";
            CreateModel.email = "";
            CreateModel.isActive = true;


            Model = new BeneficiaryViewResult();
            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            if (!string.IsNullOrEmpty(Principal))
            {
                var claims = authenticated.User.Claims;
                if (claims.Any())
                {
                    //if (authenticated.User.IsInRole("Regular")||(authenticated.User.IsInRole("Coop Memeber")))
                    //{
                    var sid = claims.ElementAt(2).Value;
                    UserSID = sid;
                    CreateModel.createdByUserId = sid;
                    var rsp1 = await DataService
                        .GetMemberProfileViewResult<IEnumerable<MemberProfileViewModelResult>>();
                    if (rsp1.IsSuccessStatusCode)
                    {
                        IEnumerable<MemberProfileViewModelResult> memberProfiles = rsp1.Content;
                        if (memberProfiles.Any())
                        {
                            var returnedProfile = memberProfiles.FirstOrDefault(p => p.applicationUserId == sid);

                            if (returnedProfile != null)
                            {
                                ProfileID = returnedProfile.id;
                                CreateModel.profileId = returnedProfile.id;
                                // await sessionStorage.SetAsync("UserProfileID", returnedProfile.id);

                                await ReloadGridAsync(returnedProfile.id);
                            }
                        }
                    }
                    //var rsp = await DataService.GetMemberBeneficiaries<IEnumerable<BeneficiaryViewResult>>();


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

        private async Task ReloadGridAsync(string profileId)
        {
            var rsp = await DataService.GetMemberBeneficiaries<IEnumerable<BeneficiaryViewResult>>();


            if (rsp.IsSuccessStatusCode)
            {
                IEnumerable<BeneficiaryViewResult> memberBeneficiaries = rsp.Content;

                if (memberBeneficiaries.Any())
                {
                    var returnedBeneficiaries = memberBeneficiaries.Where(p => p.profileId == profileId).ToList();

                    if (returnedBeneficiaries.Any())
                    {
                        int serial = 1;
                        var beneficiaryList = new List<BeneficiaryData>();
                        foreach (var item in returnedBeneficiaries)
                        {
                            beneficiaryList.Add(new BeneficiaryData
                            {
                                Serial = serial, Address = item.address, FirstName = item.firstName,
                                LastName = item.lastName, PhoneNumber = item.phone
                            });
                            serial++;
                        }

                        BeneficiariesList = beneficiaryList;
                        Beneficiaries = beneficiaryList.ToArray();
                    }
                }
            }
        }
    }

    public class BeneficiaryData
    {
        public int Serial { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        public string Email { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string Id { get; set; }
    }
}