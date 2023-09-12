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
    public partial class NextOfKin
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

        public MemberNextOfKinViewResult Model { get; set; }

        public NextOfKinCreateModel CreateModel { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }


        [Inject]

        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]

        NavigationManager NavigationManager { get; set; }

        protected string UserSID { get; set; }

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
            if (!NextOfKinList.Any())
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Input Validation Error!",
                    Description = "You Can Not Proceed To The Beneficiary Screen Until You Have Entered A Next Of Kin",
                    NotificationType = NotificationType.Error
                });
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(CreateModel.firstName) && !string.IsNullOrEmpty(CreateModel.lastName) &&
                    !string.IsNullOrEmpty(CreateModel.phone) && !string.IsNullOrEmpty(CreateModel.address) &&
                    !string.IsNullOrEmpty(CreateModel.relationship))
                {
                    var rsp =
                        await DataService.AddNewNextOfKin<NextOfKinCreateModel, CommandResult<string>>(CreateModel);
                    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(CreateModel)}");
                    if (!rsp.IsSuccessStatusCode)
                    {
                        await DisplayErrorAfterNewNextOfKinAdditionAsync(rsp);
                    }
                    else
                    {
                        //notificationText = $"Corporate Member Biodata Successfully Updated!";
                        // showPopup = true;

                        //await ReloadGridAsync(UserSID);
                        NavigationManager.NavigateTo("/kyc/beneficiary");
                    }

                    await notificationService.Open(new NotificationConfig()
                    {
                        Message = "Input Validation Error!",
                        Description = "All The Fields On The Next Of Kin Form Are Compulsory!",
                        NotificationType = NotificationType.Error
                    });
                    return;
                }

                else
                {
                    NavigationManager.NavigateTo("/kyc/beneficiary", true);
                }
            }
        }

        private async Task OnAddNewNextOfKin()
        {
            if (string.IsNullOrEmpty(CreateModel.firstName) || string.IsNullOrEmpty(CreateModel.lastName) ||
                string.IsNullOrEmpty(CreateModel.phone) || string.IsNullOrEmpty(CreateModel.address) ||
                string.IsNullOrEmpty(CreateModel.relationship))
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = "Input Validation Error!",
                    Description = "All The Fields On The Next Of Kin Form Are Compulsory!",
                    NotificationType = NotificationType.Error
                });
                return;
            }
            else
            {
                var rsp = await DataService.AddNewNextOfKin<NextOfKinCreateModel, CommandResult<string>>(CreateModel);
                Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(CreateModel)}");
                if (!rsp.IsSuccessStatusCode)
                {
                    await DisplayErrorAfterNewNextOfKinAdditionAsync(rsp);
                }
                else
                {
                    //notificationText = $"Corporate Member Biodata Successfully Updated!";
                    // showPopup = true;

                    await ReloadGridAsync(UserSID);
                    CreateModel.firstName = String.Empty;
                    CreateModel.lastName = String.Empty;
                    CreateModel.phone = String.Empty;
                    CreateModel.address = String.Empty;
                    CreateModel.relationship = String.Empty;
                }
            }
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

        protected List<NextOfKinData> NextOfKinList { get; set; }
        protected NextOfKinData[] NextOfKins { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            NextOfKinList = new List<NextOfKinData>();

            KinRelationships = new List<NextOfKinRelationships>
            {
                new NextOfKinRelationships { Name = "Brother", Code = "Brother" },
                new NextOfKinRelationships { Name = "Sister", Code = "Sister" },
                new NextOfKinRelationships { Name = "Father", Code = "Father" },
                new NextOfKinRelationships { Name = "Mother", Code = "Mother" },
                new NextOfKinRelationships { Name = "Uncle", Code = "Uncle" },
                new NextOfKinRelationships { Name = "Aunty", Code = "Aunty" },
                new NextOfKinRelationships { Name = "Other", Code = "Other" },
            };


            CreateModel = new NextOfKinCreateModel();
            CreateModel.fullText = "";
            CreateModel.caption = "";
            CreateModel.comments = "";
            CreateModel.tags = "";
            CreateModel.dateCreated = DateTime.Now;
            CreateModel.description = "";
            CreateModel.email = "";
            CreateModel.isActive = true;


            Model = new MemberNextOfKinViewResult();
            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            if (!string.IsNullOrEmpty(Principal))
            {
                var claims = authenticated.User.Claims;
                if (claims.Any())
                {
                    //if (authenticated.User.IsInRole("Regular") || authenticated.User.IsInRole("Coop Memeber"))
                    //{
                    var sid = claims.ElementAt(2).Value;
                    UserSID = sid;
                    CreateModel.createdByUserId = sid;
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
                                CreateModel.profileId = returnedProfile.id;
                                // await sessionStorage.SetAsync("UserProfileID", returnedProfile.id);
                            }
                        }
                    }

                    await ReloadGridAsync(sid);
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

        private async Task ReloadGridAsync(string sid)
        {
            var rsp = await DataService.GetMemberNextOfKinMasterView<IEnumerable<MemberNextOfKinViewResult>>();


            if (rsp.IsSuccessStatusCode)
            {
                IEnumerable<MemberNextOfKinViewResult> memberNextOfKins = rsp.Content;

                if (memberNextOfKins != null)
                {
                    if (memberNextOfKins.Any())
                    {
                        var returnedNextOfKins =
                            memberNextOfKins.Where(p => p.profileId_ApplicationUserId == sid).ToList();

                        if (returnedNextOfKins.Any())
                        {
                            int serial = 1;
                            var nextOfKinList = new List<NextOfKinData>();
                            foreach (var item in returnedNextOfKins)
                            {
                                nextOfKinList.Add(new NextOfKinData
                                {
                                    Serial = serial, Address = item.address, FirstName = item.firstName,
                                    LastName = item.lastName, PhoneNumber = item.phone, Relationship = item.relationship
                                });
                                serial++;
                            }

                            NextOfKinList = nextOfKinList;

                            NextOfKins = nextOfKinList.ToArray();
                        }
                    }
                }
            }
        }
    }

    public class NextOfKinData
    {
        public int Serial { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Relationship { get; set; }
        public string Email { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string Id { get; set; }
    }

    public class NextOfKinCreateModel
    {
        public string description { get; set; }
        public string comments { get; set; }
        public bool isActive { get; set; }
        public string fullText { get; set; }
        public string tags { get; set; }
        public string caption { get; set; }
        public string createdByUserId { get; set; }
        public DateTime dateCreated { get; set; }
        public string profileId { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string relationship { get; set; }
        public string address { get; set; }
        public string Id { get; set; }
    }

    public class NextOfKinRelationships
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}