
namespace TimeTable_api.Models
{
    public class FacultyAddDTO
    {
        public int Id { get; set; } = 0;

        public string Name { get; set; } = "";

        public int BranchId { get; set; } = 0;
        public string AvailableTime { get; set; } = "000000000000000000000000000000";
        public string Password { get; set; }
        public string Email { get; set; }
    }

}