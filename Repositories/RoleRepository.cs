using Microsoft.AspNetCore.Identity;
using MyEshop.Areas.Core.Repositories;
using MyEshop.Areas.Identity.Data;

namespace MyEshop.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly MyEshopDBContext _context;
        public RoleRepository(MyEshopDBContext context)
        {
            _context = context;
        }
        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
