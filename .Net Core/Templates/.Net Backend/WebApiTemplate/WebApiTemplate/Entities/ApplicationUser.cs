using Microsoft.AspNetCore.Identity;

namespace WebApiTemplate.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Country { get; set; }
        public string? Department { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
    }
}
