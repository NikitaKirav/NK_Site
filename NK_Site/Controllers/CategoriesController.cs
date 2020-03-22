using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NK_Site.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NK_Site.Models;

namespace NK_Site.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IArticles _allArticles;
        private readonly IArticlesCategory _allCategories;

        public CategoriesController(IArticles allArticles, IArticlesCategory allCategories)
        {
            _allArticles = allArticles;
            _allCategories = allCategories;
        }

        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        // GET: Articles
        [Authorize(Roles = "admin")]
        public ActionResult List()
        {
            ViewBag.Title = "Categories";
            var categories = _allCategories.AllCategories.ToList();
            return View(categories);
        }

        // GET: Category/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.ListOfCategory = _allCategories.OrderAllCategory();
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [Authorize(Roles = "admin")]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            _allCategories.AddCategory(category);
            return RedirectToAction("List");
        }

        // GET: Category/Edit/5
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var category = _allCategories.GetObjectCategory(id);
            ViewBag.ListOfCategory = _allCategories.OrderAllCategory(id);
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Category category)
        {
            _allCategories.UpdateCategories(category);
            return RedirectToAction("List");
        }

        // GET: Category/Delete/5
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            var category = _allCategories.GetObjectCategory(id);
            return PartialView(category);
        }

        // POST: Category/Delete/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(Category category)
        {
            _allCategories.DeleteCategory(category);
            return RedirectToAction("List");
        }
    }
}