using DocConnect.Data.Models.Entities;

namespace DocConnect.Data.Repositories
{
    public class AppointmentDetailedModel
    {
        public long Id { get; set; }

        public string PatientFullName { get; set; }

        public string DoctorFullName { get; set; }

        public DateTime TimeSlot { get; set; }

        public string Location { get; set; }

        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }

        public string SpecialityName { get; set; }
    }
}
