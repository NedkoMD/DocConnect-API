using DocConnect.Data.Models.Entities.Base;

namespace DocConnect.Data.Models.Entities
{
    public class City : BaseEntity
    {
        public uint Id { get; set; }

        public string Name { get; set; } = null!;

        public uint StateId { get; set; }

        public int UtcDifference { get; set; }

        public string TimeZoneLoc { get; set; } = null!;

        public string TimeZone { get; set; } = null!;

        public ICollection<Location> Locations { get; set; } = new List<Location>();

        public State State { get; set; } = null!;
    }
}
