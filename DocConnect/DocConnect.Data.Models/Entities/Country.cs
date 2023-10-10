using DocConnect.Data.Models.Entities.Base;

namespace DocConnect.Data.Models.Entities;

public class Country : BaseEntity
{
    public uint Id { get; set; }

    public string Name { get; set; } = null!;

    public string Alpha2 { get; set; } = null!;

    public string Alpha3 { get; set; } = null!;

    public ICollection<State> States { get; set; } = new List<State>();
}
