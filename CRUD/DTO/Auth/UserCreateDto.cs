using System.ComponentModel.DataAnnotations;

namespace CRUD.DTO
{
    public class UserCreateDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public int RoleId { get; set; } = 2;
    }
}
