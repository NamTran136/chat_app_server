using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server_proj1.Models.DTOs.ConversationDtos;
using Server_proj1.Models.DTOs.MemberDtos;
using Server_proj1.Models.Enums;
using Server_proj1.Services.ConversationServices;
using Server_proj1.Services.MemberServices;

namespace Server_proj1.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationServices _conversationServices;
        private readonly IMemberServices _memberServices;
        public ConversationController(IConversationServices conversationServices, IMemberServices memberServices)
        {
            _conversationServices = conversationServices;
            _memberServices = memberServices;
        }
        [HttpGet]
        public ActionResult<List<ConversationDto>> Get()
        {
             return Ok(_conversationServices.GetAllConversations());
        }
        [HttpPost]
        public ActionResult<List<MemberDto>> GetConversation(string id)
        {
            return Ok(_conversationServices.GetAllConversations(id));
        }
        [HttpPost("searchName"), Authorize]
        public ActionResult<List<MemberDto>> GetBySearchName(string id, string searchName)
        {
            return _conversationServices.GetConversationsBySearchName(id, searchName);
        }
        [HttpPost("create"), Authorize]
        public ActionResult<ConversationToGetDto> Create(ConversationToAddDto model)
        {
            var toReturn = new ConversationToGetDto();
            if (model.members.Count() <= 0) return BadRequest("MemberId is require.");
           var  conversationId = _conversationServices.CreateConversation();
            toReturn.Id = conversationId;
            toReturn.IsGroup = false;
            toReturn.Type = TypeConversation.DIRECT_MESSAGE;
            toReturn.MemberIds = new List<string>();
            var adminMember = _memberServices.CreateDirectMember(model.Admin, model.members[0], conversationId, Role.ADMIN);
            if (adminMember == 0) return NotFound("User is not found");
            if (adminMember == 1) return NotFound("User is not found");
            if (adminMember == 2) return NotFound("Conversation is not found");
            toReturn.MemberIds.Add(model.Admin);
            foreach (var memberId in model.members)
            {
                var newMember = _memberServices.CreateDirectMember(memberId, model.Admin, conversationId, Role.MEMBER);
                if (newMember != 0) toReturn.MemberIds.Add(memberId);
            }
            return Ok(toReturn);
        }

        [HttpPost("createGroup"), Authorize]
        public ActionResult<ConversationToGetDto> CreateGroup(ConversationToAddGroupDto model)
        {
            var conversationId = "";
            var toReturn = new ConversationToGetDto();
            if (model.members.Count() <= 0) return BadRequest("MemberId is require.");
            toReturn.GroupTitle = model.GroupTitle;
            conversationId = _conversationServices.CreateConversation(model.GroupTitle);
            toReturn.Id = conversationId;
            toReturn.IsGroup = true;
            toReturn.Type = TypeConversation.GROUP;
            toReturn.MemberIds = new List<string>();

            var adminMember = _memberServices.CreateGroupMember(model.Admin, model.GroupTitle, conversationId, Role.ADMIN);
            if (adminMember == 0) return NotFound("User is not found");
            toReturn.MemberIds.Add(model.Admin);
            foreach (var memberId in model.members)
            {
                var newMember = _memberServices.CreateGroupMember(memberId, model.GroupTitle, conversationId, Role.MEMBER);
                if (newMember != 0) toReturn.MemberIds.Add(memberId);
            }
            return Ok(toReturn);
        }
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(string id) 
        {
            if (_conversationServices.DeleteConversation(id)) return Ok("Delete this conversation successfully.");
            return NotFound();
        }
    }
}
