namespace DocConnect.Business.Models.DTOs.Appointments
{
    public class AppointmentUpdateDTO
    {
        public uint Id { get; set; }
        public uint DoctorId { get; set; }
        public uint PatientId { get; set; }
        public DateTime TimeSlot { get; set; }
        public bool IsCanceled { get; set; }
        public string Notes { get; set; }
    }
}
