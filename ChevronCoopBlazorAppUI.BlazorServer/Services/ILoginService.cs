namespace ChevronCoop.Web.AppUI.BlazorServer.Services
{
    public interface ILoginService
    {
        Task<string> GetUserToken();
    }
}
