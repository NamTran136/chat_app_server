using Server_proj1.Datalayer;
using Server_proj1.Models.DTOs.ConversationDtos;
using Server_proj1.Models.DTOs.MemberDtos;
using Server_proj1.Models.Entities;
using Server_proj1.Models.Enums;

namespace Server_proj1.Services.ConversationServices
{
    public class ConversationServices : IConversationServices
    {
        private readonly DataContext _db;
        public ConversationServices(DataContext db) { _db = db; }

        public List<ConversationDto> GetAllConversations()
        {
            var conversations = _db.Conversations.ToList();
            var toReturn = new List<ConversationDto>();
            if(conversations.Count <= 0) return toReturn;
            foreach (var conversation in conversations)
            {
                var item = new ConversationDto
                {
                    Id = conversation.Id,
                    CreatedAt = conversation.CreatedAt,
                    GroupTitle = conversation.GroupTitle,
                    IsGroup = conversation.IsGroup ?? false,
                    Type = conversation.Type ?? TypeConversation.DIRECT_MESSAGE,
                    UpdatedAt = conversation.UpdatedAt,
                    Members = _db.Members.Where(m=>m.ConversationId==conversation.Id).Select(p=>p.Id).ToList(),
                };
                toReturn.Add(item);
            }

            return toReturn;
        }
        public List<MemberDto> GetAllConversations(string loggedInUserId)
        {
            var toReturn = new List<MemberDto>();
            var fetchedMembers = _db.Members.Where(p=>p.UserId ==  loggedInUserId).ToList();
            if(fetchedMembers.Count <= 0) { return toReturn; }
            foreach(var member in fetchedMembers)
            {
                var item = new MemberDto
                {
                    Id = member.Id,
                    JoinedAt = member.JoinedAt,
                    UserId = member.UserId,
                    ConversationId = member.ConversationId,
                    Role = member.Role,
                    Title = member.Title,
                    ImageUrl = member.ImageUrl
                };
                toReturn.Add(item);
            }
            return toReturn;
        }
        public List<MemberDto> GetConversationsBySearchName(string loggedInUserId, string searchName)
        {
            var toReturn = new List<MemberDto>();
            var fetchedMembers = _db.Members.Where(p => p.UserId == loggedInUserId && p.Title.Contains(searchName)).ToList();
            if (fetchedMembers.Count <= 0) { return toReturn; }
            foreach (var member in fetchedMembers)
            {
                var item = new MemberDto
                {
                    Id = member.Id,
                    JoinedAt = member.JoinedAt,
                    UserId = member.UserId,
                    ConversationId = member.ConversationId,
                    Role = member.Role,
                    Title = member.Title,
                    ImageUrl = member.ImageUrl
                };
                toReturn.Add(item);
            }
            return toReturn;
        }
        public string CreateConversation()
        {
            var newConversation = new Conversation();
            newConversation.CreatedAt = DateTime.Now;
            newConversation.UpdatedAt = DateTime.Now;
            newConversation.Type = TypeConversation.DIRECT_MESSAGE;
            _db.Conversations.Add(newConversation);
            _db.SaveChanges();
            return newConversation.Id;
        }
        public string CreateConversation(string groupTitle)
        {
            var newConversation = new Conversation();
            newConversation.IsGroup = true;
            newConversation.GroupTitle = groupTitle;
            newConversation.CreatedAt = DateTime.Now;
            newConversation.UpdatedAt = DateTime.Now;
            newConversation.Type = TypeConversation.GROUP;
            _db.Conversations.Add(newConversation);
            _db.SaveChanges();
            return newConversation.Id;
        }
        public bool DeleteConversation(string id)
        {
            var fetchedConversation = _db.Conversations.FirstOrDefault(x => x.Id == id);
            if(fetchedConversation == null) return false;
            _db.Conversations.Remove(fetchedConversation);
            _db.SaveChanges();
            return true;

        }
    }
}
