using Microsoft.AspNetCore.Identity;
using MyEshop.Areas.Identity.Data;

namespace MyEshop.Areas.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
