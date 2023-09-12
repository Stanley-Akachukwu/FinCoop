using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using AP.ChevronCoop.Commons;
using System.Text.Json;
using Newtonsoft.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Pages.SharedComponents
{
    public partial class AllMasterViews<T>
    {
        [Inject]
        [Parameter]
        public List<T> _AllMasterViewss { get; set; } = new List<T>();

        [Inject]
        IEntityDataService DataService { get; set; }

        [Parameter]
        public string CustomerID { get; set; }

        protected async Task OnInitializedAsync()
        {
            var rsped = await DataService.GetValue<List<T>>(nameof(T), "approvalId", CustomerID);
            if (rsped.IsSuccessStatusCode)
            {
                _AllMasterViewss = new List<T>();
                var jsonDocument = JsonDocument.Parse(rsped.Content.ToJson());
                _AllMasterViewss = JsonConvert.DeserializeObject<List<T>>(rsped.Content.ToJson());
            }
        }


    }
}