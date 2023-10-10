using DocConnect.Data.Models.Entities.Base;

namespace DocConnect.Data.Models.Entities;

public class State : BaseEntity
{
    public uint Id { get; set; }

    public string Ansi { get; set; } = null!;

    public string Name { get; set; } = null!;

    public uint CountryId { get; set; }

    public ICollection<City> Cities { get; set; } = new List<City>();

    public Country Country { get; set; } = null!;

    public ICollection<Location> Locations { get; set; } = new List<Location>();
}
