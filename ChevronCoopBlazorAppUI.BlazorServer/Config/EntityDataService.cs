//using Refit;
//using System.Threading.Tasks;

//namespace ChevronCoop.Web.AppUI.BlazorServer.Config
//{
//    public class EntityDataService
//    {

//        IEntityDataService dataService;
//        public EntityDataService(IEntityDataService _dataService)
//        {
//            dataService = _dataService;
//        }

//        public async Task<ApiResponse<V>> Create<C, V>(string entity, [Body] C createPayload)
//        {
//            var rsp = await dataService.Create<C, V>(entity, createPayload);
//            return rsp;
//        }


//        public async Task<ApiResponse<V>> Update<U, V>(string entity, [Body] U updatePayload)
//        {
//            var rsp = await dataService.Update<U, V>(entity, updatePayload);
//            return rsp;
//        }

//        public async Task<ApiResponse<V>> Delete<D, V>(string entity, [Body] D deletePayload)
//        {
//            var rsp = await dataService.Delete<D, V>(entity, deletePayload);
//            return rsp;
//        }

//        public async Task<ApiResponse<V>> Process<P, V>(string entity, string command, [Body] P processPayload)
//        {
//            var rsp = await dataService.Process<P, V>(entity, command, processPayload);
//            return rsp;
//        }

//    }




//}
