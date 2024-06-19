using Server_proj1.Datalayer;
using Server_proj1.Models.DTOs.UserDtos;

namespace Server_proj1.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly DataContext _db;
        public UserServices(DataContext db)
        {
            _db = db;
        }

        public List<UserToGetDto> GetAllUsers()
        {
            var toReturn = new List<UserToGetDto>();
            var users = _db.Users.ToList();
            if(users.Count <= 0) return toReturn;
            foreach (var user in users)
            {
                var item = new UserToGetDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    ImageUrl = user.ImageUrl,
                };
                toReturn.Add(item);
            }
            return toReturn;
        }
        public List<UserToGetDto> GetUsersBySearchNameValue(string searchNameValue)
        {
            var toReturn = new List<UserToGetDto>();
            var users = _db.Users.Where(p => p.Name.Contains(searchNameValue)).ToList();
            if (users.Count <= 0) return toReturn;
            foreach (var user in users)
            {
                var item = new UserToGetDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    ImageUrl = user.ImageUrl,
                };
                toReturn.Add(item);
            }
            return toReturn;
        }
    }
}
