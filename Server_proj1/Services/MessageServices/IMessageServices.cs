using Server_proj1.Models.DTOs.MessageDtos;

namespace Server_proj1.Services.MessageServices
{
    public interface IMessageServices
    {
        int CreateMessage(string body, string conversationId, string senderId);
        List<MessageToGetDto> GetMessages(string conversationId);
        bool DeleteMessage(string id);
        int CreateMessageHasFile(MessageToAddHasFileDto model);
    }
}
