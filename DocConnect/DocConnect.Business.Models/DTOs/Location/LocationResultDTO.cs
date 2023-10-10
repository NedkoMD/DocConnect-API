namespace DocConnect.Business.Models.DTOs.Location
{
    public class LocationResultDTO
    {
        public uint Id { get; set; }

        public string Address { get; set; } = null!;

        public uint CityId { get; set; }

        public uint StateId { get; set; }

        public int Zip { get; set; }
    }
}
