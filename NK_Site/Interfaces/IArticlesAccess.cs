using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NK_Site.Interfaces
{
    public interface IArticlesAccess
    {
        IList<string> GetRoles(int articleId);
    }
}
