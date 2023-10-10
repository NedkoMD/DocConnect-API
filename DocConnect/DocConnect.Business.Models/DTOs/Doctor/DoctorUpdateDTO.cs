namespace DocConnect.Business.Models.DTOs.Doctor
{
    public class DoctorUpdateDTO
    {
        public uint Id { get; set; }

        public uint UserId { get; set; }

        public uint LocationId { get; set; }

        public string PictureLocation { get; set; } = null!;

        public string Summary { get; set; } = null!;

        public uint ExperienceSince { get; set; }
    }
}
