namespace Server_proj1.Models.DTOs.UserDtos
{
    public class UserToGetDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string? ImageUrl { get; set; } = null;
    }
}
