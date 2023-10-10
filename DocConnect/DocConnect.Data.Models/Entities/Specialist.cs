using DocConnect.Data.Models.Entities.Base;

namespace DocConnect.Data.Models.Entities;

public class Specialist : BaseEntity
{
    public uint Id { get; set; }

    public uint DoctorId { get; set; }

    public uint SpecialityId { get; set; }

    public Doctor Doctor { get; set; } = null!;

    public Speciality Speciality { get; set; } = null!;
}
