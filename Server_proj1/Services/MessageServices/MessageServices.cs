using Server_proj1.Datalayer;
using Server_proj1.Models.Entities;
using Server_proj1.Models.DTOs.MessageDtos;
using Microsoft.IdentityModel.Tokens;
namespace Server_proj1.Services.MessageServices
{
    public class MessageServices : IMessageServices
    {
        private readonly DataContext _db;
        public MessageServices(DataContext db) { _db = db; }
        public int CreateMessage(string body, string conversationId, string senderId)
        {
            var fetchedConversation = _db.Conversations.FirstOrDefault(p=>p.Id ==  conversationId);
            if (fetchedConversation == null) return 0;
            var fetchedSender = _db.Members.FirstOrDefault(p=>p.Id == senderId);
            if(fetchedSender == null) return 1;
            var addMessage = new Message(body, conversationId, senderId);
            _db.Messages.Add(addMessage);
            _db.SaveChanges();
            return 2;
        }
        public int CreateMessageHasFile(MessageToAddHasFileDto model)
        {
            var fetchedConversation = _db.Conversations.FirstOrDefault(p => p.Id == model.ConversationId);
            if (fetchedConversation == null) return 0;
            var fetchedSender = _db.Members.FirstOrDefault(p => p.Id == model.SenderId);
            if (fetchedSender == null) return 1;
            var addMessage = new Message(model.Body, model.ConversationId, model.SenderId);
            if (!String.IsNullOrEmpty(model.FileUrl) && !String.IsNullOrEmpty(model.FileName))
            {
                addMessage.FileUrl = model.FileUrl;
                addMessage.FileName = model.FileName;
            }
            _db.Messages.Add(addMessage);
            _db.SaveChanges();
            return 2;
        }
        public List<MessageToGetDto> GetMessages(string conversationId)
        {
            var toReturn = new List<MessageToGetDto>();
            var messages = _db.Messages.Where(m=>m.ConversationId ==  conversationId).OrderBy(m=>m.CreatedAt).ToList();
            if (messages.Count <= 0) return toReturn;
            foreach (var message in messages)
            {
                var item = new MessageToGetDto
                {
                    Id = message.Id,
                    Body = message.Body,
                    ConversationId = message.ConversationId,
                    SenderId = message.SenderId,
                    SenderName = GetSenderName(message.SenderId),
                    SenderImageUrl = GetSenderImageUrl(message.SenderId),
                    SenderEmail = GetSenderEmail(message.SenderId),
                    CreatedAt = message.CreatedAt,
                    FileName = message.FileName,
                    FileUrl = message.FileUrl,
                };
                toReturn.Add(item);
            }
            return toReturn;
        }

        public bool DeleteMessage(string id)
        {
            var message = _db.Messages.FirstOrDefault(m => m.Id == id);
            if (message == null) return false;
            _db.Messages.Remove(message);
            _db.SaveChanges();
            return true;
        }

        private string GetSenderName(string memberId)
        {
            var fetchedMember = _db.Members.FirstOrDefault(m=>m.Id==memberId);
            if (fetchedMember == null) return "";
            var fetchedUser = _db.Users.FirstOrDefault(u => u.Id == fetchedMember.UserId);
            if (fetchedUser == null) return "";
            return fetchedUser.Name;
        }
        private string GetSenderImageUrl(string memberId)
        {
            var fetchedMember = _db.Members.FirstOrDefault(m => m.Id == memberId);
            if (fetchedMember == null) return "";
            var fetchedUser = _db.Users.FirstOrDefault(u => u.Id == fetchedMember.UserId);
            if (fetchedUser == null) return "";
            return fetchedUser.ImageUrl;
        }
        private string GetSenderEmail(string memberId)
        {
            var fetchedMember = _db.Members.FirstOrDefault(m => m.Id == memberId);
            if (fetchedMember == null) return "";
            var fetchedUser = _db.Users.FirstOrDefault(u => u.Id == fetchedMember.UserId);
            if (fetchedUser == null) return "";
            return fetchedUser.Email;
        }
    }
}
