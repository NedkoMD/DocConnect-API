using DocConnect.Data.Models.Entities.Base;

namespace DocConnect.Data.Models.Entities;

public class Speciality : BaseEntity
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public ICollection<Specialist> Specialists { get; set; } = new List<Specialist>();
}
