using AP.ChevronCoop.Entities.Security.Approvals.ApprovalWorkflows;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.Security.Approvals
{
    public partial class ApprovalSetup
    {
        protected override async Task OnInitializedAsync()
        {
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
            }
        }
    }
}