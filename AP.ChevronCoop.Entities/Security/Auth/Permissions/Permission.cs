using AP.ChevronCoop.Commons;

namespace AP.ChevronCoop.Entities.Security.Auth.Permissions
{
    public class Permission : BaseEntity<string>
    {
        public Permission()
        {

            Id = NUlid.Ulid.NewUlid().ToString();

        }

        public string Code { get; set; }
        public string Name { get; set; }

        public string? Group { get; set; }
        public string? Category { get; set; }
        public string? Module { get; set; }
        public string? Owner { get; set; }

        //public virtual List<ApplicationRoleClaim> RoleClaims { get; set; }

        public override string DisplayCaption
        {
            get
            {
                return "";
            }
        }

        public override string DropdownCaption
        {
            get
            {
                return "";
            }
        }

        public override string ShortCaption
        {
            get
            {
                return "";
            }
        }


    }


















}
