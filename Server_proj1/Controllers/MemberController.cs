using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server_proj1.Models.DTOs.MemberDtos;
using Server_proj1.Models.Entities;
using Server_proj1.Services.MemberServices;

namespace Server_proj1.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberServices _memberServices;
        public MemberController(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }
        [HttpGet]
        public ActionResult<List<MemberDto>> Get()
        {
            return _memberServices.GetAll();
        }
    }
}
