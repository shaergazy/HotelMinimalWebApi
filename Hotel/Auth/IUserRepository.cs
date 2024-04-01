namespace HotelMinimalWebApi.Auth
{
    public interface IUserRepository
    {
        UserDto GetUser(UserModel userModel);
    }
}
