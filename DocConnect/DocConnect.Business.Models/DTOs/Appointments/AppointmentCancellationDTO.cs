namespace DocConnect.Business.Models.DTOs.Appointments
{
    public class AppointmentCancellationDTO
    {
        public uint DoctorId { get; set; }

        public uint PatientId { get; set; }

        public DateTime TimeSlot { get; set; }
    }
}
