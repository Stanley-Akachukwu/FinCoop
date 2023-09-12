using AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberProfiles;
using Microsoft.AspNetCore.Components;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.DataMigration
{
    public partial class PersonalDetail
    {
        bool showUserPersonalInformationForm { get; set; } = true;
        bool showKycDetailForm { get; set; } = false;

        [Parameter]
        public MemberProfileMapperProfile Model { get; set; }

        private async Task OnShowPersonalInformation()
        {
            showUserPersonalInformationForm = true;
            showKycDetailForm = false;
        }

        private async Task OnShowKycDetail()
        {
            showKycDetailForm = true;
            showUserPersonalInformationForm = false;
        }
    }
}