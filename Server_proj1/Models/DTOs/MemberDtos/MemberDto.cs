using Server_proj1.Models.Entities;
using Server_proj1.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server_proj1.Models.DTOs.MemberDtos
{
    public class MemberDto
    {
        public string Id { get; set; }
        public DateTime JoinedAt { get; set; }
        public string UserId { get; set; }
        public string ConversationId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public Role Role { get; set; }
    }
}
