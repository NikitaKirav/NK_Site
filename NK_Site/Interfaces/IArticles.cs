using NK_Site.Models;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NK_Site.Interfaces
{
    public interface IArticles
    {
        IEnumerable<Article> ArticlesAll { get; }
        IEnumerable<Article> GetFavArticles { get; }
        Article GetObjectArticle(int articleId);
        void AddArticles(Article article, List<string> roles);
        void UpdateArticle(Article article, List<string> roles);
        void DeleteArticle(Article article);

        //PagingList<Article> Articles(IList<string> roles, int page, string sort, string defaultSort);
        PagingList<Article> Articles(int page, int articlesOnPage, string sort, string defaultSort);
        PagingList<Article> Articles(IList<string> roles, int articlesOnPage, int page, string sort, string defaultSort, string search);
        Article GetObjectArticleAccess(int articleId, IList<string> roles);
    }
}
