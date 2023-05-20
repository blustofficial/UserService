using UserService.Database.Models;

namespace UserService.Database
{
    public interface IUserDomain
    {
        bool Registration(User user);
        User GetUsers(string email, string password);
    }
}
