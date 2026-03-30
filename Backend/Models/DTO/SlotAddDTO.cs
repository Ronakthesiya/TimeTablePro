using System.ComponentModel.DataAnnotations;
using TimeTable_api.Models.POCO;

namespace TimeTable_api.Models.DTO
{
    public class SlotAddDTO
    {
        public int SlotId { get; set; } = 0;

        [Required(ErrorMessage = "Branch Id is required")]
        public int BranchId { get; set; } = 0;

        [Required(ErrorMessage = "SlotNumber is required")]
        public int SlotNumber { get; set; } = 0;

        public WeekDay DayOfWeek { get; set; } = WeekDay.Mon;

        public string TimeStart { get; set; } = "";

        public string TimeEnd { get; set; } = "";


        [Required(ErrorMessage = "Subject Id is required")]
        public int SubjectId { get; set; } = 0;

        [Required(ErrorMessage = "Faculty Id is required")]
        public int FacultyId { get; set; } = 0;

        [Required(ErrorMessage = "Place Id is required")]
        public int PlaceId { get; set; } = 0;
    }
}