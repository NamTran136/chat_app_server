using Server_proj1.Models.Enums;

namespace Server_proj1.Models.DTOs.ConversationDtos
{
    public class ConversationToGetDto
    {
        public string Id { get; set; }
        public bool IsGroup { get; set; }
        public string GroupTitle { get; set; } = string.Empty;
        public List<string> MemberIds { get; set; }
        public TypeConversation Type {  get; set; }
    }
}
