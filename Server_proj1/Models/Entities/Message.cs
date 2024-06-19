using System.ComponentModel.DataAnnotations.Schema;

namespace Server_proj1.Models.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public string Body { get; set; }
        [ForeignKey("Conversation")]
        public string ConversationId { get; set; }
        public Conversation Conversation { get; set; }
        [ForeignKey("Member")]
        public string SenderId { get; set; }
        public Member Sender { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public Message(string body, string conversationId, string senderId) 
        {
            Id = Guid.NewGuid().ToString(); // Tạo ID không trùng lặp
            Body = body;
            ConversationId = conversationId;
            SenderId = senderId;
            CreatedAt = DateTime.Now;
            FileName = "";
            FileUrl = "";
        }
        
    }
}
