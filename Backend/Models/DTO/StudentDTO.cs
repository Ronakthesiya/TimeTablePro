using ServiceStack.OrmLite;
using System.ComponentModel.DataAnnotations;
using TimeTable_api.Models.POCO;

namespace TimeTable_api.Models
{
    public class StudentDTO
    {
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "Student Name is Required")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Student Enrollment is Required")]
        public string Enrollment { get; set; } = "";

        [Required(ErrorMessage = "Student branch id is Required")]
        public int BranchId { get; set; } = 0;
        public TTC02 Branch { get; set; } = new TTC02();
    }

}