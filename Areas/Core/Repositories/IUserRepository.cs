using MyEshop.Areas.Identity.Data;

namespace MyEshop.Areas.Core.Repositories
{
    public interface IUserRepository
    {
        ICollection<Identity.Data.MyEshopUser> GetUsers();
        MyEshopUser GetUser(string id);

        MyEshopUser UpdateUser(MyEshopUser user);
    }
}
