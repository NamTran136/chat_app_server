using Server_proj1.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server_proj1.Models.DTOs.MessageDtos
{
    public class MessageToGetDto
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public string ConversationId { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }
}
