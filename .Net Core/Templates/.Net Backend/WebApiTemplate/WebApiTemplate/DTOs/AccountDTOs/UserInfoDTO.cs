using System.ComponentModel.DataAnnotations;

namespace WebApiTemplate.DTOs.AccountDTOs
{
    public class UserInfoDTO
    {
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
