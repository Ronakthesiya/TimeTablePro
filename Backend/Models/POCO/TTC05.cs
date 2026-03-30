using Newtonsoft.Json;

namespace TimeTable_api.Models.POCO
{
    /// <summary>
    /// Place POCO
    /// </summary>
    public class TTC05
    {
        /// <summary>
        /// Place Id
        /// </summary>
        [JsonProperty("Id")]
        public int C05F01 { get; set; } = 0;


        /// <summary>
        /// Place Name
        /// </summary>
        [JsonProperty("Name")]
        public string C05F02 { get; set; } = "";


        /// <summary>
        /// Place type
        /// </summary>
        [JsonProperty("Type")]
        public string C05F03 { get; set; } = "";


        /// <summary>
        /// Place Available time
        /// </summary>
        [JsonProperty("AvailableTime")]
        public string C05F04 { get; set; } = "000000000000000000000000000000";
    }

}