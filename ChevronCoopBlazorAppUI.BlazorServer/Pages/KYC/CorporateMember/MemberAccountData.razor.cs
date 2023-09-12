using AntDesign;
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
    public partial class MemberAccountData
    {
        [Inject]

        ProtectedSessionStorage sessionStorage { get; set; }

        //public UpdateMemberProfileCommand Model { get; set; }
        string notificationText;
        bool showPopup = false;

        [Inject]
        NotificationService notificationService { get; set; }

        protected IEnumerable<BankViewResult> banks { get; set; } = new List<BankViewResult>();

        [Inject]
        ILogger<CorporateProfile> Logger { get; set; }

        protected string UserProfileID { get; set; }

        public MemberProfileMasterView ProfileModel { get; set; }

        public CustomerAccountUpdateModel Model { get; set; }


        [Inject]
        IEntityDataService DataService { get; set; }


        [Inject]

        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]

        NavigationManager NavigationManager { get; set; }

        string UserSID { get; set; }

        protected string AccountName { get; set; }

        //comment
        //Another comment


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
                        //if (authenticated.User.IsInRole("Regular") || authenticated.User.IsInRole("Coop Memeber"))
                        //{
                        var sid = claims.ElementAt(2).Value;
                        await sessionStorage.SetAsync("ApplicationUserID", sid);
                        UserProfileID = Model.id;


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
            Model = new CustomerAccountUpdateModel();
            ProfileModel = new MemberProfileMasterView();
            banks = new List<BankViewResult>();
            var rspBank = await DataService.GetBanks<IEnumerable<BankViewResult>>();
            if (rspBank.IsSuccessStatusCode)
            {
                IEnumerable<BankViewResult> returnedBanks = rspBank.Content;
                if (returnedBanks.Any())
                {
                    banks = returnedBanks.ToList();
                }
            }


            var authenticated = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var Principal = authenticated.User.Identity.Name;

            if (!string.IsNullOrEmpty(Principal))
            {
                var claims = authenticated.User.Claims;
                if (claims.Any())
                {
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
                    //            ProfileModel = returnedProfile;


                    //            Model.profileId = returnedProfile.id;
                    //            UserProfileID = returnedProfile.id;
                    //            AccountName = returnedProfile.firstName + " " + returnedProfile.lastName;


                    //            // await sessionStorage.SetAsync("UserProfileID", returnedProfile.id);


                    //        }
                    //    }
                    //}
                    await GetProfileDetail();
                    await GetUserBankAccount();
                    //var rsp2 = await DataService.GetCustomerBankAccounts<IEnumerable<CustomerBankAccountViewResult>>();
                    //if (rsp2.IsSuccessStatusCode)
                    //{
                    //    IEnumerable<CustomerBankAccountViewResult> customerAccounts = rsp2.Content;

                    //    if (customerAccounts.Any())
                    //    {
                    //        var returnedCustomer = customerAccounts.FirstOrDefault(p => p.profileId == Model.profileId);
                    //        if (returnedCustomer != null)
                    //        {
                    //            //UpdateModel.rowVersion = returnedCustomer.rowVersion;
                    //            Model.accountNumber = returnedCustomer.accountNumber;
                    //            Model.bankId = returnedCustomer.bankId;
                    //            Model.accountName = returnedCustomer.accountName;
                    //            Model.accountNumber = returnedCustomer.accountNumber;
                    //            Model.branch = returnedCustomer.branch;
                    //            Model.caption = returnedCustomer.caption;

                    //        }
                    //    }
                    //    else
                    //    {
                    //        Model = new CustomerAccountUpdateModel();
                    //        Model.profileId = UserProfileID;
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

        private async Task GetUserBankAccount()
        {
            //     var payload = ProfileModel.Id;
            //     var rsp = await DataService.GetCustomerBankAccountValue<List<CustomerBankAccountMasterView>>(
            //nameof(CustomerBankAccountMasterView), payload);

            //     if (rsp.IsSuccessStatusCode)
            //     {
            //         List<CustomerBankAccountMasterView> rspResponse = JsonSerializer.Deserialize<List<CustomerBankAccountMasterView>>(rsp.Content.ToJson());

            //         if (rspResponse?.Count > 0)
            //         {
            //             var customerBank = rspResponse.FirstOrDefault();
            //             Model = new CustomerAccountUpdateModel()
            //             {
            //                 accountName = customerBank.AccountName,
            //                 accountNumber = customerBank.AccountNumber,
            //                 bankId = customerBank.BankId,
            //                 //sortCode = customerBank.SortCode,
            //                 branch = customerBank.Branch,
            //                 profileId = customerBank.CustomerId,
            //                 id = customerBank.Id,
            //             };
            //         }

            //     }
        }

        private async Task OnSave()
        {
            //if (!string.IsNullOrEmpty(Model.id))
            //{
            //    UpdateCustomerBankAccountCommand command = new UpdateCustomerBankAccountCommand()
            //    {
            //        AccountName = Model.accountName,
            //        AccountNumber = Model.accountNumber,
            //        BankId = Model.bankId,
            //        Branch = Model.branch,
            //        ProfileId = Model.profileId,
            //        Id = Model.id,
            //        SortCode = Model.sortCode,
            //    };
            //    var rsp = await DataService.UpdateBankAccount<UpdateCustomerBankAccountCommand, CommandResult<string>>(command);

            //    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(command)}");
            //    if (!rsp.IsSuccessStatusCode)
            //    {
            //        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

            //        var msg = rspContent?.Response;
            //        if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
            //        {
            //            msg = rspContent.ValidationErrors[0].Error;
            //        }
            //        await notificationService.Open(new NotificationConfig()
            //        {
            //            Message = "Error",
            //            Description = msg,
            //            NotificationType = NotificationType.Error
            //        });
            //    }
            //    else
            //    {
            //        await notificationService.Open(new NotificationConfig()
            //        {
            //            Message = "Success",
            //            Description = "Operation successful",
            //            NotificationType = NotificationType.Success
            //        });
            //        NavigationManager.NavigateTo("/kyc/kyc-document");
            //    }
            //}
            //else
            //{
            //    CreateCustomerBankAccountCommand command = new CreateCustomerBankAccountCommand()
            //    {
            //        AccountName = Model.accountName,
            //        AccountNumber = Model.accountNumber,
            //        BankId = Model.bankId,
            //        Branch = Model.branch,
            //        ProfileId = ProfileModel.Id,
            //        SortCode = Model.sortCode,

            //    };
            //    var rsp = await DataService.AddNewBankAccount<CreateCustomerBankAccountCommand, CommandResult<CustomerBankAccountViewModel>>(command);

            //    Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(command)}");
            //    if (!rsp.IsSuccessStatusCode)
            //    {
            //        var rspContent = JsonSerializer.Deserialize<CommandResult<string>>(rsp.Error.Content);

            //        var msg = rspContent?.Response;
            //        if (rspContent != null && rspContent.ValidationErrors != null && rspContent.ValidationErrors.Any())
            //        {
            //            msg = rspContent.ValidationErrors[0].Error;
            //        }
            //        await notificationService.Open(new NotificationConfig()
            //        {
            //            Message = "Error",
            //            Description = msg,
            //            NotificationType = NotificationType.Error
            //        });
            //    }
            //    else
            //    {
            //        //await notificationService.Open(new NotificationConfig()
            //        //{
            //        //    Message = "Success",
            //        //    Description = "Operation successful",
            //        //    NotificationType = NotificationType.Success
            //        //});
            //        NavigationManager.NavigateTo("/kyc/kyc-document");
            //    }
            //}
        }
    }
}