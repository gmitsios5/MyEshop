using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Areas.Identity.Data;

namespace MyEshop.Areas.Core.ViewModels
{
    public class EditUserViewModel
    {
        public MyEshopUser User { get; set; }
        public IList<SelectListItem> Roles { get; set; }
    }
}
