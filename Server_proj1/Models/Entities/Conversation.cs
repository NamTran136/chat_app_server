using Server_proj1.Models.DTOs;
using Server_proj1.Models.Enums;

namespace Server_proj1.Models.Entities
{
    public class Conversation
    {
        public string Id { get; set; }
        public bool? IsGroup { get; set; } = false;
        public string? GroupTitle { get; set; } = string.Empty;
        public ICollection<Member> Members { get; set; }
        public ICollection<Message> Messages { get; set; }
        public TypeConversation? Type { get; set; } = TypeConversation.DIRECT_MESSAGE;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Conversation()
        {
            Id = Guid.NewGuid().ToString();
            IsGroup = false;
            GroupTitle = "";
            Type = TypeConversation.DIRECT_MESSAGE;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        public Conversation(string groupTitle)
        {
            Id = Guid.NewGuid().ToString(); // Tạo ID không trùng lặp
            IsGroup = true;
            GroupTitle = groupTitle;
            Type = TypeConversation.GROUP;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }

    
}
