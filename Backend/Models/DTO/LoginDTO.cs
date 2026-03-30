using System.ComponentModel.DataAnnotations;

namespace TimeTable_api.Models.DTO
{
    public class LoginDTO
    {
        public int Id { get; set; } = 0;
        public int BranchId { get; set; } = 0;

        [Required]
        public string Username { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
        public string Role { get; set; } = "";
    }
}