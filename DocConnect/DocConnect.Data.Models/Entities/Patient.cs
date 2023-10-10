using DocConnect.Data.Models.Entities.Base;

namespace DocConnect.Data.Models.Entities
{
    public class Patient : BaseEntity
    {
        public uint Id { get; set; }

        /// <summary>
        /// Weight in kg
        /// </summary>
        public float? Weight { get; set; }

        /// <summary>
        /// Height in cm
        /// </summary>
        public float? Height { get; set; }

        public string? BloodPressure { get; set; }

        /// <summary>
        /// Blood sugar level In mmol/L
        /// </summary>
        public float? BloodSugar { get; set; }

        public uint UserId { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public User User { get; set; } = null!;
    }
}
