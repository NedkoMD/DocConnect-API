using DocConnect.Data.Models.Entities.Base;

namespace DocConnect.Data.Models.Entities;

public class Token : BaseEntity
{
    public uint Id { get; set; }

    public uint UserId { get; set; }

    public DateTime ValidUntil { get; set; }

    public bool IsUsed { get; set; }

    public string Value { get; set; } = null!;

    public string Type { get; set; } = null!;

    public User User { get; set; } = null!;
}
