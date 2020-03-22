using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NK_Site.Interfaces;
using NK_Site.Models;
using NK_Site.ViewModels;

namespace NK_Site.Controllers
{
    //[ViewComponent]
    public class ArticlesController : Controller
    {
        private readonly IArticles _allArticles;
        private readonly IArticlesCategory _allCategories;
        private readonly IArticlesAccess _allArticlesAccess;
        private readonly IConstant _constants;
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;

        public ArticlesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            IArticles allArticles, IArticlesCategory allCategories, IArticlesAccess allArticlesAccess,
            IConstant constants)
        {
            _allArticles = allArticles;
            _allCategories = allCategories;
            _allArticlesAccess = allArticlesAccess;
            _constants = constants;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        // GET: Articles
        public async Task<IActionResult> Index(int pageindex = 1, string sort = "-DateChange", string s = "")
        {
            if (s == null) { s = ""; }
            var userRoles = await GetCurrentRolesOfUser();
            ViewBag.Title = "My Notes";
            int articlesOnPage = _constants.GetValueInt("articlesOnPage") ?? 10;
            var articles = _allArticles.Articles(userRoles, articlesOnPage, pageindex, sort, "Name", s);
            return View(articles);
        }

        // GET: Articles
        //public async Task<IActionResult> IndexSearch(int pageindex = 1, string sort = "-DateChange", string search = "")
        //{
        //    var userRoles = await GetCurrentRolesOfUser();
        //    ViewBag.Title = "My Notes";
        //    var articles = _allArticles.Articles(userRoles, pageindex, sort, "Name", search);
        //    return View(articles);
        //}

        // GET: Articles
        [Authorize(Roles = "admin")]
        public ActionResult List(int pageindex = 1, string sort = "-DateChange")
        {
            ViewBag.Title = "My Notes";
            int articlesOnPage = _constants.GetValueInt("articlesOnPageAdminPart") ?? 8;
            var articles = _allArticles.Articles(pageindex, articlesOnPage, sort, "Name");
            articles.Action = "List";
            return View(articles);
        }

        [HttpGet]
        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var userRoles = await GetCurrentRolesOfUser();
            var article = _allArticles.GetObjectArticleAccess(id, userRoles);
            return View(article);
        }

        // GET: Articles/Create
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create(int id = 0)
        {
            ChangeRoleViewModel model = GetArticleByNomber(id);
            var article = _allArticles.GetObjectArticle(id);
            ViewBag.ListOfCategory = _allCategories.OrderAllCategory();
            ViewBag.ListOfRoles = model;

            return View(article);
        }

        // POST: Articles/Create
        [HttpPost]
        [Authorize(Roles = "admin")]
        // [ValidateAntiForgeryToken]
        public IActionResult Create(Article article, List<string> roles)
        {
            if (article.Id == 0)
            {
                _allArticles.AddArticles(article, roles);
            }
            else
            {
                _allArticles.UpdateArticle(article, roles);
            }
            return Ok(article);
        }

        // GET: Articles/Edit/5
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            ChangeRoleViewModel model = GetArticleByNomber(id);
            var article = _allArticles.GetObjectArticle(id);
            ViewBag.ListOfCategory = _allCategories.OrderAllCategory();
            ViewBag.ListOfRoles = model;

            return View(article);
        }

        // POST: Articles/Edit/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Article article, List<string> roles)
        {
            _allArticles.UpdateArticle(article, roles);
            return Ok(article);
        }



        // POST: Articles/Edit/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult SaveAndExit(Article article, List<string> roles)
        {
            _allArticles.UpdateArticle(article, roles);
            return RedirectToAction("List");
        }

        // GET: Articles/Delete/5
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var article = _allArticles.GetObjectArticle(id);
            return PartialView(article);
        }

        // POST: Articles/Delete/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        //   [ValidateAntiForgeryToken]
        public ActionResult Delete(Article article)
        {
            _allArticles.DeleteArticle(article);
            return RedirectToAction("List");
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

        private ChangeRoleViewModel GetArticleByNomber(int id)
        {
            // получем список ролей пользователя
            var userRoles = _allArticlesAccess.GetRoles(id);
            var allRoles = _roleManager.Roles.ToList();
            ChangeRoleViewModel model = new ChangeRoleViewModel
            {
                UserRoles = userRoles,
                AllRoles = allRoles
            };
            return model;
        }

    }
}