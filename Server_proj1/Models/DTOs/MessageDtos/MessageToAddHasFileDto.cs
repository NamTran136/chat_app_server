namespace Server_proj1.Models.DTOs.MessageDtos
{
    public class MessageToAddHasFileDto
    {
        public string Body { get; set; }
        public string ConversationId { get; set; }
        public string SenderId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }
}
