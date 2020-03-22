using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NK_Site.Controllers
{
    public class AdminController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        public AdminController(RoleManager<IdentityRole> manager)
        {
            _roleManager = manager;
        }

        public IActionResult GetRoles()
        {
            return View(_roleManager.Roles.ToList());
        }
    }
}