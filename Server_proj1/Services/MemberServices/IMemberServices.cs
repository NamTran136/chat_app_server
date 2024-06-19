using Server_proj1.Models.DTOs.MemberDtos;
using Server_proj1.Models.Entities;
using Server_proj1.Models.Enums;

namespace Server_proj1.Services.MemberServices
{
    public interface IMemberServices
    {
        int CreateDirectMember(string adminId, string userId, string conversationId, Role role);
        int CreateGroupMember(string adminId, string groupTitle, string conversationId, Role role);
        List<MemberDto> GetAll();
    }
}
