namespace DocConnect.Business.Models.DTOs.Doctor
{
    public class DoctorSearchResultDTO
    {
        public uint Id { get; set; }

        public string ImageUrl { get; set; }

        public string FullName { get; set; }

        public string SpecialityName { get; set; }

        public string Location { get; set; }
    }
}
