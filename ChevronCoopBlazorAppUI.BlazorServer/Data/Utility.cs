using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities.MasterData.Locations;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public static class Utility
    {
        [Inject]
        static IEntityDataService DataService { get; set; }
        public static async Task<IEnumerable<LocationMasterView>> GetCountry()
        {

            var rsp = await DataService.Location<List<LocationMasterView>>(
       nameof(LocationMasterView), "COUNTRY");

            if (rsp.IsSuccessStatusCode)
            {
                List<LocationMasterView> rspResponse = JsonSerializer.Deserialize<List<LocationMasterView>>(rsp.Content.ToJson());

                return rspResponse;

            }
            return null;

        }
    }
}
