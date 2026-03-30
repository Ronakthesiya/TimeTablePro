using System.ComponentModel.DataAnnotations;

namespace TimeTable_api.Models
{
    public class StudentAddDTO
    {
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "Student Name is Required")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Student Enrollment is Required")]
        public string Enrollment { get; set; } = "";

        [Required(ErrorMessage = "Student branch id is Required")]
        public int BranchId { get; set; } = 0;
    }

}