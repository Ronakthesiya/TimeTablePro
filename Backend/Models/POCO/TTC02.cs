using Newtonsoft.Json;

namespace TimeTable_api.Models.POCO
{
    /// <summary>
    /// Branch POCO
    /// </summary>
    public class TTC02
    {
        /// <summary>
        /// Branch Id
        /// </summary>
        [JsonProperty("Id")]
        public int C02F01 { get; set; } = 0;


        /// <summary>
        /// Branch Name
        /// </summary>
        [JsonProperty("Name")]
        public string C02F02 { get; set; } = "";
    }

}