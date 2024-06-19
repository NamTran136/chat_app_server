using Server_proj1.Models.DTOs.ConversationDtos;
using Server_proj1.Models.DTOs.MemberDtos;

namespace Server_proj1.Services.ConversationServices
{
    public interface IConversationServices
    {
        List<ConversationDto> GetAllConversations();
        List<MemberDto> GetAllConversations(string loggedInUserId);
        List<MemberDto> GetConversationsBySearchName(string loggedInUserId, string searchName);
        string CreateConversation();
        string CreateConversation(string groupTitle);
        bool DeleteConversation(string id);
    }
}
