namespace ChevronCoop.Web.AppUI.BlazorServer.Services.MasterViewsService
{
	public interface IMasterViews
	{
		Task<List<T>> GetCustomMasterView<T>(string entities, string filter, string filterValue, string columns);
		Task<List<T>> GetCustomMasterVieWithBoolean<T>(string entities, string filter, string filterValue, string columns);
        Task<List<T>> Get2CustomMasterVieWithBoolean<T>(string entities, string filter, string filterValue,string filter2, string filterValue2, string columns);

        Task<List<T>> Get2FiltersCustomMasterView<T>(string entities, string filter, string filterValue, string filter2, string filtervalue2, string columns);

		Task<List<T>> GetCustomMasterViewEntityOnly<T>(string entities, string columns);

		Task<List<T>> GetCustomMasterViewEntityAllFields<T>(string entities);

		Task<List<T>> GetCustomMasterViewEntityWithFilterAndAllFields<T>(string entities, string filter, string filterValue);
		Task<List<T>> GetCustomMasterViewEntityWithFilterAndAllFieldsUsingBool<T>(string entities, string filter);


    }
}
