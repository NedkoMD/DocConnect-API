namespace DocConnect.Business.Models.DTOs.Appointments
{
    public class AppointmentDoctorResultDTO
    {
        public uint DoctorId { get; set; }

        public IEnumerable<AppointmentResultDTO> Appointments { get; set; } = new List<AppointmentResultDTO>();
    }
}
