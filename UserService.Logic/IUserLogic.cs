using System.Web.Mvc;
using UserService.Logic.Models;

namespace UserService.Logic
{
    public interface IUserLogic
    {
        bool Register(User user);
        string Auth(User user);
    }
}
