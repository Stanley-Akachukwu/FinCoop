using AP.ChevronCoop.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.MasterData.Departments
{
    public class Department : BaseEntity<string>
    {
        public Department()
        {

            Id = NUlid.Ulid.NewUlid().ToString();

        }

        public string Name { get; set; }
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
