using System.ComponentModel.DataAnnotations;

namespace Server_proj1.Models.Entities
{
    public class User
    {
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Member> Members { get; set; }
        public string Password { get; set; }
        public User(string email, string name, string password)
        {
            Id = Guid.NewGuid().ToString(); // Tạo ID không trùng lặp
            Email = email;
            Name = name;
            ImageUrl = "";
            Password = password;
            CreatedAt = DateTime.Now;
        }
    }
}
