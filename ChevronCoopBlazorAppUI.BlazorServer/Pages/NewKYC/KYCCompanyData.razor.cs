using AntDesign;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using Blazored.FluentValidation;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Asn1.Pkcs;
using Syncfusion.Blazor.Data;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.NewKYC
{
    public partial class KYCCompanyData
    {
        private FluentValidationValidator? _fluentValidationValidator;
        private Query Query_Combo;

        [Parameter]
        public EventCallback<bool> OnUpdateCompanyDataChanged { get; set; }

        [Parameter]
        public MemberProfileMasterView UpdateModel { get; set; }

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public EventCallback<MemberProfileMasterView> UpdateModelChanged { get; set; }

        public CompanyDataViewModel Model { get; set; }

        [Inject]
        AutoMapper.IMapper Mapper { get; set; }

        [Inject]
        WebConfigHelper Config { get; set; }

        public UpdateMemberProfileCommand UpdateCommand { get; set; }

        [Inject]
        NotificationService notificationService { get; set; }

        [Parameter]
        public MemberProfileViewModel MemberProfileModel { get; set; }

        ILogger<KYCCompanyData> Logger { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MemberProfileModel = new MemberProfileViewModel();
            Model = new CompanyDataViewModel();
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

                //Logger.LogInformation($"rsp content->{JsonSerializer.Serialize(UpdateCommand)}");
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
                    MemberProfileModel =
                        JsonSerializer.Deserialize<MemberProfileViewModel>(rsp.Content.Response.ToJson());
                    await OnUpdateCompanyDataChanged.InvokeAsync(true);
                }
            }
        }

        private async Task MapToCommand()
        {
            UpdateCommand = new UpdateMemberProfileCommand();
            UpdateCommand = Mapper.Map<UpdateMemberProfileCommand>(UpdateModel);
            UpdateCommand.MembershipId = Model.MembershipId;
            UpdateCommand.CAI = Model.CAI;
            UpdateCommand.JobRole = Model.JobRole;
            UpdateCommand.Rank = Model.Rank;
            //UpdateCommand.DepartmentId = Model.DepartmentId;
            UpdateCommand.YearsOfService = DateTime.UtcNow.Year - Model.DateOfEmployment.Year;
            UpdateCommand.DateOfEmployment = Model.DateOfEmployment;
        }

        private async Task MapToModel()
        {
            Model.CAI = UpdateModel.CAI;
            Model.JobRole = UpdateModel.JobRole;
            Model.Rank = UpdateModel.Rank;
            Model.DepartmentId = UpdateModel.DepartmentId;
            Model.MembershipId = UpdateModel.MembershipId;
            Model.YOS = UpdateModel.YearsOfService == null ? 0 : (int)UpdateModel.YearsOfService;

            Model.DateOfEmployment = UpdateModel.DateOfEmployment < DateTime.Now.AddYears(-100) ? DateTime.UtcNow : UpdateModel.DateOfEmployment ?? DateTime.UtcNow;


        }
    }
}