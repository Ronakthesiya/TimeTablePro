using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TimeTable_api.Models.POCO;

namespace TimeTable_api.Models
{
    public class FacultyDTO
    {
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "Faculty Name is required")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Faculty BranchId is required")]
        public int BranchId { get; set; } = 0;
        public BranchDTO Branch { get; set; } = new BranchDTO();
        public bool[] AvailableTime { get; set; } = new bool[0];
        public ICollection<TTC04> Subjects { get; set; } = new List<TTC04>();

        public string Password { get; set; }
        public string Email { get; set; }
    }

}