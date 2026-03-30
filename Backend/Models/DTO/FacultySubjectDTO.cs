using System.ComponentModel.DataAnnotations;

namespace TimeTable_api.Models
{
    public class FacultySubjectDTO
    {
        [Required(ErrorMessage = "Faculty Id is required")]
        public int FacultyId { get; set; } = 0;

        [Required(ErrorMessage = "Subject Id is required")]
        public int SubjectId { get; set; } = 0;
    }

}