namespace AP.ChevronCoop.Entities.Security.Auth.ApplicationRoles
{
    public partial class ApplicationRoleMasterView
    {

        public long? RowNumber { get; set; }
        public string Id { get; set; }
        public bool IsSystemRole { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? NormalizedName { get; set; }
        
        // public string? Code { get; set; }

        public string? ConcurrencyStamp { get; set; }

    }


















}
