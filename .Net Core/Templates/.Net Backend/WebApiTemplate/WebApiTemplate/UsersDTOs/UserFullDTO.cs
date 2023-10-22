namespace WebApiTemplate.DTOs.UsersDTOs
{
    public class UserFullDTO
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
