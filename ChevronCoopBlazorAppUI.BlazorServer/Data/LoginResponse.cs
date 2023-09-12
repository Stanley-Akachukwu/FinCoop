namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public List<MyClaim> Claims { get; set; }
        public List<RoleLookup> Roles { get; set; }
    }
    public class MyClaim
    {
        public string Id { get; set; }
        public string Code { get; set; }
    }
    public class RoleLookup
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
