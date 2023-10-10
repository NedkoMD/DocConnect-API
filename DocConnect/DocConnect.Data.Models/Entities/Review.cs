
using DocConnect.Data.Models.Entities.Base;

namespace DocConnect.Data.Models.Entities
{
    public class Review : BaseEntity
    {
        public int Id { get; set; }

        public uint PatientId { get; set; }

        public uint DoctorId { get; set; }

        public bool? Raiting { get; set; }

        public string Content { get; set; } = null!;

        public Doctor Doctor { get; set; } = null!;

        public Patient Patient { get; set; } = null!;
    }
}
