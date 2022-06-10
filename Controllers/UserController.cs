using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyEshop.Areas.Core;
using MyEshop.Areas.Core.Repositories;
using MyEshop.Areas.Core.ViewModels;
using MyEshop.Areas.Identity.Data;

namespace MyEshop.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<MyEshopUser> _signInManager;

        public UserController(IUnitOfWork unitOfWork, SignInManager<MyEshopUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }
        [Authorize(Policy = Constants.Policies.RequireAdmin)]
            /*SHOW ALL USERS*/
        public IActionResult Index()
        {
            var users = _unitOfWork.User.GetUsers();
            return View(users);
        }
            /*EDIT USER'S CREDS*/
        public async Task<IActionResult> Edit(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();
            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
            /*            var roleslist = new List<SelectListItem>();

                        foreach (var role in roles) {
                            var hasRoles = userRoles.Any(ur=>ur.Contains(role.Name));
                            roleslist.Add(new SelectListItem(role.Name, role.Id, hasRoles));
                        }*/

            var roleslist = roles.Select(role => new SelectListItem(role.Name, role.Id,
                userRoles.Any(ur => ur.Contains(role.Name)))).ToList();

            var vm = new EditUserViewModel {
                User = user,
                Roles = roleslist
            };
            return View(vm);
        }
        [HttpPost]
            /*GET ALL THE ROLES AND ADD OR REMOVE ROLES FROM USER*/
        public async Task<IActionResult> OnPostAsync(EditUserViewModel data)
        {
            var user = _unitOfWork.User.GetUser(data.User.Id);
            if (user == null) {
                return NotFound();
            }

            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

            var rolesToAdd = new List<string>();
            var rolesToDelete = new List<string>();


            foreach (var role in data.Roles)
            {
                var assingedindb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    if (assingedindb == null)
                    {
                        //Add Role
                        rolesToAdd.Add(role.Text);


                    }
                }
                else
                {
                    if (assingedindb != null)
                    {
                        rolesToDelete.Add(role.Text);
                        
                        //Remove Role
                    }
                }
               
            }

            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);

            }
            if (rolesToDelete.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToAdd);

            }

            user.FirstName = data.User.FirstName;
            user.LastName = data.User.LastName;
            user.Email = data.User.Email;

            _unitOfWork.User.UpdateUser(user);
            return RedirectToAction("Edit", new {id = user.Id});
        }
      
    }
}
