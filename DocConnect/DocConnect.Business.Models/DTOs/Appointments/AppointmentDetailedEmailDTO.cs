namespace DocConnect.Business.Models.DTOs.Appointments
{
    public class AppointmentDetailedEmailDTO
    {
        public long Id { get; set; }

        public string Email { get; set; }

        public string PatientFullName { get; set; }

        public string DoctorFullName { get; set; }

        public DateTime TimeSlot { get; set; }

        public string Location { get; set; }

        public string SpecialityName { get; set; }
    }
}
