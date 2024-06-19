using Server_proj1.Models.DTOs.UserDtos;

namespace Server_proj1.Services.UserServices
{
    public interface IUserServices
    {
        List<UserToGetDto> GetAllUsers();
        List<UserToGetDto> GetUsersBySearchNameValue(string searchNameValue);
    }
}
