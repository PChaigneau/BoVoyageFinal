using BoVoyageFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoVoyageFinal.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("BackOffice")]
    public class AdministrationsController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdministrationsController(RoleManager<IdentityRole> roleManager,
                                         UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;

        }

        /// <summary>
        /// Par Injection de dependence Nous avons access à toutes les methode de la classe RoleManager, nous pouvons donc acceder a la methode roles
        /// qui nous listera les roles trouvés
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;//Variable roles contiendra nos roles et seront retournés dans la vue
            return View(roles);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            // Nous recuperons le role de l'id recuperé via URL
            var role = await roleManager.FindByIdAsync(id);
            //Initialiser les propriétés de notre ViewModel avec le role que nous venons de récuperer
            var model = new EditRoleVm
            {
                Id = role.Id,
                RoleName = role.Name
            };

            //maintenant que les deux propriété sont connues , nous voulons la liste des users coreespondant a ce role
            foreach (var user in userManager.Users)
            {
                // avec la methode de notre classe RoleManager IsInRole nous recuperons la liste des user correspondant a ce role
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);// et nous ajoutons les users trouvés a notre model ( pour le completer)
                }
            }

            return View(model);// retourner le model complet ( id,Nom,List(User))
        }

        // This action responds to HttpPost and receives EditRoleViewModel
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleVm model)
        {
            // trouver le role que nous voulons modifié
            var role = await roleManager.FindByIdAsync(model.Id);

                role.Name = model.RoleName; // effectuer la modification

                // Update le nouveau role
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            
        }

  

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            var model = new List<UserRoleVm>();
            // var modelTrie = model.OrderBy(U=>U.UserName);
            foreach (var user in userManager.Users)
            {

                var userRoleViewModel = new UserRoleVm
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };


                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;

                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);

            }
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleVm> model, string roleId)
        {

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

    }
}