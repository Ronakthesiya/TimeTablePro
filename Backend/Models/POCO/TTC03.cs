using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TimeTable_api.Models.POCO
{
    /// <summary>
    /// Faculty POCO
    /// </summary>
    public class TTC03
    {
        /// <summary>
        /// Faculty Id
        /// </summary>
        [JsonProperty("Id")]
        public int C03F01 { get; set; }


        /// <summary>
        /// Faculty Name
        /// </summary>
        [Required(ErrorMessage = "Faculty Name is required.")]
        [JsonProperty("Name")]
        public string C03F02 { get; set; }


        /// <summary>
        /// Faculty branchId
        /// </summary>
        [Required(ErrorMessage = "Faculty branch Id is required.")]
        [JsonProperty("BranchId")]
        public int C03F03 { get; set; }


        /// <summary>
        /// Faculty AvailableTime
        /// </summary>
        [JsonProperty("AvailableTime")]
        public string C03F04 { get; set; } = "000000000000000000000000000000";


        /// <summary>
        /// Faculty Password
        /// </summary>
        [JsonProperty("Password")]
        public string C03F05 { get; set; }

        /// <summary>
        /// Faculty Email
        /// </summary>
        [JsonProperty("Email")]
        public string C03F06 { get; set; }
    }

}