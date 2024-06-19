using Microsoft.VisualBasic;
using Server_proj1.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Server_proj1.Models.Entities
{
    public class Member
    {
        public string Id { get; set; }
        public ICollection<Message> Messages { get; set; }
        public DateTime JoinedAt { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Conversation")]
        public string ConversationId { get; set; }
        public Conversation Conversation { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public Role Role { get; set; }
        public Member(string userId, string conversationId, string title, Role role)
        {
            Id = Guid.NewGuid().ToString(); // Tạo ID không trùng lặp
            JoinedAt = DateTime.Now;
            UserId = userId;
            ConversationId = conversationId;
            Role = role;
            ImageUrl = "";
            Title = title;
        }
    }
}
