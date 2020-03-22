using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NK_Site.Interfaces;
using NK_Site.Models;
using NK_Site.ViewModels;

namespace NK_Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticles _allArticles;
        private readonly IArticlesCategory _allCategories;
        private readonly IArticlesAccess _allArticlesAccess;
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;

        public HomeController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            IArticles allArticles, IArticlesCategory allCategories, IArticlesAccess allArticlesAccess)
        {
            _allArticles = allArticles;
            _allCategories = allCategories;
            _allArticlesAccess = allArticlesAccess;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userRoles = await GetCurrentRolesOfUser();
            ViewBag.Title = "Home Page";

            var articles = _allArticles.Articles(userRoles, 5, 1, "-DateChange", "Name", "");
            return View(articles);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<IList<string>> GetCurrentRolesOfUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles;
            }
            // default is an user
            return new List<string>() { "user" };
        }
    }
}
