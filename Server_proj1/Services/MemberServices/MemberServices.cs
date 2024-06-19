using Server_proj1.Datalayer;
using Server_proj1.Models.DTOs.MemberDtos;
using Server_proj1.Models.Entities;
using Server_proj1.Models.Enums;

namespace Server_proj1.Services.MemberServices
{
    public class MemberServices : IMemberServices
    {
        private readonly DataContext _db;
        public MemberServices(DataContext db) { _db = db; }

        public int CreateDirectMember(string adminId, string userId, string conversationId, Role role)
        {
            var adminUser = _db.Users.FirstOrDefault(u => u.Id == adminId);
            if (adminUser == null) return 0;
            var memberUser = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (memberUser == null) return 1;
            var conversation = _db.Conversations.FirstOrDefault(c => c.Id == conversationId);
            if (conversation == null) return 2;
            var member = new Member(adminId, conversationId, memberUser.Name, role);
            member.ImageUrl = memberUser.ImageUrl;
            _db.Members.Add(member);
            _db.SaveChanges();
            return 3;
        }
        public int CreateGroupMember(string adminId, string groupTitle, string conversationId, Role role)
        {
            var adminUser = _db.Users.FirstOrDefault(u => u.Id == adminId);
            if (adminUser == null) return 0;
            var conversation = _db.Conversations.FirstOrDefault(c => c.Id == conversationId);
            if (conversation == null) return 2;
            var member = new Member(adminId, conversationId, groupTitle, role);
            _db.Members.Add(member);
            _db.SaveChanges();
            return 2;
        }
        public List<MemberDto> GetAll()
        {
            return _db.Members.Select(x=> new MemberDto
            {
                Id = x.Id,
                JoinedAt = x.JoinedAt,
                UserId = x.UserId,
                ConversationId = x.ConversationId,
                Role = x.Role,
                Title = x.Title,
                ImageUrl = x.ImageUrl,
            }).ToList();
        }
    }
}
