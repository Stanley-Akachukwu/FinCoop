using Microsoft.JSInterop;

namespace ChevronCoop.Web.AppUI.BlazorServer.Helper
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask InitializeInactivityTimer<T>(this IJSRuntime js,
           DotNetObjectReference<T> dotNetObjectReference) where T : class
        {
            await js.InvokeVoidAsync("initializeInactivityTimer", dotNetObjectReference);
        }
    }
}
