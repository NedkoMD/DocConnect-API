namespace DocConnect.Business.Models.DTOs.City
{
    public class CityResultDTO
    {
        public uint Id { get; set; }

        public string Name { get; set; }

        public uint StateId { get; set; }

        public int UtcDifference { get; set; }

        public string TimeZoneLoc { get; set; }

        public string TimeZone { get; set; }
    }
}
