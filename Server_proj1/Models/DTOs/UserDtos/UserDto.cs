namespace Server_proj1.Models.DTOs.UserDtos
{
    public class UserDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string ImageUrl { get; set; }
    }
}
