using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server_proj1.Models.DTOs.MessageDtos;
using Server_proj1.Services.MessageServices;

namespace Server_proj1.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageServices _messageServices;
        public MessageController(IMessageServices messageServices)
        {
            _messageServices = messageServices;
        }
        [HttpPost("create"), Authorize]
        public ActionResult Create([FromBody] MessageToAddDto model)
        {
            var result = 0;

            result  = _messageServices.CreateMessage(model.Body, model.ConversationId, model.SenderId);    
            if (result == 0) return NotFound("Conversation is not found.");
            if (result == 1) return NotFound("User is not found.");
            return Ok("Add message successfully.");
        }
        [HttpPost("createhasfile"), Authorize]
        public ActionResult CreateHasFile([FromBody] MessageToAddHasFileDto model)
        {
            var result = 0;
            if (String.IsNullOrEmpty(model.FileName))
            {
                result = _messageServices.CreateMessage(model.Body, model.ConversationId, model.SenderId);
            }
            else
            {
                result = _messageServices.CreateMessageHasFile(model);
            }
            if (result == 0) return NotFound("Conversation is not found.");
            if (result == 1) return NotFound("User is not found.");
            return Ok("Add message successfully.");
        }
        [HttpPost("{conversationId}"), Authorize]
        public ActionResult<List<MessageToGetDto>> GetMessages(string conversationId)
        {
            return _messageServices.GetMessages(conversationId);
        }
        [HttpDelete("{id}"), Authorize]
        public ActionResult Delete(string id)
        {
            var result = _messageServices.DeleteMessage(id);
            if (!result) return NotFound("Message is not found.");
            return Ok("Delete this message successfully.");
        }
    }
}
