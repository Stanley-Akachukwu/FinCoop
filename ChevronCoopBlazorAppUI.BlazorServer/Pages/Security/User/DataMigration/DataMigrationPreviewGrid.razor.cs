using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.FileManager;
using Syncfusion.Blazor.Grids;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.User.DataMigration
{
    public partial class DataMigrationPreviewGrid
    {
        [Parameter]
        public List<MemberDataUpload> Model { get; set; }

        private MemberDataUpload[] ModelList { get; set; }
        string ErrorDetails = "";
        ErrorDetails errors;

        [Inject]
        protected IJSRuntime jsRuntime { get; set; }

        [Parameter]
        public bool ShowModal { get; set; }

        [Parameter]
        public EventCallback<bool> ShowModalChanged { get; set; }

        private List<GridFilterColumn> filterColumns = new List<GridFilterColumn>();
        string searchText;

        [Parameter]
        public EventCallback<List<MemberDataUpload>> ModelChanged { get; set; }

        public async Task ActionFailure(Syncfusion.Blazor.Grids.FailureEventArgs args)
        {
            //this.ErrorDetailMsg = args.Error.ToString();
            this.ErrorDetails = "No response for your request. Please refresh your page!";
            StateHasChanged();
        }

        public void ActionCompletedHandler(ActionEventArgs<MemberDataUpload> args)
        {
            jsRuntime.InvokeVoidAsync("initFlowbiteJS");
        }

        protected override async Task OnInitializedAsync()
        {
            if (Model != null)
            {
                ModelList = Model.ToArray();
            }
        }
    }
}