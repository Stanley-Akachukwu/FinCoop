using AP.ChevronCoop.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.MasterData.GlobalCodes
{
    public class GlobalCode : BaseEntity<string>
    {

        public GlobalCode()
        {

            Id = NUlid.Ulid.NewUlid().ToString();

        }

        public GlobalCodeTypeKeys CodeType { get; set; } = GlobalCodeTypeKeys.GENERIC;

        public string Code { get; set; }

        public string Name { get; set; }

        public bool SystemFlag { get; set; } = false;

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