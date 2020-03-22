using NK_Site.Interfaces;
using NK_Site.Models;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NK_Site.Data.Repository
{
    public class ArticleRepository : IArticles
    {
        private readonly ApplicationContext _db;

        public ArticleRepository(ApplicationContext db)
        {
            _db = db;
        }
        public IEnumerable<Article> ArticlesAll => _db.Articles.Include(c => c.Category);
        public IEnumerable<Article> GetFavArticles => _db.Articles.Where(x => x.IsFavorite).Include(c => c.Category);
        public Article GetObjectArticle(int articleId)
        {
            if (articleId == 0) { return new Article(); }
            return _db.Articles.FirstOrDefault(x => x.Id == articleId);
        }

        public void AddArticles(Article article, List<string> roles)
        {
            if (article.CategoryId == 0) { return; }
            article.DateCreate = DateTime.Now;
            article.DateChange = DateTime.Now;
            article.Number = _db.Articles.Max(x => x.Number) + 1;
            _db.Articles.Add(article);
            _db.SaveChanges();
            ChangeRolesOfArticle(article.Id, roles);
            _db.SaveChanges();
        }

        public PagingList<Article> Articles(int page, int articlesOnPage, string sort, string defaultSort)
        {
            // get a list of article
            var articles = _db.Articles.Include(c => c.Category).AsEnumerable().OrderBy(x => x.DateChange);
            int pageSize = (int)Math.Ceiling((double)articles.Count() / articlesOnPage);
            var model = PagingList.Create(articles, articlesOnPage, page, sort, defaultSort);

            return model;
        }

        /// <summary>
        /// Get a list of articles which have user access level
        /// </summary>
        /// <param name="roles">Names roles of user</param>
        /// <returns></returns>
        public PagingList<Article> Articles(IList<string> roles, int articlesOnPage, int page, string sort, string defaultSort, string search)
        {
            if (search == null) { return null; }
            // get Ids of roles of current user
            var rolesIdLast = _db.Roles.Where(x => roles.Any(c => c == x.Name)).Select(x => x.Id).ToArray();
            // get a list of Id article
            var listOfArticles = _db.ArticlesAccesses.Where(x => rolesIdLast.Any(c => x.RoleId == c)).ToList();
            // get a list of article
            var articles = _db.Articles.Include(c => c.Category).AsEnumerable()
                .Where(x => x.Name.Contains(search) || x.Text.Contains(search) || x.ShortDescription.Contains(search))
                            .Join(listOfArticles,
                            leftItem => leftItem.Id,
                            rightItem => rightItem.ArticleId,
                            (leftItem, rightItem) => leftItem).OrderBy(x => x.DateChange);
            var model = PagingList.Create(articles, articlesOnPage, page, sort, defaultSort);

            return model;
        }

        public Article GetObjectArticleAccess(int articleId, IList<string> roles)
        {
            // get Ids of roles of current user
            var rolesIdLast = _db.Roles.Where(x => roles.Any(c => c == x.Name)).Select(x => x.Id).ToArray();
            // get a list of Id article
            var listOfArticles = _db.ArticlesAccesses.Where(x => rolesIdLast.Any(c => x.RoleId == c)).ToList();
            // get a list of article
            var articles = _db.Articles.Include(c => c.Category).AsEnumerable()
                            .Join(listOfArticles,
                            leftItem => leftItem.Id,
                            rightItem => rightItem.ArticleId,
                            (leftItem, rightItem) => leftItem).OrderBy(x => x.DateChange);

            var article = articles.FirstOrDefault(x => x.Id == articleId);
            if (article == null)
            {
                article = _db.Articles.FirstOrDefault(x => x.Name == "Error");
            }
            return article;
        }

        public void UpdateArticle(Article article, List<string> roles)
        {
            ChangeRolesOfArticle(article.Id, roles);
            article.DateChange = DateTime.Now;
            _db.Entry(article).State = EntityState.Modified;
            _db.SaveChanges();
        }

        /// <summary>
        /// Delete Article and Roles
        /// </summary>
        /// <param name="article"></param>
        public void DeleteArticle(Article article)
        {
            _db.Entry(article).State = EntityState.Deleted;
            DeleteRolesOfArticle(article.Id);
            _db.SaveChanges();
        }

        private void ChangeRolesOfArticle(int articleId, List<string> roles)
        {
            var articleAccess = new ArticlesAccess();
            var rolesIdLast = _db.Roles.Where(x => roles.Any(c => c == x.Name)).Select(x => x.Id).ToArray();
            var userRoles = _db.ArticlesAccesses.Include(x => x.Role)
                                    .Where(x => x.ArticleId == articleId)
                                    .Select(x => x.Role.Id).ToList();
            // получаем список ролей, которые были добавлены
            var addedRoles = rolesIdLast.Except(userRoles);
            // получаем роли, которые были удалены
            var removedRoles = userRoles.Except(rolesIdLast);

            // Add new Roles
            foreach (var role in addedRoles)
            {
                _db.ArticlesAccesses.Add(new ArticlesAccess()
                {
                    ArticleId = articleId,
                    RoleId = role
                });
            }
            // Delete old roles
            foreach (var role in removedRoles)
            {
                var access = _db.ArticlesAccesses.FirstOrDefault(x => x.RoleId == role && x.ArticleId == articleId);
                if (access != null)
                {
                    _db.Entry(access).State = EntityState.Deleted;
                }
            }
        }

        /// <summary>
        /// Delete Roles in ArticleAccess
        /// </summary>
        /// <param name="articleId"></param>
        private void DeleteRolesOfArticle(int articleId)
        {
            var articleAccess = new ArticlesAccess();
            var userRoles = _db.ArticlesAccesses.Include(x => x.Role)
                                    .Where(x => x.ArticleId == articleId)
                                    .Select(x => x.Role.Id).ToList();

            // Delete old roles
            foreach (var role in userRoles)
            {
                var access = _db.ArticlesAccesses.FirstOrDefault(x => x.RoleId == role && x.ArticleId == articleId);
                if (access != null)
                {
                    _db.Entry(access).State = EntityState.Deleted;
                }
            }
        }
    }
}
