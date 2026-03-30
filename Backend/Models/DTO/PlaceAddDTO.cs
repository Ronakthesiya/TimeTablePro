namespace TimeTable_api.Models
{
    public class PlaceAddDTO
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = "";

        public string Type { get; set; } = "";

        public string AvailableTime { get; set; } = "000000000000000000000000000000";

    }

}