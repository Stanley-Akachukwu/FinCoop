namespace ChevronCoop.Web.AppUI.BlazorServer.Helper.Users
{
    public class UserService
    {
        public string ApplicationUserId { get; set; }
        public string CustomerId { get; set; }
        public string Fullname { get; set; }
        public string UserImage { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public string MemberId { get; set; }

        public string Firstname { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
