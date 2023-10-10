namespace DocConnect.Data.Models.Models
{
    public class DetailedDoctorInfo
    {
        public uint Id { get; set; }

        public string FullName { get; set; }

        public string SpecialityName { get; set; }

        public string ImageUrl { get; set; }

        public string Address { get; set; }

        public string? Summary { get; set; }
    }
}
