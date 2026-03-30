using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TimeTable_api.Models.POCO;

namespace TimeTable_api.Models
{
    public class PlaceDTO
    {
        
        public int Id { get; set; } = 0;
        [Required(ErrorMessage = "Place Name is required")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Place Type is required")]
        public string Type { get; set; } = "";
        public bool[] AvailableTime { get; set; } = new bool[0];

        public ICollection<SubjectDTO> Subjects { get; set; } = new List<SubjectDTO>();
    }

}