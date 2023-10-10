using DocConnect.Data.Models.Entities.Base;

namespace DocConnect.Data.Models.Entities;

public class Location : BaseEntity
{
    public uint Id { get; set; }

    public string Address { get; set; } = null!;

    public uint CityId { get; set; }

    public uint StateId { get; set; }

    public int Zip { get; set; }

    public City City { get; set; } = null!;

    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public State State { get; set; } = null!;
}
