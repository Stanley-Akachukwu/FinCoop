using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.MasterData.Locations;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Helper;
using CountryData;
using IntlTelInputBlazor;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.DropDowns;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.NewKYC
{
    public partial class KYCBioddata
    {
        //[IntlTelephone(ErrorMessage = "Tel. 1 incorrect format")]
        private FluentValidationValidator? _fluentValidationValidator;
        public IntlTel IntTelNumber { get; set; }

        [Parameter]
        public EventCallback<MemberProfileViewModel> OnUpdateBioDataRequestChanged { get; set; }

        [Parameter]
        public MemberProfileMasterView UpdateModel { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public EventCallback<MemberProfileMasterView> UpdateModelChanged { get; set; }

        public BioDataViewModel Model { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        public UpdateMemberProfileCommand UpdateCommand { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }


        public async Task ChangeLocation(ChangeEventArgs<string, LocationMasterView> args)
        {
            var selected = args.ItemData;
            if (selected != null)
            {
                States = await GetState(selected.Id);
            }
        }

        public IEnumerable<LocationMasterView> Countries { get; set; }
        public IEnumerable<LocationMasterView> States { get; set; }

        [Parameter]
        public MemberProfileViewModel MemberProfileModel { get; set; }


        List<ICountryInfo> AllCountriesPhoneCode = new List<ICountryInfo>();

        public string buttonTailwind { get; set; } =
            "shadow-sm bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-500 focus:border-primary-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500";

        protected override async Task OnInitializedAsync()
        {
            var countries = CountryLoader.CountryInfo;
            foreach (var country in countries)
            {
                AllCountriesPhoneCode.Add(new CountryWrapper
                {
                    Name = $"{country.Iso} +{country.PhonePrefix}",
                    DialingCode = $"+{country.PhonePrefix}",
                    PhonePrefix = $"+{country.PhonePrefix}"
                });
            }

            MemberProfileModel = new MemberProfileViewModel();
            Model = new BioDataViewModel();
         
            Countries = new List<LocationMasterView>();
            States = new List<LocationMasterView>();
            Countries = await GetLocation("COUNTRY");
            if (!string.IsNullOrEmpty(UpdateModel.Country))
            {
                States = await GetStateByCountry(UpdateModel.Country);
            }

            await MapToModel();
        }


        private async Task OnSave()
        {
            if (await _fluentValidationValidator!.ValidateAsync())
            {
                await MapToCommand();
                var rsp = await DataService
                    .UpdateMemberProfile<UpdateMemberProfileCommand, CommandResult<MemberProfileViewModel>>(
                        UpdateCommand);

                //Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(UpdateKYCBiodataModel)}");
                if (!rsp.IsSuccessStatusCode)
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
                    MemberProfileModel =
                        JsonSerializer.Deserialize<MemberProfileViewModel>(rsp.Content.Response.ToJson());
                    await OnUpdateBioDataRequestChanged.InvokeAsync(MemberProfileModel);
                }
            }
        }

        private async Task MapToCommand()
        {
            UpdateCommand = new UpdateMemberProfileCommand();
            UpdateCommand = Mapper.Map<UpdateMemberProfileCommand>(UpdateModel);
            UpdateCommand.SecondaryPhone = CountryPhoneCode.Nigeria + Model.SecondaryPhone;
            UpdateCommand.PrimaryPhone = CountryPhoneCode.Nigeria + Model.PrimaryPhone;
            UpdateCommand.SecondaryEmail = Model.SecondaryEmail;
            UpdateCommand.MiddleName = Model.MiddleName;
            UpdateCommand.LastName = Model.LastName;
            UpdateCommand.FirstName = Model.FirstName;
            UpdateCommand.Gender = Model.Gender;
            UpdateCommand.Country = Model.Country;
            UpdateCommand.CAI = Model.CAI;
            UpdateCommand.OfficeAddress = Model.OfficeAddress;
            UpdateCommand.ResidentialAddress = Model.ResidentialAddress;
            UpdateCommand.RetireeNumber = Model.RetireeNumber;
            UpdateCommand.StateOfOrigin = Model.StateOfOrigin;
            UpdateCommand.DOB = Model.DOB;
        }

        private async Task MapToModel()
        {
            if (UpdateModel != null && UpdateModel.SecondaryPhone != null)
            {
                Model.SecondaryPhone = UpdateModel.SecondaryPhone.StartsWith("+234")
                    ? UpdateModel.SecondaryPhone.Replace("+234", "")
                    : UpdateModel.SecondaryPhone;
            }
            else
            {
                Model.PrimaryPhone = UpdateModel.SecondaryPhone;
            }

            if (UpdateModel != null && UpdateModel.PrimaryPhone != null)
            {
                Model.PrimaryPhone = UpdateModel.PrimaryPhone.StartsWith("+234")
                    ? UpdateModel.PrimaryPhone.Replace("+234", "")
                    : UpdateModel.PrimaryPhone;
            }
            else
            {
                Model.PrimaryPhone = UpdateModel.PrimaryPhone;
            }

            Model.SecondaryEmail = UpdateModel.SecondaryEmail;
            Model.MiddleName = UpdateModel.MiddleName;
            Model.LastName = UpdateModel.LastName;
            Model.FirstName = UpdateModel.FirstName;
            Model.Gender = UpdateModel.Gender;
            Model.Country = UpdateModel.Country;
            Model.CAI = UpdateModel.CAI;
            Model.OfficeAddress = UpdateModel.OfficeAddress;
            Model.ResidentialAddress = UpdateModel.ResidentialAddress;
            Model.RetireeNumber = UpdateModel.RetireeNumber;
            Model.PrimaryEmail = UpdateModel.PrimaryEmail;
            Model.StateOfOrigin = UpdateModel.StateOfOrigin;
            Model.DOB =Model.DOB ?? DateTime.UtcNow.AddYears(-18);
        }

        public async Task<IEnumerable<LocationMasterView>> GetLocation(string locationType)
        {
            var rsp = await DataService.Location<List<LocationMasterView>>(
                nameof(LocationMasterView), locationType);

            if (rsp.IsSuccessStatusCode)
            {
                List<LocationMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<LocationMasterView>>(rsp.Content.ToJson());

                return rspResponse.OrderBy(f => f.Name);
            }

            return null;
        }

        public async Task<IEnumerable<LocationMasterView>> GetState(string parentId)
        {
            var rsp = await DataService.State<List<LocationMasterView>>(
                nameof(LocationMasterView), parentId);

            if (rsp.IsSuccessStatusCode)
            {
                List<LocationMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<LocationMasterView>>(rsp.Content.ToJson());

                return rspResponse.OrderBy(f => f.Name);
            }

            return null;
        }

        public async Task<IEnumerable<LocationMasterView>> GetStateByCountry(string headername)
        {
            var rsp = await DataService.StateByCountry<List<LocationMasterView>>(
                nameof(LocationMasterView), headername);

            if (rsp.IsSuccessStatusCode)
            {
                List<LocationMasterView> rspResponse =
                    JsonSerializer.Deserialize<List<LocationMasterView>>(rsp.Content.ToJson());

                return rspResponse.OrderBy(f => f.Name);
            }

            return null;
        }

        public string Dropdownstatus { get; set; } = "hidden";

        public void CountryDropDown()
        {
            Dropdownstatus = (Dropdownstatus == "hidden") ? "" : "hidden";
        }
    }
}