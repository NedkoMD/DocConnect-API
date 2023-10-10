using System.ComponentModel.DataAnnotations;

namespace DocConnect.Business.Models.DTOs.Appointments
{
    public class AppointmentAddDTO
    {
        [Required]
        public uint DoctorId { get; set; }
        [Required]
        public uint UserId { get; set; }
        [Required]
        public DateOnly TimeSlot { get; set; }
        [Required]
        public int Hour { get; set; }
    }
}
