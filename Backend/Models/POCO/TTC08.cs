using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TimeTable_api.Models.POCO
{
    /// <summary>
    /// Student POCO
    /// </summary>
    public class TTC08
    {

        /// <summary>
        /// Student Id
        /// </summary>
        [JsonProperty("Id")]
        public int C08F01 { get; set; } = 0;


        /// <summary>
        /// Student Name
        /// </summary>
        [Required(ErrorMessage = "Student Name is required")]
        [JsonProperty("Name")]
        public string C08F02 { get; set; } = "";


        /// <summary>
        /// Student Enrollment
        /// </summary>
        [Required(ErrorMessage = "Student Enrollment is required")]
        [JsonProperty("Enrollment")]
        public string C08F03 { get; set; } = "";


        /// <summary>
        /// Student Branch Id
        /// </summary>
        [Required(ErrorMessage = "Student Branch id is required")]
        [JsonProperty("BranchId")]
        public int C08F04 { get; set; } = 0;

        /// <summary>
        /// Student Password
        /// </summary>
        public string C08F05 { get; set; } = "";

        /// <summary>
        /// Student Email
        /// </summary>
        public string C08F06 { get; set; } = "";

    }

}