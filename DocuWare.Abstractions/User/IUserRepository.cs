namespace DocuWare.Abstractions.User;

public interface IUserRepository
{
    Task<UserDto?> GetUser(string email, string password);
}