using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using UserService.Database.Models;

namespace UserService.Database
{
    public interface IDataContext
    {
        public DbSet<User> Users { get; set; }
        public DatabaseFacade GetDatabase();
        public int Save();
    }
}
