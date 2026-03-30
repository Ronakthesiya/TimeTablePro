using System.ComponentModel.DataAnnotations;

namespace TimeTable_api.Models
{
    public class SubjectAddDTO
    {
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "Subject Name is required")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Subject branch id is required")]
        public int BranchId { get; set; } = 0;
        [Required(ErrorMessage = "Subject place type is required")]
        public string PlaceType { get; set; } = "";

        public int PerWeekClass { get; set; } = 0;
    }

}