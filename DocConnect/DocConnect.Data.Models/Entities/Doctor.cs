using DocConnect.Data.Models.Entities.Base;
namespace DocConnect.Data.Models.Entities
{
    public class Doctor : BaseEntity
    {
        public uint Id { get; set; }

        public uint LocationId { get; set; }

        public string? PictureLocation { get; set; }

        public string? Summary { get; set; }

        public short ExperienceSince { get; set; }

        public uint? UserId { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public Location Location { get; set; } = null!;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<Specialist> Specialists { get; set; } = new List<Specialist>();

        public User? User { get; set; }
    }
}
