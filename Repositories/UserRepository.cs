using MyEshop.Areas.Identity.Data;
using MyEshop.Areas.Core.Repositories;

namespace MyEshop.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyEshopDBContext? _context;
        public UserRepository(MyEshopDBContext context)
        {
             _context = context;
        }

        public MyEshopUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public ICollection<MyEshopUser> GetUsers()
        {
            return _context.Users.ToList();
        }

        public MyEshopUser UpdateUser(MyEshopUser user)
        {
           _context.Update(user);
           _context.SaveChanges();

            return user;
        }
    }
}
