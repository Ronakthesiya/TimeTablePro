using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TimeTable_api.Models.POCO;

namespace TimeTable_api.Models
{
    public class SubjectDTO
    {
        
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "Subject Name is required")]
        public string Name { get; set; } = "";


        [Required(ErrorMessage = "Subject branch id is required")]
        public int BranchId { get; set; } = 0;
        public TTC02 Branch { get; set; } = new TTC02();
        [Required(ErrorMessage = "Subject place type is required")]
        public string PlaceType { get; set; } = "";
        public int PerWeekClass { get; set; } = 0;

        public ICollection<TTC03> Facultys { get; set; } = new List<TTC03>();
    }

}