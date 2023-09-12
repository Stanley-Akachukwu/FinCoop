using AP.ChevronCoop.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AP.ChevronCoop.Entities.MasterData.Locations
{
    public class Location : BaseEntity<string>
    {

        public Location()
        {

            Id = NUlid.Ulid.NewUlid().ToString();

        }

        public LocationType LocationType { get; set; } = LocationType.OTHER;

        public virtual Location Parent { get; set; }
        
        public string ParentId { get; set; }

        public virtual List<Location> Children { get; set; }
        
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