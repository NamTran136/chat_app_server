namespace Server_proj1.Models.DTOs.MessageDtos
{
    public class MessageToAddDto
    {
        public string Body { get; set; }
        public string ConversationId { get; set; }
        public string SenderId { get; set; }
    }
}
