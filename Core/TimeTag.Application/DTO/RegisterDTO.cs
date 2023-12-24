using System.ComponentModel.DataAnnotations;

namespace TimeTag.Application.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool IsFirstLogin { get; set; }
    }
}