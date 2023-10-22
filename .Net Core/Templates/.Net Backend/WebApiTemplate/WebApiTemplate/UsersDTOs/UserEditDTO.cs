using System.ComponentModel.DataAnnotations;

namespace WebApiTemplate.DTOs.UsersDTOs
{
    public class UserEditDTO
    {
        public string? Username { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
