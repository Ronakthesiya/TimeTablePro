using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace TimeTable_api.Models.POCO
{
    /// <summary>
    /// Enum for Week days
    /// </summary>
    public enum WeekDay
    {
        Mon,
        Tue,
        Wed,
        Thu,
        Fri,
        Sat
    }

    /// <summary>
    /// Slot POCO
    /// </summary>
    public class TTC07
    {
        /// <summary>
        /// Slot Id
        /// </summary>
        [JsonProperty("SlotId")]
        public int C07F01 { get; set; } = 0;


        /// <summary>
        /// Slot BranchId
        /// </summary>
        [JsonProperty("BranchId")]
        public int C07F02 { get; set; } = 0;


        /// <summary>
        /// Slot Number
        /// </summary>
        [JsonProperty("SlotNumber")]
        public int C07F03 { get; set; } = 0;


        /// <summary>
        /// Slot Day Of Week
        /// </summary>
        [JsonProperty("DayOfWeek")]
        public WeekDay C07F04 { get; set; }


        /// <summary>
        /// Slot start time
        /// </summary>
        [JsonProperty("TimeStart")]
        public string C07F05 { get; set; } = "";


        /// <summary>
        /// Slot end time
        /// </summary>
        [JsonProperty("TimeEnd")]
        public string C07F06 { get; set; } = "";


        /// <summary>
        /// Slot Subject Id
        /// </summary>
        [ForeignKey(typeof(TTC04))]
        [JsonProperty("SubjectId")]
        public int C07F07 { get; set; } = 0;


        /// <summary>
        /// Slot Faculty Id
        /// </summary>
        [ForeignKey(typeof(TTC03))]
        [JsonProperty("FacultyId")]
        public int C07F08 { get; set; } = 0;


        /// <summary>
        /// Slot Place Id
        /// </summary>
        [ForeignKey(typeof(TTC05))]
        [JsonProperty("PlaceId")]
        public int C07F09 { get; set; } = 0;


        /// <summary>
        /// Referance to subject table 
        /// </summary>
        [Reference]
        public TTC04 Subject { get; set; }


        /// <summary>
        /// Referance to faculty table 
        /// </summary>
        [Reference]
        public TTC03 Faculty { get; set; }


        /// <summary>
        /// Referance to place table 
        /// </summary>
        [Reference]
        public TTC05 Place { get; set; }
    }
}