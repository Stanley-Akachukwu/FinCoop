using AntDesign;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.CoopCustomers.CustomerBankAccounts;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBeneficiaries;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberNextOfKins;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;


namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.Enrolment
{
    public partial class ViewCompletedKyc
    {
        [Parameter]
        public string id { get; set; }

        bool showPersonalInfo { get; set; } = false;
        bool showKYCDetail { get; set; } = false;
        public string ApplicationUserId { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        public MemberProfileMasterView MemberProfile { get; set; }
        public string Passport { get; set; } = "Passport.jpg";
        public string KycDoc { get; set; } = "KycDoc.jpg";


        [Inject]
        IEntityDataService DataService { get; set; }

        [Inject]

        NavigationManager NavigationManager { get; set; }

        public List<MemberNextOfKinMasterView> NextOfKinMasterViews { get; set; }
        public List<MemberBeneficiaryMasterView> MemberBeneficiaryMasterView { get; set; }
        public CustomerBankAccountMasterView CustomerBankDetail { get; set; }

        protected override async Task OnInitializedAsync()
        {
            showPersonalInfo = true;
            ApplicationUserId = id;
            CustomerBankDetail = new CustomerBankAccountMasterView();
            MemberBeneficiaryMasterView = new List<MemberBeneficiaryMasterView>();
            NextOfKinMasterViews = new List<MemberNextOfKinMasterView>();
            MemberProfile = new MemberProfileMasterView();
            await GetMemberProfile();
            await GetUserNextOfKin();
            await GetUserBeneficiary();
            await GetUserBankAccount();
        }

        public async Task GetMemberProfile()
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
                }
            }
        }


        public async Task DownloadFile(string base64, string filename)
        {
            try
            {
                var bytes = Convert.FromBase64String(base64.Substring(22));

                string fileName = filename;
                string contentType = "application/octet-stream";
                await js.InvokeVoidAsync("saveAsFile", fileName, MemberProfile.ProfileImageUrl.Substring(22));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task GetUserNextOfKin()
        {
            var payload = MemberProfile.Id;
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
                }
            }
        }

        private async Task GetUserBeneficiary()
        {
            var payload = MemberProfile.Id;
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
                }
            }
        }

        private async Task GetUserBankAccount()
        {
            var payload = MemberProfile.Id;
            var rsp = await DataService.GetCustomerBankAccountValue<List<CustomerBankAccountMasterView>>(
                nameof(CustomerBankAccountMasterView), "profileId", payload);

            if (rsp.IsSuccessStatusCode)
            {
                List<CustomerBankAccountMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<CustomerBankAccountMasterView>>(rsp.Content.ToJson());

                if (rspResponse?.Count > 0)
                {
                    CustomerBankDetail = rspResponse.FirstOrDefault();
                }
            }
        }

        private async Task OnShowPersonalInfo()
        {
            showPersonalInfo = true;
            showKYCDetail = false;
        }

        private async Task OnShowKYCDetail()
        {
            showPersonalInfo = false;
            showKYCDetail = true;
        }

        public async Task GoBack()
        {
            await js.InvokeVoidAsync("history.back");
            // NavigationManager.NavigateTo($"/security/data-migration", true);
        }
    }
}