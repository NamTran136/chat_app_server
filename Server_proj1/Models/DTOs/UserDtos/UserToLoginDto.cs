namespace Server_proj1.Models.DTOs.UserDtos
{
    public class UserToLoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
