using NK_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NK_Site.Interfaces
{
    public interface IArticlesCategory
    {
        IEnumerable<Category> AllCategories { get; }

        IEnumerable<Category> GetVisibleCategories { get; }
        Category GetObjectCategory(int categoryId);

        void AddCategory(Category category);

        void UpdateCategories(Category category);

        void DeleteCategory(Category category);

        IEnumerable<Category> OrderAllCategory(int id = 0);
    }
}
