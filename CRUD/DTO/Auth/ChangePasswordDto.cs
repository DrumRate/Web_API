using System.ComponentModel.DataAnnotations;

namespace CRUD.DTO
{
    public class ChangePasswordDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
