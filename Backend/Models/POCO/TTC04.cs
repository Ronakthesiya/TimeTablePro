using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TimeTable_api.Models.POCO
{
    /// <summary>
    /// Subject POCO
    /// </summary>
    public class TTC04
    {
        /// <summary>
        /// Subject Id
        /// </summary>
        [JsonProperty("Id")]
        public int C04F01 { get; set; } = 0;


        /// <summary>
        /// Subject Name
        /// </summary>
        [Required(ErrorMessage = "Subject Name is required")]
        [JsonProperty("Name")]
        public string C04F02 { get; set; } = "";


        /// <summary>
        /// Subject branchId
        /// </summary>
        [Required(ErrorMessage = "Subject branch id is required")]
        [JsonProperty("BranchId")]
        public int C04F03 { get; set; } = 0;


        /// <summary>
        /// Subject Place Type
        /// </summary>
        [Required(ErrorMessage = "Subject place type is required")]
        [JsonProperty("PlaceType")]
        public string C04F04 { get; set; } = "";


        /// <summary>
        /// Subject per week class
        /// </summary>
        [JsonProperty("PerWeekClass")]
        public int C04F05 { get; set; } = 0;
    }

}