using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ChevronCoop.Web.AppUI.BlazorServer.Helper
{
    public static class JsInterop
    {
        [JSInvokable]
        public static async Task<bool> IsSidebarExpanded(IJSRuntime jsRuntime, ElementReference toggleSidebarEl)
        {
            return await jsRuntime.InvokeAsync<bool>("isSidebarExpanded", toggleSidebarEl);
        }

        [JSInvokable]
        public static async Task ToggleSidebar(IJSRuntime jsRuntime, ElementReference sidebarEl, bool expand, bool setExpanded = false)
        {
            await jsRuntime.InvokeVoidAsync("toggleSidebar", sidebarEl, expand, setExpanded);
        }
    }
}
