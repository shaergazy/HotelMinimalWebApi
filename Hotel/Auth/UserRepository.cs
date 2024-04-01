namespace HotelMinimalWebApi.Auth
{
    public class UserRepository : IUserRepository
    {
        public List<UserDto> _users => new()
        {
            new UserDto("John", "1223"),
            new UserDto("Adam", "1223"),
            new UserDto("Ben", "1223"),
        };
        public UserDto GetUser(UserModel userModel) =>
            _users.FirstOrDefault(u =>
            string.Equals(u.username, userModel.UserName) &&
            string.Equals(u.password, userModel.Password)) ??
            throw new Exception();
    }
}
