using Newtonsoft.Json;

namespace TimeTable_api.Models.POCO
{
    /// <summary>
    /// Faculty Subject connection
    /// </summary>
    public class TTC06
    {
        /// <summary>
        /// Faculty Id
        /// </summary>
        [JsonProperty("FacultyId")]
        public int C06F01 { get; set; }


        /// <summary>
        /// Subject Id
        /// </summary>
        [JsonProperty("SubjectId")]
        public int C06F02 { get; set; }
    }

}