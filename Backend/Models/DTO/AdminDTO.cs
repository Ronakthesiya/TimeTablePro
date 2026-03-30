using System.ComponentModel.DataAnnotations;

namespace TimeTable_api.Models.DTO
{
    /// <summary>
    /// admin dto
    /// </summary>
    public class AdminDTO
    {
        public int Id { get; set; } = 0;

        [Required]
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}