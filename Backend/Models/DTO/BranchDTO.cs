using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace TimeTable_api.Models
{
    public class BranchDTO
    {

        public int Id { get; set; } = 0;

        [Required]
        public string Name { get; set; } = "";

    }

}