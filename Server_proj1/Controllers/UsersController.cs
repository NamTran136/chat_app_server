using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server_proj1.Models.DTOs.UserDtos;
using Server_proj1.Services.UserServices;

namespace Server_proj1.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet("GetAll"), Authorize]
        public ActionResult<List<UserToGetDto>> Get()
        {
            return _userServices.GetAllUsers();
        }
        [HttpGet("GetAll/{searchNameValue}"), Authorize]
        public ActionResult<List<UserToGetDto>> GetBySearchName(string searchNameValue)
        {
            return _userServices.GetUsersBySearchNameValue(searchNameValue);
        }
    }
}
