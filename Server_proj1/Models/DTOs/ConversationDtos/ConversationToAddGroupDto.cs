namespace Server_proj1.Models.DTOs.ConversationDtos
{
    public class ConversationToAddGroupDto
    {
        public string Admin {  get; set; }
        public string GroupTitle { get; set; }
        public List<string> members { get; set; }
    }
}
