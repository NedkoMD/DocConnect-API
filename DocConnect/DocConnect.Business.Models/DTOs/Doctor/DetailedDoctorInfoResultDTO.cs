namespace DocConnect.Business.Models.DTOs.Doctor
{
    public class DetailedDoctorInfoResultDTO
    {
        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        public string SpecialityName { get; set; }

        public string Address { get; set; }

        public string? Summary { get; set; }
    }
}
