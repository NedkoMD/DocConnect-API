using DocConnect.Data.Models.Entities.Base;

namespace DocConnect.Data.Models.Entities
{
    public class Appointment : BaseEntity
    {
        public long Id { get; set; }

        public uint DoctorId { get; set; }

        public uint PatientId { get; set; }

        public DateTime TimeSlot { get; set; }

        public bool IsCanceled { get; set; }

        public string? Notes { get; set; } = null!;

        public Doctor Doctor { get; set; } = null!;

        public Patient Patient { get; set; } = null!;
    }
}
