using Server_proj1.Models.Entities;
using Server_proj1.Models.Enums;

namespace Server_proj1.Models.DTOs.ConversationDtos
{
    public class ConversationDto
    {
        public string Id { get; set; }
        public bool IsGroup { get; set; } = false;
        public string GroupTitle { get; set; }
        public List<string> Members { get; set; }
        public TypeConversation Type { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
