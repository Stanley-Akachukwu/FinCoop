using AP.ChevronCoop.Commons;
using ChevronCoop.Web.AppUI.BlazorServer.Config;
using System.Text.Json;

namespace ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService
{
    public class MasterViews : IMasterViews
    {


        private readonly IEntityDataService DataService;

        public MasterViews(IEntityDataService entityDataService)
        {
            DataService = entityDataService;
        }


        public async Task<List<T>> GetCustomMasterView<T>(string entities, string filter, string filterValue, string columns)
        {
            var TotalList = new List<T>();
            try
            {
                var rsp = await DataService.GetCustomValues<List<T>>(entities, filter, filterValue, columns);
                if (rsp.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<List<T>>(rsp.Content.ToJson());

                    TotalList = response.ToList();
                }
                else
                {

                }
            }
            catch
            {

            }
            return TotalList;
        }

        public async Task<List<T>> GetCustomMasterVieWithBoolean<T>(string entities, string filter, string filterValue, string columns)
        {
            var TotalList = new List<T>();
            try
            {
                var rsp = await DataService.GetCustomValuesWithBoolean<List<T>>(entities, filter, filterValue, columns);
                if (rsp.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<List<T>>(rsp.Content.ToJson());

                    TotalList = response.ToList();
                }
                else
                {

                }
            }
            catch
            {

            }
            return TotalList;
        }

        public async Task<List<T>> Get2CustomMasterVieWithBoolean<T>(string entities, string filter, string filterValue, string filter2, string filterValue2, string columns)
        {
            var TotalList = new List<T>();
            try
            {
                var rsp = await DataService.Get2CustomValuesWithBoolean<List<T>>(entities, filter, filterValue,filter2,filterValue2, columns);
                if (rsp.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<List<T>>(rsp.Content.ToJson());

                    TotalList = response.ToList();
                }
                else
                {

                }
            }
            catch
            {

            }
            return TotalList;
        }

        public async Task<List<T>> Get2FiltersCustomMasterView<T>(string entities, string filter, string filterValue, string filter2, string filtervalue2, string columns)
        {
            var TotalList = new List<T>();
            try
            {
                var rsp = await DataService.GetCustom2FiltersValues<List<T>>(entities, filter, filterValue, filter2, filtervalue2, columns);
                if (rsp.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<List<T>>(rsp.Content.ToJson());

                    TotalList = response.ToList();
                }
                else
                {

                }
            }
            catch
            {

            }
            return TotalList;
        }
        public async Task<List<T>> GetCustomMasterViewEntityOnly<T>(string entities, string columns)
        {
            var TotalList = new List<T>();
            try
            {
                var rsp = await DataService.GetCustomValuesEntityOnly<List<T>>(entities, columns);
                if (rsp.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<List<T>>(rsp.Content.ToJson());

                    TotalList = response.ToList();
                }
                else
                {

                }
            }
            catch
            {

            }
            return TotalList;
        }

        public async Task<List<T>> GetCustomMasterViewEntityAllFields<T>(string entities)
        {
            var TotalList = new List<T>();
            try
            {
                var rsp = await DataService.GetCustomValuesEntityAllFields<List<T>>(entities);
                if (rsp.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<List<T>>(rsp.Content.ToJson());

                    TotalList = response.ToList();
                }
           
            }
            catch(Exception ex) {
            //add a logger here pls
            
            }
       
            return TotalList;
        }

        public async Task<List<T>> GetCustomMasterViewEntityWithFilterAndAllFields<T>(string entities, string filter, string filterValue)
        {
            var TotalList = new List<T>();
            try
            {
                var rsp = await DataService.GetCustomViewEntityWithFilterAndAllFields<List<T>>(entities, filter, filterValue);
                if (rsp.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<List<T>>(rsp.Content.ToJson());

                    TotalList = response.ToList();
                }
                else
                {

                }
            }
            catch
            {

            }
            return TotalList;
        }
        public async Task<List<T>> GetCustomMasterViewEntityWithFilterAndAllFieldsUsingBool<T>(string entities, string filter)
        {
            var TotalList = new List<T>();
            try
            {
                var rsp = await DataService.GetCustomViewEntityWithFilterAndAllFields<List<T>>(entities, filter);
                if (rsp.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<List<T>>(rsp.Content.ToJson());

                    TotalList = response.ToList();
                }
                else
                {

                }
            }
            catch
            {

            }
            return TotalList;
        }



    }
}
